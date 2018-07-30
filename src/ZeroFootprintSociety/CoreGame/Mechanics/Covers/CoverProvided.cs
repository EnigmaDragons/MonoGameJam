using System.Linq;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class CoverProvided
    {
        public Cover Cover { get; }
        public GameTile[] Providers { get; }

        public CoverProvided()
        {
            Cover = Cover.None;
            Providers = new GameTile[] { };
        }

        public CoverProvided(params GameTile[] providers)
        {
            Cover = providers.Min(p => p.Cover);
            Providers = providers;
        }
    }
}
