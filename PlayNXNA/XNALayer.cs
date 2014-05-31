using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNXNA
{
    public abstract class XNALayer : AbstractLayer
    {
        private InternalTransform localTransform = new StockInternalTransform();

        protected InternalTransform getLocalTransform(InternalTransform parentTransform)
        {
            localTransform.set(parentTransform);
            return localTransform.concatenate(transform(), originX(), originY());
        }

        protected int getLocalTint(int parentTint)
        {
            int tint = this.tint();
            return tint == Tint.NOOP_TINT ? parentTint : Tint.combine(parentTint, tint);
        }

        public abstract void draw(SpriteBatch spritebatch, InternalTransform parentTransform, int parentTint);
    }
}
