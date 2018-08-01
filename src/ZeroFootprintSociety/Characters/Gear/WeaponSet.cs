namespace ZeroFootPrintSociety.Characters.Gear
{
    public class WeaponSet
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