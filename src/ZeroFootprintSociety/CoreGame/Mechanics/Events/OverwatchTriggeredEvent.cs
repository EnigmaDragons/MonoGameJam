using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    public class OverwatchTriggeredEvent
    {
        public Character FoundCharacter { get; set; }
        public Character WatchingCharacter { get; set; }
    }
}
