using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNAImmediateLayer : XNALayer, ImmediateLayer
    {
        private ImmediateLayer.Renderer _renderer;

        public XNAImmediateLayer(ImmediateLayer.Renderer renderer)
        {
            _renderer = renderer;
        }

        public ImmediateLayer.Renderer renderer()
        {
            return _renderer;
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform parentTransform)
        {
            // TODO
        }
    }
}
