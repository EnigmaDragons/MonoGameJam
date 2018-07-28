﻿using System.Collections.Generic;
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
    public sealed class DarkAlleyScene : SimpleScene
    {
        private TacticsGame _game;

        public override void Init()
        {
            GameWorld.Map = new GameMapFactory().CreateGameMap(new Tmx(CurrentGame.GraphicsDevice, "Maps", "DarkAlleyLevel.tmx"), new Size2(48, 48));
            GameWorld.Characters = new List<Character>
            {
                new MainChar().Initialized(GameWorld.Map[12, 40])
            };
            var startingCameraTile = new Point(0, 20); ; 
            _game = new TacticsGame(
                new TurnBasedCombat(
                    GameWorld.Map,
                    GameWorld.Characters), startingCameraTile);
            _game.Init();
            Add(_game);
        }

        public override void Dispose() { }
    }
}
