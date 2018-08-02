using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI.Menus
{
    public class SwitchWeaponsMenu : IVisual
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.90.VH();
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("SwitchWeapons", 2);
        private readonly Label _titleLabel;
        private readonly TextButton _switchWeaponsButton;
        private readonly ClickUI _clickUI;

        private bool _show = false;

        public SwitchWeaponsMenu(ClickUI clickUI)
        {
            _clickUI = clickUI;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 40 };
            var menu = new UiImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            };
            _switchWeaponsButton = Buttons.Text(ctx, 0, "Switch Weapons", () => GameWorld.CurrentCharacter.Gear.SwitchWeapons(), () => _show);
            _branch.Add(_switchWeaponsButton);
            _visuals.Add(menu);
            _visuals.Add(_switchWeaponsButton);
            Event.Subscribe<TurnBegun>(_ => Show(), this);
            Event.Subscribe<MovementConfirmed>(_ => Hide(), this);
        }

        private void Show()
        {
            if (GameWorld.CurrentCharacter.Team.IsIncludedIn(TeamGroup.NeutralsAndEnemies))
                return;
            _clickUI.Add(_branch);
            _show = true;
        }

        public void Hide()
        {
            if (GameWorld.CurrentCharacter.Team.IsIncludedIn(TeamGroup.NeutralsAndEnemies))
                return;
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
