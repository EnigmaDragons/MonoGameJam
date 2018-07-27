using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class SpriteAnimation : IAutomaton, IVisual
    {
        private readonly List<SpriteAnimationFrame> _frames;
        private double _secondsRemaining;
        private int _index;

        public SpriteAnimation(params SpriteAnimationFrame[] frames)
        {
            _frames = frames.ToList();
        }

        public void Update(TimeSpan delta)
        {
            _secondsRemaining -= delta.TotalSeconds;
            if (_secondsRemaining <= 0)
            {
                _index++;
                if (_index == _frames.Count)
                    _index = 0;
                _secondsRemaining = _frames[_index].DurationInSeconds;
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            _frames[_index].Draw(parentTransform);
        }

        public void Reset()
        {
            _secondsRemaining = _frames[0].DurationInSeconds;
            _index = 0;
        }
    }
}
