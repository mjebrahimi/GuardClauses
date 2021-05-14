﻿using Ardalis.GuardClauses;
using System;
using Xunit;

namespace GuardClauses.UnitTests
{
    public class GuardAgainstOutOfRangeForTimeSpan
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 1, 3)]
        public void DoesNothingGivenInRangeValue(int input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = TimeSpan.FromSeconds(input);
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Guard.Against.OutOfRange(inputTimeSpan, "index", rangeFromTimeSpan, rangeToTimeSpan);
        }

        [Theory]
        [InlineData(-1, 1, 3)]
        [InlineData(0, 1, 3)]
        [InlineData(4, 1, 3)]
        public void ThrowsGivenOutOfRangeValue(int input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = TimeSpan.FromSeconds(input);
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.Against.OutOfRange(inputTimeSpan, "index", rangeFromTimeSpan, rangeToTimeSpan));
        }

        [Theory]
        [InlineData(-1, 3, 1)]
        [InlineData(0, 3, 1)]
        [InlineData(4, 3, 1)]
        public void ThrowsGivenInvalidArgumentValue(int input, int rangeFrom, int rangeTo)
        {
            var inputTimeSpan = TimeSpan.FromSeconds(input);
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);

            Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(inputTimeSpan, "index", rangeFromTimeSpan, rangeToTimeSpan));
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(2, 1, 3, 2)]
        [InlineData(3, 1, 3, 3)]
        public void ReturnsExpectedValueGivenInRangeValue(int input, int rangeFrom, int rangeTo, int expected)
        {
            var inputTimeSpan = TimeSpan.FromSeconds(input);
            var rangeFromTimeSpan = TimeSpan.FromSeconds(rangeFrom);
            var rangeToTimeSpan = TimeSpan.FromSeconds(rangeTo);
            var expectedTimeSpan = TimeSpan.FromSeconds(expected);

            Assert.Equal(expectedTimeSpan, Guard.Against.OutOfRange(inputTimeSpan, "index", rangeFromTimeSpan, rangeToTimeSpan));
        }

        [Theory]
        [InlineData(null, "rangeFrom should be less or equal than rangeTo")]
        [InlineData("Timespan range", "Timespan range")]
        public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage)
        {
            var inputTimeSpan = TimeSpan.FromSeconds(-1);
            var rangeFromTimeSpan = TimeSpan.FromSeconds(3);
            var rangeToTimeSpan = TimeSpan.FromSeconds(1);

            var exception = Assert.Throws<ArgumentException>(() => Guard.Against.OutOfRange(inputTimeSpan, "index", rangeFromTimeSpan, rangeToTimeSpan, customMessage));
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}