using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AttackedHealthBar : IVisual
    {
        private readonly ColoredRectangle Background = new ColoredRectangle { Color = Color.FromNonPremultiplied(66, 66, 66, 255), Transform = new Transform2(new Size2(250, 30)) };
        private readonly ColoredRectangle HealthRemaining = new ColoredRectangle { Color = Color.FromNonPremultiplied(0, 200, 83, 255) };
        private readonly ColoredRectangle PredictedDamage = new ColoredRectangle { Color = Color.FromNonPremultiplied(213, 0, 0, 255) };

        public void Update(int maxHealth, int health, int damage)
        {
            if (damage > health)
                damage = health;
            HealthRemaining.Transform = new Transform2(new Size2((int)Math.Floor(250 * ((double)health / maxHealth)), 30));
            var damageWidth = (int) Math.Ceiling(250 * ((double) damage / maxHealth));
            PredictedDamage.Transform = new Transform2(new Vector2(HealthRemaining.Transform.Size.Width - damageWidth, 0), new Size2(damageWidth, 30));
        }

        public void Draw(Transform2 parentTransform)
        {
            Background.Draw(parentTransform);
            HealthRemaining.Draw(parentTransform);
            PredictedDamage.Draw(parentTransform);
        }
    }
}
