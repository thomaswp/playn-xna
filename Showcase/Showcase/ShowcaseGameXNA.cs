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

            Game game = new playn.showcase.core.Showcase(this);
            //Game game = new CanvasTestGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }

        public string info()
        {
            return "XNA Renderer";
        }
    }
}
