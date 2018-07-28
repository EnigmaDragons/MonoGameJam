
namespace ZeroFootPrintSociety.Characters.Gear
{
    public abstract class Weapon
    {
        public abstract string Name { get; }
        public abstract string Image { get; }
        public abstract bool IsRanged { get; }
    }
}
