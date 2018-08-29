using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public sealed class CharacterDeceased
    {
        public Character Victim { get; set; }
        public Character Killer { get; set; }
    }
}
