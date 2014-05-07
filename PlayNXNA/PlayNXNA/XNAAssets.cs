using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    class XNAAssets : AbstractAssets
    {
        public XNAAssets(AbstractPlatform platform) : base(platform)
        {

        }

        protected override AsyncImage createAsyncImage(float preWidth, float preHeight)
        {
            return new XNAAsyncImage(preWidth, preHeight);
        }

        protected override Image createStaticImage(object obj, playn.core.gl.Scale s)
        {
            throw new NotImplementedException();
        }

        public override Sound getSound(string value)
        {
            throw new NotImplementedException();
        }

        public override string getTextSync(string value)
        {
            throw new NotImplementedException();
        }

        protected override Image loadImage(string str, AbstractAssets.ImageReceiver aair)
        {
            return aair.imageLoaded(new XNAImage(), new playn.core.gl.Scale(1));
        }
    }
}
