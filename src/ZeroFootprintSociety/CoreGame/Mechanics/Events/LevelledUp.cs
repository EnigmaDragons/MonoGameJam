using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    public class LevelledUp
    {
        public Character Character { get; set; }
        public CharacterStats OldStats { get; set; }
    }
}