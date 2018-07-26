using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MonoDragons.Core.AudioSystem
{
    public interface IAudioPlayer : IDisposable
    {
        void Play(ISampleProvider samples);
        void StopAll();
    }

    public class AudioPlayer : IAudioPlayer
    {
        public static readonly IAudioPlayer Instance = GetAudioPlayer();
        private static IAudioPlayer GetAudioPlayer()
        {
            try { return new AudioPlayer(); }
            catch { return new NullAudioPlayer(); }
        }

        private readonly IWavePlayer _player;
        private readonly MixingSampleProvider _mixer;

        public AudioPlayer(int sampleRate = 44100)
        {
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2)) {ReadFully = true};
            _player = new WaveOutEvent();
            _player.Init(_mixer);
            _player.Play();
        }

        public void Play(ISampleProvider samples)
        {
            _mixer.AddMixerInput(samples);
        }

        public void StopAll()
        {
            _mixer.RemoveAllMixerInputs();
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }

    internal class NullAudioPlayer : IAudioPlayer
    {
        public void Dispose() {}
        public void Play(ISampleProvider samples) {}
        public void StopAll() {}
    }
}
