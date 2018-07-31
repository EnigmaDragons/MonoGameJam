using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class FootprintsUI : IVisual
    {
        public WorldImage Footprint = new WorldImage { Image = "Effects/GlowingFootsteps" };

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.LivingCharacters.ForEach(character => character.State.Footprints.ForEach(footprint =>
            {
                if (GameWorld.Friendlies.Any(x => x.State.CanPercieve(footprint.Tile)))
                    DrawPrint(footprint, parentTransform);
            }));
        }

        private void DrawPrint(Footprint print, Transform2 parentTransform)
        {
            //var modifiedTransform = _transform;
            //modifiedTransform.Location = modifiedTransform.Location.MoveInDirection(_transform.Rotation.Value, _distanceTravled);
            //if (GameWorld.Map.Exists(GameWorld.Map.MapPositionToTile(modifiedTransform.Location)))
            //    _tile = GameWorld.Map.MapPositionToTile(modifiedTransform.Location);
            //else
            //    IsDone = true;
            //modifiedTransform += parentTransform;
            //UI.SpriteBatch.Draw(texture: _texture,
            //    destinationRectangle: modifiedTransform.ToRectangle(),
            //    sourceRectangle: null,
            //    color: Color.White,
            //    rotation: _transform.Rotation.Value - (float)(Math.PI / 2),
            //    origin: new Vector2(1, 1),
            //    effects: SpriteEffects.None,
            //    layerDepth: 0.0f);
        }
    }
}
