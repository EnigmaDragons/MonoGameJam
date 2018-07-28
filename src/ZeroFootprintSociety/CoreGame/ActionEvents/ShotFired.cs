using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    class ShotFired
    {
        public Character Target { get; set; }
        public Character Attacker { get; set; }
    }
}
