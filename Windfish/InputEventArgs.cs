using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish
{
    public class InputEventArgs : EventArgs
    {
        public InputEventArgs(InputType inputType)
        {
            InputType = inputType;
        }

        public InputType InputType { get; private set; }
    }
}
