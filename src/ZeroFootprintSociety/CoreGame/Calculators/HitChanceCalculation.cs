namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class HitChanceCalculation
    {
        private readonly int Accuaracy;
        private readonly int EnemyBlockChance;
        private readonly int EnemyAgility;

        public HitChanceCalculation(int accuracy, int enemeyBlockChance) : this(accuracy, enemeyBlockChance, 0) {}

        public HitChanceCalculation(int accuracy, int enemyBlockChance, int enemyAgility)
        {
            Accuaracy = accuracy;
            EnemyBlockChance = enemyBlockChance;
            EnemyAgility = enemyAgility;
        }

        public int Get()
        {
            return (Accuaracy - EnemyAgility) * (100 - EnemyBlockChance) / 100;
        }
    }
}
