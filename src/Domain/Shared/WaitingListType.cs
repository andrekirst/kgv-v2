namespace KgvSystem.Domain.Shared;

/// <summary>
/// Represents the two types of waiting lists in the KGV system.
/// This enum is part of the protected domain model and must not be modified.
/// </summary>
public enum WaitingListType
{
    /// <summary>
    /// Warteliste 32.2 - for specific allotment applications
    /// </summary>
    Nr32,

    /// <summary>
    /// Warteliste 33.2 - for general allotment applications
    /// </summary>
    Nr33
}