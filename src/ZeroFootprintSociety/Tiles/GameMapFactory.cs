using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;
using MonoTiled.Tiled.TmxLoading;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameMapFactory
    {
        public GameMap CreateGameMap(Tmx tmx, Size2 tileSize)
        {
            var tsxMap = new Dictionary<int, Tsx>();
            var tileMap = new Dictionary<int, TsxTile>();
            tmx.Tilesets.ForEach(tsx =>
            {
                for (int i = 0; i < tsx.Tiles.Count; i++)
                {
                    tileMap[tsx.FirstId + i] = tsx.Tiles[i];
                    tsxMap[tsx.FirstId + i] = tsx;
                }
            });

            return new GameMap(tmx.Layers
                .SelectMany(layer => layer.Tiles.Select(tile => new {Layer = layer, Tile = tile}))
                .Where(x => x.Tile.TextureId != 0)
                .GroupBy(x => new {x.Tile.Column, x.Tile.Row})
                .Select(x => new GameTile(x.Key.Column, x.Key.Row,
                    new Transform2(new Rectangle(x.Key.Column * tileSize.Width, x.Key.Row * tileSize.Height,
                        tileSize.Width, tileSize.Height)),
                    x.Select(layerAndTile => new GameTileDetail(
                        tsxMap[layerAndTile.Tile.TextureId].TileSource,
                        tileMap[layerAndTile.Tile.TextureId].SourceRect,
                        layerAndTile.Layer.ZIndex,
                        tileMap[layerAndTile.Tile.TextureId].CustomBools["Blocking"],
                        tileMap[layerAndTile.Tile.TextureId].GetEnum<Cover>("Cover"),
                        tileMap[layerAndTile.Tile.TextureId].CustomBools["Hide"],
                        tileMap[layerAndTile.Tile.TextureId].CustomStrings["FX"],
                        tileMap[layerAndTile.Tile.TextureId].CustomStrings["Character"],
                        tileMap[layerAndTile.Tile.TextureId].CustomBools["MustKill"],
                        tileMap[layerAndTile.Tile.TextureId].CustomStrings["Dialog"])).ToList())).ToList());
        }
    }
}
