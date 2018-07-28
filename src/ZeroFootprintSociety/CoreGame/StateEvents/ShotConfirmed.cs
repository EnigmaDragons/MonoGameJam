using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class ShotConfirmed
    {
        public Character Attacker { get; set; }
        public Character Defender { get; set; }
    }
}
