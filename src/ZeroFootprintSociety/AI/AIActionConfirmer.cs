using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    class AIActionConfirmer : AIActorBase
    {
        public AIActionConfirmer()
        {
            Event.Subscribe<ActionReadied>(e => IfAITurn(() => Event.Publish(new AIActionQueued(() => Event.Publish(new ActionConfirmed())))), this);
        }
    }
}
