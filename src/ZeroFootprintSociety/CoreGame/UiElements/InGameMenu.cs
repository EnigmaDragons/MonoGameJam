using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.UiElements.UiEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class InGameMenu : IVisual
    {
        private const int _menuX = 500;
        private const int _menuY = 300;
        private const int _menuWidth = 600;
        private const int _menuHeight = 400;
        private const int _actionTextHeight = 50;
        private const int _buttonWidth = 400;
        private const int _buttonHeight = 35;
        private const int _buttonMargin = 10;
        private int _buttonXOffset => (_menuWidth - _buttonWidth) / 2;

        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _interceptLayer = new ClickUIBranch("MenuBack", 9);
        private readonly ClickUIBranch _branch = new ClickUIBranch("Menu", 10);

        private bool _showingOptions = false;

        private ClickUI _clickUI;

        public InGameMenu(ClickUI clickUi)
        {
            _clickUI = clickUi;
            var menu = new ColoredRectangle { Color = Color.SkyBlue, Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)) };
            var closeButton = new TextButton(new Rectangle(_menuX + _buttonXOffset, _menuY + _actionTextHeight + _buttonMargin, _buttonWidth, _buttonHeight), () =>
                {
                    HideDisplay();
                }, "Close Menu",
                    Color.FromNonPremultiplied(0, 0, 100, 50),
                    Color.FromNonPremultiplied(0, 0, 100, 150),
                    Color.FromNonPremultiplied(0, 0, 100, 250));

            var mainMenuButton = new TextButton(new Rectangle(_menuX + _buttonXOffset, _menuY + _actionTextHeight + _buttonMargin + _buttonMargin + _buttonHeight, _buttonWidth, _buttonHeight), () =>
            {
                Scene.NavigateTo("MainMenu");
            }, "MainMenu",
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));

            _visuals.Add(new ColoredRectangle { Color = Color.FromNonPremultiplied(0, 0, 0, 100), Transform = new Transform2(new Size2(1920, 1080)) });
            _visuals.Add(menu);
            _visuals.Add(closeButton);
            _visuals.Add(mainMenuButton);
            _interceptLayer.Add(new ScreenClickable(() => { }));
            _branch.Add(closeButton);
            _branch.Add(mainMenuButton);
            Event.Subscribe(EventSubscription.Create<MenuRequested>(x => PresentOptions(), this));
        }

        public void PresentOptions()
        {
            _clickUI.Add(_interceptLayer);
            _clickUI.Add(_branch);
            _showingOptions = true;
        }

        public void HideDisplay()
        {
            _clickUI.Remove(_interceptLayer);
            _clickUI.Remove(_branch);
            _showingOptions = false;
            Event.Publish(new MenuDismissed());
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_showingOptions)
                _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
