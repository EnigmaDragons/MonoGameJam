using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class TintedImageBox : IVisual
    {
        public Transform2 Transform { get; set; }
        public string Image { get; set; } = "none";
        public Func<bool> IsActive { get; set; } = () => true;
        public Color Tint { get; set; } = Color.DarkGray;
        public Func<bool> ShouldTint { get; set; } = () => true;
        
        public void Clear()
        {
            Image = "none";
        }

        public void Draw(Transform2 parentTransform)
        {
            if ("none".Equals(Image) || !IsActive())
                return;

            var currentTint = ShouldTint() ? Tint : Color.White;
            World.Draw(Image, parentTransform + Transform, currentTint);
        }
    }
}
