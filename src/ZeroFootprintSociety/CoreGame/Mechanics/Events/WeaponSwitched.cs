using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    class WeaponSwitched
    {
        public Character Character { get; set; }

        public WeaponSwitched(Character character)
        {
            Character = character;
        }
    }
}
