using System.Threading;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CharacterData
    {
        private static int _unnamedCharacterNumber = 1;

        public string Name { get; set; } = $"Character {Interlocked.Increment(ref _unnamedCharacterNumber)}";
        public int HP { get; set; } = 30; // Hit Points until dead
        public int Movement { get; set; } = 5; // Number of movement tiles
        public int Accuracy { get; set; } = 5; // % = Accuracy * 5
        public int Guts { get; set; } = 5; // Power in melee combat. 
        public int Agility { get; set; } = 5; // Initiative. Chance to dodge bullets.
        public int Perception { get; set; } = 5; // Range and fidelity of detecting unseen enemy movement. Observe traps.
    }
}
