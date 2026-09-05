namespace CTF.Application.GameRules.Match.Teams;

/// <summary>
/// Represents the collection of players that belong to a team.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public class TeamMembers : IEnumerable<Player>
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private readonly Dictionary<int, Player> _players = [];

    /// <summary>Checks whether the team has no members.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public bool IsEmpty() => _players.Count == 0;

    /// <summary>Gets the number of team members.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public int Count => _players.Count;

    /// <summary>Clears all team members.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Clear() => _players.Clear();

    /// <summary>
    /// Removes the player from the team.
    /// </summary>
    /// <remarks>
    /// This method throws an <see cref="ArgumentException"/> if the player is not found.
    /// </remarks>
    /// <param name="player">The player to remove.</param>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Remove(Player player)
    {
        bool playerIsNotFound = !_players.Remove(player.Id);
        if (playerIsNotFound)
        {
            var message = Smart.Format(Messages.PlayerNotFound, new { player.Name });
            throw new ArgumentException(message, nameof(player));
        }
    }

    /// <summary>
    /// Adds the player as a member of a team.
    /// </summary>
    /// <remarks>
    /// This method throws an <see cref="ArgumentException"/> if the member already exists.
    /// </remarks>
    /// <param name="player">The player to add.</param>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Add(Player player)
    {
        bool exists = !_players.TryAdd(player.Id, player);
        if (exists)
        {
            var message = Smart.Format(Messages.MemberAlreadyExists, new { player.Name });
            throw new ArgumentException(message, nameof(player));
        }
    }

    /// <summary>Gets an enumerator over the team members.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public IEnumerator<Player> GetEnumerator() => _players.Values.GetEnumerator();

    /// <summary>Gets the non-generic enumerator.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
