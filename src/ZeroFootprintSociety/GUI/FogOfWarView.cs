using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.GUI
{
    public class FogOfWarView: IVisual
    {
        private readonly ColoredRectangle _darkFog = new ColoredRectangle { Color = Color.FromNonPremultiplied(0,0,0,63) };

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.Map.Tiles.Where(tile => !GameWorld.Friendlies.Any(friendly => friendly.State.SeeableTiles[tile.Position]))
                .ForEach(x =>
                {
                    _darkFog.Transform = GameWorld.Map[x.Position].Transform;
                    _darkFog.Draw(parentTransform);
                });
        }
    }
}
