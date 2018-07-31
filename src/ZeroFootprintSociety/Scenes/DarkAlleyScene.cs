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
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps2", "DarkAlley.tmx"), new Size2(48, 48));
            GameWorld.Characters = new List<Character>
            {
                new MainChar().Initialized(GameWorld.Map[48, 95]),
                //new Sidechick().Initialized(GameWorld.Map[46, 95]),
            };

            var startingCameraTile = new Point(31, 93);
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
