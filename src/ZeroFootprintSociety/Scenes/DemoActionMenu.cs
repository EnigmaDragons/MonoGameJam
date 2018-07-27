using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;

namespace ZeroFootPrintSociety.Scenes
{
    public class DemoActionMenu : IScene
    {
        private ClickUI _clickUI;
        private ActionUI _actionUI;
        private TextButton _button;

        public void Init()
        {
            _clickUI = new ClickUI();
            _actionUI = new ActionUI(_clickUI);
            _button = new TextButton(new Rectangle(50, 50, 150, 50), () => Event.Publish(new CharacterMoved()), "Move Character",
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));
            _clickUI.Add(_button);
        }

        public void Update(TimeSpan delta)
        {
            _clickUI.Update(delta);
        }

        public void Draw()
        {
            _button.Draw(Transform2.Zero);
            _actionUI.Draw(Transform2.Zero);
        }

        public void Dispose() {}
    }
}
