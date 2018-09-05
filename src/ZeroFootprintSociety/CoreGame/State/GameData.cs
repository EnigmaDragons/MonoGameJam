using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame
{
    public sealed class GameData
    {
        public int Currency { get; set; }
        public List<string> ItemNames { get; set; }
        public List<CharacterData> Characters { get; set; }
    }
}
