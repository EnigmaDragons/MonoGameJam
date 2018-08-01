using System;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Animations
{
    public interface IAnimation : IVisualAutomaton
    {
        void Start(Action onFinished);
    }

    public static class AnimationExtensions
    {
        public static IAnimation Start(this IAnimation animation)
        {
            animation.Start(() => { });
            return animation;
        }
        
        public static IAnimation Started(this IAnimation animation)
        {
            animation.Start();
            return animation;
        }
        
        public static IAnimation Started(this IAnimation animation, Action onFinished)
        {
            animation.Start(onFinished);
            return animation;
        }
    }
}
