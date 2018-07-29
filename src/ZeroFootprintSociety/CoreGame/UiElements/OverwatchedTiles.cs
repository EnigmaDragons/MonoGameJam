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

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class OverwatchedTiles : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private bool _waitingForActionSelected = false;

        public OverwatchedTiles()
        {
            Event.Subscribe<OverwatchSelected>(x => _waitingForActionSelected = true, this);
            Event.Subscribe<ActionSelected>(x => ShowIfApplicable(), this);
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
                        Color = Color.FromNonPremultiplied(0, 150, 0, 150),
                        Transform = GameWorld.Map[x.Key].Transform
                    });
                    _visuals.Add(new Label
                    {
                        Font = "Fonts/12",
                        TextColor = Color.White,
                        Text = $"{new HitChanceCalculation(GameWorld.CurrentCharacter.Accuracy, x.Value.BlockChance).Get()}%",
                        Transform = GameWorld.Map[x.Key].Transform
                    });
                });
            }
        }

        private void Hide()
        {
            _waitingForActionSelected = false;
            _visuals.Clear();
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }
    }
}
