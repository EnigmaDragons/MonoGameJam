using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public static class ActionResolvers
    {
        public static List<object> CreateAll()
        {
            return new List<object>
            {
                new ActionProposal(),
                new HideAction(),
                new RangedAction(),
                new PassAction(),
                new OverwatchAction(),
                new OverwatchResolver()
            };
        }
    }
}
