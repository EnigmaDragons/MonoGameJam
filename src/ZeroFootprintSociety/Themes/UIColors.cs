using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.Tiles;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace ZeroFootPrintSociety.Themes
{
    public static class UIColors
    {
        public static Color Unchanged { get; } = Color.White;

        public static Color HealthBar_Health { get; } = Color.LimeGreen;
        public static Color HealthBar_Damage { get; } = Color.Pink;

        public static Color AttackedHealthBar_Background { get; } = Color.FromNonPremultiplied(66, 66, 66, 255);
        public static Color AttackedHealthBar_HealthRemaining { get; } = Color.FromNonPremultiplied(0, 200, 83, 255);
        public static Color AttackedHealthBar_PredictedDamage { get; } = Color.FromNonPremultiplied(213, 0, 0, 255);

        public static Color AvailableMovesView_Rectangles { get; } = Color.FromNonPremultiplied(255, 255, 255, 150);

        public static Color AvailableTargetsUI_CoverPercentText { get; } = Color.White;

        public static Color AvailableTargetsView_Rectanges { get; } = Color.FromNonPremultiplied(200, 0, 0, 35);
        public static Color AvailableTargetsView_TileRotatingEdgesAnim { get; } = Color.FromNonPremultiplied(255, 20, 20, 255);

        public static Color Buttons_Default { get; } = Color.FromNonPremultiplied(206, 232, 245, 0);
        public static Color Buttons_Hover { get; } = Color.FromNonPremultiplied(206, 232, 245, 70);
        public static Color Buttons_Press { get; } = Color.FromNonPremultiplied(206, 232, 245, 110);
                
        public static Color InGame_Text { get; } = Color.FromNonPremultiplied(255, 255, 255, 180);

        public static Color Gunshot { get; } = Color.White;
        public static Color Gunshot_MissedShot { get; } = Color.White;
        public static Color Gunshot_TargetedShotVisual { get; } = Color.White;
        
        public static Color InGameMenu_FullScreenRectangle { get; } = Color.FromNonPremultiplied(0, 0, 0, 160);

        public static Color MovementPathDirectionsPreview_Tile { get; } = Color.FromNonPremultiplied(255, 255, 255, 80);

        public static Color MovementPathHighlights_Tile { get; } = Color.FromNonPremultiplied(80, 120, 220, 20);
        public static Color MovementPathHighlights_TileRotatingEdgesAnim { get; } = Color.FromNonPremultiplied(110, 170, 255, 255);

        public static Color OverwatchedTiles_Rectangle(int r) => Color.FromNonPremultiplied(r, 0, 0, 100);
        public static Color OverwatchedTiles_Text { get; } = new Color(248,223,223,1);
        public static Color OverwatchedTiles_OverwatchedByCharacter { get; } = Color.FromNonPremultiplied(100, 0, 0, 31);

        public static Color MainMenuScene_Background { get; } = Color.FromNonPremultiplied(0, 0, 0, 100);
        public static Color MainMenuScene_ButtonDefault { get; } = Color.Transparent;
        public static Color MainMenuScene_ButtonHover { get; } = Color.LightBlue;
        public static Color MainMenuScene_ButtonPress { get; } = Color.Blue;

        public static Color GlowEffect_DefaultTint { get; } = Color.FromNonPremultiplied(48, 140, 140, 70);

        public static Color TileFXCollection_Default { get; } = Color.FromNonPremultiplied(255, 255, 0, 120);
        public static Color TileFXCollection_Blue { get; } = Color.FromNonPremultiplied(30, 90, 230, 60);
        public static Color TileFXCollection_Turquoise { get; } = Color.FromNonPremultiplied(60, 200, 255, 80);
        public static Color TileFXCollection_Red { get; } = Color.FromNonPremultiplied(255, 0, 0, 60);
    }
}
