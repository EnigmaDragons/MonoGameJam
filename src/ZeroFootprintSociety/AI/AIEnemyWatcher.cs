using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    public class AIEnemyWatcher : AIActorBase
    {
        private readonly Dictionary<Character, AICharacterData> _characterData;
        private AICharacterData Data => _characterData[Char];
        private Mood CurrentMood = Mood.Stealth;

        public AIEnemyWatcher(Dictionary<Character, AICharacterData> characterData)
        {
            _characterData = characterData;
            Event.Subscribe<MoveResolved>(OnMoved, this);
            Event.Subscribe<CharacterDeceased>(OnDeceased, this);
        }

        public void OnMoved(MoveResolved e)
        {
            if (e.Character.Team == Team.Enemy)
            {
                GameWorld.FriendliesWhere(x => e.Character.State.CanPercieve(x.CurrentTile.Position))
                    .ForEach(x => Data.SeenEnemies[x] = x.CurrentTile.Position);
            }
            else
            {
                _characterData.Where(x => x.Key.State.CanPercieve(e.Character.CurrentTile.Position))
                    .ForEach(x => x.Value.SeenEnemies[e.Character] = e.Character.CurrentTile.Position);
            }

            if (CurrentMood == Mood.Stealth && _characterData.Where(x => !x.Key.State.IsDeceased).Any(x => x.Value.SeenEnemies.Any()))
            {
                CurrentMood = Mood.Battle;
                Event.Publish(new MoodChange { NewMood = CurrentMood });
            }
        }

        public void OnDeceased(CharacterDeceased e)
        {
            if (CurrentMood == Mood.Battle && !_characterData.Where(x => !x.Key.State.IsDeceased).Any(x => x.Value.SeenEnemies.Any()))
            {
                CurrentMood = Mood.Stealth;
                Event.Publish(new MoodChange { NewMood = CurrentMood });
            }
        }
    }
}
