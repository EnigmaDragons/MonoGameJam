using MonoDragons.Core.EventSystem;
using System;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.AI
{
    class AIActionSelector : AIActorBase
    {
        public AIActionSelector()
        {
            Event.Subscribe<ActionOptionsAvailable>(SelectAction, this);
        }

        private void SelectAction(ActionOptionsAvailable e)
        {
            IfAITurn(() =>
            {
                var o = e.Options;
                if (o.ContainsKey(ActionType.Overwatch))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Overwatch].Invoke()));
                else if(o.ContainsKey(ActionType.Pass))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Pass].Invoke()));
                else if (o.ContainsKey(ActionType.Hide))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Hide].Invoke()));
                else if (o.ContainsKey(ActionType.Shoot))
                    Event.Publish(new AIActionQueued(() => o[ActionType.Shoot].Invoke()));
                else
                    throw new Exception("No AI possible actions.");
            });
        }
    }
}
