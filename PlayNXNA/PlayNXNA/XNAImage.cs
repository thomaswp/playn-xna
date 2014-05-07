using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using playn.core.gl;
using playn.core.util;
using Microsoft.Xna.Framework;

namespace PlayNTest
{
    public abstract class XNAImage : Image
    {
        protected Texture2D texture;
        protected bool _repeatX, _repeatY;
        protected bool mipmapped;
        protected Scale _scale;

        public abstract void addCallback(Callback callback);

        public void clearTexture()
        {
        }

        public int ensureTexture()
        {
            return 0;
        }

        public void getRgb(int i1, int i2, int i3, int i4, int[] iarr, int i5, int i6)
        {
            throw new NotImplementedException();
        }

        public virtual float height()
        {
            return texture.Height;
        }

        public bool repeatX()
        {
            return _repeatX;
        }

        public bool repeatY()
        {
            return _repeatY;
        }

        public playn.core.gl.Scale scale()
        {
            return _scale;
        }

        public void setMipmapped(bool mipmapped)
        {
            this.mipmapped = mipmapped;
        }

        public void setRepeat(bool repeatX, bool repeatY)
        {
            _repeatX = repeatX;
            _repeatY = repeatY;
        }

        public Image.Region subImage(float f1, float f2, float f3, float f4)
        {
            throw new NotImplementedException();
        }

        public Pattern toPattern()
        {
            throw new NotImplementedException();
        }

        public Image transform(Image.BitmapTransformer ibt)
        {
            throw new NotImplementedException();
        }

        public virtual float width()
        {
            return texture.Width;
        }

        public bool isReady()
        {
            return texture != null;
        }

        public void draw(SpriteBatch spritebatch, InternalTransform transform)
        {
            if (texture == null) return;
            spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), new Rectangle(0, 0, texture.Width, texture.Height), 
                Microsoft.Xna.Framework.Color.White, transform.rotation(), Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);
        }
    }
}
