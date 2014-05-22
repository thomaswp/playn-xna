using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNAGraphics : Graphics
    {

        private readonly XNAGroupLayer _rootLayer;
        private readonly InternalTransform rootXform = new StockInternalTransform();
        private Dictionary<String, List<FontInfo>> fontMap = new Dictionary<string, List<FontInfo>>();

        public XNAGraphics()
        {
            _rootLayer = new XNAGroupLayer();
        }

        public Font createFont(string name, Font.Style style, float size)
        {
            size *= 0.75f;
            if (!fontMap.ContainsKey(name)) throw new Exception("No registered font: " + name);
            List<FontInfo> fonts = fontMap[name];
            FontInfo best = fonts[0];
            for (int i = 1; i < fonts.Count; i++)
            {
                FontInfo info = fonts[i];
                bool bestStyle = best.style == style;
                bool infoStyle = info.style == style;
                if (bestStyle != infoStyle)
                {
                    best = bestStyle ? best : info;
                    continue;
                }

                bool bestSizeGreater = best.size >= size;
                bool infoSizeGreater = info.size >= size;
                if (bestSizeGreater != infoSizeGreater)
                {
                    best = bestSizeGreater ? best : info;
                    continue;
                }

                best = best.size < info.size ? best : info;
            }
            return new XNAFont(this, name, style, size, best);
        }

        public GroupLayer.Clipped createGroupLayer(float f1, float f2)
        {
            throw new NotImplementedException();
        }

        public GroupLayer createGroupLayer()
        {
            return new XNAGroupLayer();
        }

        public CanvasImage createImage(float width, float height)
        {
            return new XNACanvasImage(width, height);
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
            return null;
        }

        public playn.core.gl.GL20 gl20()
        {
            return null;
        }

        public int height()
        {
            return ((XNAPlatform) PlayN.platform()).DeviceManager.PreferredBackBufferHeight;
        }

        public TextLayout layoutText(string text, TextFormat format)
        {
            return new XNATextLayout(text, format);
        }

        public TextLayout[] layoutText(string text, TextFormat format, TextWrap wrap)
        {
            return XNATextLayout.layoutText(text, format, wrap);
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
            return ((XNAPlatform)PlayN.platform()).DeviceManager.PreferredBackBufferWidth;
        }

        public void registerFont(string name, string path, float size, Font.Style style)
        {
            SpriteFont spritefont = ((XNAPlatform)PlayN.platform()).Content.Load<SpriteFont>(path);
            FontInfo info = new FontInfo();
            info.font = spritefont;
            info.size = size;
            info.style = style;
            if (!fontMap.ContainsKey(name)) fontMap.Add(name, new List<FontInfo>());
            List<FontInfo> fonts = fontMap[name];
            fonts.Add(info);
        }

        public void draw(SpriteBatch spritebatch)
        {
            _rootLayer.draw(spritebatch, rootXform, Tint.NOOP_TINT);
        }
    }
}
