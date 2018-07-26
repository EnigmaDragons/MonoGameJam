using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.Memory
{
    public static class Resources
    {
        private static SceneContents _sceneContents;

        public static int CurrentSceneResourceCount => _sceneContents?.ContentCount ?? 0;

        public static void Init()
        {
            _sceneContents = new SceneContents(CurrentGame.ContentManager);
        }

        public static void Put(string toString, IDisposable disposable)
        {
            _sceneContents.Put(disposable);
        }

        public static T Load<T>(string resourceName)
        {
            return _sceneContents.Load<T>(resourceName);
        }

        public static void Unload()
        {
            _sceneContents.Dispose();
            _sceneContents = new SceneContents(CurrentGame.ContentManager);
            DefaultFont.Load(CurrentGame.ContentManager);
        }

        public static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                _sceneContents.Dispose(disposable);
        }

        public static void NotifyDisposed(IDisposable disposable)
        {
            if (disposable != null)
                _sceneContents.NotifyDisposed(disposable);
        }
    }
}
