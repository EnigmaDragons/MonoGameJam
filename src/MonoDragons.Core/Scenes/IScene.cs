using System;

namespace MonoDragons.Core.Scenes
{
    public interface IScene: IDisposable
    {
        void Init();
        void Update(TimeSpan delta);
        void Draw();
    }
}
