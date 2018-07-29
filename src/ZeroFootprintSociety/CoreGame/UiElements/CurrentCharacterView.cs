using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class CurrentCharacterView : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly UiImage _face;
        private readonly Label _label;
        private readonly Label _hp;

        public CurrentCharacterView(Point position)
        {
            _face = new UiImage { Transform = new Transform2(new Rectangle(position.X + 10, position.Y + 12, 70, 70)) };
            _label = new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X, position.Y + 78, 220, 30)),
                TextColor = Color.FromNonPremultiplied(255, 255, 255, 180)
            };
            _hp = new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 70, position.Y + 40, 150, 30)),
                TextColor = Color.FromNonPremultiplied(255, 255, 255, 180)
            };
            _visuals.Add(new UiImage { Image = "UI/weapon-panel.png", Alpha = 180, Transform = new Transform2(new Rectangle(position.X, position.Y, 220, 114)) });
            _visuals.Add(_face);
            _visuals.Add(_label);
            _visuals.Add(_hp);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            var c = GameWorld.CurrentCharacter;
            _face.Image = c.FaceImage;
            _label.Text = c.Stats.Name;
            _hp.Text = $"HP: {c.State.RemainingHealth} / {c.Stats.HP}";
        }
    }
}
