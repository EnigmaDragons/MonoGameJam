using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Diagnostics;

namespace MonoDragons.Core.Development
{
    public sealed class Metrics : IVisualAutomaton
    {
        private readonly Timer _timer;

        private int _framesThisSecond;
        private int _updatesThisSecond;
        private int _framesPerSecond;
        private int _updatesPerSecond;
        private int _frameRateTroubleCount;

        public Metrics()
        {
            _timer = new Timer(AccumulateMetrics, 500);
        }

        public void Update(TimeSpan delta)
        {
            _timer.Update(delta);
            _updatesThisSecond++;
        }

        public void Draw(Transform2 parentTransform)
        {
            var color = DevText.Color;
            var font = DevText.Font;
            UI.DrawText($"FPS: {_framesPerSecond}", new Vector2(0, 0), color, font);
            UI.DrawText($"UPS: {_updatesPerSecond}", new Vector2(0, 22), color, font);
            UI.DrawText($"Sub: {Event.SubscriptionCount}", new Vector2(0, 44), color, font);
            UI.DrawText($"Scn: {Resources.CurrentSceneResourceCount}", new Vector2(0, 66), color, font);
            _framesThisSecond++;
        }

        private void AccumulateMetrics()
        {
            _framesPerSecond = _framesThisSecond * 2;
            _framesThisSecond = 0;
            _updatesPerSecond = _updatesThisSecond * 2;
            _updatesThisSecond = 0;
            CheckForProcessTrouble();
        }

        private void CheckForProcessTrouble()
        {
            _frameRateTroubleCount = _framesPerSecond < 12 ? _frameRateTroubleCount + 1 : 0;
            if (_framesPerSecond < 12)
                Debug.WriteLine("Framerate Warning: Framerate = " + _framesPerSecond);
            if (_frameRateTroubleCount > 10)
            {
                Debug.WriteLine("Framerate Exception: Exiting Program.");
                CurrentGame.TheGame.Exit();
            }
        }
    }
}