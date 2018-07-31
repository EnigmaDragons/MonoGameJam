using MonoDragons.Core.IO;
using ZeroFootPrintSociety.Characters.Creator;

namespace ZeroFootPrintSociety.IO
{
    public static class CurrentSave
    {
        private static int _slot = 0;
        private static SaveFileData _currentData;
        private static AppDataJsonIo _appDataJsonIo = new AppDataJsonIo("ZeroFootprintSociety");

        public static int Slot => _slot;
        public static SaveFileData TheSave => _currentData;

        public static void NewGame(int slotChosen)
        {
            _currentData = new SaveFileData();
        }

        public static void Commit(WeaponSet weaponSet)
        {
            _currentData.WeaponSet = weaponSet;
        }

        public static void Commit(Perk perk)
        {
            _currentData.Perk = perk;
        }

        public static void Commit(int podometer)
        {
            _currentData.FootprintsLeft = podometer;
        }

        public static void Save()
        {
            _appDataJsonIo.Save($"{_slot}", TheSave);
        }

        public static bool HasSave(int? slot = null)
        {
            return _appDataJsonIo.HasSave($"{slot ?? _slot}");
        }

        public static void Load(int? slot = null)
        {
            if (HasSave(slot))
            {
                _currentData = _appDataJsonIo.Load<SaveFileData>($"{slot ?? _slot}");
            }
        }

        public static void Clear()
        {
            _currentData = null;
        }
    }
}
