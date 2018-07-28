using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AttackPreview : IVisual
    {
        private readonly CombatantSummary _attackerSummary = new CombatantSummary(false);
        private readonly CombatantSummary _defenderSummary = new CombatantSummary(true);
        private readonly TextButton _confirm;
        private readonly TextButton _cancel;
        private readonly ClickUIBranch _branch = new ClickUIBranch("Attack Preview", 2);
        private readonly ClickUI _clickUI;

        private bool _hidden = true;
        private ShotProposed _shot;

        public AttackPreview(ClickUI clickUI)
        {
            _clickUI = clickUI;
            _confirm = new TextButton(new Rectangle(525, 700, 150, 50), Shoot, "Confirm",
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));
            _cancel = new TextButton(new Rectangle(925, 700, 150, 50), Cancel, "Cancel",
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));
            _branch.Add(_confirm);
            _branch.Add(_cancel);
            Event.Subscribe(EventSubscription.Create<ShotProposed>(DisplayPreview, this));
        }

        private void DisplayPreview(ShotProposed e)
        {
            _shot = e;
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
            _clickUI.Add(_branch);
        }

        private void Shoot()
        {
            _hidden = true;
            _clickUI.Remove(_branch);
            Event.Publish(new ShotConfirmed { Proposed = _shot });
        }

        private void Cancel()
        {
            _hidden = true;
            _clickUI.Remove(_branch);
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_hidden)
                return;
            _attackerSummary.Draw(parentTransform + new Vector2(425, 100));
            _defenderSummary.Draw(parentTransform + new Vector2(825, 100));
            _confirm.Draw(parentTransform);
            _cancel.Draw(parentTransform);
        }
    }
}
