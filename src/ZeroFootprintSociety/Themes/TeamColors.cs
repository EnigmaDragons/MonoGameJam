using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Themes
{
    public static class TeamColors
    {
        public static TeamColorTheme Friendly { get; } = new TeamColorTheme()
        {
            Characters_GlowColor = Color.Blue,
            TeamTurnHudDecor_Text = Color.FromNonPremultiplied(196, 233, 246, 200),
            Footprints_GlowColor = Color.FromNonPremultiplied(90, 180, 255, 255),
        };

        public static TeamColorTheme Enemy { get; } = new TeamColorTheme()
        {
            Characters_GlowColor = Color.Red,
            TeamTurnHudDecor_Text = Color.FromNonPremultiplied(255, 0, 0, 180),
            Footprints_GlowColor = Color.FromNonPremultiplied(255, 0, 0, 255),
        };

        public static TeamColorTheme Neutral { get; } = new TeamColorTheme()
        {

        };
    }
}
