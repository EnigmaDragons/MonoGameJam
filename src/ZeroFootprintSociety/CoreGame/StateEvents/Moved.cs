using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class Moved
    {
        public Point Position { get; set; }
        public Character Character { get; set; }
    }
}
