using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters.Teams;
using ZeroFootPrintSociety.Characters.Ui;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character : IVisualAutomaton
    {
        public bool IsInitialized { get; internal set; }

        public CharacterBody Body { get; }
        public CharacterStats Stats { get; }
        public CharacterGear Gear { get; }
        public CharacterState State { get; }
        public string FaceImage { get; }
        public Team Team { get; }

        private readonly HealthBar _healthBar = new HealthBar(42);
        private readonly DamageNumbersView _damageNumbers;

        public GameTile CurrentTile => Body.CurrentTile;

        public Character(CharacterBody body, CharacterStats stats, CharacterGear gear, Team team = Team.Neutral, string faceImage = "")
        {
            Stats = stats;
            Body = body;
            Gear = gear;
            FaceImage = faceImage;
            State = new CharacterState(stats, this);
            Team = team;

            _damageNumbers = new DamageNumbersView(this);

            Event.Subscribe<TurnBegun>(_ =>
            {
                if (GameWorld.CurrentCharacter == this)
                    State.IsHiding = false;
            }, this);
        }

        public void Init(GameTile tile)
        {
            Body.Init(tile);
            State.Init();
            _healthBar.Init();
            IsInitialized = true;
        }

        public Character Initialized(GameTile tile)
        {
            Init(tile);
            return this;
        }

        public void Draw(Transform2 parentTransform)
        {
            Body.Draw(parentTransform);
            _healthBar.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
            _damageNumbers.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
        }

        public void Update(TimeSpan delta)
        {
            Body.Update(delta);
            _healthBar.Update(State.PercentLeft);
            _damageNumbers.Update(delta);
        }
    }
}
