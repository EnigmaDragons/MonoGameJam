namespace MonoTiled.Tiled.TmxLoading
{
    public class TmxTile
    {
        public int Column { get; }
        public int Row { get; }
        public int TextureId { get; }

        public TmxTile(int column, int row, int textureId)
        {
            Column = column;
            Row = row;
            TextureId = textureId;
        }
    }
}
