using MonoDragons.Core.EventSystem;
using System;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.AI
{
    public abstract class AIActorBase
    {
        protected Character Char => GameWorld.CurrentCharacter;

        protected void IfAITurn(Action queueAction)
        {
            if (Char.Team.IsIncludedIn(TeamGroup.NeutralsAndEnemies))
                Event.Publish(new AIActionQueued(queueAction));            
        }
    }
}
