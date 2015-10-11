using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Windfish.Core;

namespace Windfish.Editor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class App : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World _world;
        KeyboardState _keyboardState;
        KeyboardState _lastKeyboardState;

        public App() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            _world = new World();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //if (!File.Exists("World.map")) {

            //}
            _world = World.Load("World.map");
            //_world.New();
            //_world.Save("World.map");

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
            Tileset tileset = new Tileset("Textures/overworld");
            tileset.LoadContent(Content);

            _world.LoadContent(Content);

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
            _keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Window.Title = string.Format("Windfish Editor (X: {0} Y: {1})", _world.CurrentScreenPosition.X, _world.CurrentScreenPosition.Y);

            _world.Update(gameTime);

            if (_keyboardState.IsKeyDown(Keys.Up) && !_lastKeyboardState.IsKeyDown(Keys.Up)) {
                _world.MoveToScreen(new Vector2(_world.CurrentScreenPosition.X, _world.CurrentScreenPosition.Y - 1));
            }
            if (_keyboardState.IsKeyDown(Keys.Down) && !_lastKeyboardState.IsKeyDown(Keys.Down)) {
                _world.MoveToScreen(new Vector2(_world.CurrentScreenPosition.X, _world.CurrentScreenPosition.Y + 1));
            }
            if (_keyboardState.IsKeyDown(Keys.Left) && !_lastKeyboardState.IsKeyDown(Keys.Left)) {
                _world.MoveToScreen(new Vector2(_world.CurrentScreenPosition.X - 1, _world.CurrentScreenPosition.Y));
            }
            if (_keyboardState.IsKeyDown(Keys.Right) && !_lastKeyboardState.IsKeyDown(Keys.Right)) {
                _world.MoveToScreen(new Vector2(_world.CurrentScreenPosition.X + 1, _world.CurrentScreenPosition.Y));
            }
            _lastKeyboardState = _keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(2f));

            _world.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
