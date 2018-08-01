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
                if (o.ContainsKey(ActionType.Pass))
                    o[ActionType.Pass].Invoke();
                else if (o.ContainsKey(ActionType.Hide))
                    o[ActionType.Hide].Invoke();
                else if (o.ContainsKey(ActionType.Shoot))
                    o[ActionType.Shoot].Invoke();
                else if (o.ContainsKey(ActionType.Overwatch))
                    o[ActionType.Overwatch].Invoke();
                else
                    throw new Exception("No AI possible actions.");
            });
        }
    }
}
