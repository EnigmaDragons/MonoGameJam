using System;

namespace MonoDragons.Core.AudioSystem
{
    public class Sound : IDisposable
    {
        private readonly SmartSample _smartSample;

        private float _volume;

        public bool IsPlaying { get; set; } = false;
        public bool Looping { get; set; }
        public bool Mute { get; set; }
        public SoundType Type { get; }

        public static Sound Music(string fileName, float volume = 1) => new Sound(fileName, true, false, volume, SoundType.Music);
        public static Sound Ambient(string fileName, float volume = 1) => new Sound(fileName, true, false, volume, SoundType.Ambient);
        public static Sound SoundEffect(string fileName, float volume = 1) => new Sound(fileName, false, false, volume, SoundType.Effect);

        public Sound(string fileName, bool looping, bool mute, float volume, SoundType type)
        {
            _smartSample = new SmartSample(MasterVolume.Instance, this, fileName);
            Looping = looping;
            Mute = mute;
            Volume = volume;
            Type = type;
        }

        public float Volume
        {
            get => _volume;
            set => _volume = Math.Min(1, Math.Max(0, value));
        }

        public void Play()
        {
            if (!IsPlaying)
                AudioPlayer.Instance.Play(_smartSample);
            AudioDirector.Instance.Play(this);
        }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Reset()
        {
            _smartSample.Reset();
        }

        public void Dispose()
        {
            _smartSample.Dispose();
        }
    }
}
