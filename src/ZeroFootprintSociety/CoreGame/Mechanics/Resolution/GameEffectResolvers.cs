using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class GameEffectResolvers
    {
        public static List<object> CreateAll()
        {
            return new List<object>
            {
                new GainXpOnEnemyDeath()
            };
        }
    }
}