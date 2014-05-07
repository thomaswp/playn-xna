using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
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
            impl.clear(this);
        }

        public Layer get(int i)
        {
            return (Layer) impl.children.get(i);
        }

        public void remove(Layer l)
        {
            impl.remove(this, (AbstractLayer) l);
        }

        public int size()
        {
            return impl.children.size();
        }

        public void depthChanged(Layer l, float f)
        {
            impl.depthChanged(this, l, f);
        }

        public override void draw()
        {
            for (int i = 0, size = impl.children.size(); i < size; i++)
            {
                ((XNALayer)impl.children.get(i)).draw();
            }
        }
    }
}
