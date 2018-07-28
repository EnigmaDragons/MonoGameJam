using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.Mechanics.CombatResolution;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public static class ActionResolvers
    {
        public static List<object> CreateAll()
        {
            return new List<object>
            {
                new HideAction(),
                new RangedAction()
            };
        }
    }
}
