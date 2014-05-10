using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PRectangle = pythagoras.f.Rectangle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNAImageRegion : XNAImage, Image.Region
    {
        private XNAImage _parent;
        private PRectangle bounds;

        public XNAImageRegion(XNAImage parent, float x, float y, float width, float height)
        {
            _parent = parent;
            bounds = new PRectangle(x, y, width, height);
        }

        public Image parent()
        {
            return _parent;
        }

        public void setBounds(float x, float y, float width, float height)
        {
            bounds.setBounds(x, y, width, height);
        }

        public float x()
        {
            return bounds.x();
        }

        public float y()
        {
            return bounds.y();
        }

        public override void addCallback(playn.core.util.Callback callback)
        {
            _parent.addCallback(callback);
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spritebatch, InternalTransform transform, float width, float height)
        {
            texture = _parent.Texture;
            if (texture == null) return;
            Rectangle sourceRect = new Rectangle((int)x(), (int)y(), (int)bounds.width(), (int)bounds.height());
            spritebatch.Draw(texture, new Vector2(transform.tx(), transform.ty()), sourceRect, Microsoft.Xna.Framework.Color.White, 
                transform.rotation(), Vector2.Zero, new Vector2(transform.scaleX(), transform.scaleY()), SpriteEffects.None, 0);
        }
    }
}
