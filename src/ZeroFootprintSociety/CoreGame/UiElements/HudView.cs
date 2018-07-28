using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.UiElements.UiEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class HudView : SceneContainer
    {
        private readonly ClickUI _clickUI = new ClickUI();

        public HudView() : base(true)
        {
            Add(_clickUI);
            Add(new ActionOptionsView(_clickUI));
            var menuButton = new ExpandingImageButton("UI/placeholder-menu-button.png", "UI/placeholder-menu-button.png", "UI/placeholder-menu-button.png",
                    new Transform2(new Vector2(UI.OfScreenWidth(0.87f), UI.OfScreenHeight(0.07f)), new Size2(64, 64)),
                    new Size2(16, 16),
                    () => Event.Publish(new MenuRequested()));
            _clickUI.Add(menuButton);
            Add(menuButton);
            Add(new InGameMenu(_clickUI));
            Add(new EquippedWeaponView(new Point(UI.OfScreenWidth(0.03f), UI.OfScreenHeight(0.86f))));
            Add(new CurrentCharacterView(new Point(UI.OfScreenWidth(0.03f), UI.OfScreenHeight(0.72f))));
            Add(new AttackPreview(_clickUI));
        }
    }
}
