using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculator
    {
        public VisibilityCalculator()
        {
            Event.Subscribe(EventSubscription.Create<Moved>(OnMoved, this));
        }

        private void OnMoved(Moved obj)
        {
            Event.Publish(new TilesSeen { Character = obj.Character, SeeableTiles = new VisibilityCalculation(obj.Character).Calculate() });
        }
    }
}
