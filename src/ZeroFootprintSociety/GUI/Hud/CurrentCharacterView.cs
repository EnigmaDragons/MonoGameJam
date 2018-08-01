using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    class CurrentCharacterView : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly UiImage _face;
        private readonly Label _label;
        private readonly Label _hp;

        public CurrentCharacterView(Point position)
        {
            _face = new UiImage { Transform = new Transform2(new Rectangle(position.X + 5, position.Y + 8, 100, 100)) };
            _label = new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 75, 170, 30)),
                TextColor = UIColors.InGame_Text
            };
            _hp = new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 44, 170, 30)),
                TextColor = UIColors.InGame_Text
            };
            _visuals.Add(new UiImage
            {
                Image = "UI/weapon-panel.png", Tint = 180.Alpha(),
                Transform = new Transform2(new Rectangle(position.X, position.Y, 250, 114))
            });
            _visuals.Add(_face);
            _visuals.Add(_label);
            _visuals.Add(_hp);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!GameWorld.IsGameOver && GameWorld.CurrentCharacter.Team.Equals(Team.Friendly))
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
