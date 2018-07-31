using System;
using System.Runtime.Serialization;
using ZeroFootPrintSociety.Characters.Creator;
using ZeroFootPrintSociety.Characters.Gear;

namespace ZeroFootPrintSociety.IO
{
    public class SaveDataConverter : ISerializationSurrogate
    {
        /// <summary>
        /// Serialization
        /// </summary>
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            SaveFileData data = (SaveFileData) obj;

            info.AddValue("Scene", data.Scene);

            
            info.AddValue("primaryWeapon", data.WeaponSet.Primary.GetType());
            info.AddValue("secondaryWeapon", data.WeaponSet.Secondary.GetType());
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var SaveData = (SaveFileData) obj;

            SaveData.Scene = info.GetString("Scene");

            info.GetString("PrimaryWeapon")

            SaveData.WeaponSet = new WeaponSet(info.GetString("WeaponSetName"), new (info.GetString("PrimaryWeapon") );

            
        }
    }
}
