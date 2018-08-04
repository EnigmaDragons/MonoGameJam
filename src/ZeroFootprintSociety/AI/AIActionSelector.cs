using MonoDragons.Core.EventSystem;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

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
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Shoot].Invoke()));
                else if (Data.SeenEnemies.Count == 0 && o.ContainsKey(ActionType.Pass))
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Pass].Invoke()));
                else if (o.ContainsKey(ActionType.Hide) && new SpotHasGoodCoverCalculation(Data, Char.CurrentTile.Position).Calculate())
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Hide].Invoke()));
                else if (o.ContainsKey(ActionType.Overwatch))
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Overwatch].Invoke()));
                else if (o.ContainsKey(ActionType.Hide))
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Hide].Invoke()));
                else if (o.ContainsKey(ActionType.Pass))
                    EventQueue.Instance.Add(new AIActionQueued(() => o[ActionType.Pass].Invoke()));
                else
                    throw new Exception("No AI possible actions.");
            });
        }
    }
}
