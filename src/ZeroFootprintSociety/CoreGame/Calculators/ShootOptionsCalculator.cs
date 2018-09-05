using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ShootOptionsCalculator
    {
        public ShootOptionsCalculator()
        {
            Event.Subscribe(EventSubscription.Create<MovementFinished>(CalculateTargets, this));
        }

        public void CalculateTargets(MovementFinished e)
        {
            var targetsAvailable = new RangedTargetsAvailable
            {
                Targets = GameWorld.LivingCharacters
                    .Where(x => x != GameWorld.CurrentCharacter && CanShoot(GameWorld.CurrentCharacter, x))
                    .Select(x => new Target
                    {
                        Character = x,
                        CoverToThem = new ShotCalculation(GameWorld.CurrentCharacter.CurrentTile, x.CurrentTile).GetBestShot(),
                        CoverFromThem = CanShoot(x, GameWorld.CurrentCharacter)
                            ? new ShotCalculation(x.CurrentTile, GameWorld.CurrentCharacter.CurrentTile).GetBestShot()
                            : new ShotCoverInfo(new List<CoverProvided>())
                    }).ToList()
            };
            EventQueue.Instance.Add(targetsAvailable);
        }

        private bool CanShoot(Character attacker, Character target)
        {
            return !AreSameTeam(attacker, target) 
                && attacker.Gear.EquippedWeapon.IsRanged
                && attacker.Gear.EquippedWeapon.AsRanged().EffectiveRanges.ContainsKey(attacker.CurrentTile.Position.TileDistance(target.CurrentTile.Position))
                && attacker.State.SeeableTiles[target.CurrentTile.Position];
        }

        private bool AreSameTeam(Character attacker, Character target) => attacker.Team == target.Team;
    }
}
