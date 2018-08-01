using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.GUI;

namespace ZeroFootPrintSociety.Scenes
{
    public class CharacterCreation : IScene
    {
        private ChoiceUI _classChoice;
        private const string MeleeClassName = "Sharp Edge";
        private const string MeleeClassDescrption = "";
        private const string GunClassName = "Gun Master";
        private const string GunClassDescription = "";
        private const string DroneClassName = "Rigger";
        private const string DroneClassDescription = "";

        private ChoiceUI _meleePerkChoice;
        private const string NinjaPerkName = "Cyber-Ninja";
        private const string NinjaPerkDescription = "They have a special suit that allows them to be almost invisible when moving. They are exetremely fast and lethal with their bladed weapons.";
        private const string TankPerkName = "Cybernetic Strength";
        private const string TankPerkDescription = "Damage inflicted on their body is converted into kinetic energy. This both reduces a lotta pain coming in as well as allows their melee strikes to bring far more pain.";
        private ChoiceUI _gunPerkChoice;
        private const string DualWieldPerkName = "Akimbo";
        private const string DualWeildPerkDescription = "They have mastered the art of dual wielding all types of guns.";
        private const string ArmorPiercingPerkName = "Nanite Armor Piercing Rounds";
        private const string ArmorPiercingPerkDescription = "No one even knows these rounds exist yet.";
        private ChoiceUI _dronePerkChoice;
        private const string ProxMinePerkName = "Spider Mine";
        private const string ProxMinePerkDescription = "Using high tech sensors, detect the enemies and lay down your explosive pets where they intend to go.";
        private const string DronePerkName = "Razor Wing";
        private const string DronePerkDescription = "Your own personal combat drone, perfect for scouting, flanking, and murder.";

        private ChoiceUI _ninjaWeaponChoice;
        private WeaponSet _katanaSet;
        private WeaponSet _wakizashiSet;
        private WeaponSet _bloodKatanaSet;
        private ChoiceUI _tankWeaponChoice;
        private WeaponSet _riotShieldSet;
        private WeaponSet _stunBatonSet;
        private WeaponSet _hammerSet;
        private ChoiceUI _dualWieldWeaponChoice;
        private WeaponSet _doubleShotgunSet;
        private WeaponSet _doubleAssultRifleSet;
        private WeaponSet _doubleUziSet;
        private ChoiceUI _armorPiercingWeaponChoice;
        private ChoiceUI _proxMineWeaponChoice;
        private ChoiceUI _droneWeaponChoice;
        private WeaponSet _shotgunSet;
        private WeaponSet _assultRifleSet;
        private WeaponSet _carbineSet;
        private WeaponSet _uziSet;

        private ClickUI _clickUi;
        private ChoiceUI _activeChoice;

        public void Init()
        {
            Sound.Music("customizing").Play();
            _clickUi = new ClickUI();
            _classChoice = new ChoiceUI("Choose Class", 
                    new OptionUI(
                        MeleeClassName, 
                        new Vector2(225, 100),
                        () => SwitchChoices(_meleePerkChoice),
                        CreateLargeDescriptionLabel(MeleeClassDescrption)),
                    new OptionUI(
                        GunClassName,
                        new Vector2(625, 100), 
                        () => SwitchChoices(_gunPerkChoice),
                        CreateLargeDescriptionLabel(GunClassDescription)),
                    new OptionUI(
                        DroneClassName,
                        new Vector2(1025, 100),
                        () => SwitchChoices(_dronePerkChoice),
                        CreateLargeDescriptionLabel(DroneClassDescription)));
            _meleePerkChoice = CreatePerkChoice(
                new Perk(NinjaPerkName, NinjaPerkDescription, () => SwitchChoices(_ninjaWeaponChoice)),
                new Perk(TankPerkName, TankPerkDescription, () => SwitchChoices(_tankWeaponChoice)));
            _gunPerkChoice = CreatePerkChoice(
                new Perk(DualWieldPerkName, DualWeildPerkDescription, () => SwitchChoices(_dualWieldWeaponChoice)),
                new Perk(ArmorPiercingPerkName, ArmorPiercingPerkDescription, () => SwitchChoices(_armorPiercingWeaponChoice)));
            _dronePerkChoice = CreatePerkChoice(
                new Perk(ProxMinePerkName, ProxMinePerkDescription, () => SwitchChoices(_proxMineWeaponChoice)),
                new Perk(DronePerkName, DronePerkDescription, () => SwitchChoices(_droneWeaponChoice)));
            _katanaSet = new WeaponSet("Lethal", new PowerKatana(), new PowerMagnum());
            _wakizashiSet = new WeaponSet("Speed", new PowerWakizashi(), new AutoPistol());
            _bloodKatanaSet = new WeaponSet("Fear", new NaniteEdgedKatana(), new PowerMagnum());
            _riotShieldSet = new WeaponSet("Unstoppable", new PowerShieldAndBaton(), new WristShotgun());
            _stunBatonSet = new WeaponSet("Shock & Awe", new StunStick(), new AutoPistol());
            _hammerSet = new WeaponSet("Strength", new PowerHammer(), new PowerMagnum());
            _doubleShotgunSet = new WeaponSet("Death", new DoubleShotgun(), new DoubleMagnum());
            _doubleAssultRifleSet = new WeaponSet("Sharpshooter", new DoubleAssultRifle(), new DoubleAutoPistol());
            _doubleUziSet = new WeaponSet("Raining Bullets", new DoubleUzi(), new DoubleMagnum());
            _shotgunSet = new WeaponSet("Power", new WarShotgun(), new FiliBlade());
            _assultRifleSet = new WeaponSet("Balance", new RsxAssultRifle(), new WristShotgun());
            _carbineSet = new WeaponSet("Precision", new RsxCarbine(), new AutoPistol());
            _uziSet = new WeaponSet("Full-Auto", new WarUzi(), new FiliBlade());
            _ninjaWeaponChoice = CreateWeaponChoice(_meleePerkChoice, _katanaSet, _wakizashiSet, _bloodKatanaSet);
            _tankWeaponChoice = CreateWeaponChoice(_meleePerkChoice, _riotShieldSet, _stunBatonSet, _hammerSet);
            _dualWieldWeaponChoice = CreateWeaponChoice(_gunPerkChoice, _doubleShotgunSet, _doubleAssultRifleSet, _doubleUziSet);
            _armorPiercingWeaponChoice = CreateWeaponChoice(_gunPerkChoice, _carbineSet, _assultRifleSet, _uziSet);
            _proxMineWeaponChoice = CreateWeaponChoice(_dronePerkChoice, _shotgunSet, _carbineSet, _assultRifleSet);
            _droneWeaponChoice = CreateWeaponChoice(_dronePerkChoice, _shotgunSet, _carbineSet, _uziSet);

            // TODO: Difficulty settings.

            _activeChoice = _classChoice;
            _clickUi.Add(_classChoice.Branch);
        }

        public void Update(TimeSpan delta)
        {
            _clickUi.Update(delta);
        }

        public void Draw()
        {
            _activeChoice.Draw(Transform2.Zero);
        }

        public void Dispose() {}

        private void SwitchChoices(ChoiceUI newChoice)
        {
            _clickUi.Remove(_activeChoice.Branch);
            _activeChoice = newChoice;
            _clickUi.Add(_activeChoice.Branch);
        }

        private ChoiceUI CreatePerkChoice(Perk perk1, Perk perk2)
        {
            return new ChoiceUI("Choose Perk", () => SwitchChoices(_classChoice),
                CreatePerkOption(perk1, new Vector2(425, 100)),
                CreatePerkOption(perk2, new Vector2(825, 100)));
        }

        private OptionUI CreatePerkOption(Perk perk, Vector2 offset)
        {
            return new OptionUI(
                perk.Name,
                offset,
                perk.OnClick,
                CreateLargeDescriptionLabel(perk.Description));
        }

        private Label CreateLargeDescriptionLabel(string description)
        {
            return new Label
            {
                TextColor = Color.White,
                Transform = new Transform2(new Vector2(25, 100), new Size2(300, 500)),
                Text = description
            };
        }

        private ChoiceUI CreateWeaponChoice(ChoiceUI previousChoiceUi, WeaponSet weaponSet1, WeaponSet weaponSet2, WeaponSet weaponSet3)
        {
            return new ChoiceUI("Choose Weapon Set", () => SwitchChoices(previousChoiceUi),
                CreateWeaponOption(weaponSet1, new Vector2(225, 100)),
                CreateWeaponOption(weaponSet2, new Vector2(625, 100)),
                CreateWeaponOption(weaponSet3, new Vector2(1025, 100)));
        }

        private OptionUI CreateWeaponOption(WeaponSet weaponSet, Vector2 offset)
        {
            return new OptionUI(
                weaponSet.Name, 
                offset,
                () => {},
                new Label { TextColor = Color.White, Text = weaponSet.Primary.Name, Font = "Fonts/16",
                    Transform = new Transform2(new Vector2(25, 75), new Size2(300, 50)) },
                new UiImage { Image = weaponSet.Primary.Image, Transform = new Transform2(new Vector2(50, 125), new Size2(250, 100)) },
                new Label { TextColor = Color.White, Text = weaponSet.Primary.ShortDescription, Font = "Fonts/12",
                    Transform = new Transform2(new Vector2(25, 225), new Size2(300, 50)) },
                new Label { TextColor = Color.White, Text = weaponSet.Secondary.Name, Font = "Fonts/16",
                    Transform = new Transform2(new Vector2(25, 300), new Size2(300, 50)) },
                new UiImage { Image = weaponSet.Secondary.Image, Transform = new Transform2(new Vector2(50, 350), new Size2(250, 100)) },
                new Label { TextColor = Color.White, Text = weaponSet.Secondary.ShortDescription, Font = "Fonts/12",
                    Transform = new Transform2(new Vector2(25, 450), new Size2(300, 50)) });
        }

        private class Perk
        {
            public string Name { get; }
            public string Description { get; }
            public Action OnClick { get; }

            public Perk(string name, string description, Action onClick)
            {
                Name = name;
                Description = description;
                OnClick = onClick;
            }
        }

        private class WeaponSet
        {
            public string Name { get; }
            public Weapon Primary { get; }
            public Weapon Secondary { get; }

            public WeaponSet(string name, Weapon primary, Weapon secondary)
            {
                Name = name;
                Primary = primary;
                Secondary = secondary;
            }
        }
    }
}
