using Microsoft.Xna.Framework.Input;

namespace MonoDragons.Core.Inputs.KeyboardEvents
{
    public class KeyDownEvent
    {
        public Keys Key { get; }

        public KeyDownEvent(Keys key)
        {
            Key = key;
        }
    }
}
