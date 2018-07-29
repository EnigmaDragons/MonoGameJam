using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    class MainChar : Character
    {
        public MainChar() : base(
            new CharacterBody("MainCharacter", new Vector2(-13, -42)),
            new CharacterStats
            {
                Name = "Weldon Zemke"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Friendly,
            "Characters/placeholder-mainchar-face.png") { }
    }
}
