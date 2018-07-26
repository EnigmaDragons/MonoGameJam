using System;

namespace MonoDragons.Core.AudioSystem
{
    public class MasterVolume
    {
        public static readonly MasterVolume Instance = new MasterVolume();

        private float _musicVolume = 1;
        private float _soundEffectVolume = 1;
        private float _ambientVolume = 1;

        public float MusicVolume
        {
            get => _musicVolume;
            set => _musicVolume = Math.Min(1, Math.Max(0, value));
        }

        public float SoundEffectVolume
        {
            get => _soundEffectVolume;
            set => _soundEffectVolume = Math.Min(1, Math.Max(0, value));
        }

        public float AmbienceVolume
        {
            get => _ambientVolume;
            set => _ambientVolume = Math.Min(1, Math.Max(0, value));
        }

        public float this[SoundType type]
        {
            get
            {
                if (type == SoundType.Music)
                    return MusicVolume;
                if (type == SoundType.Ambient)
                    return AmbienceVolume;
                return SoundEffectVolume;
            }
        }
    }
}
