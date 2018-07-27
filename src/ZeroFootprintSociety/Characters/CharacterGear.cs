using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters.Gear;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;

namespace ZeroFootPrintSociety.Characters
{
    public class CharacterGear
    {
        private readonly List<GearStats> _weapons = new List<GearStats>(2);

        public IEnumerable<GearStats> Weapons => _weapons.AsEnumerable();
        public GearStats EquippedWeapon => Weapons.First();

        public CharacterGear(GearStats equippedWeapon, GearStats standByWeapon = null)
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
