using Microsoft.Xna.Framework;
using System;

namespace MonoDragons.Core.UserInterface
{
    public abstract class ClickableUIElement
    {
        public abstract void OnEntered();
        public abstract void OnExitted();
        public abstract void OnPressed();
        public abstract void OnReleased();
        
        public float Scale { get; }
        public bool IsEnabled { get; set; }
        public bool IsHovered { get; set; }
        private Vector2 parentLocation;
        public Vector2 ParentLocation
        {
            get { return parentLocation; }
            set
            {
                parentLocation = value;
                TotalArea = new Rectangle(Area.Location + offset.ToPoint() + parentLocation.ToPoint(), Area.Size);
            }
        }
        private Vector2 offset;
        public Vector2 Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                TotalArea = new Rectangle(Area.Location + offset.ToPoint() + parentLocation.ToPoint(), Area.Size);
            }
        }
        public Rectangle Area { get; }
        public Rectangle TotalArea { get; private set; } 

        public string TooltipText { get; set; }

        protected ClickableUIElement(Rectangle area, bool isEnabled = true, float scale = 1)
        {
            TooltipText = "";
            Area = new Rectangle((int)Math.Round(area.X * scale), (int)Math.Round(area.Y * scale),
                (int)Math.Round(area.Width * scale), (int)Math.Round(area.Height * scale));
            Scale = scale;
            ParentLocation = Vector2.Zero;
            Offset = Vector2.Zero;
            IsEnabled = isEnabled;
            IsHovered = false;
        }
    }
}
