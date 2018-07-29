using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    public class OverwatchTilesAvailable
    {
        public Dictionary<Point, ShotCoverInfo> OverwatchedTiles { get; set; }
    }
}
