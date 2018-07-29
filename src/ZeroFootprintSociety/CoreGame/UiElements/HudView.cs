using Microsoft.Xna.Framework;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class HudView : SceneContainer
    {
        private readonly ClickUI _clickUI = new ClickUI();

        public HudView() : base(true)
        {
            Add(_clickUI);
            Add(new ActionOptionsView(_clickUI));
            Add(new InGameMenu(_clickUI));
            Add(new EquippedWeaponView(new Point(UI.OfScreenWidth(0.85f), UI.OfScreenHeight(0.89f))));
            Add(new CurrentCharacterView(new Point(UI.OfScreenWidth(0.01f), UI.OfScreenHeight(0.86f))));
            Add(new AttackPreview(_clickUI));
        }
    }
}
