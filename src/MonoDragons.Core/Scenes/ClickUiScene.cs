using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Scenes
{
    public abstract class ClickUiScene : SceneContainer, IScene
    {
        protected ClickUI _clickUi = new ClickUI();

        public abstract void Init();
        public abstract void Dispose();

        public void Draw() => Draw(Transform2.Zero);
    }
}
