using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private readonly CharacterStats _stats;
        private readonly Character _itself;
        private int _remainingHealth = 0;
        public bool IsHiding = false;

        public int RemainingHealth
        {
            get => _remainingHealth;
            set
            {
                _remainingHealth = MathHelper.Clamp(value, 0, _stats.HP);
                if (_remainingHealth == 0)
                    Event.Publish(new CharacterDeceases() {Character = _itself});
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
