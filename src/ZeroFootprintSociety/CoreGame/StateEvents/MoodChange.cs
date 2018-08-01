namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class MoodChange
    {
        public Mood NewMood { get; set; }
    }

    public enum Mood
    {
        Stealth,
        Battle,
        Boss
    }
}
