using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private readonly CharacterStats _stats;
        private readonly Character _itself;
        private int _remainingHealth = 0;
        public bool IsHiding = false;
        public bool IsOverwatching = false;
        public bool IsDeceased = false;
        public Dictionary<Point, ShotCoverInfo> OverwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
        public DictionaryWithDefault<Point, bool> SeeableTiles = new DictionaryWithDefault<Point, bool>(false);
        public ConcurrentDictionaryWithDefault<Point, bool> PercievedTiles = new ConcurrentDictionaryWithDefault<Point, bool>(false);
        public bool CanPercieve(Point point) => SeeableTiles[point] || PercievedTiles[point];
        public List<Footprint> Footprints { get; set; }

        public int RemainingHealth
        {
            get => _remainingHealth;
            set
            { 
                _remainingHealth = MathHelper.Clamp(value, 0, _stats.HP);
            }
        }

        public float PercentLeft => (float)RemainingHealth / _stats.HP;

        public CharacterState(CharacterStats stats, Character itself)
        {
            _stats = stats;
            _itself = itself;
        }

        public void Init()
        {
            RemainingHealth = _stats.HP;
        }
    }
}
