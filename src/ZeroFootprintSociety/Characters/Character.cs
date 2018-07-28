
using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character: IVisualAutomaton
    {
        protected CharacterBody Body { get; }
        public CharacterStats Stats { get; }
        public CharacterGear Gear { get; }
        public CharacterState State { get; }

        private readonly HealthBar _healthBar = new HealthBar(20);

        public GameTile CurrentTile
        {
            get => Body.CurrentTile;
            set
            {
                Body.CurrentTile = value;
                Body.CurrentTile.OnCharacterSteps(this);
            }
        }

        public Character(CharacterBody body, CharacterStats stats, CharacterGear gear)
        {
            Stats = stats;
            Body = body;
            Gear = gear;
            State = new CharacterState(stats);
        }

        public void Init()
        {
            Body.Init();
            State.Init();
            _healthBar.Init();
        }

        public void Draw(Transform2 parentTransform)
        {
            Body.Draw(parentTransform);
            _healthBar.Draw(parentTransform + Body.OffsetWhatever + new Vector2(2, -(Body.Size.Size.Height)));
        }

        public void Update(TimeSpan delta)
        {
            Body.Update(delta);
            _healthBar.Update(State.PercentLeft);
        }
    }
}
