using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using MonoDragons.Core;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    class CurrentCharacterView : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly UiImage _face;
        private readonly Label _name;
        private readonly Label _hp;
        private readonly Label _level;

        public CurrentCharacterView(Point position)
        {         
            _visuals.Add(new UiImage
            {
                Image = "UI/weapon-panel.png", Tint = 180.Alpha(),
                Transform = new Transform2(new Rectangle(position.X, position.Y, 250, 114))
            });
            _face = _visuals.Added(new UiImage
            {
                Transform = new Transform2(new Rectangle(position.X + 5, position.Y + 8, 100, 100))
            });  
            _name = _visuals.Added(new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 75, 170, 30)),
                TextColor = UiColors.InGame_Text
            });
            _hp = _visuals.Added(new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 44, 170, 30)),
                TextColor = UiColors.InGame_Text
            });
            _level = _visuals.Added(new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 10, 170, 30)),
                TextColor = UiColors.InGame_Text
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            if (GameWorld.CurrentCharacter.Team.Equals(Team.Friendly))
                _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            var c = GameWorld.CurrentCharacter;
            _face.Image = c.FaceImage;
            _name.Text = c.Stats.Name;
            _hp.Text = $"HP: {c.State.RemainingHealth.PadLeft(3, '0')} / {c.Stats.HP.PadLeft(3, '0')}";
            _level.Text = $"LVL: {c.Level.PadLeft(2, '0')} XP: {c.Xp.PadLeft(2, '0')}";
        }
    }
}
