using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PlayNXNA
{
    class XNAAssets : AbstractAssets
    {
        private XNAPlatform platform;
        private Dictionary<String, Texture2D> textureCache = new Dictionary<String, Texture2D>();

        public static string getAssetPath(String path)
        {
            path = "assets\\" + path;
            return path;
        }

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
            // this isn't possible on XNA because assets are compiled
            // TODO: think of an elegant way not to kill this request
            throw new NotImplementedException();
        }

        protected override Image createStaticImage(object obj, playn.core.gl.Scale s)
        {
            return new XNAStaticImage((Texture2D)obj, s);
        }

        public override Sound getSound(string path)
        {
            SoundEffect effect = platform.Content.Load<SoundEffect>(getAssetPath(path));
            effect.Name = path;
            return ((XNAAudio)platform.audio()).createSound(effect);
        }

        public override Sound getMusic(string path)
        {
            return base.getMusic(path);
        }

        public override string getTextSync(string file)
        {
            return platform.Content.Load<String>(getAssetPath(file));
        }

        protected override Image loadImage(string path, AbstractAssets.ImageReceiver aair)
        {
            Texture2D texture;
            if (textureCache.ContainsKey(path))
            {
                texture = textureCache[path];
            }
            else
            {
                try
                {
                    Console.WriteLine("Loading: " + path);
                    texture = platform.Content.Load<Texture2D>(getAssetPath(path));
                    textureCache.Add(path, texture);
                }
                catch (Exception e)
                {
                    return aair.loadFailed(e);
                }
            }
            return aair.imageLoaded(texture, new playn.core.gl.Scale(1));
        }
    }
}
