
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI.Hud
{
    public class FootstepsCounterView : IVisual
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();

        private readonly Label _stepsLabel;

        public FootstepsCounterView(Point position)
        {
            
            _visuals.Add(new UiImage
            {
                Image = "UI/weapon-panel.png", Tint = 180.Alpha(),
                Transform = new Transform2(new Rectangle(position.X, position.Y, 240, 100))
            });
            _visuals.Add(new UiImage
            {
                Image = "UI/giant-footstep.png",
                Transform = new Transform2(new Rectangle(position.X + 10, position.Y + 10, 80, 80)),
                Tint =  180.Alpha(),
            });
            _stepsLabel = new Label
            {
                Font = GuiFonts.Header,
                TextColor = UIColors.InGame_Text,
                Text = GameWorld.FootstepsRemaining.ToString("d2"),
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y, 160, 50))
            };
            _visuals.Add(_stepsLabel);
            
            _visuals.Add(new Label
            {
                Font = GuiFonts.Body,
                TextColor = UIColors.InGame_Text,
                Text = "Steps Before Nano-Embed",
                HorizontalAlignment = HorizontalAlignment.Center,
                Transform = new Transform2(new Rectangle(position.X + 80, position.Y + 42, 160, 50))
            });

            Event.Subscribe<FootstepCounted>(OnFootstepCounted, this);
        }

        private void OnFootstepCounted(FootstepCounted e)
        {
            _stepsLabel.Text = e.Steps.ToString("D2");
            // TODO: Change color based on percent of steps left.
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!GameWorld.IsGameOver && GameWorld.CurrentCharacter is MainChar)
                _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}
