using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Point = pythagoras.f.Point;

namespace PlayNXNA
{
    public class XNACanvas : AbstractCanvas, Canvas
    {
        private static int aax = 2;

        private XNACanvasState state;
        private List<XNACanvasState> stateStack = new List<XNACanvasState>();
        private Texture2D texture;
        private int[] data;
        private byte[] bitmap;
        private bool dirty;
        private new int width, height;
        private InternalTransform tempTransform = new StockInternalTransform();
        private Point tempPoint = new Point();

        public Texture2D Texture
        {
            get 
            {
                if (dirty) updateTexture();
                dirty = false;
                return texture;
            }
        }

        public XNACanvas(float width, float height)
            : base(width, height)
        {
            this.width = (int)width; this.height = (int)height;
            texture = new Texture2D(((XNAPlatform)PlayN.platform()).DeviceManager.GraphicsDevice, 
                this.width, this.height, false, SurfaceFormat.Color);
            data = new int[this.width * this.height];
            bitmap = new byte[this.width * this.height * aax * aax];
            texture.GetData<int>(data);
            state = XNACanvasState.create(texture);
        }

        private void updateTexture()
        {
            texture.SetData<int>(data);
        }

        private static int colorSwapRB(int color)
        {
            return Color.argb(Color.alpha(color), Color.blue(color), Color.green(color), Color.red(color));
        }

        private static int alpha(int color) { return (color >> 24) & 0xFF; }
        private static int blue(int color) { return (color >> 16) & 0xFF; }
        private static int green(int color) { return (color >> 8) & 0xFF; }
        private static int red(int color) { return color & 0xFF; }
        private static int argb(int a, int r, int g, int b) { return (a << 24) | (b << 16) | (g << 8) | r; }


        public static Microsoft.Xna.Framework.Color GetXNAColor(int playnColor)
        {
            int xnaColor = colorSwapRB(playnColor);
            return new Microsoft.Xna.Framework.Color(red(xnaColor), green(xnaColor), blue(xnaColor), alpha(xnaColor));
        }

        private void getBounds(float x, float y, float w, float h, out int x1, out int y1, out int x2, out int y2)
        {
            x1 = Math.Max((int)x, state.clipRect.X);
            y1 = Math.Max((int)y, state.clipRect.Y);
            x2 = Math.Min((int)(x + w), state.clipRect.Right);
            y2 = Math.Min((int)(y + h), state.clipRect.Bottom);
        }

        private InternalTransform getAAXform()
        {
            tempTransform.set(state.transform);
            tempTransform.scale(aax, aax);
            return tempTransform;
        }

        private void getAAXformBounds(float x, float y, float w, float h, out int x1, out int y1, out int x2, out int y2)
        {
            InternalTransform aaXform = getAAXform();
            x1 = y1 = int.MaxValue;
            x2 = y2 = int.MinValue;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    tempPoint.set(x + w * i, y + h * j);
                    aaXform.transform(tempPoint, tempPoint);
                    float tx = tempPoint.x(), ty = tempPoint.y();
                    x1 = (int)Math.Min(x1, tx); x2 = (int)Math.Max(x2, tx);
                    y1 = (int)Math.Min(y1, ty); y2 = (int)Math.Max(y2, ty);
                }
            }
            x1 = Math.Max(x1, 0); x2 = Math.Min(x2, width * aax);
            y1 = Math.Max(y1, 0); y2 = Math.Min(y2, height * aax);
        }

        private static int blend(int c1, int c2, int alpha)
        {
            int beta = 255 - alpha;
            int a = 255 - (255 - alpha) * (255 - XNACanvas.alpha(c1)) / 255;
            int r = (red(c1) * beta + red(c2) * alpha) / 255;
            int g = (green(c1) * beta + green(c2) * alpha) / 255;
            int b = (blue(c1) * beta + blue(c2) * alpha) / 255;
            return argb(a, r, g, b);
        }

        private static int textureIndex(int x, int y, int width)
        {
            return y * width + x;
        }

        private void set(int x, int y, int color, float confidence = 1)
        {
            if (confidence <= 0) return;
            if (confidence > 1) confidence = 1;
            int a = (int)(alpha(color) * confidence);
            int index = textureIndex(x, y, width);
            if (a == 255)
            {
                data[index] = color;
            }
            else
            {
                data[index] = blend(data[index], color, a);
            }
        }

        private void clear(int x, int y)
        {
            data[textureIndex(x, y, width)] = 0;
        }

        private delegate bool HitTest(float i, float j);
        private delegate void PixelAction(float tx, float ty, int i, int j, int index);

        private void operatePixels(int x1, int x2, int y1, int y2, PixelAction action)
        {
            InternalTransform aaXform = getAAXform();
            int maxIndex = bitmap.Length;
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    int index = j * width * aax + i;
                    if (index < 0 || index >= maxIndex) continue;

                    tempPoint.set(i, j);
                    aaXform.inverseTransform(tempPoint, tempPoint);
                    float tx = tempPoint.x(), ty = tempPoint.y();

                    action(tx, ty, i, j, index);
                }
            }
        }

        private void draw(float x, float y, float w, float h, int color, HitTest hitTest)
        {
            int x1, x2, y1, y2;
            getAAXformBounds(x, y, w, h, out x1, out y1, out x2, out y2);
            operatePixels(x1, x2, y1, y2, (float tx, float ty, int i, int j, int index) =>
                {
                    bool inBounds = (!(tx < x || tx >= x + w || ty < y || ty >= y + h));
                    bitmap[index] = (byte)((inBounds && hitTest.Invoke(tx, ty)) ? 1 : 0);
                });
            
            applyBitmap(x1, x2, y1, y2, color);
            dirty = true;
        }

        private void applyBitmap(int x1, int x2, int y1, int y2, int color, int[] colormap = null)
        {
            int maxIndex = bitmap.Length;
            int ax1 = x1 / aax, ax2 = x2 / aax;
            int ay1 = y1 / aax, ay2 = y2 / aax;
            int[] colors = new int[aax * aax];
            for (int i = ax1; i < ax2; i++)
            {
                for (int j = ay1; j < ay2; j++)
                {
                    int hits = 0, count = 0;
                    for (int ai = 0; ai < aax; ai++)
                    {
                        int axi = (i * aax + ai);
                        if (axi < x1) continue;
                        for (int aj = 0; aj < aax; aj++)
                        {
                            int ayj = (j * aax + aj);
                            if (ayj < y1) continue;
                            int index = ayj * width * aax + axi;
                            if (index < 0 || index >= maxIndex) continue;
                            if (colormap == null)
                            {
                                if (bitmap[index] == 1) colors[hits++] = color;
                            }
                            else
                            {
                                colors[hits++] = colormap[(ayj - y1) * (x2 - x1) + (axi - x1)];
                            }
                            count++;
                            
                        }
                    }
                    if (hits == 0) continue;
                    int a = 0, r = 0, g = 0, b = 0;
                    for (int k = 0; k < hits; k++)
                    {
                        int c = colors[k];
                        a += alpha(c); r += red(c);
                        g += green(c); b += blue(c);
                    }
                    a /= hits; r /= hits; g /= hits; b /= hits;
                    set(i, j, argb(a, r, g, b), (float)hits / count);
                }
            }
        }

        public override Canvas clear()
        {
            return clearRect(0, 0, width, height);
        }

        public override Canvas clearRect(float x, float y, float w, float h)
        {
            int x1, x2, y1, y2;
            getBounds(x, y, w, h, out x1, out y1, out x2, out y2);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    clear(i, j);
                }
            }
            return this;
        }

        public override Canvas clip(Path value)
        {
            throw new NotImplementedException();
        }

        public override Canvas clipRect(float x, float y, float w, float h)
        {
            state.clipRect = new Microsoft.Xna.Framework.Rectangle((int)x, (int)y, (int)w, (int)h);
            return this;
        }

        public override Path createPath()
        {
            throw new NotImplementedException();
        }

        public override Canvas drawImage(Image image, float dx, float dy, float dw, float dh, float sx, float sy, float sw, float sh)
        {
            return drawImage(((XNAImage)image).Texture, dx, dy, dw, dh, sx, sy, sw, sh);
        }

        public Canvas drawImage(Texture2D tex, float dx, float dy, float dw, float dh, float sx, float sy, float sw, float sh)
        {
            if (tex == null) return this;
            if (dw == 0 || dh == 0) return this;

            Microsoft.Xna.Framework.Rectangle sourceRect =
                new Microsoft.Xna.Framework.Rectangle((int)sx, (int)sy, (int)sw, (int)sh);
            if (sourceRect.X < 0) sourceRect.X = 0; if (sourceRect.Width > tex.Width) sourceRect.Width = tex.Width;
            if (sourceRect.Y < 0) sourceRect.Y = 0; if (sourceRect.Height > tex.Height) sourceRect.Height = tex.Height;
            int[] data = new int[sourceRect.Width * sourceRect.Height];
            tex.GetData<int>(0, sourceRect, data, 0, data.Length);

            int x1, x2, y1, y2;
            getAAXformBounds(dx, dy, dw, dh, out x1, out y1, out x2, out y2);
            int bw = x2 - x1, bh = y2 - y1;
            int[] colormap = new int[bw * bh];
            InternalTransform aaXform = getAAXform();

            operatePixels(x1, x2, y1, y2, (float tx, float ty, int i, int j, int index) =>
                {
                    if (!(tx < dx || tx >= dx + dw || ty < dy || ty >= dy + dh))
                    {
                        int colorIndex = (j - y1) * bw + (i - x1);
                        int si = (int)((tx - dx) / dw * sh + sx);
                        int sj = (int)((ty - dy) / dh * sh + sy);
                        int sourceIndex = (int)(sj * sw + si);
                        if (sourceIndex < 0 || sourceIndex >= data.Length) return;
                        colormap[colorIndex] = data[sourceIndex];
                    }
                });

            applyBitmap(x1, x2, y1, y2, 0, colormap);

            dirty = true;
            return this;
        }

        public override Canvas drawImage(Image image, float x, float y, float w, float h)
        {
            Texture2D tex = ((XNAImage)image).Texture;
            if (tex == null) return this;
            return drawImage(image, x, y, w, h, 0, 0, tex.Width, tex.Height);
        }

        public override Canvas drawImage(Image image, float x, float y)
        {
            if (image == null) return this;
            return drawImage(image, x, y, image.width(), image.height());
        }

        public override Canvas drawImageCentered(Image image, float x, float y)
        {
            if (image == null) return this;
            return drawImage(image, x - image.width() / 2, y - image.height() / 2);
        }

        public override Canvas drawLine(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1, dy = y2 - y1;
            float m = dx == 0 ? 0 : dy / dx;
            int ix1 = (int)x1, ix2 = (int)x2, iy1 = (int)y1, iy2 = (int)y2;
            draw(x1, y1, x2 - x1 + 1, y2 - y1 + 1, state.fillColor, (float tx, float ty) =>
                {
                    int itx = (int)tx, ity = (int)ty;
                    if (itx < ix1 || itx > ix2 || ity < iy1 || ity > iy2) return false;

                    if (dx == 0)
                    {
                        return itx == ix1;
                    }
                    else if (dy == 0)
                    {
                        return ity == iy1;
                    }

                    if (dx > dy)
                    {
                        float y = (tx - x1) * m + y1;
                        return Math.Abs(ty - y) <= 0.5f;
                    }
                    else
                    {
                        float x = (ty - y1) / m + x1;
                        return Math.Abs(tx - x) <= 0.5f;
                    }
                });
            return this;
        }

        public override Canvas drawPoint(float x, float y)
        {
            return fillRect(x, y, 1, 1);
        }

        public override Canvas drawText(string text, float x, float y)
        {
            return this;
        }

        public override Canvas fillCircle(float x, float y, float radius)
        {
            float r2 = radius * radius;
            draw(x - radius, y - radius, radius * 2, radius * 2, state.fillColor, (float tx, float ty) => 
                {
                    float dx = (x - tx);
                    float dy = (y - ty);
                    return (dx * dx + dy * dy <= r2);
                });
            return this;
        }

        public override Canvas fillPath(Path path)
        {
            throw new NotImplementedException();
        }

        public override Canvas fillRect(float x, float y, float w, float h)
        {
            draw(x, y, w, h, state.fillColor, (float tx, float ty) => { return true; });
            return this;
        }

        public override Canvas fillRoundRect(float x, float y, float w, float h, float cornerRadius)
        {
            float cr2 = cornerRadius * cornerRadius;
            draw(x, y, w, h, state.fillColor, (float tx, float ty) =>
                {
                    float dx = 0, dy = 0;
                    if (tx < x + cornerRadius)
                    {
                        dx = x + cornerRadius - tx;
                    }
                    else if (tx > x + w - cornerRadius)
                    {
                        dx = tx - x - w + cornerRadius;
                    }
                    if (ty < y + cornerRadius)
                    {
                        dy = y + cornerRadius - ty;
                    }
                    else if (ty > y + h - cornerRadius)
                    {
                        dy = ty - y - h + cornerRadius;
                    }
                    return !(dx != 0 && dy != 0 && dx * dx + dy * dy > cr2);
                });
            return this;
        }

        public override Canvas fillText(TextLayout layout, float x, float y)
        {
            XNAFont font = ((XNAFont)layout.format().font);
            FontInfo info = font.fontInfo;
            GraphicsDevice device = ((XNAPlatform) PlayN.platform()).DeviceManager.GraphicsDevice;
            RenderTarget2D renderTarget = new RenderTarget2D(device, (int)layout.bounds().width(), (int)layout.bounds().height());
            device.SetRenderTarget(renderTarget);
            device.Clear(new Microsoft.Xna.Framework.Color(0, 0, 0, 0));
            float scale = font.Scale;
            using (SpriteBatch batch = new SpriteBatch(device))
            {
                batch.Begin();
                Microsoft.Xna.Framework.Color color = GetXNAColor(colorSwapRB(state.fillColor));
                batch.DrawString(info.font, layout.text(), Microsoft.Xna.Framework.Vector2.Zero, color, 0, 
                    Microsoft.Xna.Framework.Vector2.Zero, scale, SpriteEffects.None, 0);
                batch.End();
            }
            device.SetRenderTarget(null);
            return drawImage((Texture2D)renderTarget, x, y, renderTarget.Width, renderTarget.Height, 0, 0, renderTarget.Width, renderTarget.Height);
        }

        public override Canvas restore()
        {
            state = stateStack[0];
            stateStack.RemoveAt(0);
            return this;
        }

        public override Canvas rotate(float angle)
        {
            state.transform.rotate(angle);
            return this;
        }

        public override Canvas save()
        {
            stateStack.Insert(0, state);
            state.copyFields();
            return this;
        }

        public override Canvas scale(float sx, float sy)
        {
            state.transform.scale(sx, sy);
            return this;
        }

        public override Canvas setAlpha(float alpha)
        {
            return this;
        }

        public override Canvas setCompositeOperation(Canvas.Composite op)
        {
            return this;
        }

        public override Canvas setFillColor(int color)
        {
            state.fillColor = colorSwapRB(color);
            //Console.WriteLine(alpha(state.fillColor) + ", " + red(state.fillColor) + ", " + green(state.fillColor) + ", " + blue(state.fillColor));
            return this;
        }

        public override Canvas setFillGradient(Gradient gradient)
        {
            return this;
        }

        public override Canvas setFillPattern(Pattern pattern)
        {
            return this;
        }

        public override Canvas setLineCap(Canvas.LineCap lineCap)
        {
            return this;
        }

        public override Canvas setLineJoin(Canvas.LineJoin lineJoin)
        {
            return this;
        }

        public override Canvas setMiterLimit(float miter)
        {
            return this;
        }

        public override Canvas setStrokeColor(int color)
        {
            state.strokeColor = colorSwapRB(color);
            return this;
        }

        public override Canvas setStrokeWidth(float width)
        {
            state.strokeWidth = width;
            return this;
        }

        public override Canvas strokeCircle(float x, float y, float radius)
        {
            return fillCircle(x, y, radius);
        }

        public override Canvas strokePath(Path path)
        {
            throw new NotImplementedException();
        }

        public override Canvas strokeRect(float x, float y, float w, float h)
        {
            float sWidth = 1;
            float ix1 = x + sWidth / 2, ix2 = x + w - sWidth / 2, iy1 = y + sWidth / 2, iy2 = y + h - sWidth / 2;
            int x1, x2, y1, y2;
            getBounds(x - sWidth / 2, y - sWidth / 2, w + sWidth, h + sWidth, out x1, out y1, out x2, out y2);
            for (int i = x1; i < x2; i++)
            {
                bool intX = i > ix1 && i < ix2;
                for (int j = y1; j < y2; j++)
                {
                    if (intX && j > iy1 && j < iy2) continue;
                    set(i, j, state.strokeColor);
                }
            }
            dirty = true;
            return this;
        }

        public override Canvas strokeRoundRect(float x, float y, float w, float h, float cornerRadius)
        {
            return fillRoundRect(x, y, w, h, cornerRadius);
        }

        public override Canvas strokeText(TextLayout text, float x, float y)
        {
            return this;
        }

        public override Canvas transform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            tempTransform.setTransform(m11, m12, m21, m22, dx, dy);
            state.transform.concatenate(tempTransform);
            return this;
        }

        public override Canvas translate(float dx, float dy)
        {
            state.transform.translate(dx, dy);
            return this;
        }
    }
}
