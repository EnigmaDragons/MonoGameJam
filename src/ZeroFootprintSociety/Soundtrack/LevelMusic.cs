using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;

namespace ZeroFootPrintSociety.Soundtrack
{
    public sealed class LevelMusic
    {
        private readonly DictionaryWithDefault<MusicType, string> _songs;

        public LevelMusic(string ambientSongName)
        {
            _songs = new DictionaryWithDefault<MusicType, string>(ambientSongName);
        }

        public void Play(MusicType musicType)
        {
            Sound.Music(_songs[musicType]).Play();
        }
    }
}