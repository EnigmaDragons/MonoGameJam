using System.Collections.Generic;
using Microsoft.Xna.Framework;

using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Layouts
{
    public class Layout : IVisual
    {
        private readonly Dictionary<IVisual, Transform2> _visuals = new Dictionary<IVisual, Transform2>();

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ForEach(x => x.Key.Draw(x.Value + parentTransform));
        }

        public void Add(IVisual visual, Vector2 placement)
        {
            Add(visual, new Transform2(placement));
        }

        public void Add(IVisual visual, Transform2 transform)
        {
            _visuals[visual] = transform;
        }

        public void Remove(IVisual visual)
        {
            _visuals.Remove(visual);
        }

        public void Clear()
        {
            _visuals.Clear();
        }
    }
}
