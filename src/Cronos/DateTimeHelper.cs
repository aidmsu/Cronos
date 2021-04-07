using System;

namespace Cronos
{
    internal static class DateTimeHelper
    {
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        public static DateTimeOffset FloorToSeconds(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.AddTicks(-(dateTimeOffset.Ticks % OneSecond.Ticks));
        }
    }
}