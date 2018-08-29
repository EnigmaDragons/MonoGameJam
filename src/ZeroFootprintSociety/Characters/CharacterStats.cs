using System.Threading;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CharacterStats
    {
        private static int _unnamedCharacterNumber = 1;

        public string Name { get; set; } = $"Character {Interlocked.Increment(ref _unnamedCharacterNumber)}";
        public int Level { get; set; } = 1;
        public int HP { get; set; } = 30; // Hit Points until dead
        public int Movement { get; set; } = 5; // Number of movement tiles
        public int Accuracy { get; set; } = 5; // % = Accuracy * 5
        public int AccuracyPercent => Accuracy * 5;
        public int Guts { get; set; } = 5; // Power in melee combat. 
        public int Agility { get; set; } = 5; // Initiative. Chance to dodge bullets.
        public int Perception { get; set; } = 5; // Range and fidelity of detecting unseen enemy movement. Observe traps.

        public CharacterStats WithMods(CharacterStatsMods cStatMods)
        {
            Level += cStatMods.Level;
            HP += cStatMods.HP;
            Movement += cStatMods.Movement;
            Accuracy += cStatMods.Accuracy;
            Guts += cStatMods.Guts;
            Agility += cStatMods.Agility;
            Perception += cStatMods.Perception;
            return this;
        }
    }
    
    public sealed class CharacterStatsMods
    {
        public int Level { get; set; }
        public int HP { get; set; } 
        public int Movement { get; set; }
        public int Accuracy { get; set; }
        public int Guts { get; set; }
        public int Agility { get; set; }
        public int Perception { get; set; }
    }
}
