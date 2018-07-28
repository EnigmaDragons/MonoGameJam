using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class ImageBox : IVisual
    {
        public Transform2 Transform { get; set; }
        public string Image { get; set; } = "none";
        public Func<bool> IsActive { get; set; } = () => true;
        public int Alpha { get; set; } = 255;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        
        public void Clear()
        {
            Image = "none";
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!"none".Equals(Image) && IsActive())
                World.DrawWithSpriteEffects(Image, parentTransform + Transform, Color.FromNonPremultiplied(255, 255, 255, Alpha), Effects);
        }
    }
}
