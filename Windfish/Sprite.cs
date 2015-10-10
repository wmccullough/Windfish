using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Windfish
{
    public class Sprite : Component
    {
        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _position = position;
        }

        private Texture2D _texture;
        private int _width;
        private int _height;
        private Vector2 _position;

        public override void Update(GameTime gameTime)
        {

        }

        public void Move(float x, float y)
        {
            _position = new Vector2(_position.X + x, _position.Y + y);

            Animation animation = GetComponentFromParent<Animation>();
            if (animation == null) {
                return;
            }

            if (x > 0) {
                animation.ResetCounter(StateType.Walking, DirectionType.Right);
            } else if (x < 0) {
                animation.ResetCounter(StateType.Walking, DirectionType.Left);
            } else if (y > 0) {
                animation.ResetCounter(StateType.Walking, DirectionType.Down);
            } else if (y < 0) {
                animation.ResetCounter(StateType.Walking, DirectionType.Up);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Animation animation = GetComponentFromParent<Animation>();

            if (animation != null) {
                spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), animation.CurrentFrame, Color.White);
            } else {
                spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
            }
            
        }


    }
}
