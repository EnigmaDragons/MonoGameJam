using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class NewEnemySpotter
    {
        private readonly DictionaryWithDefault<Character, bool> _hasSeen = new DictionaryWithDefault<Character, bool>(false);

        public NewEnemySpotter()
        {
            Event.Subscribe<MoveResolved>(OnMoved, this);
        }

        public void OnMoved(MoveResolved e)
        {
            if (e.Character.Team == Team.Enemy && !_hasSeen[e.Character] 
                && GameWorld.Friendlies.Any(x => GameWorld.FriendlyPerception[e.Character.CurrentTile.Position]))
            {
                _hasSeen[e.Character] = true;
                Event.Publish(new EnemySpotted { Enemy = e.Character });
            }
            else if (e.Character.Team == Team.Friendly)
            {
                GameWorld.LivingCharacters.Where(x => x.Team == Team.Enemy && !_hasSeen[x] && e.Character.State.CanPercieve(x.CurrentTile.Position))
                    .ForEach(x =>
                    {
                        _hasSeen[x] = true;
                        Event.Publish(new EnemySpotted { Enemy = x });
                    });
            }
        }
    }
}
