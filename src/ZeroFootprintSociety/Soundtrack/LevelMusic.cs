using System;
using MonoDragons.Core;
using MonoDragons.Core.AudioSystem;

using MonoDragons.Core.Engine;

namespace ZeroFootPrintSociety.Soundtrack
{
    public sealed class LevelMusic : IAutomaton
    {
        private const double _crossfadeTransitionSeconds = 5;
        private readonly DictionaryWithDefault<MusicType, Sound> _songs;
        private readonly DictionaryWithDefault<MusicType, float> _volumes;
        private MusicType _currentSound;
        private MusicType _nextSound;

        public LevelMusic(string ambientSongName, string actionSongName, string bossSongName, 
            float ambientMusicVolume = 1, float actionMusicVolume = 0.6f, float bossMusicVolume = 0.6f)
        {
            AudioDirector.Instance.MaxMusic = 3;
            _songs = new DictionaryWithDefault<MusicType, Sound>(Sound.Music(ambientSongName))
            {
                {MusicType.Ambient, Sound.Music(ambientSongName, 0)},
                {MusicType.Action, Sound.Music(actionSongName, 0)},
                {MusicType.Boss, Sound.Music(bossSongName, 0)}
            };
            _volumes = new DictionaryWithDefault<MusicType, float>(0.6f)
            {
                {MusicType.Ambient, ambientMusicVolume},
                {MusicType.Action, actionMusicVolume},
                {MusicType.Boss, bossMusicVolume}
            };
            _currentSound = MusicType.Boss;
        }

        public void Play(MusicType musicType)
        {
            _nextSound = musicType;
            _songs[_nextSound].Volume = 0.01f;
            _songs[_nextSound].Play();
        }

        public void Update(TimeSpan delta)
        {
            if (_nextSound != _currentSound)
            {
                _songs[_currentSound].Volume -= (float)Math.Min(
                    _songs[_currentSound].Volume, 
                    _volumes[_currentSound] * delta.TotalSeconds / _crossfadeTransitionSeconds);
                _songs[_nextSound].Volume += (float)Math.Min(
                    _volumes[_currentSound] - _songs[_currentSound].Volume,
                    _volumes[_currentSound] * delta.TotalSeconds / _crossfadeTransitionSeconds);
                if (_songs[_currentSound].Volume <= 0)
                    _currentSound = _nextSound;
            }
            else if (_songs[_currentSound].Volume < _volumes[_currentSound])
                _songs[_currentSound].Volume += (float)Math.Min(
                    _volumes[_currentSound] - _songs[_currentSound].Volume,
                    _volumes[_currentSound] * delta.TotalSeconds / _crossfadeTransitionSeconds);
        }
    }
}