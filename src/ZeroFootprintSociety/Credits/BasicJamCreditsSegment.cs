using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.Common;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.GUI;

namespace ZeroFootPrintSociety.Credits
{
    public abstract class BasicJamCreditSegment : IAnimation
    {
        private readonly List<HorizontalFlyInAnimation> _elements = new List<HorizontalFlyInAnimation>();

        private int _countdown;

        public abstract string Role { get; }
        public abstract string Name { get; }

        public void Update(TimeSpan delta)
        {
            _elements.ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _elements.ForEach(x => x.Draw(parentTransform));
        }

        public void Start(Action onFinished)
        {
            var yStart = Rng.Int(0.15.VH(), 0.68.VH());

            _elements.Add(new HorizontalFlyInAnimation(
                new Label
                {
                    Text = Role,
                    Transform = new Transform2(new Vector2(-800, yStart), new Size2(800, 100)),
                    Font = GuiFonts.Header
                }));
            _elements.Add(new HorizontalFlyInAnimation(
                new Label
                {
                    Text = Name,
                    Transform = new Transform2(new Vector2(1920, yStart + 135), new Size2(800, 75))
                })
            { FromDir = HorizontalDirection.Right, ToDir = HorizontalDirection.Left });

            _countdown = _elements.Count;
            _elements.ForEach(x => x.Start(() => FinishedOne(onFinished)));
        }

        private void FinishedOne(Action onFinished)
        {
            _countdown--;
            if (_countdown == 0)
                onFinished();
        }
    }
}
