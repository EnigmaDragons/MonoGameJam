using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    public class AICharacterDataInitializer : AIActorBase
    {
        private readonly Dictionary<Character, AICharacterData> _characterData;
        private bool _initialized = false;

        public AICharacterDataInitializer(Dictionary<Character, AICharacterData> characterData)
        {
            _characterData = characterData;
            Event.Subscribe<TurnBegun>(_ => Init(), this);
        }

        private void Init()
        {
            if (_initialized)
                return;
            GameWorld.LivingCharacters.Where(x => !x.IsFriendly).ForEach(x => _characterData[x] = new AICharacterData());
            _initialized = true;
        }
    }
}
