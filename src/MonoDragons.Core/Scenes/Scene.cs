using MonoDragons.Core.Common;

namespace MonoDragons.Core.Scenes
{
    public static class Scene
    {
        private static readonly MustInit<INavigation> Navigation = new MustInit<INavigation>(nameof(Scene));

        public static void Init(INavigation navigation)
        {
            Navigation.Init(navigation);
        }

        public static void NavigateTo(string sceneName)
        {
            Navigation.Get().NavigateTo(sceneName);
        }

        public static void NavigateTo(IScene scene)
        {
            Navigation.Get().NavigateTo(scene);
        }
    }
}
