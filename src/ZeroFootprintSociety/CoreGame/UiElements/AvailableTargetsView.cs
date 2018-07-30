using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;
using ZeroFootPrintSociety.UIEffects;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AvailableTargetsView : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();
        private readonly Dictionary<Point, List<IVisual>> _targetVisuals = new Dictionary<Point, List<IVisual>>();

        public AvailableTargetsView()
        {
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<ActionCancelled>(e => ClearOptions(), this));
            Event.Subscribe(EventSubscription.Create<ActionConfirmed>(e => ClearOptions(), this));
        }

        private void ClearOptions()
        {
            _visuals.Clear();
            _targetVisuals.Clear();
            _automata.Clear();
            GameWorld.Highlights.Remove(this);
        }

        private void ShowOptions(ShootSelected e)
        {
            e.AvailableTargets.ForEach(x =>
            {
                _visuals.Add(new ColoredRectangle { Transform = x.Character.CurrentTile.Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 35) });
                var anim = new TileRotatingEdgesAnim(x.Character.CurrentTile.Position, Color.FromNonPremultiplied(255, 20, 20, 255));
                anim.Init();
                _visuals.Add(anim);
                _automata.Add(anim);

                _targetVisuals[x.Character.CurrentTile.Position] = new List<IVisual>();
                x.CoverToThem.Covers.ForEach(cover => cover.Providers.ForEach(p => 
                {
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new UiImage { Alpha = 100, Image = "UI/shield-placeholder", Transform = p.Transform });
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { TextColor = Color.White, Transform = p.Transform });
                }));
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{new HitChanceCalculation(GameWorld.CurrentCharacter.Accuracy, x.CoverToThem.BlockChance, x.Character.Stats.Agility).Get()}%", Transform = x.Character.CurrentTile.Transform });
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{new HitChanceCalculation(x.Character.Accuracy, x.CoverFromThem.BlockChance, GameWorld.CurrentCharacter.Stats.Agility).Get()}% ", Transform = GameWorld.CurrentCharacter.CurrentTile.Transform });
            });
            GameWorld.Highlights.Add(this);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
            if (_targetVisuals.ContainsKey(GameWorld.HoveredTile))
                _targetVisuals[GameWorld.HoveredTile].ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            _automata.ToList().ForEach(x => x.Update(delta));
        }
    }
}
