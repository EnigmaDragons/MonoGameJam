using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    sealed class AIMoveRandomly : AIActorBase
    {
        public AIMoveRandomly()
        {
            Event.Subscribe<MovementOptionsAvailable>(ChooseMoveIfApplicable, this);
        }

        private void ChooseMoveIfApplicable(MovementOptionsAvailable e)
        {
            IfAITurn(
                () => Event.Publish(new MovementConfirmed(e.AvailableMoves.Random())));
        }
    }
}
