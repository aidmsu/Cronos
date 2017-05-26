﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using NCrontab;

namespace Cronos.Benchmarks
{
    [RyuJitX64Job]
    public class CronBenchmarks
    {
        private static readonly CronExpression SimpleExpression =
            CronExpression.Parse("* * * * * *", CronFormat.IncludeSeconds);

        private static readonly CronExpression ComplexExpression = CronExpression.Parse("*/10 12-20 * DEC 3");

        private static readonly CronExpression SimpleUnreachableExpression = CronExpression.Parse("* * 30 02 *");
        private static readonly CronExpression ComplexUnreachableExpression = CronExpression.Parse("* * LW * 1#1");

        private static readonly CronExpression LastDayOfWeekUnreachableExpression =
            CronExpression.Parse("* * 1-21 * 0L");

        private static readonly CronExpression NthDayOfWeekUnreachableExpression =
            CronExpression.Parse("* * 1-28 * SUN#5");

        private static readonly CronExpression AbmiguousExpression = CronExpression.Parse("30 1 4 11 *");

        private static readonly CrontabSchedule SimpleExpressionNCrontab = CrontabSchedule.Parse("* * * * *");
        private static readonly CrontabSchedule ComplexExpressionNCrontab = CrontabSchedule.Parse("*/10 12-20 * DEC 3");

        private static readonly Quartz.CronExpression SimpleExpressionQuartz = new Quartz.CronExpression("* * * * * ?");

        private static readonly Quartz.CronExpression ComplexExpressionQuartz =
            new Quartz.CronExpression("* */10 12-20 ? DEC 3");

        private static readonly NCrontab.Advanced.CrontabSchedule SimpleExpressionNCrontabAdvanced =
            NCrontab.Advanced.CrontabSchedule.Parse("* * * * *");

        private static readonly NCrontab.Advanced.CrontabSchedule ComplexExpressionNCrontabAdvanced =
            NCrontab.Advanced.CrontabSchedule.Parse("*/10 12-20 * DEC 3");

        private static readonly DateTime DateTimeNow = new DateTime(2017, 04, 05, 07, 46, 24, DateTimeKind.Utc);
        private static readonly DateTimeOffset DateTimeOffsetNow = new DateTimeOffset(DateTimeNow, TimeSpan.Zero);

        private static readonly TimeZoneInfo UtcTimeZone = TimeZoneInfo.Utc;

        private static readonly TimeZoneInfo PacificTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

        private static readonly DateTimeOffset SecondBeforeInvalidTime = new DateTimeOffset(2017, 03, 12, 01, 59, 59, 999, PacificTimeZone.BaseUtcOffset);
        private static readonly DateTimeOffset AmbiguousDaylightTime = new DateTimeOffset(2017, 11, 05, 00, 30, 59, 999, PacificTimeZone.BaseUtcOffset);

        [Benchmark]
        public unsafe string ParseBaseline()
        {
            fixed (char* pointer = "* * * * ?")
            {
                var ptr = pointer;
                while (*ptr != '\0' && *ptr != '\n' && *ptr != ' ') ptr++;

                return new string(pointer);
            }
        }

        [Benchmark]
        public CronExpression ParseStars()
        {
            return CronExpression.Parse("* * * * *");
        }

        [Benchmark]
        public CronExpression ParseNumber()
        {
            return CronExpression.Parse("20 * * * *");
        }

        //[Benchmark]
        //public CronExpression ParseRange()
        //{
        //    return CronExpression.Parse("20-40 * * * *");
        //}

        //[Benchmark]
        //public CronExpression ParseList()
        //{
        //    return CronExpression.Parse("20,30,40,50 * * * *");
        //}

        //[Benchmark]
        //public CronExpression ParseComplex()
        //{
        //    return CronExpression.Parse("*/10 12-20 ? DEC 3");
        //}

        //[Benchmark]
        //public CronExpression ParseMacroEverySecond()
        //{
        //    return CronExpression.Parse("@every_second");
        //}

        //[Benchmark]
        //public CronExpression ParseMacroDaily()
        //{
        //    return CronExpression.Parse("@daily");
        //}

        //[Benchmark]
        //public DateTime? NextSimpleDateTime()
        //{
        //    return SimpleExpression.GetNextOccurrence(DateTimeNow);
        //}

        //[Benchmark]
        //public DateTime? NextComplexDateTime()
        //{
        //    return ComplexExpression.GetNextOccurrence(DateTimeNow);
        //}

        //[Benchmark]
        //public DateTimeOffset? NextSimpleDateTimeOffset()
        //{
        //    return SimpleExpression.GetNextOccurrence(DateTimeOffsetNow, UtcTimeZone);
        //}

        //[Benchmark]
        //public DateTimeOffset? NextComplexDateTimeOffset()
        //{
        //    return ComplexExpression.GetNextOccurrence(DateTimeOffsetNow, UtcTimeZone);
        //}

        //[Benchmark]
        //public DateTime? NextSimpleWithTimeZone()
        //{
        //    return SimpleExpression.GetNextOccurrence(DateTimeNow, PacificTimeZone);
        //}

        //[Benchmark]
        //public DateTime? NextComplexWithTimeZone()
        //{
        //    return ComplexExpression.GetNextOccurrence(DateTimeNow, PacificTimeZone);
        //}

        //[Benchmark]
        //public void NextUnreachableSimple()
        //{
        //    var result = SimpleUnreachableExpression.GetNextOccurrence(DateTimeNow, UtcTimeZone);
        //    if (result != null) throw new InvalidOperationException();
        //}

        //[Benchmark]
        //public void NextUnreachableComplex()
        //{
        //    var result = ComplexUnreachableExpression.GetNextOccurrence(DateTimeNow, UtcTimeZone);
        //    if (result != null) throw new InvalidOperationException();
        //}

        //[Benchmark]
        //public void NextUnreachableLastDayOfWeek()
        //{
        //    var result = LastDayOfWeekUnreachableExpression.GetNextOccurrence(DateTimeNow, UtcTimeZone);
        //    if (result != null) throw new InvalidOperationException();
        //}

        //[Benchmark]
        //public void NextUnreachableNthDayOfWeek()
        //{
        //    var result = NthDayOfWeekUnreachableExpression.GetNextOccurrence(DateTimeNow, UtcTimeZone);
        //    if (result != null) throw new InvalidOperationException();
        //}

        //[Benchmark]
        //public DateTimeOffset? NextHandlesInvalidTime()
        //{
        //    var result = SimpleExpression.GetNextOccurrence(SecondBeforeInvalidTime, PacificTimeZone);
        //    if (result.Value.Hour != 3) throw new InvalidOperationException();

        //    return result;
        //}

        //[Benchmark]
        //public DateTimeOffset? NextHandlesAmbiguousDaylight()
        //{
        //    return AbmiguousExpression.GetNextOccurrence(AmbiguousDaylightTime, PacificTimeZone);
        //}

        //[Benchmark]
        //public CrontabSchedule ParseStarsNCrontab()
        //{
        //    return CrontabSchedule.Parse("* * * * *");
        //}

        //[Benchmark]
        //public CrontabSchedule ParseComplexNCrontab()
        //{
        //    return CrontabSchedule.Parse("*/10 12-20 * DEC 3");
        //}

        //[Benchmark]
        //public DateTime NextSimpleNCrontab()
        //{
        //    return SimpleExpressionNCrontab.GetNextOccurrence(DateTimeNow);
        //}

        //[Benchmark]
        //public DateTime NextComplexNCrontab()
        //{
        //    return ComplexExpressionNCrontab.GetNextOccurrence(DateTimeNow);
        //}

        //[Benchmark]
        //public Quartz.CronExpression ParseStarsQuartz()
        //{
        //    return new Quartz.CronExpression("* * * * * ?");
        //}

        //[Benchmark]
        //public Quartz.CronExpression ParseComplexQuartz()
        //{
        //    return new Quartz.CronExpression("* */10 12-20 ? DEC 3");
        //}

        //[Benchmark]
        //public DateTimeOffset? NextSimpleQuartz()
        //{
        //    return SimpleExpressionQuartz.GetTimeAfter(DateTimeOffsetNow);
        //}

        //[Benchmark]
        //public DateTimeOffset? NextComplexQuartz()
        //{
        //    return ComplexExpressionQuartz.GetTimeAfter(DateTimeOffsetNow);
        //}

        //[Benchmark]
        //public NCrontab.Advanced.CrontabSchedule ParseStarsNCrontabAdvanced()
        //{
        //    return NCrontab.Advanced.CrontabSchedule.Parse("* * * * *");
        //}

        //[Benchmark]
        //public NCrontab.Advanced.CrontabSchedule ParseComplexNCrontabAdvanced()
        //{
        //    return NCrontab.Advanced.CrontabSchedule.Parse("*/10 12-20 * DEC 3");
        //}

        //[Benchmark]
        //public DateTime NextSimpleNCrontabAdvanced()
        //{
        //    return SimpleExpressionNCrontabAdvanced.GetNextOccurrence(DateTimeNow);
        //}

        //[Benchmark]
        //public DateTime NextComplexNCrontabAdvanced()
        //{
        //    return ComplexExpressionNCrontabAdvanced.GetNextOccurrence(DateTimeNow);
        //}
    }
}
