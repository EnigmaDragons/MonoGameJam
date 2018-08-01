using System.Collections.Generic;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Soundtrack
{
    public sealed class LevelMusic
    {
        private readonly DictionaryWithDefault<MusicType, string> _songs;
        private readonly DictionaryWithDefault<MusicType, float> _volumes;
        private readonly List<Sound> _sounds = new List<Sound>();

        public LevelMusic(string ambientSongName, string actionSongName, string bossSongName, float actionMusicVolume = 0.6f)
        {
            _songs = new DictionaryWithDefault<MusicType, string>(ambientSongName)
            {
                {MusicType.Action, actionSongName},
                {MusicType.Boss, bossSongName}
            };
            _volumes = new DictionaryWithDefault<MusicType, float>(0.6f)
            {
                {MusicType.Action, actionMusicVolume},
                {MusicType.Boss, actionMusicVolume}
            };
        }

        public void Play(MusicType musicType)
        {
            var music = Sound.Music(_songs[musicType], _volumes[musicType]);
            music.Play();
            _sounds.Add(music);
        }
    }
}