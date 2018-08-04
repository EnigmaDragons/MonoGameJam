using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculator
    {
        public VisibilityCalculator()
        {
            Event.Subscribe(EventSubscription.Create<Moved>(e => UpdateSight(e.Character), this));
        }

        public void UpdateSight(Character character)
        {
            EventQueue.Instance.Add(new TilesSeen { Character = character, SeeableTiles = new VisibilityCalculation(character).Calculate() });
        }
    }
}
