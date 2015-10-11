using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Windfish.Core
{
    public class PlayerInput : Component
    {
        public PlayerInput()
        {
            InputManager.BeginInput += InputManager_BeginInput;
        }

        private void InputManager_BeginInput(object sender, InputEventArgs e)
        {
            Sprite sprite = GetComponentFromParent<Sprite>();
            if (sprite == null) {
                return;
            }

            if (e.InputType == InputType.Up) {
                sprite.Move(0, -1.5f);
            }
            if (e.InputType == InputType.Down) {
                sprite.Move(0, 1.5f);
            }
            if (e.InputType == InputType.Left) {
                sprite.Move(-1.5f, 0);
            }
            if (e.InputType == InputType.Right) {
                sprite.Move(1.5f, 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
