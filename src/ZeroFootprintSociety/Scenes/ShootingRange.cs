using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public class ShootingRange : SimpleScene
    {
        private TacticsGame _game;

        public override void Init()
        {
            GameWorld.Map =
                new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps2", "TestFogOfWar.tmx"),
                    new Size2(48, 48));

            GameWorld.Characters = GameWorld.Map
                .GetStartingCharacters(
                    new CorpSec1().Initialized(GameWorld.Map[16, 10]),
                    new CorpSec1().Initialized(GameWorld.Map[11, 28]),
                    new CorpSec1().Initialized(GameWorld.Map[3, 3]),
                    new CorpSec1().Initialized(GameWorld.Map[24, 13]),
                    new CorpSec1().Initialized(GameWorld.Map[7, 9]),
                    new CorpSec1().Initialized(GameWorld.Map[7, 19]));
            var startingCameraTile = new Point(10, 10);
            _game = new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters),
                startingCameraTile);
            _game.Init();
            Add(_game);
        }

        public override void Dispose() {}
    }
}
