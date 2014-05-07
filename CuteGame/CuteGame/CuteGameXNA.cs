using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PlayNXNA;

namespace CuteGame
{
    public class CuteGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            Game game = new playn.sample.cute.core.CuteGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }
    }
}
