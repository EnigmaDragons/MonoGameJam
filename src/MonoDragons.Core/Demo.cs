using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
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

namespace MonoDragons.Core
{
    public static class Demo
    {
        [STAThread]
        static void Main()
        {
            var appDetails = new MetaAppDetails("MonoDragons.Core", "1.0", Environment.OSVersion.VersionString);
            var fatalErrorReporter = new ReportErrorHandler(appDetails);
            Metric.AppDetails = appDetails;
            Error.Handle(() =>
            {
                using (var game = Perf.Time("Startup", () => new NeedlesslyComplexMainGame(appDetails.Name, "Logo", new Display(1600, 900, false), SetupScene(), CreateKeyboardController(), fatalErrorReporter)))
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

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new OilLogoScene()) },
                { "Intro", () => new VolumeDemo() },
            });
        }

        private static IController CreateKeyboardController()
        {
            return new KeyboardController(new Map<Keys, Control>
            {
                { Keys.OemTilde, Control.Select },
                { Keys.Enter, Control.Start },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }
    }
}
