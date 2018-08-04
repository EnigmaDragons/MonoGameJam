using System.Linq;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class PerceptionCalculator
    {
        public PerceptionCalculator()
        {
            Event.Subscribe<Moved>(e => UpdatePerception(e.Character), this);
        }

        public void UpdatePerception(Character character)
        {
            EventQueue.Instance.Add(new TilesPercieved
            {
                Character = character,
                Tiles = new PointRadiusCalculation(character.CurrentTile.Position, character.Stats.Perception).Calculate()
                    .Where(x => GameWorld.Map.Exists(x)).ToList() 
            });
        }
    }
}
