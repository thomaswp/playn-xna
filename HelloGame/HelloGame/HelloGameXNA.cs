using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using PlayNXNA;

namespace HelloGame
{
    public class HelloGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            Game game = new playn.sample.hello.core.HelloGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }
    }
}
