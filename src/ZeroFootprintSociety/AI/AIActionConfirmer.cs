using System;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    class AIActionConfirmer : AIActorBase
    {
        public AIActionConfirmer()
        {
            Event.Subscribe<ActionReadied>(e => IfAITurn(() => Event.Publish(new AIActionQueued(
                () => Event.Publish(new ActionConfirmed()),
                GameWorld.FriendlyPerception[GameWorld.CurrentCharacter.CurrentTile.Position]
                    ? TimeSpan.FromSeconds(2.5f)
                    : TimeSpan.FromMilliseconds(0)))), this);
        }
    }
}
