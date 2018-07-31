using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    // TODO: Remove this after the jam
    [Obsolete("Not actually obsolete. But don't use this unless you are drawing a sprite.")]
    public sealed class WorldImage : IVisual
    {
        public Transform2 Transform { get; set; }
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
                World.DrawWithSpriteEffects(Image, parentTransform + Transform, Tint, Effects);
        }
    }
}
