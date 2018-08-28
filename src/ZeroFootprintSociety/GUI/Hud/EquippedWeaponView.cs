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
    class EquippedWeaponView : ClickableUIElement, IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly UiImage _weaponArt;
        private readonly Label _nameLabel;
        private readonly Label _rangeLabel;

        public EquippedWeaponView(Point position) 
            : base(new Rectangle(position.X, position.Y, 220, 84), true, 1)
        {
            _weaponArt = new UiImage { Transform = new Transform2(new Rectangle(position.X + 60, position.Y + 4, 130, 52)) };
            _nameLabel = new Label
            {
                Font = GuiFonts.Body,
                Transform = new Transform2(new Rectangle(position.X, position.Y + 54, 250, 30)),
                TextColor = UiColors.InGame_Text
            };
            _rangeLabel = new Label
            {
                Font = GuiFonts.Body,
                Transform = new Transform2(new Rectangle(position.X, position.Y + 80, 250, 30)),
                TextColor = UiColors.InGame_Text
            };
            _visuals.Add(new UiImage
            {
                Image = "UI/weapon-panel.png", 
                Tint = 180.Alpha(),
                Transform = new Transform2(new Rectangle(position.X, position.Y, 250, 114))
            });
            _visuals.Add(_weaponArt);
            _visuals.Add(_nameLabel);
            _visuals.Add(_rangeLabel);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (GameWorld.CurrentCharacter.Team.Equals(Team.Friendly))
                _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            var w = GameWorld.CurrentCharacter.Gear.EquippedWeapon;
            _weaponArt.Image = w.Image;
            _nameLabel.Text = w.Name;
            _rangeLabel.Text = w.IsRanged 
                ? $"Rng: {w.AsRanged().Range.ToString()}" 
                : "";
        }

        public override void OnEntered()
        {
        }

        public override void OnExitted()
        {
        }

        public override void OnPressed()
        {
        }

        public override void OnReleased()
        {
        }
    }
}
