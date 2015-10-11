using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish.Core
{
    public class InputManager
    {
        public InputManager()
        {
            ThrottleInput = false;
            LockMovement = false;
        }
        private KeyboardState _keyState;
        private KeyboardState _lastKeyState;
        private Keys _lastKey;

        private double _cooldown = 0;
        private double _counter = 0;

        public static bool ThrottleInput { get; set; }
        public static bool LockMovement { get; set; }
        public static event EventHandler<InputEventArgs> BeginInput = delegate { };

        public void Update(GameTime gameTime)
        {
            if (_cooldown > 0) {
                _counter += gameTime.ElapsedGameTime.Milliseconds;
                if (_counter > gameTime.ElapsedGameTime.Milliseconds) {
                    _cooldown = 0;
                    _counter = 0;
                } else {
                    return;
                }
            }

            HandleComputerInput(gameTime);
        }

        public void HandleComputerInput(GameTime gameTime)
        {
            _keyState = Keyboard.GetState();

            if (_keyState.IsKeyUp(_lastKey) && _lastKey != Keys.None) {
                BeginInput(this, new InputEventArgs(InputType.None));
            }

            CheckKeyState(Keys.Left, InputType.Left);
            CheckKeyState(Keys.Right, InputType.Right);
            CheckKeyState(Keys.Up, InputType.Up);
            CheckKeyState(Keys.Down, InputType.Down);

            _lastKeyState = _keyState;
        }

        public void CheckKeyState(Keys key, InputType inputType)
        {
            if (_keyState.IsKeyDown(key)) {
                if (!ThrottleInput || (ThrottleInput && _lastKeyState.IsKeyUp(key))){
                    BeginInput(this, new InputEventArgs(inputType));
                    _lastKey = key;
                }
            }
        }
    }
}
