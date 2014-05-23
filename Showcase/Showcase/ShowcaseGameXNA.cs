using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PlayNXNA;

namespace Showcase
{
    public class ShowcaseGameXNA : XNAGame, playn.showcase.core.Showcase.DeviceService
    {
        protected override void Initialize()
        {
            base.Initialize();

            XNAGraphics graphics = (XNAGraphics) PlayN.graphics();
            graphics.registerFont("Helvetica", "Helvetica-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Helvetica", "Helvetica-24-Bold", 24, Font.Style.BOLD);
            graphics.registerFont("Helvetica", "Helvetica-24-Italic", 24, Font.Style.ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-24-Bold-Italic", 24, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-12", 12, Font.Style.PLAIN);
            graphics.registerFont("Helvetica", "Helvetica-12-Bold", 12, Font.Style.BOLD);
            graphics.registerFont("Helvetica", "Helvetica-12-Italic", 12, Font.Style.ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-12-Bold-Italic", 12, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-9", 9, Font.Style.PLAIN);
            graphics.registerFont("Museo-300", "Museo-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Courier", "CourierNew-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Courier", "CourierNew-12", 12, Font.Style.PLAIN);
            //Game game = new playn.showcase.core.Showcase(this);
            Game game = new CanvasTestGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            XNAPlatform.Config config = new XNAPlatform.Config();
            config.width = 640;
            config.name = "PlayN Showcase XNA";
            return XNAPlatform.register(config);
        }

        public string info()
        {
            return "XNA Renderer";
        }
    }
}
