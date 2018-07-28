using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CorpSec1 : Character
    {
        public CorpSec1() : base(
            new CharacterBody("CorporateSecurity", new Vector2(-15, -42)),
            new CharacterStats
            {
                Name = "Corporate Security"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine())) {}
    }
}
