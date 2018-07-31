using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.GUI
{
    public class OptionUI : VisualClickableUIElement
    {
        private readonly UiImage _image;
        private readonly Label _optionName;
        private readonly Vector2 _offset;
        private readonly Action _onClick;
        private readonly List<IVisual> _visuals;

        public OptionUI(string name, Vector2 offset, Action onClick, params IVisual[] visuals) : base(new Rectangle((int)offset.X, (int)offset.Y, 350, 600))
        {
            _image = new UiImage { Image = "UI/menu-tall-panel.png", Transform = new Transform2(Area), Tint = 127.Alpha() };
            _optionName = new Label { Text = name, Transform = new Transform2(new Vector2(25 + Area.X, 25 + Area.Y), new Size2(300, 50))} ;
            _offset = offset;
            _onClick = onClick;
            _visuals = visuals.ToList();
        }

        public override void OnEntered()
        {
            _image.Tint = 191.Alpha();
        }

        public override void OnExitted()
        {
            _image.Tint = 127.Alpha();
        }

        public override void OnPressed()
        {
            _image.Tint = 255.Alpha();
        }

        public override void OnReleased()
        {
            _onClick();
        }

        public override void Draw(Transform2 parentTransform)
        {
            _image.Draw(parentTransform);
            _optionName.Draw(parentTransform);
            _visuals.ForEach(x => x.Draw(parentTransform + _offset));
        }
    }
}
