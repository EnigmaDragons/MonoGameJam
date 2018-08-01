using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    public sealed class CharacterGroundFootprints : IVisualAutomaton
    {        
        private static readonly List<IAnimation> Empty = new List<IAnimation>(0);

        private static readonly Dictionary<Direction, string> _images = new Dictionary<Direction, string>()
        {
            {Direction.Up, "Effects/GlowingFootsteps"},
            {Direction.Down, "Effects/GlowingFootsteps-down"},
            {Direction.Left, "Effects/GlowingFootsteps-left"},
            {Direction.Right, "Effects/GlowingFootsteps-right"},
        };
        
        private List<IAnimation> _anims = Empty;

        public CharacterGroundFootprints()
        {
            Event.Subscribe<Moved>(Add, this);
        }
        
        public void Update(TimeSpan delta)
        {
            _anims.ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _anims.ForEach(x => x.Draw(parentTransform));
        }

        private void Add(Moved e)
        {
            var color = e.Character.Theme.Footprints_GlowColor;
            var image = new WorldImage
            {
                Image = _images[e.Character.Body.Facing],
                Transform = GameWorld.Map.TileToWorldTransform(e.Position).WithSize(TileData.RenderSize),
                Effects = (e.Position.X + e.Position.Y) % 2 == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                Tint = color
            };
            var anim = new FadingVisual(image, c => image.Tint = c)
            {
                Duration = TimeSpan.FromSeconds(1.0), 
                SourceColor = color
            };
            var animAsList = new List<IAnimation> { anim };
            anim.Start(() => _anims = _anims.Except(animAsList).ToList());
            _anims = _anims.Concat(animAsList).ToList();
        }
    }
}