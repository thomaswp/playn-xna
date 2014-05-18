using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PlayNXNA;

namespace TuxBlocks
{
    public class TuxBlocksGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            XNAGraphics graphics = (XNAGraphics)PlayN.graphics();
            graphics.registerFont("Arial", "Arial-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Arial", "Arial-24-Bold", 24, Font.Style.BOLD);
            graphics.registerFont("Arial", "Arial-24-Italic", 24, Font.Style.ITALIC);
            graphics.registerFont("Arial", "Arial-24-Bold-Italic", 24, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Arial", "Arial-12", 12, Font.Style.PLAIN);
            graphics.registerFont("Arial", "Arial-12-Bold", 12, Font.Style.BOLD);
            graphics.registerFont("Arial", "Arial-12-Italic", 12, Font.Style.ITALIC);
            graphics.registerFont("Arial", "Arial-12-Bold-Italic", 12, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Arial", "Arial-9", 9, Font.Style.PLAIN);
            graphics.registerFont("Arial", "Arial-48", 48, Font.Style.PLAIN);
            graphics.registerFont("Raavi", "Raavi-12", 12, Font.Style.PLAIN);
            graphics.registerFont("Mangal", "Mangal-12", 12, Font.Style.PLAIN);

            Game game = new tuxkids.tuxblocks.core.TuxBlocksGame("en");
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            XNAPlatform.Config config = new XNAPlatform.Config();
            config.width = 1000;
            config.height = 620;
            return XNAPlatform.register(config);
        }
    }
}
