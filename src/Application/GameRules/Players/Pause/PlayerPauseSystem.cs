using SampSharp.OpenMp.Core.Std.Chrono;

namespace CTF.Application.GameRules.Players.Pause;

/// <summary>
/// Detects when players enter or leave the paused state.
/// </summary>
/// <remarks>
/// The paused state is detected by monitoring <c>OnPlayerUpdate</c>. If no update
/// packets are received from the client for a period of time, the player is
/// considered paused. Once updates resume, the player is considered active again.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): timerService -> CD-41; entityManager -> CD-32; timeProvider -> CD-41. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
public class PlayerPauseSystem(
    ITimerService timerService,
    IEntityManager entityManager,
    TimeProvider timeProvider) : ISystem
{
    /// <summary>
    /// Represents the minimum amount of time (in ticks) required for the player to be considered paused.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    private readonly long _minPauseTimeTicks = TimeSpan.FromMilliseconds(4000).Ticks;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    private TimerReference _timerReference;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    private readonly List<PlayerDataComponent> _playerDataComponents = new(capacity: 32);

    /// <summary>Handles the player pause state change.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-31 (player pause-state event) → CD-02</remarks>
    public delegate void PauseEventHandler(Player player, bool pauseState);

    /// <summary>Raised when a player enters or leaves the paused state.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-31 (player pause-state event) → CD-02</remarks>
    public event PauseEventHandler PauseEvent;

    /// <summary>Registers the player for pause detection on connect.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        var createdComponent = player.AddComponent<PlayerDataComponent>(player);
        _playerDataComponents.Add(createdComponent);
        if (_playerDataComponents.Count == 1)
        {
            TimeSpan interval = TimeSpan.FromMilliseconds(600);
            _timerReference = timerService.Start(CheckPauseStatus, interval);
        }
    }

    /// <summary>Unregisters the player from pause detection on disconnect.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    [Event]
    public void OnPlayerDisconnect(PlayerDataComponent playerDataComponent, DisconnectReason _) 
    {
        _playerDataComponents.Remove(playerDataComponent);
        if (_playerDataComponents.Count == 0)
        {
            if (_timerReference is null)
                return;

            timerService.Stop(_timerReference);
        }
    }

    /// <summary>Updates the last-update timestamp for the player.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    [Event]
    public void OnPlayerUpdate(PlayerDataComponent playerDataComponent, TimePoint _) 
    {
        playerDataComponent.LastUpdateTick = timeProvider.GetUtcNow().Ticks;
    }

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
    private void CheckPauseStatus(IServiceProvider serviceProvider)
    {
        int count = _playerDataComponents.Count;
        for (int i = 0; i < count; i++)
        {
            PlayerDataComponent playerDataComponent = _playerDataComponents[i];
            if (playerDataComponent.State == PlayerState.None)
                continue;

            if (playerDataComponent.State == PlayerState.Wasted)
                continue;

            long currentTicks = timeProvider.GetUtcNow().Ticks;
            long elapsedTicks = currentTicks - playerDataComponent.LastUpdateTick;
            if (!playerDataComponent.IsPaused && elapsedTicks >= _minPauseTimeTicks)
            {
                Player player = entityManager.GetComponent<Player>(playerDataComponent.Entity);
                playerDataComponent.IsPaused = true;
                Console.WriteLine($"[CTF:INFO] {player.Name} went into pause mode");
                PauseEvent?.Invoke(player, playerDataComponent.IsPaused);
            }
            else if (playerDataComponent.IsPaused && elapsedTicks < _minPauseTimeTicks)
            {
                Player player = entityManager.GetComponent<Player>(playerDataComponent.Entity);
                playerDataComponent.IsPaused = false;
                Console.WriteLine($"[CTF:INFO] {player.Name} exited pause mode");
                PauseEvent?.Invoke(player, playerDataComponent.IsPaused);
            }
        }
    }
}
