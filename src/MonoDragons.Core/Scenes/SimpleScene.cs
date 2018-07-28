using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Scenes
{
    public abstract class SimpleScene : SceneContainer, IScene
    {
        public abstract void Init();
        public abstract void Dispose();

        public void Draw() => Draw(Transform2.Zero);
    }
}
