using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.GUI
{
    public class ChatBox : IVisualAutomaton
    {
        public const int FontLineSpacing = -1;

        private readonly int _maxLineWidth;
        private readonly SpriteFont _spriteFont;
        private readonly double _millisToCharacter;
        private string _currentlyDisplayedMessage;
        private readonly int _lineSpacing;
        private string _messageToDisplay;
        private long _totalMessageTime;

        public Vector2 Position { get; set; }
        public Color Color { get; set; } = Color.White;
        
        public ChatBox(string message, int maxLineWidth, SpriteFont spriteFont, double millisToCharacter = 35, int lineSpacing = FontLineSpacing)
        {
            _millisToCharacter = millisToCharacter;
            _lineSpacing = lineSpacing == FontLineSpacing ? spriteFont.LineSpacing : lineSpacing;
            _spriteFont = spriteFont;
            _maxLineWidth = maxLineWidth;
            _currentlyDisplayedMessage = "";
            _messageToDisplay = WrapText(message);
        }

        public bool IsMessageCompletelyDisplayed()
        {
            return _currentlyDisplayedMessage.Length == _messageToDisplay.Length;
        }

        public void ShowMessage(string message)
        {
            _currentlyDisplayedMessage = "";
            _messageToDisplay = WrapText(message);
            _totalMessageTime = 0;
        }

        public void CompletelyDisplayMessage()
        {
            _currentlyDisplayedMessage = _messageToDisplay;
            _totalMessageTime = (int)(_millisToCharacter * _messageToDisplay.Length);
        }

        public void Update(TimeSpan deltaMillis)
        {
            _totalMessageTime += deltaMillis.Milliseconds;
            var length = _millisToCharacter != 0 ? (int)((double)_totalMessageTime / (double)_millisToCharacter): int.MaxValue;
            length = _messageToDisplay.Length < length ? _messageToDisplay.Length : length;
            _currentlyDisplayedMessage = _messageToDisplay.Substring(0, length);
        }

        public void Draw(Transform2 parentTransform)
        {
            _currentlyDisplayedMessage.Split('\n').ForEachIndex((l, i)
                => UI.DrawText(l, new Vector2(parentTransform.Location.X, parentTransform.Location.Y + i * _lineSpacing) + Position, Color));
        }

        private string WrapText(string text)
        {
            var words = text.Split(' ');
            var sb = new StringBuilder();
            var lineWidth = 0f;
            var spaceWidth = _spriteFont.MeasureString(" ").X;
            foreach (var word in words)
            {
                var size = _spriteFont.MeasureString(word);
                if (word == "\n")
                {
                    sb.Append(word);
                    lineWidth = 0;
                }
                else if (word.StartsWith("\n"))
                {
                    sb.Append("\n" + new string(word.Skip(1).ToArray()) + " ");
                    lineWidth = size.X + spaceWidth;
                }
                else if (lineWidth + size.X < _maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
    }
}