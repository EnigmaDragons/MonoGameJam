using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Gear;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterGear
    {
        private readonly List<RangedWeapon> _weapons = new List<RangedWeapon>(2);

        public IEnumerable<RangedWeapon> Weapons => _weapons.AsEnumerable();
        public RangedWeapon EquippedWeapon => Weapons.First();

        public CharacterGear(RangedWeapon equippedWeapon, RangedWeapon standByWeapon = null)
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
