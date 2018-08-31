using System;
using Microsoft.Xna.Framework;

using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.Graphics
{
    public sealed class BobbingEffect : IAutomaton
    {
        private static readonly int[] DefaultEffectPairs =
        {
            0, 0, -1, 0, -1, -1, -1, -2, -2, -2, -2, -3, -2, -4, -2, -5, -2, -5, -2, -4, -2, -3, -2, -2, -1, -2, -1, -1,
            -1, 0, 0, 0, 1, 0, 1, -1, 1, -2, 2, -2, 2, -3, 2, -4, 2, -5, 2, -4, 2, -3, 2, -2, 1, -2, 1, -1, 1, 0
        };

        private readonly int _framesPerIndexerIncrement;
        private int _index;
        private int _frame = 0;
        private readonly Vector2[] _bobbingEffect;

        public BobbingEffect(int framesPerChange = 12)
        {
            _index = Rng.Int(DefaultEffectPairs.Length / 2);
            _framesPerIndexerIncrement = framesPerChange;
            _bobbingEffect = new Vector2[DefaultEffectPairs.Length / 2];
            for (var i = 0; i * 2 < DefaultEffectPairs.Length; i++)
                _bobbingEffect[i] = new Vector2(DefaultEffectPairs[i * 2], DefaultEffectPairs[i * 2 + 1]);
        }

        public void Update(TimeSpan delta)
        {
            if (++_frame == _framesPerIndexerIncrement)
            {
                _frame = 0;
                _index = ++_index < _bobbingEffect.Length ? _index : 0;
            }
        }

        public void Draw(IVisual visual, Transform2 parentTransform)
        {
            visual.Draw(parentTransform + _bobbingEffect[_index]);
        }
    }
}