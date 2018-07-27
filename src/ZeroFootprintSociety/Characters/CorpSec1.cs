using Microsoft.Xna.Framework;
using System;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CorpSec1 : Character
    {
        public CorpSec1(Action onTurnStart) : base(
            new CharacterBody("CorporateSecurity", new Vector2(-15, -42)),
            new CharacterData
            {
                Name = "Corporate Security"
            },
            onTurnStart) {}
    }
}
