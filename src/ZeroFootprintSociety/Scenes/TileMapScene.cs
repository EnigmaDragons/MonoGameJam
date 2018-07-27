using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoTiled.Tiled.Orthographic;
using MonoTiled.Tiled.TmxLoading;

namespace ZeroFootPrintSociety.Scenes
{
    public abstract class TileMapScene : IScene
    {
        protected abstract string TmxName { get; }

        private TileMap _tileMap;

        public void Init()
        {
            _tileMap = new TileMapFactory().CreateMap(new Tmx(CurrentGame.GraphicsDevice, "Maps/" + TmxName));
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
            _tileMap.Draw(World.SpriteBatch);
        }

        public void Dispose()
        {
        }
    }
}
