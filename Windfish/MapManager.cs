using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish
{
    public class MapManager
    {
        public MapManager(string name)
        {
            _tiles = new List<Tile>();
            Name = name;
        }
        public string Name { get; set; }
        private List<Tile> _tiles;

        public void LoadContent(ContentManager contentManager)
        {
            var tiles = new List<Tile>();

            XmlSerializationUtil.LoadXML(out tiles, string.Format("Content\\{0}_map.xml", Name));

            if (tiles != null) {
                _tiles = tiles;
                _tiles = _tiles.OrderBy(o => o.Position.Z).ToList();

                foreach (Tile tile in _tiles) {
                    tile.LoadContent(contentManager);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in _tiles) {
                tile.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles) {
                tile.Draw(spriteBatch);
            }
        }
    }
}
