using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameState
    {
        public static GameMap Map { get; set; } 
        public static List<Character> Characters { get; set; }
        public static Character CurrentCharacter { get; set; }
    }
}
