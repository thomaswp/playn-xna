using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    class XNAImmediateSurface : Surface
    {
        public SpriteBatch SpriteBatch { get; set; }
        public InternalTransform RootTransform { get; set; }

        private InternalTransform tempTransform = new StockInternalTransform();

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

        public Surface drawImage(Image i, float f1, float f2)
        {
            tempTransform.set(RootTransform);
            tempTransform.translate(f1, f2);
            ((XNAImage)i).draw(SpriteBatch, tempTransform);
            return this;
        }

        public Surface drawImageCentered(Image i, float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public Surface drawLayer(Layer l)
        {
            throw new NotImplementedException();
        }

        public Surface drawLine(float f1, float f2, float f3, float f4, float f5)
        {
            throw new NotImplementedException();
        }

        public Surface fillRect(float f1, float f2, float f3, float f4)
        {
            throw new NotImplementedException();
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

        public Surface setFillColor(int i)
        {
            throw new NotImplementedException();
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
