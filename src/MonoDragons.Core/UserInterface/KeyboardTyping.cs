using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class KeyboardTyping : IAutomaton
    {
        private static TimeSpan backspaceRepeatInterval = TimeSpan.FromMilliseconds(75);

        public string Result { get; private set; } = "";

        Keys[] keys;
        bool[] IskeyUp;
        private TimeSpan _backspaceHeldDurationSinceInvocation;

        public KeyboardTyping(string startingValue = "")
        {
            Result = startingValue;
            InitValidKeys();
        }

        private void InitValidKeys()
        {
            keys = new Keys[38];
            var tempkeys = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToArray();
            var j = 0;
            for (var i = 0; i < tempkeys.Length; i++)
            {
                if (i == 1 || i == 11 || (i > 26 && i < 63))
                {
                    keys[j] = tempkeys[i];
                    j++;
                }
            }
            IskeyUp = new bool[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                IskeyUp[i] = true;
        }

        public void Update(TimeSpan delta)
        {
            var state = Keyboard.GetState();
            var i = 0;
            foreach (Keys key in keys)
            {
                if (state.IsKeyDown(key))
                {
                    if (IskeyUp[i])
                    {
                        if (key == Keys.Back && Result != "") Result = Result.Remove(Result.Length - 1);
                        if (key == Keys.Space) Result += " ";
                        if (i > 1 && i < 12)
                        {
                            if (state.IsKeyDown(Keys.RightShift) || state.IsKeyDown(Keys.LeftShift))
                                Result += key.ToString()[1];
                        }
                        if (i > 11 && i < 38)
                        {
                            if ((state.IsKeyDown(Keys.RightShift) || state.IsKeyDown(Keys.LeftShift))
                                == System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
                                Result += key.ToString().ToLower();
                            else Result += key.ToString();
                        }
                    }
                    IskeyUp[i] = false;
                }
                else if (state.IsKeyUp(key)) IskeyUp[i] = true;
                i++;
            }
            UpdateBackspace(state, delta);
        }

        private void UpdateBackspace(KeyboardState state, TimeSpan delta)
        {
            if (state.IsKeyDown(Keys.Back))
            {
                _backspaceHeldDurationSinceInvocation += delta;
                if (_backspaceHeldDurationSinceInvocation > backspaceRepeatInterval)
                {
                    Result = Result.Substring(0, Math.Max(0, Result.Length - 1));
                    _backspaceHeldDurationSinceInvocation -= backspaceRepeatInterval;
                }
            }
            if (!state.IsKeyDown(Keys.Back))
                _backspaceHeldDurationSinceInvocation = TimeSpan.Zero;
        }
    }
}
