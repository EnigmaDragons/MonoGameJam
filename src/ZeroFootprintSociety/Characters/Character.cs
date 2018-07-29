﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters.Ui;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character : IVisualAutomaton
    {
        private readonly HealthBar _healthBar = new HealthBar(42);
        private readonly DamageNumbersView _damageNumbers;

        public bool IsInitialized { get; internal set; }

        public CharacterBody Body { get; }
        public CharacterStats Stats { get; }
        public CharacterGear Gear { get; }
        public CharacterState State { get; }
        public string FaceImage { get; }
        public Team Team { get; }
       
        public GameTile CurrentTile => Body.CurrentTile;
        public int Accuracy => Gear.EquippedWeapon.IsRanged ? Stats.AccuracyPercent + Gear.EquippedWeapon.AsRanged().AccuracyPercent : 0;

        public Character(CharacterBody body, CharacterStats stats, CharacterGear gear, Team team = Team.Neutral, string faceImage = "")
        {
            Stats = stats;
            Body = body;
            Gear = gear;
            FaceImage = faceImage;
            State = new CharacterState(stats, this);
            Team = team;

            _damageNumbers = new DamageNumbersView(this);

            Event.Subscribe<TurnBegun>(OnTurnBegan, this);
            Event.Subscribe<OverwatchTilesAvailable>(UpdateOverwatch, this);
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

        public void OnTurnBegan(TurnBegun e)
        {
            if (GameWorld.CurrentCharacter == this)
            {
                State.IsHiding = false;
                State.IsOverwatching = false;
                State.OverwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
            }
        }

        public void UpdateOverwatch(OverwatchTilesAvailable e)
        {
            if (GameWorld.CurrentCharacter == this)
                State.OverwatchedTiles = e.OverwatchedTiles;
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
