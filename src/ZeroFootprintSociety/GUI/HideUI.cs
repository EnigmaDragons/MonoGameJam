using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    public class HideUI : IVisual
    {
        private readonly UiImage _hideBonusImage = new UiImage { Image = "UI/shield-placeholder-1" };
        private readonly Label _hideBonusLabel = new Label { Text = "+100%", TextColor = UIColors.InGame_Text };
        private bool _hidden = true;

        public HideUI()
        {
            Event.Subscribe<HideSelected>(Show, this);
            Event.Subscribe<ActionCancelled>(_ => Hide(), this);
            Event.Subscribe<ActionConfirmed>(_ => Hide(), this);
        }

        private void Show(HideSelected e)
        {
            _hideBonusImage.Transform = GameWorld.CurrentCharacter.CurrentTile.Transform;
            _hideBonusLabel.Transform = GameWorld.CurrentCharacter.CurrentTile.Transform;
            _hidden = false;
        }

        private void Hide()
        {
            _hidden = true;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_hidden)
                return;
            _hideBonusImage.Draw(parentTransform);
            _hideBonusLabel.Draw(parentTransform);
        }
    }
}
