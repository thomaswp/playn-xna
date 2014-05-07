using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    class XNAImageLayer : XNALayer, ImageLayer
    {
        public XNAImageLayer(Image image = null)
        {
        }

        public void clearHeight()
        {
            throw new NotImplementedException();
        }

        public void clearWidth()
        {
            throw new NotImplementedException();
        }

        public Image image()
        {
            throw new NotImplementedException();
        }

        public void setHeight(float f)
        {
            throw new NotImplementedException();
        }

        public ImageLayer setImage(Image i)
        {
            throw new NotImplementedException();
        }

        public void setSize(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public void setWidth(float f)
        {
            throw new NotImplementedException();
        }


        public float scaledHeight()
        {
            throw new NotImplementedException();
        }

        public float scaledWidth()
        {
            throw new NotImplementedException();
        }

        public override void draw()
        {
            Console.WriteLine("Draing image layer: " + this);
        }
    }
}
