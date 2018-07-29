using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class HideAction
    {
        public HideAction()
        {
            Event.Subscribe(EventSubscription.Create<HideSelected>(OnHideChosen, this));
        }

        private void OnHideChosen(HideSelected e)
        {
            Event.Publish(new ActionConfirmed());
            // TODO: Add Hide game state update
            Event.Publish(new ActionResolved());
        }
    }
}
