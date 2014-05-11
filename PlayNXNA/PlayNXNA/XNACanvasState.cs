using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public struct XNACanvasState
    {
        public int strokeColor, fillColor;
        public float strokeWidth;
        public Rectangle clipRect;
        public InternalTransform transform;

        public static XNACanvasState create(Texture2D texture)
        {
            XNACanvasState state = new XNACanvasState();
            state.clipRect = new Rectangle(0, 0, texture.Width, texture.Height);
            state.transform = new StockInternalTransform();

            state.strokeWidth = 1;
            return state;
        }

        public void copyFields()
        {
            transform = transform.copy();
        }
    }
}
