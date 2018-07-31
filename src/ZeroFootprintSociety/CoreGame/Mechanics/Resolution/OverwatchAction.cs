using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class OverwatchAction
    {
        public OverwatchAction()
        {
            Event.Subscribe(EventSubscription.Create<OverwatchSelected>(OnOverwatchSelected, this));
        }

        private void OnOverwatchSelected(OverwatchSelected e)
        {
            if (!GameWorld.CurrentCharacter.State.OverwatchedTiles.Any())
            {
                var overwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
                var tiles = ValidTiles(new PointRadiusCalculation(GameWorld.CurrentCharacter.CurrentTile.Position, GameWorld.CurrentCharacter.Gear.EquippedWeapon.AsRanged().Range).Calculate());
                tiles.ForEach(x =>
                {
                    var shot = new ShotCalculation(GameWorld.CurrentCharacter.CurrentTile, GameWorld.Map[x]).BestShot();
                    if (new HitChanceCalculation(GameWorld.CurrentCharacter.Accuracy, shot.BlockChance).Get() > 0)
                        overwatchedTiles[x] = shot;
                });
                Event.Publish(new OverwatchTilesAvailable { OverwatchedTiles = overwatchedTiles });
            }
            Event.Publish(new ActionReadied(() =>
            {
                GameWorld.CurrentCharacter.State.IsOverwatching = true;
                Event.Publish(new ActionResolved());
            }));
        }

        private List<Point> ValidTiles(List<Point> points)
        {
            return points.Where(x => GameWorld.Map.Exists(x) && GameWorld.Map[x].IsWalkable).ToList();
        }
    }
}
