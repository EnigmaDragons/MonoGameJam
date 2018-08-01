using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class RangedTargetInspected
    {
        public Character Attacker { get; set; }
        public Character Defender { get; set; }
        public ShotCoverInfo AttackerBlockInfo { get; set; }
        public ShotCoverInfo DefenderBlockInfo { get; set; }
    }
}
