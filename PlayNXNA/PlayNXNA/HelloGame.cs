using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.sample.hello.core;
using playn.core;
using playn.sample.cute.core;

namespace PlayNTest
{
    public class HelloGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            Game game = new HelloGame();
            //Game game = new CuteGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }
    }
}
