using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class PassAction
    {
        public PassAction()
        {
            Event.Subscribe<PassSelected>(OnPassSelected, this);
        }

        private void OnPassSelected(PassSelected e)
        {
            Event.Publish(new ActionSelected(() =>
            {
                Event.Publish(new ActionResolved());
            }));
        }
    }
}
