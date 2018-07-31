using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.GUI
{
    public class AttackPreview : IVisual
    {
        private readonly CombatantSummary _attackerSummary = new CombatantSummary(false);
        private readonly CombatantSummary _defenderSummary = new CombatantSummary(true);

        private bool _hidden = true;

        public AttackPreview()
        {
            Event.Subscribe(EventSubscription.Create<ShotProposed>(DisplayPreview, this));
            Event.Subscribe(EventSubscription.Create<ActionCancelled>(e => Hide(), this));
            Event.Subscribe(EventSubscription.Create<ActionConfirmed>(e => Hide(), this));
        }

        private void DisplayPreview(ShotProposed e)
        {
            _hidden = false;
            _attackerSummary.Update(
                e.Attacker.FaceImage, 
                e.Attacker.Stats.Name, 
                e.Attacker.Gear.EquippedWeapon.Image, 
                e.Attacker.Gear.EquippedWeapon.Name, 
                e.AttackerHitChance.ToString(), 
                e.AttackerBullets.ToString(), 
                e.AttackerBulletDamage.ToString(), 
                e.Attacker.Stats.HP, 
                e.Attacker.State.RemainingHealth, 
                e.AttackerDamage);
            _defenderSummary.Update(
                e.Defender.FaceImage,
                e.Defender.Stats.Name,
                e.Defender.Gear.EquippedWeapon.Image,
                e.Defender.Gear.EquippedWeapon.Name,
                e.DefenderHitChance.ToString(),
                e.DefenderBullets.ToString(),
                e.DefenderBulletDamage.ToString(),
                e.Defender.Stats.HP,
                e.Defender.State.RemainingHealth,
                e.DefenderDamage);
        }

        private void Hide()
        {
            _hidden = true;
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_hidden)
                return;

            _attackerSummary.Draw(parentTransform + new Vector2(425, 100));
            _defenderSummary.Draw(parentTransform + new Vector2(825, 100));
        }
    }
}
