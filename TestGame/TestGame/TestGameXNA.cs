using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PlayNXNA;

namespace TestGame
{
    public class TestGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            Game game = new CanvasTestGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }
    }
}
