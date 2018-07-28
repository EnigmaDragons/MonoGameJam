using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character: IVisualAutomaton
    {
        public CharacterBody Body { get; }
        public CharacterStats Stats { get; }
        public CharacterGear Gear { get; }
        public CharacterState State { get; }
        public string FaceImage { get; }

        private readonly HealthBar _healthBar = new HealthBar(42);

        public GameTile CurrentTile => Body.CurrentTile;

        public Character(CharacterBody body, CharacterStats stats, CharacterGear gear, string faceImage)
        {
            Stats = stats;
            Body = body;
            Gear = gear;
            FaceImage = faceImage;
            State = new CharacterState(stats);
        }

        public void Init(GameTile tile)
        {
            Body.Init(tile);
            State.Init();
            _healthBar.Init();
        }

        public void Draw(Transform2 parentTransform)
        {
            Body.Draw(parentTransform);
            _healthBar.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
        }

        public void Update(TimeSpan delta)
        {
            Body.Update(delta);
            _healthBar.Update(State.PercentLeft);
        }
    }
}
