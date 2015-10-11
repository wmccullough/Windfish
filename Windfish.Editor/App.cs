using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Linq;
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
        Tileset _tileset;
        public Viewport TilesViewport = new Viewport(160, 0, 384, 400);
        public Viewport MapViewport = new Viewport(0, 0, 320, 256);
        Tile _primaryTile;
        Tile _secondaryTile;
        MouseState _mouse;
        Vector2 _cursorMapPosition;

        public App() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1088;
            _world = new World();
            IsMouseVisible = true;
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
            _tileset = new Tileset("Textures/overworld");
            _tileset.LoadContent(Content);
            _primaryTile = _tileset.Tiles[0];
            _secondaryTile = _tileset.Tiles[1];

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
            _mouse = Mouse.GetState();

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

            if (MapViewport.Bounds.Contains(_mouse.Position)) {
                if (_mouse.LeftButton == ButtonState.Pressed) {
                    Tile tile = (Tile)_primaryTile.Clone();
                    tile.LocalPosition = new Vector3(_cursorMapPosition.X, _cursorMapPosition.Y, 0);
                    tile.WorldPosition = new Vector3(_cursorMapPosition.X + (_world.CurrentScreenPosition.X * Screen.ScreenWidth), _cursorMapPosition.Y + (_world.CurrentScreenPosition.Y * Screen.ScreenHeight), 0);
                    _world.CurrentScreen.SetTile(_cursorMapPosition.X.ToInt32(), _cursorMapPosition.Y.ToInt32(), tile);
                }
                if (_mouse.RightButton == ButtonState.Pressed) {
                    Tile tile = (Tile)_secondaryTile.Clone();
                    tile.LocalPosition = new Vector3(_cursorMapPosition.X, _cursorMapPosition.Y, 0);
                    tile.WorldPosition = new Vector3(_cursorMapPosition.X + (_world.CurrentScreenPosition.X * Screen.ScreenWidth), _cursorMapPosition.Y + (_world.CurrentScreenPosition.Y * Screen.ScreenHeight), 0);
                    _world.CurrentScreen.SetTile(_cursorMapPosition.X.ToInt32(), _cursorMapPosition.Y.ToInt32(), tile);
                }
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

            int counter = 0;

            foreach (int y in Enumerable.Range(0, TilesViewport.Height / 16)) {
                foreach (int x in Enumerable.Range(0, TilesViewport.Width / 16)) {
                    try {
                        _tileset.Tiles[counter].Draw(spriteBatch, new Vector2(TilesViewport.X + (x * 16), y * 16));
                        counter++;
                    } catch (Exception ex) {

                    }
                }
            }

            _secondaryTile.Draw(spriteBatch, new Vector2(15, 15));
            _primaryTile.Draw(spriteBatch, new Vector2(5, 5));

            if (MapViewport.Bounds.Contains(_mouse.Position)) {
                _cursorMapPosition = new Vector2(_mouse.Position.X / 32, _mouse.Position.Y / 32);

                _primaryTile.Draw(spriteBatch, _cursorMapPosition * 16, new Color(255, 255, 255, 150));
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
