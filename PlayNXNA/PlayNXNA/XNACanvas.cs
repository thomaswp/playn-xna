using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNACanvas : AbstractCanvas, Canvas
    {
        private XNACanvasState state;
        private List<XNACanvasState> stateStack = new List<XNACanvasState>();
        private Texture2D texture;
        private int[] data;
        private bool dirty;
        private new int width, height;

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

        private void set(int x, int y, int color)
        {
            int a = alpha(color);
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
            Texture2D tex = ((XNAImage)image).Texture;
            if (tex == null) return this;
            sx = Math.Max(0, sx); sy = Math.Max(0, sy);
            sw = Math.Min(tex.Width, sw); sh = Math.Min(tex.Height, sh);
            Microsoft.Xna.Framework.Rectangle sourceRect = 
                new Microsoft.Xna.Framework.Rectangle((int)sx, (int)sy, (int)sw, (int)sh);
            int[] data = new int[sourceRect.Width * sourceRect.Height];
            tex.GetData<int>(0, sourceRect, data, 0, data.Length);

            int isw = (int)sw, ish = (int)sh, idw = (int)dw, idh = (int)dh;

            int x1, x2, y1, y2;
            getBounds(dx, dy, dw, dh, out x1, out y1, out x2, out y2);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    int si = (i - x1) * isw / idw;
                    int sj = (j - y1) * ish / idh;
                    int index = textureIndex(si, sj, tex.Width);
                    if (index >= 0 && index <= data.Length)
                    {
                        set(i, j, data[index]);
                    }
                }
            }
            return this;
        }

        public override Canvas drawImage(Image image, float x, float y, float w, float h)
        {
            Texture2D tex = ((XNAImage)image).Texture;
            if (tex == null) return this;
            return drawImage(image, 0, 0, tex.Width, tex.Height, x, y, w, h);
        }

        public override Canvas drawImage(Image image, float x, float y)
        {
            return drawImage(image, x, y, image.width(), image.height());
        }

        public override Canvas drawImageCentered(Image image, float x, float y)
        {
            return drawImage(image, x - image.width() / 2, y - image.height() / 2);
        }

        public override Canvas drawLine(float x1, float y1, float x2, float y2)
        {
            int ix1, ix2, iy1, iy2;
            getBounds(x1, y1, x2 - x1 + 1, y2 - y1 + 1, out ix1, out iy1, out ix2, out iy2);

            if (ix2 - ix1 > iy2 - iy1)
            {
                for (int i = ix1; i < ix2; i++)
                {
                    int j = iy1 + (i - ix1) * (iy2 - iy1) / (ix2 - ix1);
                    set(i, j, state.fillColor);
                }
            }
            else
            {
                for (int i = iy1; i < iy2; i++)
                {
                    int j = ix1 + (i - iy1) * (ix2 - ix1) / (iy2 - iy1);
                    set(j, i, state.fillColor);
                }
            }

            return this;
        }

        public override Canvas drawPoint(float x, float y)
        {
            if (state.clipRect.Contains(new Microsoft.Xna.Framework.Point((int)x, (int)y)))
            {
                set((int)x, (int)y, state.fillColor);
            }
            return this;
        }

        public override Canvas drawText(string text, float x, float y)
        {
            return this;
        }

        public override Canvas fillCircle(float x, float y, float radius)
        {
            int x1, x2, y1, y2;
            getBounds(x - radius, y - radius, radius * 2, radius * 2, out x1, out y1, out x2, out y2);
            float r2 = radius * radius;
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    float dx = (x - i);
                    float dy = (y - j);
                    if (dx * dx + dy * dy <= r2)
                    {
                        set(i, j, state.fillColor);
                    }
                }
            }
            return this;
        }

        public override Canvas fillPath(Path path)
        {
            throw new NotImplementedException();
        }

        public override Canvas fillRect(float x, float y, float w, float h)
        {
            int x1, x2, y1, y2;
            getBounds(x, y, w, h, out x1, out y1, out x2, out y2);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    set(i, j, state.fillColor);
                }
            }
            dirty = true;
            return this;
        }

        public override Canvas fillRoundRect(float x, float y, float w, float h, float cornerRadius)
        {
            int x1, x2, y1, y2;
            getBounds(x, y, w, h, out x1, out y1, out x2, out y2);
            float cr2 = cornerRadius * cornerRadius;
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    if (i < x1 + cornerRadius)
                    {
                        float dx = x1 + cornerRadius - i;
                        if (j < y1 + cornerRadius)
                        {
                            float dy = y1 + cornerRadius - j;
                            if (dx * dx + dy * dy > cr2) continue;
                        }
                        else if (j > y2 - cornerRadius)
                        {
                            float dy = j - y2 + cornerRadius;
                            if (dx * dx + dy * dy > cr2) continue;
                        }
                    }
                    else if (i > x2 - cornerRadius)
                    {
                        float dx = i - x2 + cornerRadius;
                        if (j < y1 + cornerRadius)
                        {
                            float dy = y1 + cornerRadius - j;
                            if (dx * dx + dy * dy > cr2) continue;
                        }
                        else if (j > y2 - cornerRadius)
                        {
                            float dy = j - y2 + cornerRadius;
                            if (dx * dx + dy * dy > cr2) continue;
                        }
                    }
                    set(i, j, state.fillColor);
                }
            }
            dirty = true;
            return this;
        }

        public override Canvas fillText(TextLayout text, float x, float y)
        {
            return this;
        }

        public override Canvas restore()
        {
            state = stateStack[0];
            stateStack.RemoveAt(0);
            return this;
        }

        public override Canvas rotate(float angle)
        {
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
            return setFillColor(color);
        }

        public override Canvas setStrokeWidth(float with)
        {
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
            return fillRect(x, y, w, h);
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
            return this;
        }

        public override Canvas translate(float dx, float dy)
        {
            return this;
        }
    }
}
