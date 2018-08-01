using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.GUI;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class GameWorld
    {
        public static bool IsInitialized => Map != null && Characters != null && Turns != null && Highlights != null && HighHighlights != null;
        public static GameMap Map { get; set; } 
        public static IReadOnlyList<Character> Characters { get; set; }
        public static IReadOnlyList<Character> LivingCharacters => Characters.ToList().Where(x => !x.State.IsDeceased).ToList();
        public static CharacterTurns Turns { get; set; }
        public static Character CurrentCharacter => Turns.CurrentCharacter;
        public static bool IsEnemyTurn => CurrentCharacter.Team.Equals(Team.Enemy);
        public static Highlights Highlights { get; set; }
        public static HighHighlights HighHighlights { get; set; }
        public static Point HoveredTile { get; set; } = new Point(0, 0);
        public static IEnumerable<Character> Friendlies => FriendliesWhere();
        public static bool IsGameOver { get; internal set; }
        public static IEnumerable<Character> FriendliesWhere(Predicate<Character> wherePredicate = null)
            => LivingCharacters.Where(x => x.Team == Team.Friendly && (wherePredicate?.Invoke(x) ?? true) );
        public static DictionaryWithDefault<Point, bool> FriendlyPerception { get; set; } = new DictionaryWithDefault<Point, bool>(false);
        
        private static int _footsteps = 0;
        public static int FootstepsRemaining
        {
            get => _footsteps;
            set
            {
                _footsteps = value;
                Event.Publish(new FootstepCounted {Steps = value});
            }
        }

        internal static void Clear()
        {
            Map = null;
            Turns = null;
            Characters = new List<Character>();
            Highlights = null;
            HighHighlights = null;
            FriendlyPerception = new DictionaryWithDefault<Point, bool>(false);
            HoveredTile = Point.Zero;
            _footsteps = 0;
        }
    }
}
