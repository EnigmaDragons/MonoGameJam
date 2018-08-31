using Microsoft.Xna.Framework;
using MonoDragons.Core;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class TilesSeen
    {
        public Character Character { get; set; }
        public DictionaryWithDefault<Point, bool> SeeableTiles { get; set; }
    }
}
