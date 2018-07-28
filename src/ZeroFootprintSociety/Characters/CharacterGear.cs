using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterGear
    {
        private readonly List<Weapon> _weapons = new List<Weapon>(2);

        public IEnumerable<Weapon> Weapons => _weapons.AsEnumerable();
        public Weapon EquippedWeapon => _weapons.First();

        public CharacterGear(Weapon equippedWeapon, Weapon standByWeapon = null)
        {
            _weapons.Add(equippedWeapon);
            _weapons.Add(standByWeapon);
        }

        public void SwitchWeapons()
        {
            _weapons.Reverse();
        }
    }
}
