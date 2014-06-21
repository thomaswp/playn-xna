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

        public virtual void getRgb(int startX, int startY, int width, int height, int[] array, int offset, int count)
        {
            if (texture == null) return;
            int[] data = new int[width * height];
            texture.GetData<int>(0, new Rectangle(startX, startY, width, height), data, 0, data.Length);
            int max = array.Length;
            for (int i = 0; i < data.Length; i++)
            {
                int x = i % width, y = i / width;
                int offI = offset + y * count + x;
                if (offI < max) array[offI] = XNACanvas.colorSwapRB(data[i]);
                else break;    
            }
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
            return new XNAPattern(this, repeatX(), repeatY());
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

        protected Microsoft.Xna.Framework.Color getDrawColor(int color)
        {   
            Microsoft.Xna.Framework.Color xcolor = XNACanvas.GetXNAColor(color);
            xcolor *= xcolor.A / 255f;
           // if (xcolor.A < 255) Console.WriteLine(xcolor.A - alpha * 255);
            return xcolor;
        }

        public virtual void draw(SpriteBatch spritebatch, InternalTransform transform, float width, float height, int color)
        {
            if (texture == null) return;
            Microsoft.Xna.Framework.Color xcolor = getDrawColor(color);
            if (repeatX() || repeatY())
            {
                spritebatch.End();
                spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap,
                        DepthStencilState.Default, RasterizerState.CullNone);

                Rectangle srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
                if (repeatX()) srcRect.Width = (int)width;
                if (repeatY()) srcRect.Height = (int)height;
                spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), srcRect,
                    xcolor, transform.rotation(), Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);

                spritebatch.End();
                spritebatch.Begin();
            }
            else
            {
                float rotation = 0;
                try
                {
                    rotation = transform.rotation();
                }
                catch { }
                spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), new Rectangle(0, 0, texture.Width, texture.Height),
                    xcolor, rotation, Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);
            }
        }

        public void draw(SpriteBatch spritebatch, InternalTransform transform)
        {
            draw(spritebatch, transform, width(), height(), XNACanvas.argb(255, 255, 255, 255));
        }
    }
}
