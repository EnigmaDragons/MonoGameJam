using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class EquippedWeaponView : ClickableUIElement, IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ImageBox _weaponArt;
        private readonly Label _label;

        public EquippedWeaponView(Point position) 
            : base(new Rectangle(position.X, position.Y, 220, 84), true, 1)
        {
            _weaponArt = new ImageBox { Transform = new Transform2(new Rectangle(position.X + 40, position.Y + 8, 150, 42)) };
            _label = new Label
            {
                Font = "Fonts/12",
                Transform = new Transform2(new Rectangle(position.X, position.Y + 40, 220, 60)),
                TextColor = Color.FromNonPremultiplied(255, 255, 255, 180)
            };
            _visuals.Add(new ImageBox { Image = "UI/weapon-panel.png", Alpha = 180, Transform = new Transform2(new Rectangle(position.X, position.Y, 220, 84)) });
            _visuals.Add(_weaponArt);
            _visuals.Add(_label);
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            _weaponArt.Image = GameWorld.CurrentCharacter.Gear.EquippedWeapon.Image;
            _label.Text = GameWorld.CurrentCharacter.Gear.EquippedWeapon.Name;
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
