using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNAPattern : Pattern
    {
        private bool repeatX, repeatY;
        private XNAImage image;

        public XNAImage Image { get { return image; } }

        public XNAPattern(XNAImage image, bool repeatX, bool repeatY)
        {
            this.image = image;
            this.repeatX = repeatX;
            this.repeatY = repeatY;
        }

        bool Pattern.repeatX()
        {
            return repeatX;
        }

        bool Pattern.repeatY()
        {
            return repeatY;
        }
    }
}
