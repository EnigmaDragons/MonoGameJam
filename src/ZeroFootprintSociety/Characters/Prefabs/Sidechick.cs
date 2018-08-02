using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class Sidechick : Character
    {
        public Sidechick() : base(
            new CharacterBody("Sidechick", new Vector2(-13, -42), Color.Blue),
            new CharacterStats
            {
                Name = "Cassia Lanthe",
                HP = 125,
                Movement = 8,
                Accuracy = 5,
                Guts = 4,
                Agility = 9,
                Perception = 6
            },
            new CharacterGear(new WarShotgun(), new PowerMagnum()),
            Team.Friendly,
            "Characters/Sidechick/Sidechick-face.png",
            "Characters/Sidechick/Sidechick-bust.png")
        { }
    }
}
