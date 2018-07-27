﻿using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Scenes
{
    public class SampleCorporationScene : IScene
    {
        private TacticsGame _game;

        public void Init()
        {
            _game = new TacticsGame("SampleCorporate.tmx");
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
