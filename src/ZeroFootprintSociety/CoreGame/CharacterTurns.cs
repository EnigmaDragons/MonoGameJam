using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame
{
    public class CharacterTurns : IInitializable
    {
        private int _activeCharacterIndex;
        private readonly List<Character> _characters;

        public Character CurrentCharacter { get; private set; }

        public CharacterTurns(IReadOnlyList<Character> characters)
        {
            _characters = characters.OrderByDescending(x => x.Stats.Agility).ToList();
            CurrentCharacter = _characters.First();
            Event.Subscribe(EventSubscription.Create<TurnEnded>(BeginNextTurn, this));
            Event.Subscribe(EventSubscription.Create<CharacterDeceased>(OnCharacterDeath, this));
        }

        public void Init()
        {
            if (_characters.Any(x => x.IsFriendly))
                while (!CurrentCharacter.IsFriendly)
                    Advance();
            Event.Publish(new TurnBegun());
        }

        private void BeginNextTurn(TurnEnded e)
        {
            if (GameWorld.IsGameOver)
                return;

            Advance();
            while (CurrentCharacter.State.IsDeceased)
                Advance();
            Event.Publish(new TurnBegun());
        }

        private void Advance()
        {
            _activeCharacterIndex++;
            if (_activeCharacterIndex == _characters.Count)
                _activeCharacterIndex = 0;
            CurrentCharacter = _characters[_activeCharacterIndex];
        }

        private void OnCharacterDeath(CharacterDeceased _event)
        {
            if (GameWorld.Friendlies.All(x => x.State.IsDeceased) || GameWorld.MainCharacter.State.IsDeceased)
            {
                Event.Publish(new GameOver());
                GameWorld.IsGameOver = true;
            }
            else if (CurrentCharacter == _event.Character)
            {
                Event.Publish(new ActionResolved());
            }
        }
    }
}
