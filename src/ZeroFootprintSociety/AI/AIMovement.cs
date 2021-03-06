﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.AI
{
    sealed class AIMovement : AIActorBase
    {
        private readonly Dictionary<Character, AICharacterData> _characterData;
        private AICharacterData Data => _characterData[Char];

        public AIMovement(Dictionary<Character, AICharacterData> characterData)
        {
            _characterData = characterData;
            Event.Subscribe<MovementOptionsAvailable>(ChooseMoveIfApplicable, this);
        }

        private void ChooseMoveIfApplicable(MovementOptionsAvailable e)
        {
            IfAITurn(() => EventQueue.Instance.Add(new AIActionQueued(() =>
            {
                if (Char.Team != Team.Enemy)
                    return;
                EventQueue.Instance.Add(new MovementConfirmed(GetBestMovement(e.AvailableMoves)));
            })));
        }

        private IReadOnlyList<Point> GetBestMovement(IReadOnlyList<IReadOnlyList<Point>> optionalPaths)
        {
            var data = Data;
            if (data.SeenEnemies.Any())
            {
                return optionalPaths
                    .Preferred(path => CanShootFrom(path.Last()))
                    .Preferred(path => path.Last() != Char.CurrentTile.Position)
                    .Preferred(path => new SpotHasGoodCoverCalculation(data, path.Last()).Calculate())
                    .GroupBy(path => data.SeenEnemies.Sum(enemy => enemy.Value.TileDistance(path.Last())))
                    .OrderBy(x => x.Key)
                    .First()
                    .ToList()
                    .Random()
                    .ToList();
            }
            return optionalPaths.Where(x => x.Count == 3 || x.Count == 4).ToList().Random();
        }

        private bool CanShootFrom(Point point)
        {
            return Char.Gear.EquippedWeapon.IsRanged
                && Data.SeenEnemies.Any(x => Char.Gear.EquippedWeapon.AsRanged().EffectiveRanges.ContainsKey(point.TileDistance(x.Value)));
        }
    }
}
