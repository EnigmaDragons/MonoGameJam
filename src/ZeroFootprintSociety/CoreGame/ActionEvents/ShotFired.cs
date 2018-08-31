using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    public class ShotFired
    {
        public Character Target { get; set; }
        public Character Attacker { get; set; }
    }
}
