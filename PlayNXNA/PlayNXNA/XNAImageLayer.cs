using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNXNA
{
    class XNAImageLayer : XNALayer, ImageLayer
    {
        XNAImage _image; 
        private float _width, _height;
        private bool widthSet, heightSet;

        public XNAImageLayer(XNAImage image = null)
        {
            _image = image;
        }

        public void clearHeight()
        {
            heightSet = false;
        }

        public void clearWidth()
        {
            widthSet = false;
        }

        public override float height()
        {
            return heightSet ? _height : ((Image)Asserts.checkNotNull(_image)).height();
        }

        public Image image()
        {
            return _image;
        }

        public void setHeight(float height)
        {
            heightSet = true;
            _height = height;
        }

        public ImageLayer setImage(Image image)
        {
            this._image = (XNAImage) image;
            return this;
        }

        public void setSize(float width, float height)
        {
            setWidth(width);
            setHeight(height);
        }

        public void setWidth(float width)
        {
            widthSet = true;
            _width = width;
        }

        public override float width()
        {
            return widthSet ? _width : ((Image)Asserts.checkNotNull(_image)).width();
        }

        public float scaledHeight()
        {
            return scaleY() * height();
        }

        public float scaledWidth()
        {
            return scaleX() * width();
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform parentTransform)
        {
            if (_image == null || !visible()) return;
            _image.draw(spritebatch, getLocalTransform(parentTransform), width(), height(), tint(), alpha());
        }
    }
}
