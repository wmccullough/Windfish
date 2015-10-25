using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Windfish.Core
{
    public class World
    {
        public World()
        {
            Screens = new Screen[16, 16];
            CurrentScreenPosition = Vector2.Zero;
        }

        public Vector2 CurrentScreenPosition { get; private set; }

        public Screen[,] Screens { get; set; }

        public Screen CurrentScreen
        {
            get
            {
                return Screens[CurrentScreenPosition.X.ToInt32(), CurrentScreenPosition.Y.ToInt32()];
            }
        }

        public void New()
        {
            foreach (int y in Enumerable.Range(0, 16)) {
                foreach (int x in Enumerable.Range(0, 16)) {
                    Screens[x, y] = new Screen(new Vector2(x, y));
                    Screens[x, y].New();
                }
            }
        }

        public void MoveToScreen(Vector2 value)
        {
            CurrentScreenPosition = new Vector2(MathHelper.Clamp(value.X, 0, 15), MathHelper.Clamp(value.Y, 0, 15));
        }

        public void NewScreen(Vector2 worldPosition)
        {
            if (worldPosition.X < 0 || worldPosition.X > 16 || worldPosition.Y < 0 || worldPosition.Y > 16) {
                throw new InvalidOperationException("worldPosition coordinates must fall between 0 and 16");
            }

            Screens[worldPosition.X.ToInt32(), worldPosition.Y.ToInt32()] = new Screen(worldPosition);
        }

        public void Save(string filename)
        {
            World world = this;
            string worldJson = JsonConvert.SerializeObject(world);
            File.WriteAllText(filename, worldJson);

        }

        public static World Load(string filename)
        {
            string worldJson = File.ReadAllText(filename);
            World world = JsonConvert.DeserializeObject<World>(worldJson);
            return world;
        }
        public void LoadContent(ContentManager contentManager)
        {
            foreach (Screen screen in Screens) {
                if (screen != null) {
                    screen.LoadContent(contentManager);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentScreen == null) return;

            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentScreen == null) return;

            CurrentScreen.Draw(spriteBatch);
        }

        public void DrawWorld(SpriteBatch spriteBatch)
        {
            foreach (int y in Enumerable.Range(0, 16)) {
                foreach (int x in Enumerable.Range(0, 16)) {
                    Screens[x, y].Draw(spriteBatch);
                }
            }
        }
    }
}
