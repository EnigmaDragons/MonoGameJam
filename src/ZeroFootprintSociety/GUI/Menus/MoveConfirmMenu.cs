using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    class ActionConfirmMenu : IVisual
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.85.VH();
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);
        private readonly Label _actionLabel;
        private readonly ImageButton _confirmButton;

        private bool _show = false;
        private bool _isReady = false;

        public ClickUI _clickUI;

        public ActionConfirmMenu(ClickUI clickUI)
        {
            _clickUI = clickUI;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 30 };

            _visuals.Add(new UiImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            });

            _confirmButton = new ImageButton("UI/confirm.png", "UI/confirm-hover.png", "UI/confirm-press.png",
                new Transform2(new Vector2(UI.OfScreenWidth(0.5f) + 30, _menuY + 64), new Size2(52, 52)), () =>
                {;
                    Hide();
                    Event.Publish(new ActionConfirmed());
                }, () => _isReady);
            var cancelButton = new ImageButton("UI/cancel.png", "UI/cancel-hover.png", "UI/cancel-press.png",
                new Transform2(new Vector2(UI.OfScreenWidth(0.5f) - 52 - 30, _menuY + 64), new Size2(52, 52)), 
                () =>
                {
                    Hide();
                    Event.Publish(new ActionCancelled());
                });

            _actionLabel = new Label
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY + 20, _menuWidth, 52)),
                BackgroundColor = Color.Transparent,
                TextColor = UiColors.InGame_Text,
                Font = GuiFonts.Body,
            };

            _visuals.Add(_actionLabel);
            _visuals.Add(_confirmButton);
            _branch.Add(_confirmButton);
            _visuals.Add(cancelButton);
            _branch.Add(cancelButton);
            Event.Subscribe<ActionSelected>(Show, this);
            Event.Subscribe<ActionReadied>(Show, this);
        }

        private void Show(ActionReadied obj)
        {
            Show(true);
        }

        private void Show(ActionSelected a)
        {
            _actionLabel.Text = a.Name;
            Show(false);
        }

        public void Show(bool isReady)
        {
            if (GameWorld.CurrentCharacter.Team.IsIncludedIn(TeamGroup.NeutralsAndEnemies))
                return;

            _confirmButton.IsEnabled = isReady;
            _isReady = isReady;
            _clickUI.Add(_branch);
            _show = true;
        }

        public void Hide()
        {
            _clickUI.Remove(_branch);
            _show = false;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_show)
                return;

            _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
