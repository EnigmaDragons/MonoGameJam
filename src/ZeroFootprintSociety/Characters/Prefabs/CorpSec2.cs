using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class CorpSec2 : Character
    {
        public CorpSec2() : base(
            new CharacterBody("CorporationSecurity2", new Vector2(-13, -42), Color.Red),
            new CharacterStats
            {
                Name = "Corporate Security"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Enemy,
            "Characters/placeholder-soldier-face.png")
        { }
    }
}
