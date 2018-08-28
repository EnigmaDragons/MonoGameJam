using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    class InGameMenu : IVisual
    {
        private const int _menuWidth = 600;
        private const int _menuHeight = 400;
        private readonly int _menuX = UI.OfScreenWidth(0.5f) - (_menuWidth / 2);
        private readonly int _menuY = UI.OfScreenHeight(0.3f);

        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _interceptLayer = new ClickUIBranch("MenuBack", 9);
        private readonly ClickUIBranch _branch = new ClickUIBranch("Menu", 10);

        private bool _showingOptions = false;

        private ClickUI _clickUI;

        public InGameMenu(ClickUI clickUi)
        {
            _clickUI = clickUi;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 50 };

            var menu = new UiImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-wide-panel.png"
            };

            var mainMenuButton = Buttons.Text(ctx, 4, "Return to Main Menu", () =>  Scene.NavigateTo("MainMenu"), () => true);
            var characterStatus = Buttons.Text(ctx, 3, "Character Status", () => Event.Publish(new DisplayCharacterStatusRequested(GameWorld.CurrentCharacter)), () => true);

            _visuals.Add(new ColoredRectangle
            {
                Color = UiColors.InGameMenu_FullScreenRectangle, 
                Transform = new Transform2(new Size2(1920, 1080))
            });
            _visuals.Add(menu);
            _visuals.Add(mainMenuButton);
            _visuals.Add(characterStatus);
            _interceptLayer.Add(new ScreenClickable(HideDisplay));
            _branch.Add(mainMenuButton);
            _branch.Add(characterStatus);
            Input.On(Control.Menu, ToggleMenu);
            Event.Subscribe(EventSubscription.Create<MenuRequested>(x => PresentOptions(), this));
            Event.Subscribe(EventSubscription.Create<SubviewRequested>(x => HideDisplay(), this));
            Event.Subscribe(EventSubscription.Create<SubviewDismissed>(x => PresentOptions(), this));
        }

        public void ToggleMenu()
        {
            if (_showingOptions)
                HideDisplay();
            else
                Event.Publish(new MenuRequested());
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
