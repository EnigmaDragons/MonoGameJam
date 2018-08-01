using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class CorpSec3 : Character
    {
        public CorpSec3() : base(
            new CharacterBody("CorporationSecurity3", new Vector2(-13, -42), TeamColors.Enemy.Characters_GlowColor),
            new CharacterStats
            {
                Name = "CorpSec Elite"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Enemy,
            "Characters/placeholder-soldier-face.png")
        { }
    }
}
