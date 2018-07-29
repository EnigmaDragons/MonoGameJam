using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class OverwatchAction
    {
        public OverwatchAction()
        {
            Event.Subscribe(EventSubscription.Create<OverwatchSelected>(OnOverwatchSelected, this));
        }

        private void OnOverwatchSelected(OverwatchSelected e)
        {
            Event.Publish(new ActionConfirmed());
            // TODO: Add Hide game state update
            Event.Publish(new ActionResolved());
        }
    }
}
