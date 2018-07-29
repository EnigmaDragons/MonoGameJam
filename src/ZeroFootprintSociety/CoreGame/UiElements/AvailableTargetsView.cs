using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AvailableTargetsView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly Dictionary<Point, List<IVisual>> _targetVisuals = new Dictionary<Point, List<IVisual>>();
        private List<Target> _availableTargets = new List<Target>();
        private bool _isDisplaying = false;

        public AvailableTargetsView()
        {
            Event.Subscribe(EventSubscription.Create<RangedTargetsAvailable>(e => _availableTargets = e.Targets, this));
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<ShotConfirmed>(ClearOptions, this));
        }

        private void ClearOptions(ShotConfirmed e)
        {
            _visuals.Clear();
            _targetVisuals.Clear();
            GameWorld.Highlights.Remove(this);
        }

        private void ShowOptions(ShootSelected e)
        {
            _availableTargets.ForEach(x =>
            {
                var coloredBox = new ColoredRectangle { Transform = x.Character.CurrentTile.Transform, Color = Color.FromNonPremultiplied(200, 0, 0, 50) };
                _visuals.Add(coloredBox);
                _targetVisuals[x.Character.CurrentTile.Position] = new List<IVisual>();
                x.CoverToThem.Where(cover => cover.Cover > Cover.None).ForEach(cover =>
                {
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new ImageBox { Alpha = 100, Image = "UI/shield-placeholder", Transform = cover.Provider.Transform });
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { TextColor = Color.White, Transform = cover.Provider.Transform });
                });
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{x.TargetBlockChance}%", Transform = x.Character.CurrentTile.Transform });
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{x.TargetterBlockChance}%", Transform = GameWorld.CurrentCharacter.CurrentTile.Transform });
            });
            GameWorld.Highlights.Add(this);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
            if (_targetVisuals.ContainsKey(GameWorld.HoveredTile))
                _targetVisuals[GameWorld.HoveredTile].ForEach(x => x.Draw(parentTransform));
        }
    }
}
