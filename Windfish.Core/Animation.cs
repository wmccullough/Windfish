using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Windfish.Core
{
    public class Animation : Component
    {
        public Animation(int width, int height)
        {
            _width = width;
            _height = height;
            State = StateType.Standing;
        }

        private int _width;
        private int _height;
        private double _counter;
        private int _animationIndex;

        public Rectangle CurrentFrame { get; private set; }
        public DirectionType Direction { get; set; }
        public StateType State { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (State == StateType.Standing) {

            }
            if (State == StateType.Walking) {
                _counter += gameTime.ElapsedGameTime.Milliseconds;
                if (_counter > 250) {
                    ChangeState();
                    _counter = 0d;
                }
            }
        }

        public void ResetCounter(StateType state, DirectionType direction)
        {
            if (direction != Direction) {
                _counter = 1000;
                _animationIndex = 0;
            }

            State = state;
            Direction = direction;
        }

        private void ChangeState()
        {
            if (Direction == DirectionType.Up) {
                CurrentFrame = new Rectangle(_width * _animationIndex, 0, _width, _height);
            }
            if (Direction == DirectionType.Down) {
                CurrentFrame = new Rectangle(32 + (_width * _animationIndex), 0, _width, _height);
            }
            if (Direction == DirectionType.Left) {
                CurrentFrame = new Rectangle(64 + (_width * _animationIndex), 0, _width, _height);
            }
            if (Direction == DirectionType.Right) {
                CurrentFrame = new Rectangle(96 + (_width * _animationIndex), 0, _width, _height);
            }

            _animationIndex = _animationIndex == 0 ? 1 : 0;
            State = StateType.Standing;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //throw new NotImplementedException();
        }


    }
}
