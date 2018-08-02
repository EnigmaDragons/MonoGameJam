using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class CorpSec3 : Character
    {
        public static string Bust = "Characters/Sidechick/Sidechick-bust.png";

        public CorpSec3(bool mustKill) : base(
            new CharacterBody("CorporationSecurity3", new Vector2(-13, -42), TeamColors.Enemy.Characters_GlowColor),
            new CharacterStats
            {
                Name = "CorpSec Elite",
                HP = 99,
                Movement = 8,
                Accuracy = 7,
                Guts = 9,
                Agility = 13,
                Perception = 9,
            },
            new CharacterGear(WeaponLists.RandomPrimary(), WeaponLists.RandomSecondary()),
            Team.Enemy,
            "Characters/placeholder-soldier-face.png",
            Bust)
        {
            State.MustKill = true;
            State.NextScene = "Credits";
        }
    }
}
