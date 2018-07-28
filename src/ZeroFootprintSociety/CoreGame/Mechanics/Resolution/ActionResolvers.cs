using System.Collections.Generic;

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
