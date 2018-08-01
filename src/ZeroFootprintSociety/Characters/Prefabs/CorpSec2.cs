using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class CorpSec2 : Character
    {
        public CorpSec2(bool mustKill) : base(
            new CharacterBody("CorporationSecurity2", new Vector2(-13, -42), TeamColors.Enemy.Characters_GlowColor),
            new CharacterStats
            {
                Name = "Corporate Security"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Enemy,
            "Characters/placeholder-soldier-face.png")
        {
            State.MustKill = mustKill;
        }
    }
}
