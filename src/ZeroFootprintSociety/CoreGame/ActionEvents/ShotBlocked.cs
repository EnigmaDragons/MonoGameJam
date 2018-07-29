using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.ActionEvents
{
    public class ShotBlocked
    {
        public Character Target { get; set; }
        public Character Attacker { get; set; }
    }
}
