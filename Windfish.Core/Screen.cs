using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windfish.Core
{
    public class Screen
    {
        public Screen() { }

        public Screen(Vector2 worldPosition)
        {
            WorldPosition = worldPosition;
            Tiles = new Tile[ScreenWidth, ScreenHeight];
            GameObjects = new List<GameObject>();

            
        }

        public Vector2 WorldPosition { get; set; }
        public const int ScreenWidth = 10;
        public const int ScreenHeight = 8;

        public Tile[,] Tiles { get; set; }
        public List<GameObject> GameObjects { get; set; }

        public void New()
        {
            foreach (int y in Enumerable.Range(0, ScreenHeight)) {
                foreach (int x in Enumerable.Range(0, ScreenWidth)) {
                    Tiles[x, y] = new Tile(
                        new Vector3(x, y, 0), 
                        new Vector3(x + (WorldPosition.X + ScreenWidth), y + (WorldPosition.Y + ScreenHeight), 0),
                        new Vector2(0, 0), 16, 16, "Textures/overworld");
                }
            }
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && x < ScreenWidth && y >= 0 && y < ScreenHeight) {
                Tiles[x, y] = tile;
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (Tile tile in Tiles) {
                if (tile != null) {
                    tile.LoadContent(contentManager);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (int y in Enumerable.Range(0, ScreenHeight)) {
                foreach (int x in Enumerable.Range(0, ScreenWidth)) {
                    if (Tiles[x, y] != null) {
                        Tiles[x, y].Update(gameTime);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (int y in Enumerable.Range(0, ScreenHeight)) {
                foreach (int x in Enumerable.Range(0, ScreenWidth)) {
                    if (Tiles[x, y] != null) {
                        Tiles[x, y].Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
