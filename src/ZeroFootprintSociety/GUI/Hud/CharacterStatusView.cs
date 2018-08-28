using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI.Views
{
    public sealed class CharacterStatusView : IVisual
    {        
        private static readonly int _viewWidth = 0.34.VW();
        private static readonly int _viewHeight = 0.66.VH();
        
        private readonly IReadOnlyList<IVisual> _visuals;

        private readonly UiImage _face;
        private readonly Label _name;
        private readonly Label _maxHp;
        private readonly Label _movement;
        private readonly Label _accuracy;
        private readonly Label _guts;
        private readonly Label _agility;
        private readonly Label _perception;

        private bool _shouldShow;

        public CharacterStatusView(Point position)
        {
            var visuals = new List<IVisual>();            
            visuals.Add(new UiImage
            {
                Image = "UI/menu-tall-panel.png", Tint = 220.Alpha(),
                Transform = new Transform2(new Rectangle(position.X, position.Y, _viewWidth, _viewHeight))
            });

            _face = visuals.Added(new UiImage { Transform = new Transform2(new Rectangle(position.X + 50, position.Y + 60, 200, 200)) });
            _name = visuals.Added(new Label
            {
                Font = GuiFonts.Large,
                Transform = new Transform2(new Rectangle(position.X + 20, position.Y + 0.02.VH(), _viewWidth, 50)),
                TextColor = UiColors.InGame_Text
            });
            _maxHp = visuals.Added(StatLabel(position, 0));
            _movement = visuals.Added(StatLabel(position, 1));
            _accuracy = visuals.Added(StatLabel(position, 2));
            _guts = visuals.Added(StatLabel(position, 3));
            _agility = visuals.Added(StatLabel(position, 4));
            _perception = visuals.Added(StatLabel(position, 5));
            
            _visuals = visuals;
            Event.Subscribe<DisplayCharacterStatusRequested>(DisplayCharacter, this);
        }

        private Label StatLabel(Point position, int index)
        {
            return new Label
            {
                Font = GuiFonts.Medium,
                Transform = new Transform2(new Rectangle(position.X + 20,
                    position.Y + 0.02.VH() + 270 + (index) * 40, _viewWidth / 2, 30)),
                TextColor = UiColors.InGame_Text
            };
        }
        
        private void DisplayCharacter(DisplayCharacterStatusRequested e)
        {
            Event.Publish(new SubviewRequested());
            var c = e.Character;
            _face.Image = c.FaceImage;
            _name.Text = c.Stats.Name;
            _maxHp.Text = $"HP: {c.Stats.HP}";
            _movement.Text = $"MOV: {c.Stats.Movement}";
            _accuracy.Text = $"ACC: {c.Stats.Accuracy}";
            _guts.Text = $"GUT: {c.Stats.Guts}";
            _agility.Text = $"AGI: {c.Stats.Agility}";
            _perception.Text = $"PER: {c.Stats.Perception}";
            _shouldShow = true;
        }
        
        public void Draw(Transform2 parentTransform)
        {
            if (_shouldShow)
                _visuals.ForEach(x => x.Draw(parentTransform));
        }
    }
}