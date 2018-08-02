using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class Sidechick : Character
    {
        public static string Bust = "Characters/Sidechick/Sidechick-bust.png";

        public Sidechick() : base(
            new CharacterBody("Sidechick", new Vector2(-13, -42), Color.Blue),
            new CharacterStats
            {
                Name = "Cassia Lanthe",
                HP = 125,
                Movement = 8,
                Accuracy = 6,
                Guts = 5,
                Agility = 9,
                Perception = 6
            },
            new CharacterGear(new PowerMagnum(), new WarShotgun()),
            Team.Friendly,
            "Characters/Sidechick/Sidechick-face.png",
            Bust)
        { }
    }
}
