using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public class GameLevel : SimpleScene
    {
        private string MapDir { get; }
        private string MapFileName { get; }
        private Point CameraStartingTile { get; }

        public GameLevel(string mapFileName)
            : this("Maps2", mapFileName, new Point(10, 10)) { }

        public GameLevel(string mapDir, string mapFileName)
            : this(mapDir, mapFileName, new Point(10, 10)) { }

        private GameLevel(string mapDir, string mapFileName, Point cameraStartingPosition)
        {
            MapDir = mapDir;
            MapFileName = mapFileName;
            CameraStartingTile = CameraStartingTile;
        }            

        public override void Init()
        {
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, MapDir, MapFileName), TileData.RenderSize);
            GameWorld.Characters = GameWorld.Map.GetStartingCharacters();

            Add(new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters),
                CameraStartingTile).Initialized());
        }

        public override void Dispose() { }
    }
}
