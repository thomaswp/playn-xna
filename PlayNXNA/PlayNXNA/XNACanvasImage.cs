using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNACanvasImage : XNAImage, CanvasImage
    {
        XNACanvas _canvas;

        public XNACanvasImage(float width, float height)
        {
            _canvas = new XNACanvas(width, height);
        }

        public Canvas canvas()
        {
            return _canvas;
        }

        public void setRgb(int i1, int i2, int i3, int i4, int[] iarr, int i5, int i6)
        {
            throw new NotImplementedException();
        }

        public Image snapshot()
        {
            throw new NotImplementedException();
        }

        public override void addCallback(playn.core.util.Callback callback)
        {
            callback.onSuccess(this);
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform transform)
        {
            this.texture = _canvas.Texture;
            base.draw(spritebatch, transform);
        }
    }
}
