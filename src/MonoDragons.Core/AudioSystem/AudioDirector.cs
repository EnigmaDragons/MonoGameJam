using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.AudioSystem
{
    public class AudioDirector
    {
        public static readonly AudioDirector Instance = new AudioDirector();

        private readonly List<Sound> _soundEffects = new List<Sound>();
        private readonly List<Sound> _ambience = new List<Sound>();
        private readonly List<Sound> _musics = new List<Sound>();

        public int MaxSoundEffects { get; set; } = 9;
        public int MaxAmbience { get; set; } = 2;
        public int MaxMusic { get; set; } = 1;

        public void Play(Sound sound)
        {
            if (sound.Type == SoundType.Effect)
                PlaySound(sound, _soundEffects, MaxSoundEffects);
            if (sound.Type == SoundType.Ambient)
                PlaySound(sound, _ambience, MaxAmbience);
            PlaySound(sound, _musics, MaxMusic);
        }

        private void PlaySound(Sound sound, List<Sound> category, int max)
        {
            if (category.Contains(sound))
                category.Remove(sound);
            category.Add(sound);
            sound.IsPlaying = true;
            while (category.Count(x => x.IsPlaying) > max)
            {
                var effect = category.First(x => x.IsPlaying);
                effect.Pause();
                effect.Reset();
            }
        }
    }
}
