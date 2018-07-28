using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;
using Control = MonoDragons.Core.Inputs.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MonoDragons.Core.Engine;
using MonoDragons.Core.AudioSystem;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class MainMenuScene : IScene
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private ClickUI _clickUi = new ClickUI();
        
        public void Init()
        {
            Input.ClearTransientBindings();
            Sound.Music("placeholder-main-theme").Play();
            _visuals.Add(new ImageBox { Image = "Backgrounds/mainmenu-bg", Transform = new Transform2(new Size2(1920, 1080)) });
            _visuals.Add(new ColoredRectangle { Color = Color.FromNonPremultiplied(0, 0, 0, 100), Transform = new Transform2(new Size2(1920, 1080)) });
            _visuals.Add(new ImageBox { Image = "UI/title-placeholder", Transform = new Transform2(new Vector2(UI.OfScreenWidth(0.5f)-452, 180), new Size2(904, 313)) });
            var button = new TextButton(new Rectangle(UI.OfScreenWidth(0.5f)-150, 700, 300, 50), 
                () => 
                {
                    GameWorld.Clear();
                    Scene.NavigateTo("SampleLevel");
                }, "New Game", Color.Transparent, Color.LightBlue, Color.Blue);
            _clickUi.Add(button);
            _visuals.Add(button);

            // TODO: Make inputs react to menu choice.
            // TODO: Have cursor activity override keyboard selection (with delay between switch of input type).

            Input.SetController(new KeyboardController(new Map<Keys, Control>()
            {
                { Keys.Enter, Control.A }, // Confirm
                { Keys.Escape, Control.B }, // Exit or Back
            }));
        }
    
        public void Update(TimeSpan delta)
        {
            _clickUi.Update(delta);
        }

        public void Draw()
        {
            _visuals.ForEach(x => x.Draw());            
        }

        public void Dispose()
        {
        }
    }
}
