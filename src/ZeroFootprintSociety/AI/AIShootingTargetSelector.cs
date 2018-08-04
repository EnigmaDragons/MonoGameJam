using System.Collections.Generic;
using MonoDragons.Core.EventSystem;
using System.Linq;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.AI
{
    class AIShootingTargetSelector : AIActorBase
    {
        public AIShootingTargetSelector()
        {
            Event.Subscribe<ShootSelected>(SelectTarget, this);
        }

        private void SelectTarget(ShootSelected e)
        {
            IfAITurn(() => Shoot(e.AvailableTargets
                .OrderBy(x => new ShotCalculation(Char.CurrentTile, x.Character.CurrentTile).GetBestShot().BlockChance)
                .ThenByDescending(x => x.Character.Stats.Guts).First()));
        }

        private void Shoot(Target target)
        {
            EventQueue.Instance.Add(new RangedTargetInspected
            {
                Attacker = GameWorld.CurrentCharacter,
                Defender = target.Character,
                AttackerBlockInfo = target.CoverFromThem,
                DefenderBlockInfo = target.CoverToThem
            });
        }
    }
}
