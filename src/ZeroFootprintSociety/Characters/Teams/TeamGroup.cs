using System.Collections.Generic;

namespace ZeroFootPrintSociety.Characters
{
    /// <summary>
    /// Groups of teams in an Enum.
    /// It's similar to the UNIX style of assigning file permissions.
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chmod#Numerical_permissions"/>
    public enum TeamGroup
    {
        None = 0,
        Friendlies = 1,
        Neutrals = 2,
        FriendliesAndNeutrals = 3,
        Enemies = 4,
        FriendliesAndEnemies = 5,
        NeutralsAndEnemies = 6,
        All = 7
    }

    public static class TeamGroupEnumExtensions
    {
        public static bool Includes(this TeamGroup teamGroup, Team subjectTeam)
            => subjectTeam.IsIncludedIn(teamGroup);

        public static List<Team> AsTeamList(this TeamGroup teamGroup)
        {
            List<Team> teamList = new List<Team>();

            bool isAll = teamGroup == TeamGroup.All,
                isFrAndEn = teamGroup == TeamGroup.FriendliesAndEnemies,
                isFrAndNe = teamGroup == TeamGroup.FriendliesAndNeutrals,
                isNeAndEn = teamGroup == TeamGroup.NeutralsAndEnemies;


            if (isAll || teamGroup == TeamGroup.Friendlies || isFrAndEn || isFrAndNe)
                teamList.Add(Team.Friendly);
            if (isAll || teamGroup == TeamGroup.Neutrals || isFrAndNe || isNeAndEn)
                teamList.Add(Team.Neutral);
            if (isAll || teamGroup == TeamGroup.Enemies || isFrAndEn || isNeAndEn)
                teamList.Add(Team.Enemy);

            return teamList;
        }

        public static Team? AsSingleTeam(this TeamGroup teamGroup)
        {
            switch (teamGroup)
            {
                case TeamGroup.Friendlies:
                    return Team.Friendly;
                case TeamGroup.Neutrals:
                    return Team.Neutral;
                case TeamGroup.Enemies:
                    return Team.Enemy;
                default:
                    return null;
            }
        }
    }
}
