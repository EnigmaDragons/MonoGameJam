using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    internal abstract class CameraControl : IAutomaton
    {
        public int CameraSpeed { get; set; }

        public Point Offset { get; protected set; } = new Point();
        public Func<bool> CustomCanUpdateFunc { get; set; } = () => true;

        public abstract void Update(TimeSpan delta);
        
        /// <summary>
        /// Returns true if you need to break out of the camera controls loop after this one's update is done.
        /// </summary>
        /// <returns></returns>
        public virtual bool TestBreakAfterUpdate() => false;
        
        /// <summary>
        /// Returns true if update can be done.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanUpdate() => true;
    }
}
