using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    public class CombatantSummary : IVisual
    {
        private readonly ColoredRectangle _background;
        private readonly Label _name;
        private readonly ImageBox _face;
        private readonly AttackedHealthBar _damageBar = new AttackedHealthBar();
        private readonly Vector2 _damageBarOffset;
        private readonly Label _weaponName;
        private readonly ImageBox _weapon;
        private readonly Label _hitChance;
        private readonly Label _bullets;
        private readonly Label _bulletDamage;

        public CombatantSummary()
        {
            _background = new ColoredRectangle { Color = Color.DarkBlue, Transform = new Transform2(new Size2(350, 675)) };
            _name = new Label { Transform = new Transform2(        new Rectangle(25, 25, 300, 50)), TextColor = Color.White };
            _face = new ImageBox { Transform = new Transform2(     new Rectangle(125, 75, 100, 100)) };
            _damageBarOffset =                                       new Vector2(50, 200); //250, 50
            _weaponName = new Label { Transform = new Transform2(  new Rectangle(25, 275, 300, 50)), TextColor = Color.White };
            _weapon = new ImageBox { Transform = new Transform2(   new Rectangle(100, 350 + 4, 150, 42)) };
            _hitChance = new Label { Transform = new Transform2(   new Rectangle(25, 425, 300, 50)), TextColor = Color.White };
            _bullets = new Label { Transform = new Transform2(     new Rectangle(25, 475, 300, 50)), TextColor = Color.White };
            _bulletDamage = new Label { Transform = new Transform2(new Rectangle(25, 525, 300, 50)), TextColor = Color.White };
        }

        public void Update(string characterImage, string name, string weaponImage, string weaponName, string hitChance, string bullets, string bulletDamage,
            int maxHP, int health, int damage)
        {
            _face.Image = characterImage;
            _name.Text = name;
            _weapon.Image = weaponImage;
            _weaponName.Text = $"Weapon: {weaponName}";
            _hitChance.Text = $"Hit Chance: {hitChance}";
            _bullets.Text = $"Bullets: {bullets}";
            _bulletDamage.Text = $"Bullet Damage: {bulletDamage}";
            _damageBar.Update(maxHP, health, damage);
        }

        public void Draw(Transform2 parentTransform)
        {
            _background.Draw(parentTransform);
            _name.Draw(parentTransform);
            _face.Draw(parentTransform);
            _damageBar.Draw(parentTransform + _damageBarOffset);
            _weaponName.Draw(parentTransform);
            _weapon.Draw(parentTransform);
            _hitChance.Draw(parentTransform);
            _bullets.Draw(parentTransform);
            _bulletDamage.Draw(parentTransform);
        }
    }
}
