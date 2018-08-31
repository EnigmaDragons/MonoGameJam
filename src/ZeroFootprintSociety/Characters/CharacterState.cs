using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private readonly CharacterStats _stats;
        private int _remainingHealth = 0;
        public int Xp { get; set; } = 0;
        public bool IsHiding { get; set; } = false;
        public bool IsOverwatching { get; set; } = false;
        public bool IsDeceased { get; set; } = false;
        public Dictionary<Point, ShotCoverInfo> OverwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
        public DictionaryWithDefault<Point, bool> SeeableTiles = new DictionaryWithDefault<Point, bool>(false);
        public ConcurrentDictionaryWithDefault<Point, bool> PerceivedTiles { get; set; } = new ConcurrentDictionaryWithDefault<Point, bool>(false);
        public bool CanPercieve(Point point) => SeeableTiles[point] || PerceivedTiles[point];
        //TODO: garbage properties
        public bool MustKill { get; set; }
        public string NextScene { get; set; }

        public int RemainingHealth
        {
            get => _remainingHealth;
            set => _remainingHealth = MathHelper.Clamp(value, 0, _stats.HP);
        }

        public float PercentLeft => (float)RemainingHealth / _stats.HP;

        public CharacterState(CharacterStats stats)
        {
            _stats = stats;
        }

        public void Init()
        {
            RemainingHealth = _stats.HP;
        }
    }
}
