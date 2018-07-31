using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.Characters.Creator
{
    public class WeaponSet : ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Primary", this.Primary.GetType().Name );
            
        }
    }
}
