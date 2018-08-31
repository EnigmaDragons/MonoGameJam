using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
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
    public class AvailableTargetsUI : IVisual
    {
        private readonly Dictionary<Point, List<IVisual>> _targetVisuals = new Dictionary<Point, List<IVisual>>();

        public AvailableTargetsUI()
        {
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<ActionCancelled>(e => ClearOptions(), this));
            Event.Subscribe(EventSubscription.Create<ActionConfirmed>(e => ClearOptions(), this));
        }

        private void ClearOptions()
        {
            _targetVisuals.Clear();
        }

        private void ShowOptions(ShootSelected e)
        {
            e.AvailableTargets.ForEach(x =>
            {
                _targetVisuals[x.Character.CurrentTile.Position] = new List<IVisual>();
                x.CoverToThem.Covers.SelectMany(c => c.Providers).GroupBy(p => p.Position).ForEach(g =>
                {
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new UiImage { Tint = 100.Alpha(), Image = "UI/shield-placeholder-" + g.Count(), Transform = g.First().Transform });
                    _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { TextColor = UiColors.AvailableTargetsUI_CoverPercentText, Transform = g.First().Transform });
                });
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{new HitChanceCalculation(GameWorld.CurrentCharacter.Accuracy, x.CoverToThem.BlockChance, x.Character.Stats.Agility, x.Character.State.IsHiding).Get()}%", Transform = x.Character.CurrentTile.Transform });
                _targetVisuals[x.Character.CurrentTile.Position].Add(new Label { Text = $"{new HitChanceCalculation(x.Character.Accuracy, x.CoverFromThem.BlockChance, GameWorld.CurrentCharacter.Stats.Agility, GameWorld.CurrentCharacter.State.IsHiding).Get()}% ", Transform = GameWorld.CurrentCharacter.CurrentTile.Transform });
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_targetVisuals.ContainsKey(GameWorld.HoveredTile))
                _targetVisuals[GameWorld.HoveredTile].ForEach(x => x.Draw(parentTransform));
        }
    }
}
