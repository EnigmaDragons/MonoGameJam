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

        public static bool CanShootAt(this Team team, Team teamToShoot)
        {
            throw new NotImplementedException();
        }

        public static bool CanShootAt(this Team team, TeamGroup teamGroupToShoot)
        {
            throw new NotImplementedException();
        }

        public static bool IsPartOfTeamGroup(this Team subjectTeam, TeamGroup teamGroup)
        {
            if (teamGroup == TeamGroup.None)
                return false;

            // Test the `All` group, and if the group has the same value as the team.
            if (teamGroup == TeamGroup.All || (int)subjectTeam == (int)teamGroup)
                return true;

            switch (teamGroup)
            {
                case TeamGroup.FriendliesAndNeutrals:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Neutral);
                case TeamGroup.FriendliesAndEnemies:
                    return subjectTeam.Equals(Team.Friendly) || subjectTeam.Equals(Team.Enemy);
                case TeamGroup.NeutralsAndEnemies:
                    return subjectTeam.Equals(Team.Neutral) || subjectTeam.Equals(Team.Enemy);
                default:
                    throw new Exception($"Given Multi-Teamed TeamGroup "
                                        + Enum.GetName(typeof(TeamGroup), teamGroup)
                                        + " isn't implemented for extension method IsPartOfTeamGroup.");
            }
        }

        #endregion

        #region TeamGroup Enum Extensions

        public static bool IncludesTeam(this TeamGroup teamGroup, Team subjectTeam)
            => subjectTeam.IsPartOfTeamGroup(teamGroup);

        #endregion
    }
}
