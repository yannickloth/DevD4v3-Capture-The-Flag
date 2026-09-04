namespace CTF.Application.GameRules;

/// <summary>
/// Represents the collection of players that belong to a team.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership); CD-31 (player entity) → CD-02</remarks>
public class TeamMembers : IEnumerable<Player>
{
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership); CD-31 (player entity, keyed by player id) → CD-02</remarks>
    private readonly Dictionary<int, Player> _players = [];

    /// <summary>Checks whether the team has no members.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public bool IsEmpty() => _players.Count == 0;

    /// <summary>Gets the number of team members.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing)</remarks>
    public int Count => _players.Count;

    /// <summary>Clears all team members.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: round/team reset rule)</remarks>
    public void Clear() => _players.Clear();

    /// <summary>
    /// Removes the player from the team.
    /// </summary>
    /// <remarks>
    /// This method throws an <see cref="ArgumentException"/> if the player is not found.
    /// </remarks>
    /// <param name="player">The player to remove.</param>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public IEnumerator<Player> GetEnumerator() => _players.Values.GetEnumerator();

    /// <summary>Gets the non-generic enumerator.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
