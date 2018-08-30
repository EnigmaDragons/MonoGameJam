using Microsoft.Xna.Framework;
using MonoDragons.Core.Development;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using System;
using System.Linq;
using MonoDragons.Core.Animations;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Soundtrack;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public class GameLevel : SimpleScene
    {
        private string MapDir { get; }
        private string MapFileName { get; }
        private Point CameraStartingTile { get; }
        private LevelMusic Music { get; }

        public GameLevel(string mapFileName)
            : this("Maps2", mapFileName) { }
        
        public GameLevel(string mapFileName, LevelMusic music)
            : this("Maps2", mapFileName, new Point(10, 10), music) { }

        public GameLevel(string mapDir, string mapFileName)
            : this(mapDir, mapFileName, new Point(10, 10), new LevelMusic("corp-amb", "corp-action", "corp-boss", 0.5f, 0.36f, 0.36f)) { }

        private GameLevel(string mapDir, string mapFileName, Point cameraStartingPosition, LevelMusic music)
        {
            MapDir = mapDir;
            MapFileName = mapFileName;
            CameraStartingTile = CameraStartingTile;
            Music = music;
            Add(Music);
        }            

        public override void Init()
        {
            Music.Play(MusicType.Ambient);
            LoadMap();
            SpawnCharacters();
            Add(new TheSoundGuy());

            Add(new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters),
                CameraStartingTile).Initialized());
            Event.Subscribe<MoodChange>(OnMoodChange, this);
            Add(new ScreenFade {Duration = TimeSpan.FromSeconds(2)}.Started());
        }

        private void OnMoodChange(MoodChange change)
        {
            if (change.NewMood == Mood.Stealth)
                Music.Play(MusicType.Ambient);
            if (change.NewMood == Mood.Battle)
                Music.Play(MusicType.Action);
            if (change.NewMood == Mood.Boss)
                Music.Play(MusicType.Boss);
        }

        private void LoadMap()
        {
            GameWorld.Map = Perf.Time("Loaded Map", 
                () => new GameMapFactory().CreateGameMap(
                    new Tmx(CurrentGame.GraphicsDevice, MapDir, MapFileName), 
                    TileData.RenderSize));
        }

        private void SpawnCharacters()
        {
            var characters = GameWorld.Map.GetStartingCharacters();
            if (!characters.Any())
                throw new InvalidOperationException($"Map '{MapDir}/{MapFileName}' has no characters.");
            GameWorld.Characters = characters;
        }

        public override void Dispose() { }
    }
}
