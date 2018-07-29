﻿using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    class AIActionConfirmer : AIActorBase
    {
        public AIActionConfirmer()
        {
            Event.Subscribe<ActionSelected>(e => IfAITurn(() => Event.Publish(new ActionConfirmed())), this);
        }
    }
}