using System.Collections.Generic;
using System.Linq;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Covors
{
    public class ShotCoverInfo
    {
        public List<CoverProvided> Covers { get; }
        public int BlockChance { get; }

        public ShotCoverInfo(List<CoverProvided> covers)
        {
            Covers = covers;
            BlockChance = covers.Sum(x => (int)x.Cover);
        }
    }
}
