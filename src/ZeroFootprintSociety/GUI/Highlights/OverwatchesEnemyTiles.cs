﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    public class OverwatchesEnemyTiles : IVisualAutomaton
    {
        private readonly ConcurrentDictionary<Character, List<IVisual>> _visuals = new ConcurrentDictionary<Character, List<IVisual>>(); 

        public OverwatchesEnemyTiles()
        {
            Event.Subscribe<TurnBegun>(x => UpdateOverwatchers(), this);
        }

        public void UpdateOverwatchers()
        {
            _visuals.Clear();
            GameWorld.LivingCharacters.Where(x => x.State.IsOverwatching && x.Team != GameWorld.CurrentCharacter.Team && GameWorld.FriendlyPerception[x.CurrentTile.Position]).ForEach(
                x =>
                {
                    var visuals = x.State.OverwatchedTiles.Select(tile => new ColoredRectangle
                    {
                        Transform = GameWorld.Map[tile.Key].Transform,
                        Color = UiColors.OverwatchedTiles_OverwatchedByCharacter
                    }).Cast<IVisual>().ToList();

                    _visuals[x] = visuals;
                });
        }

        public void Update(TimeSpan delta)
        {
            _visuals.Keys.Where(x => !x.State.IsOverwatching).ForEach(x => _visuals.TryRemove(x, out List<IVisual> _));
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.Values.ForEach(list => list.ForEach(x => x.Draw(parentTransform)));
        }
    }
}
