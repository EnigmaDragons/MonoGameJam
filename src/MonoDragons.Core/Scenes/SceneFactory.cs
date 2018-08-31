using System;

namespace MonoDragons.Core.Scenes
{
    public sealed class SceneFactory
    {
        private readonly Map<string, Func<IScene>> _sceneInstructions;

        public SceneFactory(Map<string, Func<IScene>> sceneInstructions)
        {
            _sceneInstructions = sceneInstructions;
        }

        public IScene Create(string sceneName)
        {
            return _sceneInstructions[sceneName]();
        }
    }
}
