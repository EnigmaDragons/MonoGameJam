﻿using MonoDragons.Core.Engine;
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

        public Character CurrentCharacter => Characters[_activeCharacterIndex];
        public List<Character> Characters => GameWorld.Characters.OrderByDescending(x => x.Stats.Agility).ToList();

        public CharacterTurns()
        {
            Event.Subscribe(EventSubscription.Create<TurnEnded>(BeginNextTurn, this));
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
    }
}
