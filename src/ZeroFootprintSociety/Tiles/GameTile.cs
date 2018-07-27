using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTile : IVisual
    {
        public int Column { get; }
        public int Row { get; }
        public Transform2 Transform { get; }
        public List<GameTileDetail> Details { get; }
        public Events TriggerEvents { get; }
        public bool IsWalkable { get; set; } = true;

        public GameTile(int column, int row, Transform2 transform, List<GameTileDetail> details, Events baseEvents = null)
        {
            Column = column;
            Row = row;
            Transform = transform;
            Details = details.OrderBy(x => x.ZIndex).ToList();
            TriggerEvents = baseEvents ?? new Events();
        }

        public void OverwatchThis(Character ownerChar)
        {
        }

        public void OnCharacterSteps(Character character)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            Details.ForEach(x => World.SpriteBatch.Draw(x.Texture, (parentTransform + Transform).ToRectangle(), x.SourceRect, Color.White));
        }
    }
}
