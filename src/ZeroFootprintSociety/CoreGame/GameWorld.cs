using System.Collections.Generic;
using MonoDragons.Core.Engine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameWorld
    {
        public static GameMap Map { get; set; } 
        public static List<Character> Characters { get; set; }
        public static CharacterTurns Turns { get; set; }
        public static List<IVisual> Highlights { get; } = new List<IVisual>();
    }
}
