using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlayNXNA
{
    public class XNAGroupLayer : XNALayer, GroupLayer, ParentLayer
    {
        GroupLayerImpl impl;

        public XNAGroupLayer()
        {
            impl = new GroupLayerImpl();
        }

        public void add(Layer l)
        {
            impl.add(this, (AbstractLayer) l);
        }

        public void addAt(Layer l, float f1, float f2)
        {
            impl.addAt(this, l, f1, f2);
        }

        public void clear()
        {
            removeAll();
        }

        public void destroyAll()
        {
            impl.destroyAll(this);
        }

        public Layer get(int i)
        {
            return (Layer) impl.children.get(i);
        }

        public void remove(Layer l)
        {
            impl.remove(this, (AbstractLayer) l);
        }

        public void removeAll()
        {
            impl.removeAll(this);
        }

        public int size()
        {
            return impl.children.size();
        }

        public void depthChanged(Layer l, float f)
        {
            impl.depthChanged(this, l, f);
        }

        public override void draw(SpriteBatch spritebatch, InternalTransform parentTransform, int parentTint)
        {
            if (!visible()) return;

            for (int i = 0, size = impl.children.size(); i < size; i++)
            {
                ((XNALayer)impl.children.get(i)).draw(spritebatch, getLocalTransform(parentTransform), getLocalTint(parentTint));
            }
        }

        public override Layer hitTestDefault(pythagoras.f.Point p)
        {
            Layer answer = impl.hitTest(this, p);
            return answer;
        }
    }
}
