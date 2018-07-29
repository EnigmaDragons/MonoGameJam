using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    sealed class GoNowhereAIMovement : AIActorBase
    {
        public GoNowhereAIMovement()
        {
            Event.Subscribe<MovementOptionsAvailable>(ChooseMoveIfApplicable, this);
        }

        private void ChooseMoveIfApplicable(MovementOptionsAvailable e)
        {
            IfAITurn(
                () => Event.Publish(new MovementConfirmed { Path = new List<Point> { Char.CurrentTile.Position } }));
        }
    }
}
