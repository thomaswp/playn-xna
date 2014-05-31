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

            Importer.Import();
            Game game = new playn.showcase.core.Showcase(this);
            //Game game = new CanvasTestGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            XNAPlatform.Config config = new XNAPlatform.Config();
            config.name = "PlayN Showcase XNA";
            return XNAPlatform.register(config);
        }

        public string info()
        {
            return "XNA Renderer";
        }
    }
}
