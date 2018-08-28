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
using System.Windows.Forms;
using ZeroFootPrintSociety.Soundtrack;
using Control = MonoDragons.Core.Inputs.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace ZeroFootPrintSociety
{
    public static class Program
    {
        static MetaAppDetails AppMeta = new MetaAppDetails("ZeroFootprintSociety", "0.1", Environment.OSVersion.VersionString);
#if DEBUG
        static IErrorHandler FatalErrorHandler = new MessageBoxErrorHandler();
#else
        static IErrorHandler FatalErrorHandler = new ReportErrorHandler(AppMeta);
#endif

        [STAThread]
        static void Main()
        {
#if DEBUG
            RunGame("DarkAlley");
#else
            RunGame("Logo");
#endif
        }

        private static SceneFactory CreateSceneFactory()
        {
            return new SceneFactory(new Map<string, Func<IScene>>
            {
                { "Credits", () => new CreditsScene() },
                { "Logo", () => new LogoScene("MainMenu") },
                { "MainMenu", () => new MainMenuScene("Intro") },
                { "Intro", () => new IntroCutscene("CharacterCreation") },
                { "CharacterCreation", () => new CharacterCreationScene("DarkAlley") },
                { "SampleLevel", () => new GameLevel("SampleCorporate.tmx") },
                { "ShootingRange", () => new GameLevel("TestFogOfWar.tmx") },
                { "DarkAlley", () => new GameLevel("DarkAlley.tmx", new LevelMusic("alley-amb", "alley-action-loop", "alley-action-loop", 0.6f, 0.34f, 0.34f)) },
                { "SpawnTest", () => new GameLevel("SpawnTest.tmx") },
                { "FinalFloor", () => new GameLevel("FinalFloor.tmx") },
            });
        }

        private static void RunGame(string sceneName)
        {
            try
            {
                using (var game = Perf.Time("Startup", () => new NeedlesslyComplexMainGame(AppMeta.Name, sceneName, 
                    new Display(1600, 900, false), SetupScene(), CreateKeyboardController(), FatalErrorHandler)))
                        game.Run();
            }
            catch(Exception e)
            {
                FatalErrorHandler.ResolveError(e);
                MessageBox.Show($"Your game has crashed, probably due to hacking by ZantoCorp.\n\n" +
                                                     $"The Fatal Error has been automatically reported.\n\n" +
                                                     $"Thank you for helping us with Quality Assurance!\n\n" +
                                                     $"Credits have been automatically deposited into your bank account.\n\n" +
                                                     $"Error: '{e.Message}'\n" +
                                                     $"StackTrace: {e.StackTrace}");
            }
        }

        private static IScene SetupScene()
        {
            var currentScene = new CurrentScene();
                Scene.Init(new CurrentSceneNavigation(currentScene, CreateSceneFactory(),
                    Input.ClearTransientBindings,
                    Resources.Unload, () => AudioPlayer.Instance.StopAll()));
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
