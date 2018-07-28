using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;
using MonoDragons.Core.Development;
using MonoDragons.Core.EngimaDragons;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Errors;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.Scenes;
using System;
using Control = MonoDragons.Core.Inputs.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace ZeroFootPrintSociety
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            LaunchGameWithScene("SampleLevel");
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new OilLogoScene("MainMenu")) },
                { "MainMenu", () => new MainMenuScene() },
                { "SampleLevel", () => new SampleCorporationScene() },
                { "DemoActionUI", () => new DemoActionMenu() },
            });
        }

        private static void LaunchGameWithScene(string sceneName)
        {
            var appDetails = new MetaAppDetails("ZeroFootprintSociety", "0.1", Environment.OSVersion.VersionString);
            var fatalErrorReporter = new ReportErrorHandler(appDetails);
            Metric.AppDetails = appDetails;
            Error.Handle(() =>
            {
                using (var game = Perf.Time("Startup", () => new NeedlesslyComplexMainGame(appDetails.Name, sceneName, new Display(1600, 900, true), SetupScene(), CreateKeyboardController(), fatalErrorReporter)))
                    game.Run();
            }, x => fatalErrorReporter.ResolveError(x));
        }

        private static IScene SetupScene()
        {
            var currentScene = new CurrentScene();
            Scene.Init(new CurrentSceneNavigation(currentScene, CreateSceneFactory(),
                Input.ClearTransientBindings,
                AudioPlayer.Instance.StopAll,
                Resources.Unload));
            return new HideViewportExternals(currentScene);
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.Space, Control.Select },
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }
    }
}
