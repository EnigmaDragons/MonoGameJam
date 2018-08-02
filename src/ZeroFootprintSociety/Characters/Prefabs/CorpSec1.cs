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
                Name = "CorpSec Guard",
                HP = 25,
                Movement = 6,
                Accuracy = 3,
                Guts = 3,
                Agility = 3,
                Perception = 5
            },
            new CharacterGear(WeaponLists.RandomPrimary(), WeaponLists.RandomSecondary()),
            Team.Enemy,
            "Characters/CorpSec-face.png",
            "Characters/CorpSec-bust.png") {}
    }
}
