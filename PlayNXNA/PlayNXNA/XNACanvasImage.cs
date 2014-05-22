using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNXNA
{
    public class XNACanvasImage : XNAImage, CanvasImage
    {
        XNACanvas _canvas;

        public XNACanvasImage(float width, float height)
        {
            _canvas = new XNACanvas(width, height);
            this.texture = _canvas.Texture;
        }

        public override float width()
        {
            return _canvas.width();
        }

        public override float height()
        {
            return _canvas.height();
        }

        public Canvas canvas()
        {
            return _canvas;
        }

        public void setRgb(int startX, int startY, int width, int height, int[] array, int offset, int count)
        {
            if (texture == null) return;
            int[] data = new int[width * height];
            int max = array.Length;
            for (int i = 0; i < data.Length; i++)
            {
                int x = i % width, y = i / width;
                int offI = offset + y * count + x;
                if (offI < max) data[i] = XNACanvas.colorSwapRB(array[offI]);
                else break;
            }
            texture.SetData<int>(0, new Rectangle(startX, startY, width, height), data, 0, data.Length);
        }

        public override void getRgb(int startX, int startY, int width, int height, int[] array, int offset, int count)
        {
            texture = _canvas.Texture;
            base.getRgb(startX, startY, width, height, array, offset, count);
        }

        public Image snapshot()
        {
            throw new NotImplementedException();
        }

        public override void addCallback(playn.core.util.Callback callback)
        {
            callback.onSuccess(this);
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform transform, float width, float height, int color)
        {
            this.texture = _canvas.Texture;
            base.draw(spritebatch, transform, width, height, color);
        }
    }
}
