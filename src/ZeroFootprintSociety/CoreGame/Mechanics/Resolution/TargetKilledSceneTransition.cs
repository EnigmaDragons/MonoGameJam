using System.Linq;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.Characters.Prefabs;
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
                if (GameWorld.Enemies.All(x => !x.State.MustKill || x.State.IsDeceased))
                    Scene.NavigateTo(GameWorld.Enemies.Any(x => x.GetType() == typeof(CorpSec3)) ? "Credits" : "FinalFloor");
            }, this);
        }
    }
}
