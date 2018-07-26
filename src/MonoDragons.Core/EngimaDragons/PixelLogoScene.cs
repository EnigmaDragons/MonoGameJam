using System;
using System.Threading.Tasks;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.EngimaDragons
{
    public sealed class PixelLogoScene : IScene
    {
        private readonly string _nextScene;

        private bool _begunTransition;
        private bool _transitionComplete;

        public PixelLogoScene(string nextScene = "Intro")
        {
            _nextScene = nextScene;
        }

        public void Init()
        {
            Input.ClearTransientBindings();
            Input.On(Control.Start, NavigateToMainMenu);
        }

        private void NavigateToMainMenu()
        {
            if (_transitionComplete)
                return;

            _transitionComplete = true;
            Scene.NavigateTo(_nextScene);
        }

        public async void Update(TimeSpan delta)
        {
            if (_begunTransition)
                return;

            _begunTransition = true;
            await Task.Delay(3000);
            NavigateToMainMenu();
        }

        public void Draw()
        {
            UI.DrawCentered("Images/Logo/enigmadragons-presents");
        }

        public void Dispose()
        {
        }
    }
}
