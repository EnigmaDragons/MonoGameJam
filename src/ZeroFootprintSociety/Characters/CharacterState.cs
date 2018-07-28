namespace ZeroFootPrintSociety.Characters
{
    public class CharacterState
    {
        private CharacterStats _stats;

        public int Damage { get; set; } = 0; // Hit points lost. If < 0 ---> death
        public int RealHP => (_stats.HP - Damage);
        public float PercentLeft => ((float)RealHP / _stats.HP);

        public CharacterState(CharacterStats stats)
        {
            _stats = stats;
        }

        // TODO: Add more stats that will change over time.

        public void Init()
        {
            Damage = 0;
        }

    }
}
