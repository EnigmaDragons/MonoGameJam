using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.GUI
{
    public class ActionOptionsView : IVisual
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5f.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.76f.VH();
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _branch = new ClickUIBranch("Actions", 2);

        private bool _showingOptions = false;
        private Dictionary<ActionType, Action> _options = new Dictionary<ActionType, Action>();

        public ClickUI _clickUI;

        public ActionOptionsView(ClickUI clickUI)
        {
            _clickUI = clickUI;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 30 };

            var menu = new UiImage
            {
                Transform = new Transform2(new Rectangle(_menuX, _menuY, _menuWidth, _menuHeight)),
                Image = "UI/menu-tall-panel.png"
            };

            var buttons = new List<TextButton>
            {
                Buttons.Text(ctx, 0, "Hide", () => Select(ActionType.Hide), () => _options.ContainsKey(ActionType.Hide)),
                Buttons.Text(ctx, 1, "Shoot", () => Select(ActionType.Shoot), () => _options.ContainsKey(ActionType.Shoot)),
                Buttons.Text(ctx, 2, "Overwatch", () => Select(ActionType.Overwatch), () => _options.ContainsKey(ActionType.Overwatch)),
                Buttons.Text(ctx, 3, "Pass", () => Select(ActionType.Pass), () => _options.ContainsKey(ActionType.Pass))
            };

            _visuals.Add(menu);
            buttons.ForEach(x =>
            {
                _visuals.Add(x);
                _branch.Add(x);
            });

            Event.Subscribe<ActionOptionsAvailable>(UpdateOptions, this);
            Event.Subscribe<ActionSelected>(e => HideDisplay(), this);
            Event.Subscribe<ActionCancelled>(x => PresentOptions(), this);
            Event.Subscribe<ActionConfirmed>(x => _options.Clear(), this);
        }

        private void Select(ActionType actionType)
        {
            _options[actionType].Invoke();
        }

        private void UpdateOptions(ActionOptionsAvailable e)
        {
            _options = e.Options;
            PresentOptions();
        }

        private void PresentOptions()
        {
            if (!GameWorld.CurrentCharacter.Team.IsIncludedIn(TeamGroup.Friendlies))
                return;

            _showingOptions = true;
            _clickUI.Add(_branch);
        }

        private void HideDisplay()
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
