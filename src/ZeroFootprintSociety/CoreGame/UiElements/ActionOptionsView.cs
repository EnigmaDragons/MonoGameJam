using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class ActionOptionsView : IVisual
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = UI.OfScreenWidth(0.5f) - (_menuWidth / 2);
        private readonly int _menuY = UI.OfScreenHeight(0.76f);
        private readonly int _buttonWidth = 200;
        private readonly int _buttonHeight = 35;
        private readonly int _buttonMargin = 10;
        private int _buttonXOffset => (_menuWidth - _buttonWidth) / 2;
        private int _buttonYOffset => 30;
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);

        private bool _showingOptions = false;
        private bool _shootAvailable = false;

        public ClickUI _clickUI;

        public ActionOptionsView(ClickUI clickUI)
        {
            _clickUI = clickUI;

            var menu = new ImageBox
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            };

            var hideButton = Create(0, "Hide", () => Select(new HideChosen()), () => true);
            var shootButton = Create(1, "Shoot", () => Select(new ShootSelected()), () => _shootAvailable);
            var overwatchButton = Create(2, "Overwatch", () => Select(new OverwatchSelected()), 
                () => GameWorld.CurrentCharacter.Gear.EquippedWeapon.IsRanged);

            _visuals.Add(menu);
            _visuals.Add(hideButton);
            _branch.Add(hideButton);
            _visuals.Add(overwatchButton);
            _branch.Add(overwatchButton);
            _visuals.Add(shootButton);
            _branch.Add(shootButton);
            Event.Subscribe(EventSubscription.Create<MovementFinished>(x => PresentOptions(), this));
            Event.Subscribe(EventSubscription.Create<ActionCancelled>(x => PresentOptions(), this));
            Event.Subscribe(EventSubscription.Create<RangedTargetsAvailable>(x => _shootAvailable = x.Targets.Any(), this));
        }

        private TextButton Create(int index, string text, Action action, Func<bool> condition)
        {
            return new TextButton(
                new Rectangle(
                    _menuX + _buttonXOffset,
                    _menuY + _buttonYOffset + ((_buttonMargin + _buttonHeight) * index),
                    _buttonWidth,
                    _buttonHeight),
                action,
                text,
                Color.FromNonPremultiplied(206, 232, 245, 0),
                Color.FromNonPremultiplied(206, 232, 245, 70),
                Color.FromNonPremultiplied(206, 232, 245, 110),
                condition)
            { Font = "Fonts/12" };
        }

        private void Select(object option)
        {
            Event.Publish(option);
            HideDisplay();
        }

        public void PresentOptions()
        {
            _clickUI.Add(_branch);
            _showingOptions = true;
        }

        public void HideDisplay()
        {
            _clickUI.Remove(_branch);
            _showingOptions = false;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_showingOptions)
                return;
            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
