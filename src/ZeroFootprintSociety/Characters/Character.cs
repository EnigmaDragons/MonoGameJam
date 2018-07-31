using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters.GUI;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.GUI;

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
            State = new CharacterState(stats);
            Team = team;

            _damageNumbers = new DamageNumbersView(this);

            Event.Subscribe<TurnBegun>(OnTurnBegan, this);
            Event.Subscribe<OverwatchTilesAvailable>(UpdateOverwatch, this);
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotAnimationsFinished>(OnShotsResolved, this);
            Event.Subscribe<TilesSeen>(OnTilesSeen, this);
            Event.Subscribe<TilesPercieved>(OnTilesPercieved, this);
            Event.Subscribe<MovementConfirmed>(OnMovementConfirmed, this);
            Event.Subscribe<MoveResolved>(ContinueMoving, this);
            Event.Subscribe<Moved>(FootstepsIfMainChar, this);
        }

        private void FootstepsIfMainChar(Moved obj)
        {
            if (this is MainChar && obj.Character.Equals(this))
                if (GameWorld.FootstepsRemaining-- == 0)
                    Event.Publish(new OutOfFootsteps());
        }

        private void ContinueMoving(MoveResolved e)
        {
            if (e.Character == this)
            {
                Body.Footprints.Add(new Footprint(Body.Path.First(), Body.Facing));
                Body.Stopped = false;
                Body.Path.RemoveAt(0);
                if (!Body.Path.Any() && !e.Character.State.IsDeceased)
                    Event.Publish(new MovementFinished());
            }
        }

        private void OnMovementConfirmed(MovementConfirmed movement)
        {
            if (GameWorld.Turns.CurrentCharacter == this)
            {
                Body.Footprints.Add(new Footprint(movement.Path.First(), Body.Facing));
                Body.Path = movement.Path.Skip(1).ToList();
                if (!Body.Path.Any())
                    Event.Publish(new MovementFinished());
            }
        }

        private void OnTilesSeen(TilesSeen e)
        {
            if (e.Character == this)
                State.SeeableTiles = e.SeeableTiles;
        }

        private void OnTilesPercieved(TilesPercieved e)
        {
            if (e.Character == this)
            {
                State.PercievedTiles.Clear();
                e.Tiles.ForEach(x => State.PercievedTiles[x] = true);
            }
        }

        private void OnShotsResolved(ShotAnimationsFinished e)
        {
            if (State.RemainingHealth <= 0 && !State.IsDeceased)
            {
                State.IsDeceased = true;
                Event.Publish(new CharacterDeceased { Character = this });
            }
        }

        public void Init(GameTile tile)
        {
            Body.Init(tile);
            State.Init();
            _healthBar.Init();
            // Initialize visible tiles.

            IsInitialized = true;
        }

        public Character Initialized(GameTile tile)
        {
            Init(tile);
            State.SeeableTiles = new VisibilityCalculation(this).Calculate();
            return this;
        }

        public void OnTurnBegan(TurnBegun e)
        {
            if (GameWorld.CurrentCharacter == this)
            {
                Body.Footprints.Clear();
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

        private void OnShotHit(ShotHit e)
        {
            if (e.Target.Equals(this))
                State.RemainingHealth -= e.DamageAmount;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (State.IsDeceased || (Team == Team.Enemy && !GameWorld.Friendlies.Any(x => x.State.SeeableTiles.ContainsKey(CurrentTile.Position))))
                return;
            Body.Draw(parentTransform);
        }

        public void DrawUI(Transform2 parentTransform)
        {
            if (State.IsDeceased || (Team == Team.Enemy && !GameWorld.Friendlies.Any(x => x.State.SeeableTiles.ContainsKey(CurrentTile.Position))))
                return;
            _healthBar.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
            _damageNumbers.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
        }

        public void Update(TimeSpan delta)
        {
            if (State.IsDeceased)
                return;
            Body.Update(delta);
            _healthBar.Update(State.PercentLeft);
            _damageNumbers.Update(delta);
        }
    }
}
