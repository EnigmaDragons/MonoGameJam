using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class RangedTargetInspected
    {
        public Character Attacker { get; set; }
        public Character Defender { get; set; }
        public int AttackerBlockChance { get; set; }
        public int DefenderBlockChance { get; set; }
    }
}
