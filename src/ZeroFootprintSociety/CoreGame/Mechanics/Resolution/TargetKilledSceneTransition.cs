using System.Linq;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class TargetKilledSceneTransition
    {
        public TargetKilledSceneTransition()
        {
            //TODO: This some trash temp code
            Event.Subscribe<CharacterDeceased>(_ =>
            {
                if (GameWorld.Characters.All(x => !x.State.MustKill || x.State.IsDeceased))
                    Scene.NavigateTo(GameWorld.Characters.First(x => x.State.MustKill).State.NextScene);
            }, this);
        }
    }
}
