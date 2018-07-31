using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    public class OverwatchedTiles : IVisual
    {
        private readonly BlockingCollection<IVisual> _visuals = new BlockingCollection<IVisual>();
        private bool _waitingForActionSelected = false;

        public OverwatchedTiles()
        {
            Event.Subscribe<OverwatchSelected>(x => _waitingForActionSelected = true, this);
            Event.Subscribe<ActionReadied>(x => ShowIfApplicable(), this);
            Event.Subscribe<ActionConfirmed>(x => Hide(), this);
            Event.Subscribe<ActionCancelled>(x => Hide(), this);
        }

        private void ShowIfApplicable()
        {
            if (_waitingForActionSelected)
            {
                GameWorld.CurrentCharacter.State.OverwatchedTiles.ForEach(x =>
                {
                    _visuals.Add(new ColoredRectangle
                    {
                        Color = UIColors.OverwatchedTiles_Rectangle(Math.Min(255, (int)(150 * GameWorld.CurrentCharacter.Gear.EquippedWeapon.AsRanged().EffectiveRanges[GameWorld.CurrentCharacter.CurrentTile.Position.TileDistance(x.Key)]))),
                        Transform = GameWorld.Map[x.Key].Transform
                    });
                    _visuals.Add(new Label
                    {
                        Font = "Fonts/12",
                        TextColor = UIColors.OverwatchedTiles_Text,
                        Text = $"{new HitChanceCalculation(GameWorld.CurrentCharacter.Accuracy, x.Value.BlockChance).Get()}%",
                        Transform = GameWorld.Map[x.Key].Transform
                    });
                });
            }
        }

        private void Hide()
        {
            _waitingForActionSelected = false;
            while(_visuals.Count > 0)
                _visuals.Take();
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }
    }
}
