using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTile
    {
        public Point Position { get; }
        public Transform2 Transform { get; }
        public List<GameTileDetail> Details { get; }
        public bool IsWalkable => Details.All(x => !x.IsBlocking);

        public GameTile(int column, int row, Transform2 transform, List<GameTileDetail> details)
        {
            Position = new Point(column, row);
            Transform = transform;
            Details = details.OrderBy(x => x.ZIndex).ToList();
        }

        public void OverwatchThis(Character ownerChar)
        {
        }

        public void OnCharacterSteps(Character character)
        {
            // TODO: Include necessary properties for `OverwatchTriggeredEvent`.

            // If Overwatch was triggered
            // Event.Publish(new OverwatchTriggeredEvent() {FoundCharacter = character});
        }

        public void Draw(int layer, Transform2 parentTransform)
        {
            Details.Where(x => x.ZIndex == layer)
                .ForEach(x => World.SpriteBatch.Draw(x.Texture, (parentTransform + Transform).ToRectangle(), x.SourceRect, Color.White));
        }
    }
}
