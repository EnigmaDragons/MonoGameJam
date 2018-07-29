using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Teams;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameWorld
    {
        public static GameMap Map { get; set; } 
        public static List<Character> Characters { get; set; }
        public static CharacterTurns Turns { get; set; }
        public static Character CurrentCharacter => Turns.CurrentCharacter;
        public static List<IVisual> Highlights { get; } = new List<IVisual>();

        public static int CountCharactersIn(Team team) => Characters.Count(x => x.Team == team);

        internal static void Clear()
        {
            Map = null;
            Turns = null;
            Characters?.Clear();
            Highlights?.Clear();
        }
    }
}
