using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.GUI
{
    public sealed class DisplayCharacterStatusRequested
    {
        public Character Character { get; }

        public DisplayCharacterStatusRequested(Character character) => Character = character;
    }
}