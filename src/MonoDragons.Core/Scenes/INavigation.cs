
namespace MonoDragons.Core.Scenes
{
    public interface INavigation
    {
        void NavigateTo(string sceneName);
        void NavigateTo(IScene scene);
    }
}
