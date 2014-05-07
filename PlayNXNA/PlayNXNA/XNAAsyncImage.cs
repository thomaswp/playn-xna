using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using playn.core.util;

namespace PlayNXNA
{
    class XNAAsyncImage : XNAImage, AsyncImage
    {
        private float preWidth;
        private float preHeight;
        private Exception error;
        private java.util.List callbacks = new java.util.ArrayList();

        public XNAAsyncImage(float preWidth, float preHeight)
        {
            this.preWidth = preWidth;
            this.preHeight = preHeight;
        }

        public override float width()
        {
            return texture == null ? preWidth : base.width();
        }

        public override float height()
        {
            return texture == null ? preHeight : base.height();
        }
        
        public void setError(Exception error)
        {
            this.error = error;
            callbacks = Callbacks.dispatchFailureClear(callbacks, error);
        }

        public void setImage(object tex, playn.core.gl.Scale s)
        {
            texture = (Texture2D)tex;
            _scale = s;
            callbacks = Callbacks.dispatchSuccessClear(callbacks, this);
        }

        public override void addCallback(Callback callback)
        {
            callbacks.add(callback);
        }
    }
}
