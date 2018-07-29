using System;

namespace ZeroFootPrintSociety.Characters
{
    public enum Team
    {
        Friendly,
        Neutral,
        Enemy
    }

    public static class TeamEnumExtensions
    {
        public static bool IsIncludedIn(this Team subjectTeam, TeamGroup teamGroup)
        {
            // Test multi-team TeamGroups.
            switch (teamGroup)
            {
                case TeamGroup.None:
                    // TODO: See if it's necessary (we might not need to check for `TeamGroup.None` ever).
                    return false;
                case TeamGroup.All:
                    return true;
                case TeamGroup.FriendliesAndNeutrals:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Neutral);
                case TeamGroup.FriendliesAndEnemies:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Enemy);
                case TeamGroup.NeutralsAndEnemies:
                    return subjectTeam.Equals(Team.Neutral) || subjectTeam.Equals(Team.Enemy);
                case TeamGroup.Friendlies:
                case TeamGroup.Neutrals:
                    return ((int)subjectTeam) + 1 == (int)teamGroup;
                case TeamGroup.Enemies:
                    return ((int)subjectTeam) + 2 == (int)teamGroup;
                default:
                    throw new Exception($"Given multi-team TeamGroup {teamGroup.ToString()} isn't implemented for extension method 'IsPartOfTeamGroup'.");
            }
        }

        public static TeamGroup AsTeamGroup(this Team subjectTeam)
        {
            switch (subjectTeam)
            {
                case Team.Friendly:
                    return TeamGroup.Friendlies;
                case Team.Neutral:
                    return TeamGroup.Neutrals;
                case Team.Enemy:
                    return TeamGroup.Enemies;
                default:
                    throw new Exception("Team enum was modified; There can only be 3 enums in it.");
            }
        }

        public static int ToOctal(this Team subjectTeam) => (int)subjectTeam.AsTeamGroup();
    }
}
