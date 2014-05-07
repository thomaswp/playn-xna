using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    class XNAAssets : AbstractAssets
    {
        XNAPlatform platform;

        public XNAAssets(XNAPlatform platform) : base(platform)
        {
            this.platform = platform;
        }

        protected override AsyncImage createAsyncImage(float preWidth, float preHeight)
        {
            return new XNAAsyncImage(preWidth, preHeight);
        }

        protected override Image createStaticImage(object obj, playn.core.gl.Scale s)
        {
            return new XNAStaticImage();
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
            try
            {
                int lastDot = str.IndexOf('.');
                if (lastDot >= 0) str = str.Substring(0, lastDot);
                Texture2D texture = platform.Content.Load<Texture2D>(str);
                return aair.imageLoaded(texture, new playn.core.gl.Scale(1));
            }
            catch (Exception e)
            {
                return aair.loadFailed(e);
            }
        }
    }
}
