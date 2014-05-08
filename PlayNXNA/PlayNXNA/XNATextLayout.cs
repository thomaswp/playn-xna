using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNATextLayout : AbstractTextLayout
    {
        public XNATextLayout(string text, TextFormat format, pythagoras.f.Rectangle bounds)
            : base(text, format, bounds)
        {

        }

        public override float ascent()
        {
            return 1; //TODO'
        }

        public override float descent()
        {
            return 1; //TODO'
        }

        public override float leading()
        {
            return 1; //TODO'
        }
    }
}
