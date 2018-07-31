
using ZeroFootPrintSociety.Characters.Creator;

namespace ZeroFootPrintSociety.IO
{
    public class SaveFileData 
    {
        public string Scene { get; set; }
        public WeaponSet WeaponSet { get; set; }
        public Perk Perk { get; set; }
        public int FootprintsLeft { get; set; }

        /// <summary>
        /// Makes a clean save file, from the character creation screen.
        /// </summary>
        public SaveFileData()
        {
            Scene = "CharacterCreation";
            Perk = null;
            WeaponSet = null;
            FootprintsLeft = 500; // TODO: Change footprints left data.
        }

        public SaveFileData(string sceneName, SaveFileData previousData)
        {
            Scene = sceneName;
            WeaponSet = previousData.WeaponSet;
        }
    }
}
