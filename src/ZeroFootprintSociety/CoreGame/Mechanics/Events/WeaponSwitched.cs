using ZeroFootPrintSociety.Characters;

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
