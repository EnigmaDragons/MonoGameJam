using MonoDragons.Core.EventSystem;
using System.Linq;
using ZeroFootPrintSociety.CoreGame;
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
            IfAITurn(() =>
            {
                var target = e.AvailableTargets.First();
                Event.Publish(new RangedTargetInspected
                {
                    Attacker = GameWorld.CurrentCharacter,
                    Defender = target.Character,
                    AttackerBlockChance = target.CoverFromThem.BlockChance,
                    DefenderBlockChance = target.CoverToThem.BlockChance
                });
            });
        }
    }
}
