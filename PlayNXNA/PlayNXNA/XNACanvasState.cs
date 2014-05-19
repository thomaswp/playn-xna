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
        public float alpha;
        public float strokeWidth;
        public Rectangle clipRect;
        public XNAPath clipPath;
        public InternalTransform transform;
        public Canvas.LineCap lineCap;
        public Canvas.LineJoin lineJoin;

        public static XNACanvasState create(Texture2D texture)
        {
            XNACanvasState state = new XNACanvasState();
            state.clipRect = new Rectangle(0, 0, texture.Width, texture.Height);
            state.transform = new StockInternalTransform();

            state.strokeWidth = 1;
            state.alpha = 1;
            state.strokeColor = XNACanvas.argb(255, 0, 0, 255);
            state.fillColor = XNACanvas.argb(255, 0, 0, 0);
            state.lineCap = Canvas.LineCap.SQUARE;
            state.lineJoin = Canvas.LineJoin.MITER;
            return state;
        }

        public void copyFields()
        {
            transform = transform.copy();
        }
    }
}
