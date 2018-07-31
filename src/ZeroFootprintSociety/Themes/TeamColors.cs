using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Themes
{
    public static class TeamColors
    {
        public static TeamColorTheme Friendly { get; } = new TeamColorTheme()
        {
            Characters_GlowColor = Color.Blue,
            TeamTurnHudDecor_Text = Color.FromNonPremultiplied(196, 233, 246, 176),
            Footprints_GlowColor = Color.FromNonPremultiplied(200, 200, 200, 255),
        };

        public static TeamColorTheme Enemy { get; } = new TeamColorTheme()
        {
            Characters_GlowColor = Color.Red,
            TeamTurnHudDecor_Text = Color.FromNonPremultiplied(255, 0, 0, 120),
            Footprints_GlowColor = Color.FromNonPremultiplied(200, 0, 0, 255),
        };

        public static TeamColorTheme Neutral { get; } = new TeamColorTheme()
        {

        };
    }
}
