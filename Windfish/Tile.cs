using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish
{
    public class Tile
    {
        public Tile()
        {

        }

        public Tile(Vector3 position, Vector2 texturePosition, string textureName)
        {
            Position = position;
            TexturePosition = texturePosition;
            TextureName = textureName;
        }
        public const int TileWidth = 16;
        public const int TileHeight = 16;

        public Vector3 Position { get; set; }
        public Vector2 TexturePosition { get; set; }
        public Texture2D Texture { get; set; }
        public string TextureName { get; set; }

        public void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(TextureName);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X * TileWidth, (int)Position.Y * TileHeight, TileWidth, TileHeight), new Rectangle((int)TexturePosition.X + TileWidth, (int)TexturePosition.Y + TileHeight, TileWidth, TileHeight), Color.White);
        }
    }
}
