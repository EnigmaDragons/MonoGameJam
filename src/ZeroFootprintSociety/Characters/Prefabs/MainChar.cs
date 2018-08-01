using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    class MainChar : Character
    {
        public MainChar() : base(
            new CharacterBody("MainCharacter", new Vector2(-13, -42), TeamColors.Friendly.Characters_GlowColor),
            new CharacterStats
            {
                Name = "Weldon Zemke",
                HP = 150,
                Movement = 8,
                Accuracy = 5,
                Guts = 6,
                Agility = 4,
                Perception = 7,
            },
            new CharacterGear(new RsxCarbine(), new GoldenGun()),
            Team.Friendly,
            "Characters/MainCharacter/MainCharacter-face.png",
            "Characters/MainCharacter/MainCharacter-bust.png")
        {
        }

        public MainChar(CharacterClass c) : base(
            new CharacterBody("MainCharacter", new Vector2(-13, -42), TeamColors.Friendly.Characters_GlowColor),
            new CharacterStats
            {
                Name = "Weldon Zemke",
                HP = 110,
                Movement = 8,
            }.WithMods(c.StatMods),
            new CharacterGear(c.WeaponSet.Primary, c.WeaponSet.Secondary),
            Team.Friendly,
            "Characters/MainCharacter/MainCharacter-face.png",
            "Characters/MainCharacter/MainCharacter-bust.png")
        {
        }
    }
}
