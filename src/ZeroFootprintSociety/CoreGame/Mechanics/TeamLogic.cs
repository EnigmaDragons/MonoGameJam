using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoDragons.Core.Text;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Teams;

namespace ZeroFootPrintSociety.CoreGame
{
    public static class TeamLogic
    {
        
        #region Team Enum Extensions

        public static bool AllowedToShootAt(this Team team, Team teamToShoot)
        {
            throw new NotImplementedException();
        }

        public static bool AllowedToShootAt(this Team team, TeamGroup teamGroupToShoot)
        {
            throw new NotImplementedException();
        }

        public static bool IsIncludedIn(this Team subjectTeam, TeamGroup teamGroup)
        {
            // Test the `None` group, which always includes nobody.
            // TODO: See if it's necessary (we might not need to check for `TeamGroup.None` ever).
            if (teamGroup == TeamGroup.None)
                return false;

            // Test the `All` group, and if the group has the same value as the team.
            if (teamGroup == TeamGroup.All || (int)subjectTeam == (int)teamGroup)
                return true;

            // Test multi-team TeamGroups.
            switch (teamGroup)
            {
                case TeamGroup.FriendliesAndNeutrals:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Neutral);
                case TeamGroup.FriendliesAndEnemies:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Enemy);
                case TeamGroup.NeutralsAndEnemies:
                    return subjectTeam.Equals(Team.Neutral) || subjectTeam.Equals(Team.Enemy);
                default:
                    throw new Exception($"Given multi-team TeamGroup \""
                                        + Enum.GetName(typeof(TeamGroup), teamGroup)
                                        + "\" isn't implemented for extension method \"IsPartOfTeamGroup.\"");
            }
        }

        #endregion

        #region TeamGroup Enum Extensions

        public static bool IncludesTeam(this TeamGroup teamGroup, Team subjectTeam)
            => subjectTeam.IsIncludedIn(teamGroup);

        #endregion
    }
}
