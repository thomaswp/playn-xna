using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    class XNAAsyncImage : XNAImage, AsyncImage
    {
        private float preWidth;
        private float preHeight;

        public XNAAsyncImage(float preWidth, float preHeight)
        {
            this.preWidth = preWidth;
            this.preHeight = preHeight;
        }

        public void setError(Exception t)
        {
            throw new NotImplementedException();
        }

        public void setImage(object obj, playn.core.gl.Scale s)
        {
            throw new NotImplementedException();
        }
    }
}
