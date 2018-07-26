using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Text;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Render;

namespace MonoDragons.Core.UserInterface
{
    public static class UI
    {
        private static readonly Dictionary<string, bool> _scaledFontAvailability = new Dictionary<string, bool>();
        private static readonly ColoredRectangle _darken;

        private static readonly Dictionary<HorizontalAlignment, Func<Rectangle, Vector2, Vector2>> _alignPositions = new Dictionary<HorizontalAlignment, Func<Rectangle, Vector2, Vector2>>
        {
            { HorizontalAlignment.Left, GetLeftPosition },
            { HorizontalAlignment.Center, GetCenterPosition },
            { HorizontalAlignment.Right, GetRightPosition },
        };

        static UI()
        {
            _darken = new ColoredRectangle
            {
                Color = Color.FromNonPremultiplied(0, 0, 0, 92),
                Transform = new Transform2(new Size2(1920, 1080))
            };
        }

        private static SpriteBatch _spriteBatch;

        public static void Init(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public static int OfScreenWidth(float part)
        {
            return (int)Math.Round(part * CurrentDisplay.GameWidth / CurrentDisplay.Scale);
        }

        public static int OfScreenHeight(float part)
        {
            return (int)Math.Round(part * CurrentDisplay.GameHeight / CurrentDisplay.Scale);
        }

        public static Vector2 OfScreen(float xFactor, float yFactor)
        {
            return new Vector2(OfScreenWidth(xFactor), OfScreenHeight(yFactor));
        }

        public static Size2 OfScreenSize(float xFactor, float yFactor)
        {
            return new Size2(OfScreenWidth(xFactor), OfScreenHeight(yFactor));
        }

        public static void Darken()
        {
            _darken.Draw(Transform2.Zero);
        }

        public static void Darken(int amount)
        {
            new ColoredRectangle{ Color = Color.FromNonPremultiplied(0, 0, 0, amount), Transform = new Transform2(new Size2(1920, 1080)) }
                .Draw(Transform2.Zero);
        }

        public static void DrawBackgroundColor(Color color)
        {
            CurrentGame.GraphicsDevice.Clear(color);
        }

        public static void FillScreen(string imageName)
        {
            DrawCenteredWithOffset(imageName, new Vector2(CurrentDisplay.GameWidth / CurrentDisplay.Scale, CurrentDisplay.GameHeight / CurrentDisplay.Scale), Vector2.Zero);
        }

        public static void DrawCentered(string imageName, float scale = 1)
        {
            DrawCenteredWithOffset(imageName, Vector2.Zero, scale);
        }

        public static void DrawCentered(string imageName, Vector2 widthHeight)
        {
            DrawCenteredWithOffset(imageName, widthHeight, Vector2.Zero);
        }
        
        public static void DrawCentered(string imageName, Transform2 transform)
        {
            DrawCenteredWithOffset(imageName, transform.ToRectangle().Size.ToVector2(), transform.Location);
        }

        public static void DrawCenteredWithOffset(string imageName, Vector2 offSet, float scale = 1)
        {
            var texture = Resources.Load<Texture2D>(imageName);
            DrawCenteredWithOffset(imageName, new Vector2(texture.Width * scale, texture.Height * scale), offSet);
        }

        public static void DrawCenteredWithOffset(string imageName, Vector2 widthHeight, Vector2 offSet)
        {
            _spriteBatch.Draw(Resources.Load<Texture2D>(imageName), null,
                new Rectangle(ScalePoint(CurrentDisplay.GameWidth / 2 / CurrentDisplay.Scale - widthHeight.X / 2 + offSet.X,
                    CurrentDisplay.GameHeight / 2 / CurrentDisplay.Scale - widthHeight.Y / 2 + offSet.Y),
                    ScalePoint(widthHeight.X, widthHeight.Y)),
                null, null, 0, new Vector2(1, 1));
        }

        public static void DrawText(string text, Vector2 position, Color color)
        {
            if (DefaultFont.ScaledFontSet.Contains(CurrentDisplay.Scale))
                _spriteBatch.DrawString(DefaultFont.ScaledFontSet[CurrentDisplay.Scale], text, ScalePoint(position.X, position.Y).ToVector2(), color,
                    0, Vector2.Zero, 1, SpriteEffects.None, 1);
            else
                _spriteBatch.DrawString(DefaultFont.ScaledFontSet.DefaultFont, text, ScalePoint(position.X, position.Y).ToVector2(), color,
                    0, Vector2.Zero, CurrentDisplay.Scale, SpriteEffects.None, 1);
        }

        public static void DrawText(string text, Vector2 position, Color color, string font)
        {
            if (CurrentDisplay.Scale == 1)
                DrawUnscaledString(text, position, color, font);
            else DrawStringScalingSpriteBatchIfNeeded(text, position, color, font);
        }

        private static void DrawUnscaledString(string text, Vector2 position, Color color, string font)
        {
            _spriteBatch.DrawString(Resources.Load<SpriteFont>(font), text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        private static void DrawStringScalingSpriteBatchIfNeeded(string text, Vector2 position, Color color, string font)
        {
            var scaledFontName = font + "-" + CurrentDisplay.Scale.ToString();
            if (_scaledFontAvailability.ContainsKey(scaledFontName))
            {
                if (_scaledFontAvailability[scaledFontName])
                    DrawUnscaledSpriteBatchString(text, position, color, scaledFontName);
                else
                    DrawScaledSpriteBatchString(text, position, color, font);
            }
            else
                try
                {
                    DrawUnscaledSpriteBatchString(text, position, color, scaledFontName);
                    _scaledFontAvailability.Add(scaledFontName, true);
                }
                catch
                {
                    DrawScaledSpriteBatchString(text, position, color, font);
                    _scaledFontAvailability.Add(scaledFontName, false);
                }
        }

        private static void DrawUnscaledSpriteBatchString(string text, Vector2 position, Color color, string scaledFontName)
        {
            _spriteBatch.DrawString(Resources.Load<SpriteFont>(scaledFontName), text, ScalePoint(position.X, position.Y).ToVector2(), color,
                    0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        private static void DrawScaledSpriteBatchString(string text, Vector2 position, Color color, string font)
        {
            _spriteBatch.DrawString(Resources.Load<SpriteFont>(font), text, ScalePoint(position.X, position.Y).ToVector2(), color,
                    0, Vector2.Zero, CurrentDisplay.Scale, SpriteEffects.None, 1);
        }

        public static void DrawTextCentered(string text, Rectangle area, Color color)
        {
            DrawTextCentered(text, area, color, DefaultFont.Name);
        }
        
        public static void DrawTextCentered(string text, Rectangle area, Color color, string font)
        {
            DrawTextAligned(text, area, color, font, HorizontalAlignment.Center);
        }

        public static void DrawTextLeft(string text, Rectangle area, Color color, string font)
        {
            DrawTextAligned(text, area, color, font, HorizontalAlignment.Left);
        }

        public static void DrawTextRight(string text, Rectangle area, Color color, string font)
        {
            DrawTextAligned(text, area, color, font, HorizontalAlignment.Right);
        }

        public static void DrawTextAligned(string text, Rectangle area, Color color, string font, HorizontalAlignment horizontalAlignment)
        {
            var spriteFont = Resources.Load<SpriteFont>(font);
            var wrapped = new WrappingText(() => spriteFont, () => area.Width).Wrap(text);
            var size = spriteFont.MeasureString(wrapped);
            DrawText(wrapped, _alignPositions[horizontalAlignment](area, size), color, font);
        } 

        private static Vector2 GetLeftPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X, area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }

        private static Vector2 GetCenterPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X + (area.Width / 2) - (size.X / 2), area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }

        private static Vector2 GetRightPosition(Rectangle area, Vector2 size)
        {
            return new Vector2(area.Location.X + area.Width - size.X, area.Location.Y + (area.Height / 2) - (size.Y / 2));
        }

        private static Point ScalePoint(float x, float y)
        {
            return new Point((int)Math.Round(x * CurrentDisplay.Scale), (int)Math.Round(y * CurrentDisplay.Scale));
        }
    }
}
