namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private readonly CharacterStats _stats;

        public int RemainingHealth { get; set; }
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
