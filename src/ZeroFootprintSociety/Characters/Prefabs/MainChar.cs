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
                Name = "Weldon Zemke"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Friendly,
            "Characters/MainCharacter/MainCharacter-face.png",
            "Characters/MainCharacter/MainCharacter-bust.png") { }
    }
}
