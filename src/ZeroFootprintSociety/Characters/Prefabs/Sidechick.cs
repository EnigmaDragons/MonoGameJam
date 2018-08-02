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
                Accuracy = 5,
                Guts = 4,
                Agility = 9,
                Perception = 6
            },
<<<<<<< HEAD
            new CharacterGear(new PowerMagnum(), new AutoPistol()),
=======
            new CharacterGear(new WarShotgun(), new PowerMagnum()),
>>>>>>> ba14534f2c974df8fe10585b193bbcfb5e0b2eab
            Team.Friendly,
            "Characters/Sidechick/Sidechick-face.png",
            Bust)
        { }
    }
}
