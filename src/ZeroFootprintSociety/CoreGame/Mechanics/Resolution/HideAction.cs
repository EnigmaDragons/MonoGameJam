using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class HideAction
    {
        public HideAction()
        {
            Event.Subscribe(EventSubscription.Create<HideChosen>(OnHideChosen, this));
        }

        private void OnHideChosen(HideChosen e)
        {
            Event.Publish(new ActionConfirmed());
            // TODO: Add Hide game state update
            Event.Publish(new ActionResolved());
        }
    }
}
