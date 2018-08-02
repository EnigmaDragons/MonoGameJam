using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.AI
{
    public class AICharacterData
    {
        public Dictionary<Character, Point> SeenEnemies { get; set; } = new Dictionary<Character, Point>();
    }
}
