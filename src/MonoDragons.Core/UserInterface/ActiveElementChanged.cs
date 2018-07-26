using System.Diagnostics;

namespace MonoDragons.Core.UserInterface
{
    public class ActiveElementChanged
    {
        public ClickableUIElement OldElement { get; }
        public ClickableUIElement NewElement { get; }

        internal ActiveElementChanged(ClickableUIElement oldElement)
        {
            OldElement = oldElement;
            NewElement = ClickUI.None;
        }

        internal ActiveElementChanged(ClickableUIElement oldElement, ClickableUIElement newElement)
        {
            OldElement = oldElement;
            NewElement = newElement;
        }
    }
}
