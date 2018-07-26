using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.EventSystem;

namespace MonoDragons.Core.Inputs.KeyboardEvents
{
    public class KeyUpEventSubscription
    {
        private readonly Keys _key;
        private readonly Action<KeyUpEvent> _onEvent;
        private readonly object _owner;

        public KeyUpEventSubscription(Action<KeyUpEvent> onEvent, object owner, Keys key)
        {
            _onEvent = onEvent;
            _key = key;
            _owner = owner;
        }

        public void Subscribe()
        {
            Event.Subscribe(EventSubscription.Create<KeyUpEvent>(TriggerActionOnProperKey, _owner));
        }

        private void TriggerActionOnProperKey(KeyUpEvent e)
        {
            if (e.Key.Equals(_key))
                _onEvent(e);
        }
    }
}
