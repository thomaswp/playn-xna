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
        private XNAImmediateSurface surface;

        public XNAImmediateLayer(ImmediateLayer.Renderer renderer)
        {
            _renderer = renderer;
            surface = new XNAImmediateSurface();
        }

        public ImmediateLayer.Renderer renderer()
        {
            return _renderer;
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform parentTransform, int parentTint) //TODO: tint
        {
            if (!visible()) return;
            InternalTransform xform = getLocalTransform(parentTransform);
            surface.RootTransform = xform;
            surface.SpriteBatch = spritebatch;
            _renderer.render(surface);
        }
    }
}
