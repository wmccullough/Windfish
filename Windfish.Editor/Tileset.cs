using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windfish.Core;

namespace Windfish.Editor
{
    public class Tileset
    {
        public Tileset(string textureName)
        {
            TextureName = textureName;
        }

        public Tile[] Tiles { get; set; }
        public Texture2D Texture { get; set; }
        public string TextureName { get; set; }

        public void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(TextureName);
            int tileWidth = Texture.Width / 16;
            int tileHeight = Texture.Height / 16;
            Tiles = new Tile[tileWidth * tileHeight];
            int counter = 0;

            foreach (int y in Enumerable.Range(0, tileHeight)) {
                foreach (int x in Enumerable.Range(0, tileWidth)) {
                    Tiles[counter] = new Tile(Vector3.Zero, Vector3.Zero, new Vector2(x * 16, y * 16), 16, 16, TextureName);
                    counter++;
                }
            }

        }
    }
}
