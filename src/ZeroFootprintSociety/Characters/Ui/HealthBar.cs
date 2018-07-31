using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Size2 = MonoDragons.Core.PhysicsEngine.Size2;

namespace ZeroFootPrintSociety.Characters.Ui
{
    class HealthBar : IVisual
    {
        private const int HEIGHT = 4;

        public int MaxWidth;

        public ColoredRectangle DamageRectangle;
        public ColoredRectangle HealthRectangle;

        public HealthBar(int maxWidth)
        {
            MaxWidth = maxWidth;
            HealthRectangle = new ColoredRectangle() {Color = UIColors.HealthBar_Health, Transform = _MakeHealthTransform()};
            DamageRectangle = new ColoredRectangle() {Color = UIColors.HealthBar_Damage, Transform = _MakeDamageTranform()};
        }

        public void Init()
        {
            HealthRectangle.Transform = _MakeHealthTransform();
            DamageRectangle.Transform = _MakeDamageTranform();
        }

        private Transform2 _MakeHealthTransform(int? width = null)
            => new Transform2(new Size2(width ?? MaxWidth, HEIGHT));

        private Transform2 _MakeDamageTranform(int? damagePos = null, int damageWidth = 0)
            => new Transform2(new Rectangle(damagePos ?? MaxWidth, 0, damageWidth, HEIGHT));

        public void Update(float percentLeft)
        {
            int healthWidth = (int) Math.Floor(MaxWidth * percentLeft),
                damageWidth = (int) Math.Floor(MaxWidth * (Math.Abs(percentLeft - 1.0f)));

            HealthRectangle.Transform = _MakeHealthTransform(healthWidth);
            DamageRectangle.Transform = _MakeDamageTranform(healthWidth, damageWidth);
        }

        public void Draw(Transform2 parentTransform)
        {
            HealthRectangle.Draw(parentTransform);
            DamageRectangle.Draw(parentTransform);
        }
    }
}