using System;
using System.Collections.Generic;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Scenes
{
    public sealed class CurrentSceneNavigation : INavigation
    {
        private readonly CurrentScene _currentScene;
        private readonly SceneFactory _sceneFactory;
        private readonly IEnumerable<Action> _beforeNavigate;

        public CurrentSceneNavigation(CurrentScene currentScene, SceneFactory sceneFactory, params Action[] beforeNavigate)
        {
            _currentScene = currentScene;
            _sceneFactory = sceneFactory;
            _beforeNavigate = beforeNavigate;
        }

        public void NavigateTo(string sceneName)
        {
            NavigateTo(_sceneFactory.Create(sceneName));
        }

        public void NavigateTo(IScene scene)
        {
            _beforeNavigate.ForEach(x => x());
            _currentScene.Dispose();
            scene.Init();
            _currentScene.Set(scene);
        }
    }
}
