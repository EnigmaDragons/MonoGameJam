using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.UiElements;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameWorld
    {
        public static GameMap Map { get; set; } 
        public static List<Character> Characters { get; set; }
        public static CharacterTurns Turns { get; set; }
        public static Character CurrentCharacter => Turns.CurrentCharacter;
        public static Highlights Highlights { get; set; }
        public static Point HoveredTile { get; set; } = new Point(0, 0);
        public static int CountCharactersIn(Team team) => Characters.Count(x => x.Team == team);

        internal static void Clear()
        {
            Map = null;
            Turns = null;
            Characters?.Clear();
            Highlights = null;
            HoveredTile = Point.Zero;
        }
    }
}
