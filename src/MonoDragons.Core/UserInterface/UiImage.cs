using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class UiImage : IVisual
    {
        public Transform2 Transform { get; set; } = Transform2.Zero;
        public string Image { get; set; } = "none";
        public Func<bool> IsActive { get; set; } = () => true;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public Color Tint { get; set; } = Color.White;

        public void Clear()
        {
            Image = "none";
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!"none".Equals(Image) && IsActive())
                UI.DrawWithSpriteEffects(Image, parentTransform + Transform, Tint, Effects);
        }
    }
}
