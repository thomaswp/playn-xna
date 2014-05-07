using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNTest
{
    public abstract class XNALayer : AbstractLayer
    {
        private InternalTransform localTransform = new StockInternalTransform();

        protected InternalTransform getLocalTransform(InternalTransform parentTransform)
        {
            localTransform.set(parentTransform);
            return localTransform.concatenate(transform(), originX(), originY());
        }

        public abstract void draw(SpriteBatch spritebatch, InternalTransform parentTransform);
    }
}
