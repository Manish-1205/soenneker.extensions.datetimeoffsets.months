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
    /// <returns>
    /// A <see cref="DateTimeOffset"/> representing the first moment of the month containing <paramref name="dateTimeOffset"/>.
    /// </returns>
    /// <remarks>
    /// No time zone conversion is performed and the offset is preserved.
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
    /// Computed as one tick before the start of the next month. No time zone conversion is performed and the offset is preserved.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToStartOfMonth()
                      .AddMonths(1)
                      .AddTicks(-1);

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
    /// <remarks>
    /// Computed as one tick before the start of the current month. No time zone conversion is performed and the offset is preserved.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfPreviousMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToStartOfMonth()
                      .AddTicks(-1);

    /// <summary>
    /// Returns the end of the next month relative to <paramref name="dateTimeOffset"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The value to adjust.</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the last tick of the next month.</returns>
    /// <remarks>
    /// Computed as one tick before the start of the month after next. No time zone conversion is performed and the offset is preserved.
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfNextMonth(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.ToStartOfMonth()
                      .AddMonths(2)
                      .AddTicks(-1);

    /// <summary>
    /// Computes the start of the month in <paramref name="tz"/> that contains the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>
    /// A UTC <see cref="DateTimeOffset"/> representing the start of the month in <paramref name="tz"/> that contains <paramref name="utcInstant"/>.
    /// </returns>
    /// <remarks>
    /// This computes the boundary as a local wall time (00:00 on the 1st) and maps it to UTC using the time zone's rules
    /// at that wall time (DST-safe).
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToStartOfTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz)
    {
        DateTimeOffset utc = utcInstant.ToUniversalTime();
        DateTimeOffset local = TimeZoneInfo.ConvertTime(utc, tz);

        DateTime localStart = new(local.Year, local.Month, 1, 0, 0, 0, DateTimeKind.Unspecified);

        DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(localStart, tz);
        return new DateTimeOffset(utcStart, TimeSpan.Zero);
    }

    /// <summary>
    /// Computes the end of the month in <paramref name="tz"/> that contains the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the month in <paramref name="tz"/>.</returns>
    /// <remarks>
    /// Computed as one tick before the start of the next month in <paramref name="tz"/> (DST-safe).
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToStartOfTzMonth(tz)
                  .AddMonths(1)
                  .AddTicks(-1);

    /// <summary>
    /// Computes the start of the previous month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the start of the previous month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToStartOfPreviousTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToStartOfTzMonth(tz)
                  .AddMonths(-1);

    /// <summary>
    /// Computes the end of the previous month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the previous month in <paramref name="tz"/>.</returns>
    /// <remarks>
    /// Computed as one tick before the start of the current month in <paramref name="tz"/> (DST-safe).
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfPreviousTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToStartOfTzMonth(tz)
                  .AddTicks(-1);

    /// <summary>
    /// Computes the start of the next month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the start of the next month in <paramref name="tz"/>.</returns>
    [Pure]
    public static DateTimeOffset ToStartOfNextTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToStartOfTzMonth(tz)
                  .AddMonths(1);

    /// <summary>
    /// Computes the end of the next month in <paramref name="tz"/> relative to the instant <paramref name="utcInstant"/>,
    /// returning the result as a UTC <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="utcInstant">
    /// An instant in time. It is normalized to UTC before conversion and treated as an instant (not a local wall time).
    /// </param>
    /// <param name="tz">The time zone whose local calendar rules determine month boundaries.</param>
    /// <returns>A UTC <see cref="DateTimeOffset"/> representing the last tick of the next month in <paramref name="tz"/>.</returns>
    /// <remarks>
    /// Computed as one tick before the start of the month after next in <paramref name="tz"/> (DST-safe).
    /// </remarks>
    [Pure]
    public static DateTimeOffset ToEndOfNextTzMonth(this DateTimeOffset utcInstant, TimeZoneInfo tz) =>
        utcInstant.ToStartOfTzMonth(tz)
                  .AddMonths(2)
                  .AddTicks(-1);
}