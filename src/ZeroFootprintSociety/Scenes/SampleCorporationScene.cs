using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public class SampleCorporationScene : IScene
    {
        private TacticsGame _game;

        public void Init()
        {
            _game = new TacticsGame(
                new TurnBasedCombat(
                    new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps", "SampleCorporate.tmx"), new Size2(48, 48)),
                    new List<Character>
                    {
                        new CorpSec1(),
                        new CorpSec1(),
                    }));
            _game.Init();
        }

        public void Update(TimeSpan delta)
        {
            _game.Update(delta);
        }

        public void Draw()
        {
            _game.Draw();
        }

        public void Dispose() {}
    }
}
