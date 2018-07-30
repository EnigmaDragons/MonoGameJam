using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class FogOfWarView: IVisual
    {
        private readonly ColoredRectangle _darkFog;
        private readonly ColoredRectangle _lightFog;

        public FogOfWarView()
        {
            // Lol.
            Transform2 autobotsRollOut = new Transform2(TileData.RenderSize);
            _darkFog = new ColoredRectangle()
            {
                Color = Color.FromNonPremultiplied(0,0,0,80),
                Transform = autobotsRollOut
            };
            _lightFog = new ColoredRectangle()
            {
                Color = Color.FromNonPremultiplied(0,0,0,160),
                Transform = autobotsRollOut
            };
        }

        public void Draw(Transform2 parentTransform)
        {
            ColoredRectangle coloredRectToUse;
            foreach (GameTile tile in GameWorld.Map.Tiles)
            {
                Transform2 newTransform = GameWorld.Map[tile.Position].Transform;

                _darkFog.Transform = newTransform;
                _darkFog.Draw(parentTransform);
                //_lightFog.Transform = newTransform;
                //_lightFog.Draw(parentTransform);
            }
            
        }
    }
}
