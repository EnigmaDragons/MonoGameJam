
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI.Hud
{
    public class FootstepsCounterView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private int _currentCount => GameWorld.FootstepsRemaining;

        public UiImage GiantFootstepImage;
        public Label StepsDisplay;

        public FootstepsCounterView(Point position)
        {
            GiantFootstepImage = new UiImage
            {
                Image = "UI/giant-footstep.png",
                IsActive = () => !GameWorld.IsGameOver,
                Transform = new Transform2(new Rectangle(position.X, position.Y, 240, 120)),
                Tint =  Color.White,
            };
            _visuals.Add(GiantFootstepImage);
            StepsDisplay = new Label()
            {
                Font = "Fonts/18",
                TextColor = DisplayTextColor(),
                Text = _currentCount.ToString("D2"),
                Transform = new Transform2(new Rectangle(position.X + 300, position.Y + 60, 180, 50))
            };
            _visuals.Add(StepsDisplay);

            Event.Subscribe(EventSubscription.Create<FootstepCounted>(OnFootstepCounted, this));
        }

        public Color DisplayTextColor(int r = 255) => UIColors.FootstepsCounterView_Text(r);

        private void OnFootstepCounted(FootstepCounted e)
        {
            StepsDisplay.Text = e.Steps.ToString("D2");
            StepsDisplay.TextColor = DisplayTextColor(); // TODO: Change color based on percent of steps left.
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!GameWorld.IsGameOver && GameWorld.CurrentCharacter is MainChar)
             _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
