using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MonoDragons.Core.AudioSystem
{
    public class SmartSample : ISampleProvider, IDisposable
    {
        private readonly MasterVolume _volume;
        private readonly Sound _sound;
        private readonly AudioFileReader _reader;
        private readonly VolumeSampleProvider _volumeProvider;
        private readonly MeteringSampleProvider _preVolumeMeter;

        public WaveFormat WaveFormat { get; }

        public SmartSample(MasterVolume volume, Sound sound, string fileName) : this(volume, sound, new AudioFileReader(fileName)) { }

        public SmartSample(MasterVolume volume, Sound sound, AudioFileReader reader)
        {
            _volume = volume;
            _sound = sound;
            _reader = reader;

            var sampleProvider = ConvertWaveProviderIntoSampleProvider(_reader);
            if (sampleProvider.WaveFormat.Channels == 1)
                sampleProvider = new MonoToStereoSampleProvider(sampleProvider);
            WaveFormat = sampleProvider.WaveFormat;
            _preVolumeMeter = new MeteringSampleProvider(sampleProvider);
            _volumeProvider = new VolumeSampleProvider(_preVolumeMeter);
        }

        public event EventHandler<StreamVolumeEventArgs> PreVolumeMeter
        {
            add { _preVolumeMeter.StreamVolume += value; }
            remove { _preVolumeMeter.StreamVolume -= value; }
        }

        public int Read(float[] buffer, int offset, int sampleCount)
        {
            if (!_sound.IsPlaying)
                return 0;
            _volumeProvider.Volume = _sound.Mute ? 0 : _sound.Volume * _volume[_sound.Type];
            var read = _volumeProvider.Read(buffer, offset, sampleCount);
            if (read < sampleCount)
            {
                _reader.Position = 0;
                if (_sound.Looping)
                    read = _volumeProvider.Read(buffer, offset, sampleCount);
                else
                    _sound.IsPlaying = false;
            }
            return read;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public void Reset()
        {
            _reader.Position = 0;
        }

        private ISampleProvider ConvertWaveProviderIntoSampleProvider(IWaveProvider waveProvider)
        {
            if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                if (waveProvider.WaveFormat.BitsPerSample == 8)
                    return new Pcm8BitToSampleProvider(waveProvider);
                if (waveProvider.WaveFormat.BitsPerSample == 16)
                    return new Pcm16BitToSampleProvider(waveProvider);
                if (waveProvider.WaveFormat.BitsPerSample == 24)
                    return new Pcm24BitToSampleProvider(waveProvider);
                if (waveProvider.WaveFormat.BitsPerSample == 32)
                    return new Pcm32BitToSampleProvider(waveProvider);
                throw new InvalidOperationException("Unsupported bit depth");
            }
            if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                throw new ArgumentException("Unsupported source encoding");
            if (waveProvider.WaveFormat.BitsPerSample == 64)
                return new WaveToSampleProvider64(waveProvider);
            return new WaveToSampleProvider(waveProvider);
        }
    }
}
