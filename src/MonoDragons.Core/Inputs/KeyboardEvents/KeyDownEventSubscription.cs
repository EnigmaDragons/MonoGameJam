using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Inputs.KeyboardEvents
{
    public class KeyDownEventSubscription
    {
        private readonly Keys _key;
        private readonly Action<KeyDownEvent> _onEvent;
        private readonly object _owner;

        public KeyDownEventSubscription(Action<KeyDownEvent> onEvent, object owner, Keys key)
        {
            _onEvent = onEvent;
            _key = key;
            _owner = owner;
        }

        public void Subscribe()
        {
            Event.Subscribe(EventSubscription.Create<KeyDownEvent>(TriggerActionOnProperKey, _owner));
        }

        private void TriggerActionOnProperKey(KeyDownEvent eventt)
        {
            if (eventt.Key.Equals(_key))
                _onEvent(eventt);
        }
    }
}
