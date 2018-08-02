using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    class MainChar : Character
    {
        public const string Name = "Weldon Zemke";
        public const string Bust = "Characters/MainCharacter/MainCharacter-bust.png";

        public MainChar(CharacterClass c) : base(
            new CharacterBody("MainCharacter", new Vector2(-13, -42), TeamColors.Friendly.Characters_GlowColor),
            new CharacterStats
            {
                Name = Name,
                HP = 110,
                Movement = 8,
            }.WithMods(c.StatMods),
            new CharacterGear(c.WeaponSet.Primary, c.WeaponSet.Secondary),
            Team.Friendly,
            "Characters/MainCharacter/MainCharacter-face.png",
            Bust)
        {
        }
    }
}
