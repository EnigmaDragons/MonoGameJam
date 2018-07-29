using System.Collections.Generic;
using System.Linq;

namespace ZeroFootPrintSociety.Characters
{
    public static class CharactersListExtensions
    {
        public static IEnumerable<IGrouping<Team, Character>> GroupByTeam(this List<Character> listCharacters)
            => listCharacters.GroupBy(x => x.Team, x => x);
    }
}
