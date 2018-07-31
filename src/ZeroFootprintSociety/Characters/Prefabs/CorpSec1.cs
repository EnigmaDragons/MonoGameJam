using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public sealed class CorpSec1 : Character
    {
        public CorpSec1() : base(
            new CharacterBody("CorporationSecurity1", new Vector2(-13, -42), TeamColors.Enemy.Characters_GlowColor),
            new CharacterStats
            {
                Name = "Corporate Security"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Enemy,
            "Characters/placeholder-soldier-face.png") {}
    }
}
