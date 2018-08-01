using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    public class ShotBlocked
    {
        public GameTile Blocker { get; set; }
        public Character Attacker { get; set; }
        public Character Target { get; set; }
    }
}
