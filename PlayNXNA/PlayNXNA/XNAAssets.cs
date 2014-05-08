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

        public override byte[] getBytesSync(string value)
        {
            throw new NotImplementedException();
        }

        protected override Image createStaticImage(object obj, playn.core.gl.Scale s)
        {
            return new XNAStaticImage((Texture2D)obj, s);
        }

        public override Sound getSound(string value)
        {
            return new XNASound();
        }

        public override string getTextSync(string file)
        {
            int ext = file.LastIndexOf(".");
            if (ext >= 0) file = file.Substring(0, ext);
            return platform.Content.Load<String>(file);
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
