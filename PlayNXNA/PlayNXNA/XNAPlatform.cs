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
            public int width = 800, height = 480;
            public bool fullscreen = false;
            public string name = "Game";
        }

        public readonly Config config;

        public static XNAPlatform register()
        {
            return register(new Config());
        }

        public static XNAPlatform register(Config config)
        {
            XNAPlatform platform = new XNAPlatform(config);
            PlayN.setPlatform(platform);
            return platform;
        }

        private readonly XNAGraphics _graphics;
        private readonly XNAAssets _assets;
        private readonly XNAMouse _mouse;
        private readonly XNAPointer _pointer;
        private readonly XNATouch _touch;
        private readonly XNAKeyboard _keyboard;
        private readonly XNAJson _json;
        private readonly XNANet _net;
        private readonly XNAAudio _audio;
        private readonly XNAStorage _storage;

        private Game game;
        private readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public Microsoft.Xna.Framework.GraphicsDeviceManager DeviceManager { get; set; }
        public Microsoft.Xna.Framework.Content.ContentManager Content { get; set; }

        public XNAPlatform(Config config) : base(new XNALog())
        {
            this.config = config;

            _graphics = new XNAGraphics();
            _assets = new XNAAssets(this);
            _mouse = new XNAMouse();
            _pointer = new XNAPointer();
            _touch = new XNATouch();
            _keyboard = new XNAKeyboard();
            _json = new XNAJson();
            _net = new XNANet(this);
            _audio = new XNAAudio(this);
            _storage = new XNAStorage();
        }

        public override Assets assets()
        {
            return _assets;
        }

        public override Audio audio()
        {
            return _audio;
        }

        public override Graphics graphics()
        {
            return _graphics;
        }

        public override Json json()
        {
            return _json;
        }

        public override Keyboard keyboard()
        {
            return _keyboard;
        }

        public override Mouse mouse()
        {
            return _mouse;
        }

        public override Net net()
        {
            return _net;
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
            _pointer.setPropagateEvents(value);
            _mouse.setPropagateEvents(value);
        }

        public override Storage storage()
        {
            return _storage;
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
            return _touch;
        }

        public override Platform.Type type()
        {
            return null;
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
                _keyboard.update();
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
