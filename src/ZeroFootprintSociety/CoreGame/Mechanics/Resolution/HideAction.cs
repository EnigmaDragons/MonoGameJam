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
            EventQueue.Instance.Add(new ActionReadied(() =>
            {
                GameWorld.CurrentCharacter.State.IsHiding = true;
                EventQueue.Instance.Add(new ActionResolved());
            }));
        }
    }
}
