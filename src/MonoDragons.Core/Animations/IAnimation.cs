using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Animations
{
    public interface IAnimation : IVisualAutomaton
    {
        void Start(Action onFinished);
    }
}
