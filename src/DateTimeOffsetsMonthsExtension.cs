using System;
using System.Diagnostics.Contracts;
using Soenneker.Enums.UnitOfTime;

namespace Soenneker.Extensions.DateTimeOffsets.Months;

/// <summary>
/// Provides extension methods for <see cref="DateTimeOffset"/> that operate on month boundaries,
/// including helpers that compute month starts/ends in a specified time zone while returning UTC instants.
/// </summary>
public static class DateTimeOffsetsMonthsExtension
{
    /// <summary>
    /// Returns the start of the month containing <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the first moment of the month containing <paramref name="dateTimeOffset"/>.</returns>
    /// <remarks>
    /// This delegates to <c>Trim(UnitOfTime.Month)</c> (or equivalent). No time zone conversion is performed and the offset is preserved.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToStartOfMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.Trim(UnitOfTime.Month);

    /// <summary>
    /// Returns the end of the month containing <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>
    /// A <see cref="DateTimeOffset"/> representing the last tick of the month containing <paramref name="dateTimeOffset"/>.
    /// </returns>
    /// <remarks>
    /// This delegates to <c>TrimEnd(UnitOfTime.Month)</c>, which is typically defined as one tick before the start of the next month.
    /// No time zone conversion is performed and the offset is preserved.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.TrimEnd(UnitOfTime.Month);

    /// <summary>
    /// Returns the start of the next month relative to <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the first moment of the next month.</returns>
    /// <remarks>No time zone conversion is performed and the offset is preserved.</remarks>
    [Pure]
    public static DateTimeOffset ToStartOfNextMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToStartOfMonth()
                      .AddMonths(1);

    /// <summary>
    /// Returns the start of the previous month relative to <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the first moment of the previous month.</returns>
    /// <remarks>No time zone conversion is performed and the offset is preserved.</remarks>
    [Pure]
    public static DateTimeOffset ToStartOfPreviousMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToStartOfMonth()
                      .AddMonths(-1);

    /// <summary>
    /// Returns the end of the previous month relative to <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the last tick of the previous month.</returns>
    /// <remarks>This is computed as the end of the current month minus one month.</remarks>
    [Pure]
    public static DateTimeOffset ToEndOfPreviousMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToEndOfMonth()
                      .AddMonths(-1);

    /// <summary>
    /// Returns the end of the next month relative to <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the last tick of the next month.</returns>
    /// <remarks>This is computed as the end of the current month plus one month.</remarks>
    [Pure]
    public static DateTimeOffset ToEndOfNextMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToEndOfMonth()
                      .AddMonths(1);

    /// <summary>
    /// Computes the start of the month in <paramref name="tz"/> that contains the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>
    /// A UTC <see cref="DateTimeOffset"/> representing the start of the target time zone's month containing <paramref name="utcInstant"/>.
    /// </returns>
    /// <remarks>
    /// This converts <paramref name="utcInstant"/> into <paramref name="tz"/>, trims to month start in that zone, then returns the same instant in UTC.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToStartOfTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToStartOfMonth()
                  .ToUtc();

    /// <summary>
    /// Computes the end of the month in <paramref name="tz"/> that contains the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToEndOfTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToEndOfMonth()
                  .ToUtc();

    /// <summary>
    /// Computes the end of the previous month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the previous month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToEndOfPreviousTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToEndOfPreviousMonth()
                  .ToUtc();

    /// <summary>
    /// Computes the start of the previous month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the start of the previous month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToStartOfPreviousTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToStartOfPreviousMonth()
                  .ToUtc();

    /// <summary>
    /// Computes the start of the next month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the start of the next month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToStartOfNextTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToStartOfNextMonth()
                  .ToUtc();

    /// <summary>
    /// Computes the end of the next month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">An instant in time. It is treated as a UTC instant (any offset is normalized to UTC).</param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the next month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToEndOfNextTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToUniversalTime()
                  .ToTz(tz)
                  .ToEndOfNextMonth()
                  .ToUtc();
}