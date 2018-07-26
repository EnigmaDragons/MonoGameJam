using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Animations
{
    public sealed class WithOffset : IVisual
    {
        private readonly IVisual _visual;
        private readonly Vector2 _offset;

        public WithOffset(IVisual visual, Vector2 offset)
        {
            _visual = visual;
            _offset = offset;
        }

        public void Draw(Transform2 parentTransform)
        {
            _visual.Draw(parentTransform + _offset);
        }
    }
}
