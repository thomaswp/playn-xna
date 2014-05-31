﻿using System;
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

            Importer.Import();

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
