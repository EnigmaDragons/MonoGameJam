using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    public struct XpGained
    {
        public Character Character { get; set; }
        public int XpAmount { get; set; }
    }
}