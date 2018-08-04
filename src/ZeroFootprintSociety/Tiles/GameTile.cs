using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameTile
    {
        private const int FogLayer = 99;
        private static readonly ColoredRectangle _lightFog = new ColoredRectangle { Color = UIColors.FogColor, Transform = new Transform2(TileData.RenderSize)};
        
        public static GameTile None { get; } = new GameTile(-1, -1, Transform2.Zero, new List<GameTileDetail> { GameTileDetail.None });

        public Point Position { get; }
        public Transform2 Transform { get; }
        public List<GameTileDetail> Details { get; }
        public bool IsWalkable => Details.All(x => !x.IsBlocking) && GameWorld.LivingCharacters.All(x => x.CurrentTile != this);
        public Cover Cover { get; }
        public List<string> PostFX { get; }
        public string SpawnCharacter { get; }
        public bool EverSeenByFriendly { get; set; }
        public bool CurrentlyFriendlyVisible { get; set; } = true; // TODO: Set this;
        public string Dialog { get; }
        //TODO: these are trash properties
        public bool MustKill { get; }

        public GameTile(int column, int row, Transform2 transform, List<GameTileDetail> details)
        {
            Position = new Point(column, row);
            Transform = transform;
            Details = details.OrderBy(x => x.ZIndex).ToList();
            Cover = Details.OrderByDescending(x => (int)x.Cover).First().Cover;
            PostFX = details.Select(x => x.PostFX).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            SpawnCharacter = details.Select(x => x.SpawnCharacter).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? "None";
            Dialog = details.Select(x => x.Dialog).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? "None";
            MustKill = details.Any(x => x.MustKill);
        }

        public void Draw(int layer, Transform2 parentTransform)
        {
            Draw(layer, parentTransform, UIColors.Unchanged);
        }

        public void Draw(int layer, Transform2 parentTransform, Color tint)
        {
            if (!EverSeenByFriendly)
                return;
            if (layer.Equals(FogLayer) && !CurrentlyFriendlyVisible)
                _lightFog.Draw(parentTransform + Transform);
            else
                Details.Where(x => x.ZIndex == layer)
                    .Where(x => x.IsVisible)
                    .ForEach(x =>
                        World.SpriteBatch.Draw(x.Texture, World.ScaleRectangle((parentTransform + Transform).ToRectangle()), x.SourceRect,
                            tint));
        }
    }
}
