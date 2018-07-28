using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    class ShotMissed
    {
        public Character Target { get; set; }
        public Character Attacker { get; set; }
    }
}
