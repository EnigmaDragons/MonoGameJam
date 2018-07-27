using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame
{
    class CharacterTurns : IInitializable
    {
        private int _activeCharacterIndex;

        public List<Character> Characters { get; }
        public Character CurrentCharacter => Characters[_activeCharacterIndex];

        public CharacterTurns(List<Character> characters)
        {
            Characters = characters.OrderByDescending(x => x.Stats.Agility).ToList();
            Event.Subscribe(EventSubscription.Create<TurnEnded>(BeginNextTurn, this));
        }

        public void Init()
        {
            Characters.ForEach(x => x.Init());
            Event.Publish(new TurnBegun { Character = CurrentCharacter });
        }

        private void BeginNextTurn(TurnEnded e)
        {
            _activeCharacterIndex++;
            if (_activeCharacterIndex == Characters.Count)
                _activeCharacterIndex = 0;
            Event.Publish(new TurnBegun { Character = CurrentCharacter });
        }
    }
}
