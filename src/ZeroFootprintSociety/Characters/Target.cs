using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;

namespace ZeroFootPrintSociety.Characters
{
    public class Target
    {
        public Character Character { get; set; }
        public ShotCoverInfo CoverToThem { get; set; }
        public ShotCoverInfo CoverFromThem { get; set; }
    }
}
