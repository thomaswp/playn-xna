using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.sample.hello.core;
using playn.core;

namespace PlayNTest
{
    public class HelloGameXNA : XNAGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            HelloGame game = new HelloGame();
            PlayN.run(game);
        }

        protected override XNAPlatform registerPlatform()
        {
            return XNAPlatform.register();
        }
    }
}
