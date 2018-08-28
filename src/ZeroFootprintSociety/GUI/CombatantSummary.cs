using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    public class CombatantSummary : IVisual
    {
        private readonly UiImage _background;
        private readonly Label _name;
        private readonly UiImage _face;
        private readonly AttackedHealthBar _damageBar = new AttackedHealthBar();
        private readonly Vector2 _damageBarOffset;
        private readonly Label _weaponName;
        private readonly UiImage _weapon;
        private readonly Label _hitChance;
        private readonly Label _bullets;

        public CombatantSummary(bool shouldFlip)
        {
            var width = 300;
            var textHeight = 30;
            _background = new UiImage
            {
                Image = "UI/menu-tall-panel.png", 
                Transform = new Transform2(new Size2(350, 400))
            };
            var yOff = 25;
            
            _name = new Label
            {
                Font = GuiFonts.Large,
                Transform = new Transform2(new Rectangle(25, yOff, width, 50)), 
                TextColor = UiColors.InGame_Text
            };
            yOff += 60;
            
            _face = new UiImage
            {
                Transform = new Transform2(new Rectangle(40, yOff, 120, 120)), 
                Effects = shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None
            };
            _hitChance = new Label
            {
                Font = GuiFonts.Large,
                Transform = new Transform2(new Rectangle(130, yOff + 25, 220, 50)),
                TextColor = UiColors.InGame_Text
            };
            _bullets = new Label
            {
                Font = GuiFonts.Large,
                Transform = new Transform2(new Rectangle(130, yOff + 60, 220, 50)),
                TextColor = UiColors.InGame_Text
            };
            yOff += 140;
            
            _damageBarOffset = new Vector2(50, yOff); //250, 50
            yOff += 50;
            
            _weapon = new UiImage
            {
                Transform = new Transform2(new Rectangle(100, yOff, 150, 60)),
                Effects = shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None
            };
            yOff += 60;
            _weaponName = new Label
            {
                Font = GuiFonts.Body,
                Transform = new Transform2(new Rectangle(25, yOff, width, textHeight)), 
                TextColor = UiColors.InGame_Text
            };
            yOff += textHeight;
        }

        public void Update(string characterImage, string name, string weaponImage, string weaponName, string hitChance, string bullets, string bulletDamage,
            int maxHP, int health, int damage, Team team)
        {
            _background.Tint = team.Equals(Team.Enemy) ? TeamColors.Enemy.Characters_GlowColor.WithAlpha(150) : UiColors.Unchanged.WithAlpha(180);
            _face.Image = characterImage;
            _name.Text = name;
            _weapon.Image = weaponImage;
            _weaponName.Text = $"{weaponName}";
            _hitChance.Text = $"Hit:    {hitChance}%";
            _bullets.Text = $"Dmg: {bulletDamage}   x{bullets}";
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
        }
    }
}
