using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XColor = Microsoft.Xna.Framework.Color;

namespace PlayNXNA
{
    class XNAImmediateSurface : Surface
    {
        public SpriteBatch SpriteBatch { get; set; }
        public InternalTransform RootTransform { get; set; }

        private int _fillColor;
        private Texture2D rectTexture;

        private InternalTransform tempTransform = new StockInternalTransform();

        public XNAImmediateSurface()
        {
            rectTexture = new Texture2D(((XNAPlatform)PlayN.platform()).DeviceManager.GraphicsDevice, 1, 1);
            rectTexture.SetData<XColor>(new XColor[] { XColor.White });
        }

        public Surface clear()
        {
            return this;   
        }

        public Surface drawImage(Image i, float f1, float f2, float f3, float f4, float f5, float f6, float f7, float f8)
        {
            throw new NotImplementedException();
        }

        public Surface drawImage(Image i, float f1, float f2, float f3, float f4)
        {
            throw new NotImplementedException();
        }

        public Surface drawImage(Image image, float x, float y)
        {
            tempTransform.set(RootTransform);
            tempTransform.translate(x, y);
            ((XNAImage)image).draw(SpriteBatch, tempTransform);
            return this;
        }

        public Surface drawImageCentered(Image image, float x, float y)
        {
            tempTransform.set(RootTransform);
            tempTransform.translate(x - image.width() * 0.5f * tempTransform.scaleX(), y - image.height() * 0.5f * tempTransform.scaleY());
            ((XNAImage)image).draw(SpriteBatch, tempTransform);
            return this;
        }

        public Surface drawLayer(Layer layer)
        {
            tempTransform.set(RootTransform);
            ((XNALayer)layer).draw(SpriteBatch, tempTransform, Tint.NOOP_TINT);
            return this;
        }

        public Surface drawLine(float f1, float f2, float f3, float f4, float f5)
        {
            throw new NotImplementedException();
        }

        public Surface fillRect(float x, float y, float w, float h)
        {
            tempTransform.set(RootTransform);
            tempTransform.translate(x, y);
            tempTransform.scale(w, h);
            XColor color = XNACanvas.GetXNAColor(_fillColor);
            SpriteBatch.Draw(rectTexture, new Vector2(tempTransform.tx(), tempTransform.ty()), new Rectangle(0, 0, rectTexture.Width, rectTexture.Height),
                color, tempTransform.rotation(), Vector2.Zero, new Vector2(tempTransform.scaleX(), tempTransform.scaleY()), SpriteEffects.None, 0);
            return this;
        }

        public Surface fillTriangles(float[] farr1, float[] farr2, int[] iarr)
        {
            throw new NotImplementedException();
        }

        public Surface fillTriangles(float[] farr, int[] iarr)
        {
            throw new NotImplementedException();
        }

        public float height()
        {
            return PlayN.graphics().height();
        }

        public Surface restore()
        {
            throw new NotImplementedException();
        }

        public Surface rotate(float f)
        {
            throw new NotImplementedException();
        }

        public Surface save()
        {
            throw new NotImplementedException();
        }

        public Surface scale(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public Surface setAlpha(float f)
        {
            throw new NotImplementedException();
        }

        public Surface setFillColor(int fillColor)
        {
            this._fillColor = fillColor;
            return this;
        }

        public Surface setFillPattern(Pattern p)
        {
            throw new NotImplementedException();
        }

        public Surface setShader(playn.core.gl.GLShader gls)
        {
            throw new NotImplementedException();
        }

        public Surface setTint(int i)
        {
            throw new NotImplementedException();
        }

        public Surface setTransform(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            throw new NotImplementedException();
        }

        public Surface transform(float f1, float f2, float f3, float f4, float f5, float f6)
        {
            throw new NotImplementedException();
        }

        public Surface translate(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public float width()
        {
            return PlayN.graphics().width();
        }
    }
}
