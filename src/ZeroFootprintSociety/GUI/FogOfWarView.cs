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
    public class FogOfWarView : IVisualAutomaton
    {
        private readonly ColoredRectangle _lightFog = new ColoredRectangle { Color = Color.FromNonPremultiplied(0,0,0,63) };
        private readonly ColoredRectangle _darkFog = new ColoredRectangle { Color = Color.FromNonPremultiplied(0,0,0,255) };
        private readonly DictionaryWithDefault<Point, bool> _hasSeen = new DictionaryWithDefault<Point, bool>(false);

        public void Update(TimeSpan deltaTime)
        {
             GameWorld.Friendlies.ForEach(friendly => friendly.State.SeeableTiles.ForEach(x => _hasSeen[x.Key] = true));
        }

        public void Draw(Transform2 parentTransform)
        {
            GameWorld.Map.Tiles.Where(tile => !GameWorld.Friendlies.Any(friendly => friendly.State.SeeableTiles[tile.Position]))
                .ForEach(x =>
                {
                    if (_hasSeen[x.Position])
                    {
                        _lightFog.Transform = GameWorld.Map[x.Position].Transform;
                        _lightFog.Draw(parentTransform);
                    }
                    else
                    {
                        _darkFog.Transform = GameWorld.Map[x.Position].Transform;
                        _darkFog.Draw(parentTransform);
                    }
                });
        }
    }
}
