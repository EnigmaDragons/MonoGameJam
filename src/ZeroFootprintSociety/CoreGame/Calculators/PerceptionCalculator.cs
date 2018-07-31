using System.Linq;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class PerceptionCalculator
    {
        public PerceptionCalculator()
        {
            Event.Subscribe<Moved>(OnMove, this);
        }

        private void OnMove(Moved e)
        {
            Event.Publish(new TilesPercieved
            {
                Character = e.Character,
                Tiles = new PointRadiusCalculation(e.Character.CurrentTile.Position, e.Character.Stats.Perception).Calculate()
                    .Where(x => GameWorld.Map.Exists(x)).ToList() 
            });
        }
    }
}
