using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameWorld
    {
        public static GameMap Map { get; set; } 
        public static IReadOnlyList<Character> Characters { get; set; }
        public static CharacterTurns Turns { get; set; }
        public static Character CurrentCharacter => Turns.CurrentCharacter;
        public static List<IVisual> Highlights { get; } = new List<IVisual>();
        public static Point HoveredTile { get; set; } = new Point(0, 0);
        public static int CountCharactersIn(Team team) => Characters.Count(x => x.Team == team);

        internal static void Clear()
        {
            Map = null;
            Turns = null;
            Characters = new List<Character>();
            Highlights.Clear();
            HoveredTile = Point.Zero;
        }
    }
}
