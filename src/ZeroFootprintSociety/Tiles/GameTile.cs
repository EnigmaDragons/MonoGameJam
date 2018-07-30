using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTile
    {
        public Point Position { get; }
        public Transform2 Transform { get; }
        public List<GameTileDetail> Details { get; }
        public bool IsWalkable => Details.All(x => !x.IsBlocking) && GameWorld.Characters.All(x => x.CurrentTile != this);
        public Cover Cover { get; }
        public string PostFX { get; }

        public GameTile(int column, int row, Transform2 transform, List<GameTileDetail> details)
        {
            Position = new Point(column, row);
            Transform = transform;
            Details = details.OrderBy(x => x.ZIndex).ToList();
            Cover = Details.OrderByDescending(x => (int)x.Cover).First().Cover;
            PostFX = details.Where(x => !string.IsNullOrWhiteSpace(x.PostFX)).FirstOrDefault()?.PostFX ?? "None";
        }

        public void Draw(int layer, Transform2 parentTransform)
        {
            Details.Where(x => x.ZIndex == layer)
                .Where(x => x.IsVisible)
                .ForEach(x => World.SpriteBatch.Draw(x.Texture, (parentTransform + Transform).ToRectangle(), x.SourceRect, Color.White));
        }
    }
}
