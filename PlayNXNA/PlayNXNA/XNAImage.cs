using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using playn.core.gl;
using playn.core.util;
using Microsoft.Xna.Framework;

namespace PlayNXNA
{
    public abstract class XNAImage : Image
    {
        protected Texture2D texture;
        protected bool _repeatX, _repeatY;
        protected bool mipmapped;
        protected Scale _scale;

        internal Texture2D Texture { get { return texture; } }

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
            return isReady() ? texture.Height : 0;
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

        public Image.Region subImage(float x, float y, float width, float height)
        {
            return new XNAImageRegion(this, x, y, width, height);
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
            return isReady() ? texture.Width : 0;
        }

        public bool isReady()
        {
            return texture != null;
        }

        public virtual void draw(SpriteBatch spritebatch, InternalTransform transform, float width, float height)
        {
            if (texture == null) return;
            if (repeatX() || repeatY())
            {
                spritebatch.End();
                spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap,
                        DepthStencilState.Default, RasterizerState.CullNone);

                Rectangle srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
                if (repeatX()) srcRect.Width = (int)width;
                if (repeatY()) srcRect.Height = (int)height;
                spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), srcRect,
                    Microsoft.Xna.Framework.Color.White, transform.rotation(), Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);

                spritebatch.End();
                spritebatch.Begin();
            }
            else
            {
                spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), new Rectangle(0, 0, texture.Width, texture.Height),
                    Microsoft.Xna.Framework.Color.White, transform.rotation(), Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);
            }
        }

        public void draw(SpriteBatch spritebatch, InternalTransform transform)
        {
            draw(spritebatch, transform, width(), height());
        }
    }
}
