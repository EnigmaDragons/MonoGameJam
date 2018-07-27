using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoTiled.Tiled.Orthographic;
using MonoTiled.Tiled.TmxLoading;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameMapFactory
    {
        public GameMap CreateGameMap(Tmx tmx, Size2 tileSize)
        {
            var details = new TileMapDetails(tmx);
            //TODO add blocking calculation
            return new GameMap(tmx.Layers
                .SelectMany(layer => layer.Tiles.Select(tile => new {Layer = layer, Tile = tile}))
                .Where(x => x.Tile.TextureId != 0)
                .GroupBy(x => new {x.Tile.Column, x.Tile.Row})
                .Select(x => new GameTile(x.Key.Column, x.Key.Row,
                    new Transform2(new Rectangle(x.Key.Column * tileSize.Width, x.Key.Row * tileSize.Height,
                        tileSize.Width, tileSize.Height)),
                    x.Select(layerAndTile => new GameTileDetail(
                        details.Get(layerAndTile.Tile.TextureId).Texture,
                        details.Get(layerAndTile.Tile.TextureId).SourceRect,
                        layerAndTile.Layer.ZIndex)).ToList())).ToList());
        }
    }
}
