﻿using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Prefabs
{
    public class Sidechick : Character
    {
        public Sidechick() : base(
            new CharacterBody("Sidechick", new Vector2(-13, -42), Color.Blue),
            new CharacterStats
            {
                Name = "Nameless One"
            },
            new CharacterGear(new RsxCarbine(), new RsxCarbine()),
            Team.Friendly,
            "Characters/placeholder-mainchar-face.png")
        { }
    }
}