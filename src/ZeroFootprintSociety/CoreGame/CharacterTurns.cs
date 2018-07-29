using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame
{
    public class CharacterTurns : IInitializable
    {
        private int _activeCharacterIndex;
        private List<Character> _characters;

        public Character CurrentCharacter => Characters[_activeCharacterIndex];
        public IReadOnlyList<Character> Characters => _characters;

        public CharacterTurns(IReadOnlyList<Character> characters)
        {
            _characters = characters.OrderByDescending(x => x.Stats.Agility).ToList();
            Event.Subscribe(EventSubscription.Create<TurnEnded>(BeginNextTurn, this));
            Event.Subscribe(EventSubscription.Create<CharacterDeceased>(OnCharacterDeath, this));
        }

        public void Init()
        {
            Event.Publish(new TurnBegun());
        }

        private void BeginNextTurn(TurnEnded e)
        {
            _activeCharacterIndex++;
            if (_activeCharacterIndex == Characters.Count)
                _activeCharacterIndex = 0;
            Event.Publish(new TurnBegun());
        }

        private void OnCharacterDeath(CharacterDeceased _event)
        {
            if (CurrentCharacter == _event.Character)
                Event.Publish(new ActionResolved());
            var charIndex = _characters.IndexOf(_event.Character);
            _activeCharacterIndex = charIndex >= _activeCharacterIndex 
                ? _activeCharacterIndex 
                : Math.Min(_activeCharacterIndex - 1, 0);
            _characters.Remove(_event.Character);
        }
    }
}
