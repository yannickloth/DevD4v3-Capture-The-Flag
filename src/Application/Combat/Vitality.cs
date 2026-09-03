namespace CTF.Application.Combat;

/// <summary>
/// Represents a bounded health/armour amount.
/// </summary>
/// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification)</remarks>
public class Vitality
{
    /// <summary>Gets the health/armour amount.</summary>
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification)</remarks>
    public float Amount { get; private set; }

    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification)</remarks>
    private Vitality(float amount) => Amount = amount;

    /// <summary>Creates a vitality amount within the valid range.</summary>
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification)</remarks>
    public static Result<Vitality> Create(float amount)
    {
        if (amount < 0 || amount > 100)
            return Result<Vitality>.Failure(Messages.InvalidVitality);

        return Result<Vitality>.Success(new Vitality(amount));
    }
}
