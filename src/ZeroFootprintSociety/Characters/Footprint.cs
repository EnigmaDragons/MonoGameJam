using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Characters
{
    public class Footprint
    {
        public Point Tile { get; }
        public Direction Direction { get; }

        public Footprint(Point tile, Direction direction)
        {
            Tile = tile;
            Direction = direction;
        }
    }
}
