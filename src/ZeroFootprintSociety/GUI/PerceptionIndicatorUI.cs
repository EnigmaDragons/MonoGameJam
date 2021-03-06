﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    public class PerceptionIndicatorUI : IVisualAutomaton
    {
        private static readonly List<Transform2> Empty = new List<Transform2>();
        
        private bool _shouldRecalc;
        private List<Transform2> _peceptionMarkers = Empty;
        
        public PerceptionIndicatorUI()
        {
            Event.Subscribe<MoveResolved>(e => _shouldRecalc = true, this);
        }
        
        public void Draw(Transform2 parentTransform)
        {
            _peceptionMarkers.ForEach(
                x => World.Draw("Effects/perception-marker.png", parentTransform + x, TeamColors.Enemy.Footprints_GlowColor.WithAlpha(140)));
        }

        public void Update(TimeSpan delta)
        {
            if (!_shouldRecalc) return;
            
            _peceptionMarkers = GameWorld.Enemies
                .Where(x => GameWorld.FriendlyPerception[x.CurrentTile.Position] && !GameWorld.Friendlies.Any(friendly => friendly.State.SeeableTiles[x.CurrentTile.Position]))
                .Select(x => x.CurrentTile.Transform.WithSize(TileData.RenderSize)).ToList();
            _shouldRecalc = false;
        }
    }
}
