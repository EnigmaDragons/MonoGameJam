using System;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class HitChanceCalculation
    {
        private readonly int Accuaracy;
        private readonly int EnemyBlockChance;
        private readonly int EnemyAgility;
        private readonly bool IsEnemyHiding;

        public HitChanceCalculation(int accuracy, int enemeyBlockChance) : this(accuracy, enemeyBlockChance, 0, false) {}

        public HitChanceCalculation(int accuracy, int enemyBlockChance, int enemyAgility, bool isEnemyHiding)
        {
            Accuaracy = accuracy;
            EnemyBlockChance = enemyBlockChance;
            EnemyAgility = enemyAgility;
            IsEnemyHiding = isEnemyHiding;
        }

        public int Get()
        {
            return Math.Max(Math.Min(Accuaracy - EnemyAgility, 100), 0) * (100 - Math.Min(EnemyBlockChance * (IsEnemyHiding ? 2 : 1), 100)) / 100;
        }
    }
}
