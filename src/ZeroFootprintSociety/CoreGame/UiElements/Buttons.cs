using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    static class Buttons
    {
        private const int _buttonWidth = 200;
        private const int _buttonHeight = 35;
        private const int _buttonMargin = 10;

        public class MenuContext
        {
            public int Width { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int FirstButtonYOffset { get; set; }
        }

        public static TextButton Text(MenuContext ctx, int index, string text, Action action, Func<bool> condition)
        {
            return new TextButton(
                new Rectangle(
                    ctx.X + (ctx.Width - _buttonWidth) / 2,
                    ctx.Y + ctx.FirstButtonYOffset + ((_buttonMargin + _buttonHeight) * index),
                    _buttonWidth,
                    _buttonHeight),
                action,
                text,
                UIColors.Buttons_Default,
                UIColors.Buttons_Hover,
                UIColors.Buttons_Press,
                condition)
            { Font = "Fonts/12" };
        }
    }
}
