using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    public class XNAPlatform : AbstractPlatform
    {
        public class Config
        {

        }

        public static XNAPlatform register()
        {
            return register(new Config());
        }

        public static XNAPlatform register(Config config)
        {
            XNAPlatform platform = new XNAPlatform();
            PlayN.setPlatform(platform);
            return platform;
        }

        private readonly XNAGraphics _graphics;
        private readonly XNAAssets _assets;
        private readonly XNAPointer _pointer;

        private Game game;
        private readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public XNAPlatform() : base(new XNALog())
        {
            _graphics = new XNAGraphics();
            _assets = new XNAAssets(this);
            _pointer = new XNAPointer();
        }

        public override Analytics analytics()
        {
            throw new NotImplementedException();
        }

        public override Assets assets()
        {
            return _assets;
        }

        public override Audio audio()
        {
            throw new NotImplementedException();
        }

        public override Graphics graphics()
        {
            return _graphics;
        }

        public override Json json()
        {
            throw new NotImplementedException();
        }

        public override Keyboard keyboard()
        {
            throw new NotImplementedException();
        }

        public override Mouse mouse()
        {
            throw new NotImplementedException();
        }

        public override Net net()
        {
            throw new NotImplementedException();
        }

        public override void openURL(string value)
        {
            throw new NotImplementedException();
        }

        public override Pointer pointer()
        {
            return _pointer;
        }

        public override float random()
        {
            throw new NotImplementedException();
        }

        public override RegularExpression regularExpression()
        {
            throw new NotImplementedException();
        }

        public override void run(Game game)
        {
            this.game = game;
            stopwatch.Start();
            game.init();
        }

        public override void setPropagateEvents(bool value)
        {
            throw new NotImplementedException();
        }

        public override Storage storage()
        {
            throw new NotImplementedException();
        }

        public override int tick()
        {
            return (int) stopwatch.ElapsedMilliseconds;
        }

        public override double time()
        {
            throw new NotImplementedException();
        }

        public override Touch touch()
        {
            throw new NotImplementedException();
        }

        public override Platform.Type type()
        {
            throw new NotImplementedException();
        }

        public override void invokeAsync(java.lang.Runnable action)
        {
            action.run(); // TODO: async
        }

        internal void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (game != null)
            {
                game.tick(tick());
            }
        }

        internal void draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (game != null)
            {
                _graphics.draw();
            }
        }
    }
}
