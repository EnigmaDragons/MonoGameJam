using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public class OverwatchResolver
    {
        private List<Character> _overwatchingEnemies = new List<Character>();

        public OverwatchResolver()
        {
            Event.Subscribe<TurnBegun>(x => UpdateOverwatchers(), this);
            Event.Subscribe<Moved>(OnMovement, this);
        }

        public void UpdateOverwatchers()
        {
            _overwatchingEnemies = GameWorld.LivingCharacters.Where(x => x.State.IsOverwatching && x.Team != GameWorld.CurrentCharacter.Team).ToList();
        }

        public void OnMovement(Moved moved)
        {
            var shots = _overwatchingEnemies
                .Where(x => x.State.IsOverwatching && x.State.OverwatchedTiles.ContainsKey(moved.Position))
                .Select(x => new ShotConfirmed
                    {
                        Proposed = new ProposedShotCalculation(x, GameWorld.CurrentCharacter, 
                            new ShotCalculation(GameWorld.CurrentCharacter.CurrentTile, x.CurrentTile).BestShot().BlockChance, 
                            x.State.OverwatchedTiles[moved.Position].BlockChance).CalculateShot()
                    })
                .ToList();
            Action action = () => Event.Publish(new MoveResolved { Character = moved.Character });
            shots.ForEach(x =>
            {
                x.OnFinished = action;
                action = () => Event.Publish(x);
            });
            action();
        }
    }
}
