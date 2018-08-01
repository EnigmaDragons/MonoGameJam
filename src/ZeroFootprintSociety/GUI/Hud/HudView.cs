using Microsoft.Xna.Framework;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI.Hud;

namespace ZeroFootPrintSociety.GUI
{
    class HudView : SceneContainer
    {
        public HudView(ClickUI clickUi) : base(true)
        {
            Add(new InGameMenu(clickUi));
            Add(new ActionConfirmMenu(clickUi));
            Add(new EquippedWeaponView(new Point(UI.OfScreenWidth(0.85f), UI.OfScreenHeight(0.89f))));
            Add(new CurrentCharacterView(new Point(UI.OfScreenWidth(0.01f), UI.OfScreenHeight(0.86f))));
            Add(new FootstepsCounterView((new Point(UI.OfScreenWidth(0.82f), UI.OfScreenHeight(0.054f)))));
            Add(new AttackPreview());
            Add(new TeamTurnHudDecor());
            Add(new ActionOptionsMenu(clickUi));
            Add(new GameOverMenu(clickUi));
        }
    }
}
