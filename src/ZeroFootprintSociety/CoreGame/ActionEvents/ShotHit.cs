using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    class ShotHit
    {
        public Character Target { get; set; }
        public Character Attacker { get; set; }
        public int DamageAmount { get; set; }
    }
}
