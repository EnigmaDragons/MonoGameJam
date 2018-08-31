using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters.GUI;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.GUI;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character : IVisualAutomaton
    {
        private readonly HealthBar _healthBar = new HealthBar(42);
        private readonly DamageNumbersView _damageNumbers;

        public bool IsInitialized { get; private set; }

        public CharacterBody Body { get; }
        public CharacterStats Stats { get; private set; }
        public CharacterGear Gear { get; }
        public CharacterState State { get; }
        public string FaceImage { get; }
        public string BustImage { get; }
        public Team Team { get; }
        public TeamColorTheme Theme { get; }

        public bool IsFriendly => Team.IsIncludedIn(TeamGroup.Friendlies);
        public GameTile CurrentTile => Body.CurrentTile;
        public int Accuracy => Gear.EquippedWeapon.IsRanged ? Stats.AccuracyPercent + Gear.EquippedWeapon.AsRanged().AccuracyPercent : 0;
        public int Level => Stats.Level;
        public int Xp => State.Xp;

        public Character(CharacterBody body, CharacterStats stats, CharacterGear gear, Team team = Team.Neutral, string faceImage = "", string bustImage = "")
        {
            Stats = stats;
            Body = body;
            Gear = gear;
            FaceImage = faceImage;
            BustImage = bustImage;
            State = new CharacterState(stats);
            Team = team;
            Theme = IsFriendly ? TeamColors.Friendly : TeamColors.Enemy;

            _damageNumbers = new DamageNumbersView(this);
        }
        
        public Character Initialized(GameTile tile)
        {
            Body.Init(tile);
            State.Init();
            _healthBar.Init();
            State.SeeableTiles = new VisibilityCalculation(this).Calculate();
            IsInitialized = true;
            return this;
        }
        
        internal void Notify(XpGained e)
        {
            State.Xp += e.XpAmount;
            while (State.Xp > 100)
            {
                var oldStats = Stats.Snapshot();
                Stats = Stats.WithMods(new CharacterStatsMods { Level = 1 });
                State.Xp -= 100;
                EventQueue.Instance.Add(new LevelledUp { Character = this, OldStats = oldStats });
            }
        } 

        internal void Notify(MoveResolved e)
        {
            Body.Path.Dequeue();
            Body.ShouldContinue = true;
            if (!Body.Path.Any() && !e.Character.State.IsDeceased)
                EventQueue.Instance.Add(new MovementFinished());
        }

        internal void Notify(MovementConfirmed movement)
        {
            Body.Path = new Queue<Point>(movement.Path.Skip(1).ToList());
            Body.ShouldContinue = true;
            if (!Body.Path.Any())
                EventQueue.Instance.Add(new MovementFinished());
        }

        internal void Notify(TilesSeen e) => State.SeeableTiles = e.SeeableTiles;
        
        internal void Notify(TilesPerceived e)
        {
            State.PerceivedTiles.Clear();
            e.Tiles.ForEach(x => State.PerceivedTiles[x] = true);
        }

        internal void Notify(AttackAnimationsFinished e)
        {
            if (State.RemainingHealth <= 0 && !State.IsDeceased)
            {
                State.IsDeceased = true;
                EventQueue.Instance.Add(new CharacterDeceased { Victim = e.Target, Killer = e.Attacker });
            }
        }

        internal void Notify(TurnBegun e)
        {
            State.IsHiding = false;
            State.IsOverwatching = false;
            State.OverwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
        }

        internal void Notify(OverwatchTilesAvailable e) => State.OverwatchedTiles = e.OverwatchedTiles;
        internal void Notify(ShotHit e) => State.RemainingHealth -= e.DamageAmount;
        internal void Notify(ShotFired e) => Body.FaceToward(e.Target);

        internal void Notify(object obj) => Logger.WriteLine($"Character {Stats.Name} Received Unknown Notification {obj.GetType()}");
        
        private bool ShouldDraw { get; set; }   
        
        public void Update(TimeSpan delta)
        {
            ShouldDraw = !(State.IsDeceased || 
                         Team == Team.Enemy && !GameWorld.Friendlies.Any(x => x.State.SeeableTiles.ContainsKey(CurrentTile.Position)));
            if (State.IsDeceased)
                return;
            
            Body.Update(delta);
            _healthBar.Update(State.PercentLeft);
            _damageNumbers.Update(delta);
        }
        
        public void Draw(Transform2 parentTransform)
        {
            if (!ShouldDraw)
                return;
            
            Body.Draw(parentTransform);
        }

        public void DrawUI(Transform2 parentTransform)
        {
            if (!ShouldDraw)
                return;
            
            _healthBar.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
            _damageNumbers.Draw(parentTransform + Body.CurrentTileLocation + new Vector2(3, -Body.Transform.Size.Height - 2));
        }
    }
}
