using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.AI
{
    class AIActionSelector : AIActorBase
    {
        private readonly Dictionary<Character, AICharacterData> _characterData;
        private AICharacterData Data => _characterData[Char];

        public AIActionSelector(Dictionary<Character, AICharacterData> characterData)
        {
            _characterData = characterData;
            Event.Subscribe<ActionOptionsAvailable>(SelectAction, this);
        }

        private void SelectAction(ActionOptionsAvailable e)
        {
            IfAITurn(() =>
            {
                var o = e.Options;
                if (o.ContainsKey(ActionType.Shoot))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Shoot].Invoke()));
                else if (Data.SeenEnemies.Count == 0 && o.ContainsKey(ActionType.Pass))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Pass].Invoke()));
                else if (o.ContainsKey(ActionType.Hide) && new SpotHasGoodCoverCalculation(Data, Char.CurrentTile.Position).Calculate())
                    Event.Publish(new AIActionQueued(() => o[ActionType.Hide].Invoke()));
                else if (o.ContainsKey(ActionType.Overwatch))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Overwatch].Invoke()));
                else if (o.ContainsKey(ActionType.Hide))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Hide].Invoke()));
                else if (o.ContainsKey(ActionType.Pass))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Pass].Invoke()));
                else
                    throw new Exception("No AI possible actions.");
            });
        }
    }
}
