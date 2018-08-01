using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.AudioSystem
{
    public class AudioDirector
    {
        public static readonly AudioDirector Instance = new AudioDirector();

        private List<Sound> _soundEffects = new List<Sound>();
        private List<Sound> _ambience = new List<Sound>();
        private List<Sound> _musics = new List<Sound>();

        public int MaxSoundEffects { get; set; } = 9;
        public int MaxAmbience { get; set; } = 2;
        public int MaxMusic { get; set; } = 1;

        public void Play(Sound sound)
        {
            if (sound.Type == SoundType.Effect)
                _soundEffects = PlaySound(sound, _soundEffects, MaxSoundEffects);
            if (sound.Type == SoundType.Ambient)
                _ambience = PlaySound(sound, _ambience, MaxAmbience);
            if (sound.Type == SoundType.Music)
                _musics = PlaySound(sound, _musics, MaxMusic);
        }

        private List<Sound> PlaySound(Sound sound, List<Sound> category, int max)
        {
            var copy = category.ToList();
            if (copy.Contains(sound))
                copy.Remove(sound);
            copy.Add(sound);
            sound.IsPlaying = true;
            while (copy.Count(x => x.IsPlaying) > max)
            {
                var effect = copy.First(x => x.IsPlaying);
                effect.Pause();
                effect.Reset();
            }
            return copy;
        }
    }
}
