using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface
{
    public class SmartControl : IVisualControl
    {
        private IVisual _visual;

        public ClickUIBranch Branch { get; }

        public SmartControl(VisualClickableUIElement element, int priority)
        {
            Branch = new ClickUIBranch("Smart Control", priority);
            Branch.Add(element);
            _visual = element;
        }

        public void Draw(Transform2 parentTransform)
        {
            Branch.ParentLocation = parentTransform.Location;
            _visual.Draw(parentTransform);
        }
    }
}
