using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Engine
{
    public interface IVisual
    {
        void Draw(Transform2 parentTransform);
    }

    public static class VisualExtensions
    {
        public static void Draw(this IVisual visual)
        {
            visual.Draw(Transform2.Zero);
        }
    }
}
