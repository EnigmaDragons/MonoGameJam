using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.Development;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Errors;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

namespace MonoDragons.Core.Engine
{
    public class NeedlesslyComplexMainGame : Game
    {
        private readonly string _startingViewName;
        private readonly GraphicsDeviceManager _graphics;
        private readonly IScene _scene;
        private readonly IController _controller;
        private readonly Metrics _metrics;
        private readonly bool _areScreenSettingsPreCalculated;
        private readonly IErrorHandler _errorHandler;
        
        private SpriteBatch _sprites;
        private Display _display;
        private Size2 _defaultScreenSize;
        
        // @todo #1 fix this so we config everything before the game
        public NeedlesslyComplexMainGame(string title, string startingViewName, Size2 defaultGameSize, 
                IScene scene, IController controller, IErrorHandler errorHandler)
            : this(title, startingViewName, scene, controller, errorHandler)
        {
            _areScreenSettingsPreCalculated = false;
            _defaultScreenSize = defaultGameSize;
        }

        public NeedlesslyComplexMainGame(string title, string startingViewName, Display screenSettings, 
                IScene scene, IController controller, IErrorHandler errorHandler)
            : this(title, startingViewName, scene, controller, errorHandler)
        {
            _areScreenSettingsPreCalculated = true;
            _display = screenSettings;
        }

        private NeedlesslyComplexMainGame(string title, string startingViewName,
            IScene scene, IController controller, IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _startingViewName = startingViewName;
            _scene = scene;
            _controller = controller;
#if DEBUG
            _metrics = new Metrics();
#endif
            Window.Title = title;
            ((Form)Form.FromHandle(Window.Handle)).Closing += (o, e) => Environment.Exit(0);
        }

        protected override void Initialize()
        {
            Error.Handle(() =>
            {
                Perf.Time($"{nameof(NeedlesslyComplexMainGame)}.Initialize", () =>
                {
                    CurrentGame.Init(this);
                    Resources.Init();
                    InitDisplayIfNeeded();
                    // @todo #1 Bug: Update the GraphicsDeviceManager in the constructor, to avoid the window being mispositioned and visibly changing size
                    CurrentDisplay.Init(_graphics, _display);
                    Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - CurrentDisplay.GameWidth) / 2, 
                        (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - CurrentDisplay.GameHeight) / 2 - 40); // Delete this once the above issue is fixed
                    IsMouseVisible = true;
                    _sprites = new SpriteBatch(GraphicsDevice);
                    Input.SetController(_controller);
                    World.Init(_sprites);
                    UI.Init(_sprites);
                    _scene.Init();
                    base.Initialize();
                });
            }, x => _errorHandler.ResolveError(x));
        }

        private void InitDisplayIfNeeded()
        {
            if (!_areScreenSettingsPreCalculated)
                _display = new Display(_defaultScreenSize.Width, _defaultScreenSize.Height, true, 1);
        }

        protected override void LoadContent()
        {
            Error.Handle(() =>
            {
                Scene.NavigateTo(_startingViewName);
            }, x => _errorHandler.ResolveError(x));
        }

        protected override void UnloadContent()
        {
            Error.Handle(() =>
            {
                Content.Unload();
            }, x => _errorHandler.ResolveError(x));
        }

        protected override void Update(GameTime gameTime)
        {
            Error.Handle(() =>
            {
#if DEBUG
                _metrics.Update(gameTime.ElapsedGameTime);
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                    Environment.Exit(0);
#endif
                _controller.Update(gameTime.ElapsedGameTime);
                _scene.Update(gameTime.ElapsedGameTime);
            }, x => _errorHandler.ResolveError(x));
        }

        protected override void Draw(GameTime gameTime)
        {
            Error.Handle(() =>
            {
                _sprites.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp);
                GraphicsDevice.Clear(Color.Black);
                _scene.Draw();
#if DEBUG
                _metrics.Draw(Transform2.Zero);
#endif
                _sprites.End();
            }, x => _errorHandler.ResolveError(x));
        }
    }
}
