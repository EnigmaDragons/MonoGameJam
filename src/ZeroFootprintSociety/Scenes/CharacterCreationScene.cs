using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.GUI;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.Scenes
{
    public class CharacterCreationScene : ClickUiScene
    {
        private readonly string _nextSceneName;
        private ChoiceUI _activeChoice;
        private readonly IAnimation _fadeOut;
        private bool _selected;

        public CharacterCreationScene(string nextSceneName)
        {
            _nextSceneName = nextSceneName;
            _fadeOut = new ScreenFade { Duration = TimeSpan.FromSeconds(1), FromAlpha = 0, ToAlpha = 255};
        }

        public override void Init()
        {
            Sound.Music("customizing", 0.38f).Play();
            var classChoice = CreateWeaponChoice(
                new WeaponSet("Sniper", new RsxCarbine(), new SideArm()),
                new WeaponSet("Bulleteer", new WarUzi(), new AutoPistol()),
                new WeaponSet("Up Close", new WarShotgun(), new PowerMagnum()));

            _activeChoice = classChoice;
            ClickUi.Add(classChoice.Branch);
            Add(new Label
            {
                Text = "Weldon Zemke",
                Font = GuiFonts.Header,
                TextColor = UiColors.InGame_Text,
                Transform = new Transform2(new Vector2(0.02.VW(), 0.2.VH()), new Size2(0.20.VW(), 80))
            });
            Add(new Label
            {
                Text = "Leader of ZFS",
                Font = GuiFonts.Large,
                TextColor = UiColors.InGame_Text,
                Transform = new Transform2(new Vector2(0.02.VW(), 0.25.VH()), new Size2(0.20.VW(), 80))
            });
            Add(new UiImage
            {
                Image = "Characters/MainCharacter/MainCharacter-bust.png",
                Transform = new Transform2(new Vector2(-0.06.VW(), 0.3.VH()), new Size2(0.7.VH(), 0.7.VH()))
            });
            Add(_activeChoice);
            Add(new ScreenFade {Duration = TimeSpan.FromSeconds(1)}.Started());
            Add(_fadeOut);
        }

        private void Select(WeaponSet set)
        {
            if (_selected)
                return;
            
            _selected = true;
            Buttons.PlayClickSound();
            GameWorld.MainCharClass = new CharacterClass { WeaponSet = set };
            _fadeOut.Start(() => Scene.NavigateTo(_nextSceneName));
        }
        
        private ChoiceUI CreateWeaponChoice(WeaponSet weaponSet1, WeaponSet weaponSet2, WeaponSet weaponSet3)
        {
            var height = 0.24.VH();
            return new ChoiceUI("Choose Weapon Loadout",
                CreateWeaponOption(weaponSet1, new Vector2(0.24.VW(), height)),
                CreateWeaponOption(weaponSet2, new Vector2(0.49.VW(), height)),
                CreateWeaponOption(weaponSet3, new Vector2(0.74.VW(), height)));
        }

        private OptionUI CreateWeaponOption(WeaponSet weaponSet, Vector2 offset)
        {
            var xMar = 25;
            var width = 300;
            var tWidth = 280;
            return new OptionUI(
                weaponSet.Name, 
                offset,
                () => Select(weaponSet),
                new Label { 
                    TextColor = UiColors.InGame_Text, 
                    Text = weaponSet.Primary.Name, 
                    Font = "Fonts/16",
                    Transform = new Transform2(new Vector2(xMar + 10, 85), new Size2(tWidth, 50)) 
                },
                new UiImage
                {
                    Image = weaponSet.Primary.Image, 
                    Transform = new Transform2(new Vector2(xMar + 30, 135), new Size2(240, 94))
                },
                new Label 
                { 
                    TextColor = UiColors.InGame_Text, 
                    Text = weaponSet.Primary.ShortDescription, 
                    Font = GuiFonts.Body,
                    Transform = new Transform2(new Vector2(xMar + 10, 240), new Size2(tWidth, 50)) 
                },
                
                new Label 
                { 
                    TextColor = UiColors.InGame_Text, 
                    Text = weaponSet.Secondary.Name, 
                    Font = "Fonts/16",
                    Transform = new Transform2(new Vector2(xMar + 10, 333), new Size2(tWidth, 50)) 
                },
                new UiImage
                {
                    Image = weaponSet.Secondary.Image, 
                    Transform = new Transform2(new Vector2(xMar + 30, 383), new Size2(240, 94))
                },
                new Label 
                { 
                    TextColor = UiColors.InGame_Text, 
                    Text = weaponSet.Secondary.ShortDescription, 
                    Font = GuiFonts.Body,
                    Transform = new Transform2(new Vector2(xMar + 10, 493), new Size2(tWidth, 50)) 
                });
        }
    }
}