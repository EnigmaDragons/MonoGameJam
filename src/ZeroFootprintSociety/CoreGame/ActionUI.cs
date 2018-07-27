using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;

namespace ZeroFootPrintSociety.CoreGame
{
    public class ActionUI : IVisual
    {
        private const int _menuX = 1400;
        private const int _menuY = 650;
        private const int _menuWidth = 150;
        private const int _menuHeight = 200;
        private const int _actionTextHeight = 50;
        private const int _buttonWidth = 100;
        private const int _buttonHeight = 35;
        private const int _buttonMargin = 10;
        private int _buttonXOffset => (_menuWidth - _buttonWidth) / 2;
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);

        private bool _showingOptions = false;

        public ClickUI _ClickUI;

        public ActionUI(ClickUI clickUI)
        {
            _ClickUI = clickUI;
            var menu = new ColoredRectangle { Color = Color.Green, Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)) };
            var button = new TextButton(new Rectangle(_menuX + _buttonXOffset, _menuY + _actionTextHeight + _buttonMargin, _buttonWidth, _buttonHeight), () =>
                {
                    Event.Publish(new HideChosen());
                    HideDisplay();
                }, "Hide", 
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));
            _visuals.Add(menu);
            _visuals.Add(button);
            _branch.Add(button);
            Event.Subscribe(EventSubscription.Create<CharacterMoved>(x => PresentOptions(), this));
        }

        public void PresentOptions()
        {
            _ClickUI.Add(_branch);
            _showingOptions = true;
        }

        public void HideDisplay()
        {
            _ClickUI.Remove(_branch);
            _showingOptions = false;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_showingOptions)
                return;
            _visuals.ForEach(x => x.Draw(parentTransform));
            UI.DrawTextCentered("Actions", new Rectangle(_menuX, _menuY, _menuWidth, _actionTextHeight), Color.White);
        }
    }
}
