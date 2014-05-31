using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace PlayNXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public abstract class XNAGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private XNAPlatform platform;

        protected abstract XNAPlatform registerPlatform();

        public XNAGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = new ThreadSafeContentManager(Services);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            platform = registerPlatform();
            platform.DeviceManager = graphics;
            platform.Content = Content;
            platform.XnaGame = this;
            Window.Title = platform.config.name;
            graphics.PreferredBackBufferWidth = platform.config.width;
            graphics.PreferredBackBufferHeight = platform.config.height;
            graphics.IsFullScreen = platform.config.fullscreen;
            graphics.ApplyChanges();
            sw.Start();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            platform.update(gameTime);

            base.Update(gameTime);
        }

        int frames;
        Stopwatch sw = new Stopwatch();

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frames++;
            if (frames > 60)
            {
                int fps = frames * 1000 / (int)sw.ElapsedMilliseconds;
                Console.WriteLine("fps: " + fps);
                sw.Reset();
                sw.Start();
                frames = 0;
            }

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            platform.draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
