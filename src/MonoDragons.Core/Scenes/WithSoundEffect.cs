using MonoDragons.Core.AudioSystem;
using System;

namespace MonoDragons.Core.Scenes
{
    public sealed class WithSoundEffect : IScene
    {
        private readonly IScene _inner;
        private readonly Sound _sound;

        public WithSoundEffect(IScene inner, Sound sound)
        {
            _inner = inner;
            _sound = sound;
        }

        public void Dispose() => _inner.Dispose();
        public void Draw() => _inner.Draw();
        public void Update(TimeSpan delta) => _inner.Update(delta);

        public void Init()
        {
            _inner.Init();
            _sound.Play();
        }
    }
}
