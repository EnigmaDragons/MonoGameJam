using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTiled.Tiled
{
    public class Texture2DFromPath
    {
        private readonly GraphicsDevice _device;
        private readonly string _path;

        public Texture2DFromPath(GraphicsDevice device, string path)
        {
            _device = device;
            _path = path;
        }

        public Texture2D Get()
        {
            using (var fileStream = new FileStream(_path, FileMode.Open))
                return Texture2D.FromStream(_device, fileStream);
        }
    }
}
