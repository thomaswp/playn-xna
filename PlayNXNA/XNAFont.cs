using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public struct FontInfo
    {
        public SpriteFont font;
        public float size;
        public Font.Style style;
    }

    public class XNAFont : AbstractFont
    {
        public readonly FontInfo fontInfo;
        public float Scale { get { return size() / fontInfo.size; } }

        public XNAFont(Graphics graphics, string name, Font.Style style, float size, FontInfo info) : base(graphics, name, style, size)
        {
            fontInfo = info;
        }
    }
}
