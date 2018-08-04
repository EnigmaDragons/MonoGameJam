using System;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    public abstract class AIActorBase
    {
        protected Character Char => GameWorld.CurrentCharacter;

        protected void IfAITurn(Action queueAction)
        {
            if (Char.Team.IsIncludedIn(TeamGroup.NeutralsAndEnemies))
                EventQueue.Instance.Add(new AIActionQueued(queueAction));            
        }
    }
}
