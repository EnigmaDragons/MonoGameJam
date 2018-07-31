using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class FootprintsUI : IVisual
    {
        private readonly Dictionary<Direction, double> _directionToRadians = new Dictionary<Direction, double>
        {
            { Direction.Up, 0 },
            { Direction.Right, Math.PI * 0.5 },
            { Direction.Down, Math.PI },
            { Direction.Left, Math.PI * 1.5 }
        };
        private string _footprint = "Effects/GlowingFootsteps";

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.LivingCharacters.ForEach(character => character.Body.Footprints.ForEach(footprint =>
            {
                if (GameWorld.Friendlies.Any(x => x.State.CanPercieve(footprint.Tile)))
                    DrawPrint(footprint, parentTransform);
            }));
        }

        private void DrawPrint(Footprint print, Transform2 parentTransform)
        {
            var texture = Resources.Load<Texture2D>(_footprint);
            var rotationOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            var transform = GameWorld.Map[print.Tile].Transform + parentTransform + new Transform2(rotationOrigin);
            World.SpriteBatch.Draw(texture: texture,
                destinationRectangle: transform.ToRectangle(),
                sourceRectangle: null,
                color: Color.White,
                rotation: (float)_directionToRadians[print.Direction],
                origin: rotationOrigin,
                effects: (print.Tile.X + print.Tile.Y) % 2 == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                layerDepth: 0.0f);
        }
    }
}
