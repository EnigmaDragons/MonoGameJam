using System.Collections.Generic;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Soundtrack
{
    public sealed class LevelMusic
    {
        private readonly DictionaryWithDefault<MusicType, string> _songs;
        private readonly List<Sound> _sounds = new List<Sound>();

        public LevelMusic(string ambientSongName)
        {
            _songs = new DictionaryWithDefault<MusicType, string>(ambientSongName);
        }

        public LevelMusic(string ambientSongName, string actionSongName, string bossSongName)
        {
            _songs = new DictionaryWithDefault<MusicType, string>(ambientSongName)
            {
                {MusicType.Action, actionSongName},
                {MusicType.Boss, bossSongName}
            };
        }

        public void Play(MusicType musicType)
        {
            var music = Sound.Music(_songs[musicType]);
            music.Play();
            _sounds.Add(music);
        }
    }
}