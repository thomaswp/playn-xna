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
            : base(text, format, new Rectangle(0, 0, 100, 50))
        {

        }

        public override float ascent()
        {
            return 15; //TODO'
        }

        public override float descent()
        {
            return 10; //TODO'
        }

        public override float leading()
        {
            return 1; //TODO'
        }
    }
}
