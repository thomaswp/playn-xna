using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using pythagoras.f;

namespace PlayNXNA
{
    public class XNATextLayout : AbstractTextLayout
    {

        public XNATextLayout(string text, TextFormat format)
            : base(text, format, getBounds(text, format))
        { }

        private static Rectangle getBounds(string text, TextFormat format)
        {
            XNAFont font = (XNAFont)format.font;
            Microsoft.Xna.Framework.Vector2 size = font.fontInfo.font.MeasureString(text);
            float scale = font.size() / font.fontInfo.size;
            return new Rectangle(0, 0, size.X * scale, size.Y * scale);
        }

        public override float ascent()
        {
            return bounds().height() * 0.8f;
        }

        public override float descent()
        {
            return bounds().height() - ascent();
        }

        public override float leading()
        {
            return 0;
        }
    }
}
