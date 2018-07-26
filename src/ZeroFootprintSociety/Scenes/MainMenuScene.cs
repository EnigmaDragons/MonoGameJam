using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using MonoDragons.Core.UserInterface.Layouts;
using MonoDragons.Core.Common;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using Control = MonoDragons.Core.Inputs.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using MonoDragons.Core.Engine;

namespace ZeroFootPrintSociety.Scenes
{
    public class MainMenuScene : IScene
    {
        private ClickUI _clickUi;
        private GridLayout _gridLayout; // TODO: Implement a grid layout.

        private ColoredRectangle _backdropRect;

        public void Init()
        {
            _clickUi = new ClickUI();
            Input.ClearTransientBindings();

            // TODO: Make inputs react to menu choice.
            // TODO: Have cursor activity override keyboard selection (with delay between switch of input type).

            Input.SetController(new KeyboardController(new Map<Keys, Control>()
            {
                { Keys.Enter, Control.A }, // Confirm
                { Keys.Escape, Control.B }, // Exit or Back
            }));

            // REMOVE_ME: This is for testing stuff.
            _gridLayout = new GridLayout(new Size2(640, 480), 1, 5);
            _gridLayout.Add(new TextButton(new Rectangle(Point.Zero, _gridLayout.GetBlockSize(1, 1).ToPoint()), () => { }, "Text", Color.Gray, Color.LightGray, Color.DarkGray), 0, 0);
            _gridLayout.Add(new TextButton(new Rectangle(Point.Zero, _gridLayout.GetBlockSize(1, 1).ToPoint()), () => { }, "Another Text", Color.Gray, Color.LightGray, Color.DarkGray), 0, 1);
            
            _backdropRect = new ColoredRectangle() {Color = Color.Blue, Transform = new Transform2(_gridLayout.Size)};
            // END_REMOVE_ME;
        }

        public void Update(TimeSpan delta)
        {

        }

        public void Draw()
        {
            var pos = new Transform2(new Vector2(
                (CurrentDisplay.GameWidth / 2) - (_gridLayout.Size.Width / 2),
                (CurrentDisplay.GameHeight) - (_gridLayout.Size.Height)
            ));
            // REMOVE_ME: This is for testing stuff.   
            _backdropRect.Draw(pos);
            _gridLayout.Draw(pos);
            // END_REMOVE_ME;
        }

        public void Dispose()
        {
        }
    }
}
