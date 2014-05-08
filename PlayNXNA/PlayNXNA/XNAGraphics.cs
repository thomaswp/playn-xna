using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    class XNAGraphics : Graphics
    {
        private readonly XNAGroupLayer _rootLayer;
        private readonly InternalTransform rootXform = new StockInternalTransform();

        public XNAGraphics()
        {
            _rootLayer = new XNAGroupLayer();
        }

        public Font createFont(string name, Font.Style style, float size)
        {
            return new XNAFont(this, name, style, size);
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
            return new XNAImageLayer((XNAImage) i);
        }

        public ImmediateLayer createImmediateLayer(ImmediateLayer.Renderer ilr)
        {
            return new XNAImmediateLayer(ilr);
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
            return ((XNAPlatform) PlayN.platform()).GraphicsDevice.PreferredBackBufferHeight;
        }

        public TextLayout layoutText(string text, TextFormat format)
        {
            return new XNATextLayout(text, format, new pythagoras.f.Rectangle());
        }

        public TextLayout[] layoutText(string text, TextFormat format, TextWrap wrap)
        {
            return new XNATextLayout[] { new XNATextLayout(text, format, new pythagoras.f.Rectangle()) };
        }

        public GroupLayer rootLayer()
        {
            return _rootLayer;
        }

        public float scaleFactor()
        {
            return 1;
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
            return ((XNAPlatform)PlayN.platform()).GraphicsDevice.PreferredBackBufferWidth;
        }

        public void draw(SpriteBatch spritebatch)
        {
            _rootLayer.draw(spritebatch, rootXform);
        }
    }
}
