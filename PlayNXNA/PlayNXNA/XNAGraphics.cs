using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    class XNAGraphics : Graphics
    {
        private readonly XNAGroupLayer _rootLayer;

        public XNAGraphics()
        {
            _rootLayer = new XNAGroupLayer();
        }

        public Font createFont(string str, Font.Style fs, float f)
        {
            throw new NotImplementedException();
        }

        public GroupLayer.Clipped createGroupLayer(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public GroupLayer createGroupLayer()
        {
            return new XNAGroupLayer();
        }

        public CanvasImage createImage(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public ImageLayer createImageLayer()
        {
            return new XNAImageLayer();
        }

        public ImageLayer createImageLayer(Image i)
        {
            return new XNAImageLayer(i);
        }

        public ImmediateLayer createImmediateLayer(ImmediateLayer.Renderer ilr)
        {
            throw new NotImplementedException();
        }

        public ImmediateLayer.Clipped createImmediateLayer(int i1, int i2, ImmediateLayer.Renderer ilr)
        {
            throw new NotImplementedException();
        }

        public Gradient createLinearGradient(float f1, float f2, float f3, float f4, int[] iarr, float[] farr)
        {
            throw new NotImplementedException();
        }

        public Gradient createRadialGradient(float f1, float f2, float f3, int[] iarr, float[] farr)
        {
            throw new NotImplementedException();
        }

        public SurfaceImage createSurface(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public SurfaceLayer createSurfaceLayer(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public playn.core.gl.GLContext ctx()
        {
            throw new NotImplementedException();
        }

        public playn.core.gl.GL20 gl20()
        {
            throw new NotImplementedException();
        }

        public int height()
        {
            throw new NotImplementedException();
        }

        public TextLayout layoutText(string str, TextFormat tf)
        {
            throw new NotImplementedException();
        }

        public GroupLayer rootLayer()
        {
            return _rootLayer;
        }

        public float scaleFactor()
        {
            throw new NotImplementedException();
        }

        public int screenHeight()
        {
            throw new NotImplementedException();
        }

        public int screenWidth()
        {
            throw new NotImplementedException();
        }

        public int width()
        {
            throw new NotImplementedException();
        }

        public void draw()
        {
            _rootLayer.draw();
        }
    }
}
