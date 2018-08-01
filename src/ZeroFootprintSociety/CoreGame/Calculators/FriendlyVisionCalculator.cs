using System;
using System.Linq;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public sealed class FriendlyVisionCalculator : IAutomaton
    {
        private bool _shouldCalc;
        
        public FriendlyVisionCalculator()
        {
            _shouldCalc = true;
            Event.Subscribe<Moved>(e => _shouldCalc = e.Character.Team.IsIncludedIn(TeamGroup.Friendlies), this);
        }

        public void Update(TimeSpan delta)
        {
            if (!_shouldCalc) 
                return;
            
            var friendlyVision = GameWorld.Friendlies
                .SelectMany(x => x.State.SeeableTiles
                    .Where(t => t.Value)
                    .Select(t2 => t2.Key))
                .Distinct()
                .ToList();
                
            GameWorld.Map.Tiles.ForEach(tile =>
            {
                var canSee = friendlyVision.Contains(tile.Position);
                tile.CurrentlyFriendlyVisible = canSee;
                if (canSee)
                    tile.EverSeenByFriendly = true;
            });
        }
    }
}