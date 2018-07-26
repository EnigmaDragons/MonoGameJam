using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public abstract class VisualClickableUIElement : ClickableUIElement, IVisual
    {
        public VisualClickableUIElement(Rectangle area, bool isEnabled = true, float scale = 1) 
            : base(area, isEnabled, scale) {}

        public abstract void Draw(Transform2 parentTransform);
    }
}
