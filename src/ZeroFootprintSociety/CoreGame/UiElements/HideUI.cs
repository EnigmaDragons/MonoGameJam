using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class HideUI : IVisual
    {
        private readonly UiImage _hideBonusImage = new UiImage { Image = "UI/shield-placeholder" };
        private readonly Label _hideBonusLabel = new Label { Text = "+100%", TextColor = Color.White };

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
            GameWorld.Highlights.Add(this);
        }

        private void Hide()
        {
            if (GameWorld.Highlights.Contains(this))
                GameWorld.Highlights.Remove(this);
        }

        public void Draw(Transform2 parentTransform)
        {
            _hideBonusImage.Draw(parentTransform);
            _hideBonusLabel.Draw(parentTransform);
        }
    }
}
