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
        static MetaAppDetails AppMeta = new MetaAppDetails("ZeroFootprintSociety", "0.1", Environment.OSVersion.VersionString);
        static ReportErrorHandler FatalErrorHandler = new ReportErrorHandler(AppMeta);

        [STAThread]
        static void Main()
        {
            RunGame("CharacterCreation");
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Logo", () => new FadingInScene(new OilLogoScene("MainMenu")) },
                { "MainMenu", () => new MainMenuScene() },
                { "SampleLevel", () => new SampleCorporationScene() },
                { "Level1", () => new CorporationOutsideScene() },
                { "ShootingRange", () => new ShootingRange() },
                { "Credits", () => new CreditsScene() },
                { "CharacterCreation", () => new CharacterCreation() },
            });
        }

        private static void RunGame(string sceneName)
        {
            try
            {
                using (var game = Perf.Time("Startup", () => new NeedlesslyComplexMainGame(AppMeta.Name, sceneName, new Display(1600, 900, false), SetupScene(), CreateKeyboardController(), FatalErrorHandler)))
                    game.Run();
            }
            catch(Exception e)
            {
                FatalErrorHandler.ResolveError(e);
            }
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
                { Keys.Escape, Control.Menu },
                { Keys.V, Control.A },
                { Keys.O, Control.X }
            });
        }
    }
}
