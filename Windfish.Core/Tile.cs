using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish.Core
{
    public class Tile : ICloneable
    {
        public Tile()
        {

        }

        public Tile(Vector3 localPosition, Vector3 worldPosition, Vector2 texturePosition,  int tileWidth, int tileHeight, string textureName)
        {
            LocalPosition = localPosition;
            WorldPosition = worldPosition;
            TexturePosition = texturePosition;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TextureName = textureName;
        }

        public Tile(Vector3 localPosition, Vector3 worldPosition, Vector2 texturePosition, int tileWidth, int tileHeight, string textureName, Texture2D texture)
        {
            LocalPosition = localPosition;
            WorldPosition = worldPosition;
            TexturePosition = texturePosition;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TextureName = textureName;
            _texture = texture;
        }

        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public Vector3 LocalPosition { get; set; }
        public Vector3 WorldPosition { get; set; }

        public Vector2 TexturePosition { get; set; }

        private Texture2D _texture { get; set; }
        public string TextureName { get; set; }

        public void LoadContent(ContentManager contentManager)
        {
            if (!string.IsNullOrEmpty(TextureName)) {
                _texture = contentManager.Load<Texture2D>(TextureName);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Use overload chain
            if (_texture == null) return;

            spriteBatch.Draw(_texture,
                new Rectangle(LocalPosition.X.ToInt32() * TileWidth, LocalPosition.Y.ToInt32() * TileHeight, TileWidth, TileHeight),
                new Rectangle(TexturePosition.X.ToInt32(), TexturePosition.Y.ToInt32(), TileWidth, TileHeight), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Draw(spriteBatch, position, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            if (_texture == null) return;

            spriteBatch.Draw(_texture,
                new Rectangle(position.X.ToInt32(), position.Y.ToInt32(), TileWidth, TileHeight),
                new Rectangle(TexturePosition.X.ToInt32(), TexturePosition.Y.ToInt32(), TileWidth, TileHeight), color);
        }

        public object Clone()
        {
            return new Tile(LocalPosition, WorldPosition, TexturePosition, TileWidth, TileHeight, TextureName, _texture);
        }
    }
}
