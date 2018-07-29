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
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);

        private bool _showingOptions = false;
        private bool _shootAvailable = false;

        public ClickUI _clickUI;

        public ActionOptionsView(ClickUI clickUI)
        {
            _clickUI = clickUI;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 30 };

            var menu = new WorldImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            };

            var hideButton = Buttons.Text(ctx, 0, "Hide", () => Select(new HideChosen()), () => true);
            var shootButton = Buttons.Text(ctx, 1, "Shoot", () => Select(new ShootSelected()), () => _shootAvailable);
            var overwatchButton = Buttons.Text(ctx, 2, "Overwatch", () => Select(new OverwatchSelected()), 
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
