using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
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
        private readonly XNAMouse _mouse;
        private readonly XNAPointer _pointer;
        private readonly XNAKeyboard _keyboard;

        private Game game;
        private readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public Microsoft.Xna.Framework.GraphicsDeviceManager GraphicsDevice { get; set; }
        public Microsoft.Xna.Framework.Content.ContentManager Content { get; set; }

        public XNAPlatform() : base(new XNALog())
        {
            _graphics = new XNAGraphics();
            _assets = new XNAAssets(this);
            _mouse = new XNAMouse();
            _pointer = new XNAPointer();
            _keyboard = new XNAKeyboard();
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
            return _keyboard;
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
            return new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
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
                _mouse.update();
                game.tick(tick());
                runQueue.execute();
            }
        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (game != null)
            {
                _graphics.draw(spriteBatch);
            }
        }
    }
}
