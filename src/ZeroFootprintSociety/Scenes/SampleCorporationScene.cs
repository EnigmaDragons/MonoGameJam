using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.Orthographic;
using MonoTiled.Tiled.TmxLoading;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Scenes
{
    public class SampleCorporationScene : IScene
    {
        private TileMap _tileMap;
        private Character _character;

        public void Init()
        {
            _tileMap = new TileMapFactory().CreateMap(new Tmx(CurrentGame.GraphicsDevice, "Maps/SampleCorporate.tmx"));
            _character = new Character("CorporateSecurity") { Position = new Vector2(100, 100)};
            _character.Init();
        }

        public void Update(TimeSpan delta)
        {
            _character.Update(delta);
        }

        public void Draw()
        {
            _tileMap.Draw(World.SpriteBatch);
            _character.Draw(Transform2.Zero);
        }

        public void Dispose()
        {
        }
    }
}
