using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class CorpSec3 : Character
    {
        public static string Bust = "Characters/CorporationSecurity3/Corporate_Villain_bust.png";

        public CorpSec3(bool mustKill) : base(
            new CharacterBody("CorporationSecurity3", new Vector2(-13, -42), TeamColors.Enemy.Characters_GlowColor),
            new CharacterStats
            {
                Name = "CorpSec Elite",
                HP = 99,
                Movement = 9,
                Accuracy = 9,
                Guts = 9,
                Agility = 9,
                Perception = 9,
            },
            new CharacterGear(new WarUzi(), WeaponLists.RandomSecondary()),
            Team.Enemy,
            "Characters/CorporationSecurity3/Corporate_Villain_face.png",
            Bust)
        {
            State.MustKill = true;
            State.NextScene = "Credits";
        }
    }
}
