using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters.Prefabs;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class DarkAlleyScene : SimpleScene
    {
        private TacticsGame _game;

        public override void Init()
        {
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps2", "DarkAlley.tmx"), new Size2(100, 100));
            GameWorld.Characters = new List<Character>
            {
                new MainChar().Initialized(GameWorld.Map[49, 97]),
                //new Sidechick().Initialized(GameWorld.Map[50, 98]),
            };

            var startingCameraTile = new Point(49, 54);
            _game = new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters),
                startingCameraTile);
            _game.Init();
            Add(_game);
        }

        public override void Dispose() { }
    }
}
