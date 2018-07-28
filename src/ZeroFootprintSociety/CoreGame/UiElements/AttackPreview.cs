using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class AttackPreview : IVisual
    {
        private readonly ColoredRectangle _background = new ColoredRectangle { Color = Color.DarkBlue, Transform = new Transform2(new Size2(600, 500)) };
        private readonly AttackedHealthBar _attackerBar = new AttackedHealthBar();
        private readonly AttackedHealthBar _defenderBar = new AttackedHealthBar();
        private readonly TextButton _confirm;
        private readonly TextButton _cancel;
        private readonly ClickUIBranch _branch = new ClickUIBranch("Attack Preview", 2);
        private readonly ClickUI _clickUI;

        private bool _hidden = true;
        private ShotProposed _shot;

        public AttackPreview(ClickUI clickUI)
        {
            _clickUI = clickUI;
            _confirm = new TextButton(new Rectangle(550, 425, 200, 70), Shoot, "Confirm",
                Color.FromNonPremultiplied(0, 0, 100, 50),
                Color.FromNonPremultiplied(0, 0, 100, 150),
                Color.FromNonPremultiplied(0, 0, 100, 250));
            _cancel = new TextButton(new Rectangle(850, 425, 200, 70), Cancel, "Cancel",
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
            _attackerBar.Update(e.AttackerMaxHealth, e.AttackerCurrentHealth, e.AttackerDamage);
            _defenderBar.Update(e.DefenderMaxHealth, e.DefenderCurrentHealth, e.DefenderDamage);
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
            _background.Draw(parentTransform + new Vector2(500, 150));
            _attackerBar.Draw(parentTransform + new Vector2(525, 175));
            _defenderBar.Draw(parentTransform + new Vector2(825, 175));
            UI.DrawTextCentered($"Hit Chance: {_shot.AttackerHitChance}", new Rectangle(500, 250, 300, 50), Color.White);
            UI.DrawTextCentered($"Bullets: {_shot.AttackerBullets}", new Rectangle(500, 300, 300, 50), Color.White);
            UI.DrawTextCentered($"Damage: {_shot.AttackerBulletDamage}", new Rectangle(500, 350, 300, 50), Color.White);
            UI.DrawTextCentered($"Hit Chance: {_shot.DefenderHitChance}", new Rectangle(800, 250, 300, 50), Color.White);
            UI.DrawTextCentered($"Bullets: {_shot.DefenderBullets}", new Rectangle(800, 300, 300, 50), Color.White);
            UI.DrawTextCentered($"Damage: {_shot.DefenderBulletDamage}", new Rectangle(800, 350, 300, 50), Color.White);
            _confirm.Draw(parentTransform);
            _cancel.Draw(parentTransform);
        }
    }
}
