using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    public class AttackedHealthBar : IVisual
    {
        private readonly ColoredRectangle _background = new ColoredRectangle { Color = UiColors.AttackedHealthBar_Background, Transform = new Transform2(new Size2(250, 25)) };
        private readonly ColoredRectangle _healthRemaining = new ColoredRectangle { Color = UiColors.AttackedHealthBar_HealthRemaining };
        private readonly ColoredRectangle _predictedDamage = new ColoredRectangle { Color = UiColors.AttackedHealthBar_PredictedDamage };

        public void Update(int maxHealth, int health, int damage)
        {
            if (damage > health)
                damage = health;
            _healthRemaining.Transform = new Transform2(new Size2((int)Math.Floor(250 * ((double)health / maxHealth)), 25));
            var damageWidth = (int) Math.Ceiling(250 * ((double) damage / maxHealth));
            _predictedDamage.Transform = new Transform2(new Vector2(_healthRemaining.Transform.Size.Width - damageWidth, 0), new Size2(damageWidth, 25));
        }

        public void Draw(Transform2 parentTransform)
        {
            _background.Draw(parentTransform);
            _healthRemaining.Draw(parentTransform);
            _predictedDamage.Draw(parentTransform);
        }
    }
}
