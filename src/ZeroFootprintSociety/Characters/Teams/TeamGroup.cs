namespace ZeroFootPrintSociety.Characters.Teams
{
    /// <summary>
    /// Groups of teams as an enumerator.
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
}
