using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private readonly CharacterStats _stats;
        private int _remainingHealth = 0;

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
