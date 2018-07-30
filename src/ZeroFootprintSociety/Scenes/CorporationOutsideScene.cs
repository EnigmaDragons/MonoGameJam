using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class CorporationOutsideScene : SimpleScene
    {
        private TacticsGame _game;

        public override void Init()
        {
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps", "OutsideCorporation.tmx"), new Size2(48, 48));
            GameWorld.Characters = new List<Character>
            {
                new MainChar().Initialized(GameWorld.Map[14, 14]),
            };
            var startingCameraTile = new Point(10, 10);
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
