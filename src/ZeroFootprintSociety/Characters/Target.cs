using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Characters
{
    public class Target
    {
        public Character Character { get; set; }
        public List<CoverProvided> CoverToThem { get; set; }
        public List<CoverProvided> CoverFromThem { get; set; }
        public int TargetBlockChance { get; set; }
        public int TargetterBlockChance { get; set; }
    }
}
