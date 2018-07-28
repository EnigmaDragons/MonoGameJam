using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Characters;
using Prefabs = ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class SampleCorporationScene : SimpleScene
    {
        private TacticsGame _game;

        public override void Init()
        {
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps", "SampleCorporate.tmx"), new Size2(48, 48));
            GameWorld.Characters = new List<Character>
            {
                new Prefabs.CorpSec1(),
                new Prefabs.CorpSec1(),
            };
            _game = new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters));
            _game.Init();
            Add(_game);
        } 

        public override void Dispose() {}
    }
}
