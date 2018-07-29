using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class MoveConfirmView : IVisual
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.85.VH();
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);

        private bool _show = false;

        public ClickUI _clickUI;

        public MoveConfirmView(ClickUI clickUI)
        {
            _clickUI = clickUI;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 30 };

            var menu = new UiImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            };

            var confirmButton = new ImageButton("UI/confirm.png", "UI/confirm-hover.png", "UI/confirm-press.png",
                new Transform2(new Vector2(UI.OfScreenWidth(0.5f) + 30, _menuY + 55), new Size2(52, 52)), () => Event.Publish(new ActionConfirmed()));
            var cancelButton = new ImageButton("UI/cancel.png", "UI/cancel-hover.png", "UI/cancel-press.png",
                new Transform2(new Vector2(UI.OfScreenWidth(0.5f) - 52 - 30, _menuY + 55), new Size2(52, 52)), () => Event.Publish(new ActionCancelled()));

            _visuals.Add(menu);
            _visuals.Add(confirmButton);
            _branch.Add(confirmButton);
            _visuals.Add(cancelButton);
            _branch.Add(cancelButton);
            Event.Subscribe<ActionSelected>(e => Show(), this);
            Event.Subscribe<ActionCancelled>(e => Hide(), this);
            Event.Subscribe<ActionConfirmed>(e => Hide(), this);
        }

        public void Show()
        {
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
