using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.GUI.Hud
{
    public class GameDialog : IVisualAutomaton
    {
        private readonly UiImage _dialogBox;
        private readonly ChatBox _chatBox;
        private readonly UiImage _faceImage;
        private int _index;
        private readonly List<Dialog> _dialogs;
        public bool IsDone { get; private set; }
        public ClickUIBranch Branch { get; } = new ClickUIBranch("Dialog", 10);

        public GameDialog(List<Dialog> dialogs)
        {
            _dialogs = dialogs;
            _dialogBox = new UiImage
            {
                Image = "UI/weapon-panel.png",
                Transform = new Transform2(new Rectangle(350, 200, 900, 300))
            };
            _chatBox = new ChatBox(_dialogs[_index].Message, 400, GuiFonts.BodySpriteFont, 40, 40) { Position = new Vector2(625, 225) };
            _faceImage = new UiImage { Image = _dialogs[_index].CharacterImage, Transform = new Transform2(new Rectangle(375, 225, 250, 250))};
            Input.On(Control.Start, Advance);
            Branch.Add(new ScreenClickable(Advance));
        }

        public void Update(TimeSpan delta)
        {
            _chatBox.Update(delta);
            _faceImage.Image = _dialogs[_index].CharacterImage;
        }

        public void Draw(Transform2 parentTransform)
        {
            _dialogBox.Draw(parentTransform);
            _chatBox.Draw(parentTransform);
            _faceImage.Draw(parentTransform);
        }

        private void Advance()
        {
            if (_index == _dialogs.Count - 1 && _chatBox.IsMessageCompletelyDisplayed())
                IsDone = true;
            else if (_chatBox.IsMessageCompletelyDisplayed())
                _chatBox.ShowMessage(_dialogs[++_index].Message);
            else
                _chatBox.CompletelyDisplayMessage();
        }
    }

    public class Dialog
    {
        public string Message { get; set; }
        public string CharacterImage { get; set; }
    }
}
