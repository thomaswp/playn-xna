using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNTest
{
    class XNAImageLayer : XNALayer, ImageLayer
    {
        XNAImage _image;

        public XNAImageLayer(XNAImage image = null)
        {
            _image = image;
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
            return _image;
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

        public override void draw(SpriteBatch spritebatch, InternalTransform parentTransform)
        {
            if (!visible()) return;
            _image.draw(spritebatch, getLocalTransform(parentTransform));
        }
    }
}
