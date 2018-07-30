using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
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
            GameWorld.LivingCharacters.Where(x => x.State.IsOverwatching && x.Team != GameWorld.CurrentCharacter.Team).ForEach(
                x =>
                {
                    var visuals = x.State.OverwatchedTiles.Select(tile => new ColoredRectangle
                    {
                        Transform = GameWorld.Map[tile.Key].Transform,
                        Color = Color.FromNonPremultiplied(100, 0, 0, 31)
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
            _visuals.Values.ToList().ForEach(x => x.ForEach(y => y.Draw(parentTransform)));
        }
    }
}
