﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class TilesPercieved
    {
        public Character Character { get; set; }
        public List<Point> Tiles { get; set; }
    }
}
