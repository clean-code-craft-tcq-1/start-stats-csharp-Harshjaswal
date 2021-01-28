using System;
using System.Collections.Generic;
using Xunit;
using Statistics;
using Xunit.Sdk;

namespace Statistics.Test
{
    public class StatsUnitTest
    {
        [Fact]
        public void ReportsAverageMinMax()
        {
            var statsComputer = new StatsComputer();
            statsComputer.CalculateStatistics(new List<double> { 1.5, 8.9, 3.2, 4.5 });
            float epsilon = 0.001F;
            Assert.True(Math.Abs(statsComputer.average - 4.525) <= epsilon);
            Assert.True(Math.Abs(statsComputer.max - 8.9) <= epsilon);
            Assert.True(Math.Abs(statsComputer.min - 1.5) <= epsilon);
        }
        [Fact]
        public void ReportsNaNForEmptyInput()
        {
            var statsComputer = new StatsComputer();
            statsComputer.CalculateStatistics(
                new List<double> { 1.5, 8.9, 3.2, 4.5, double.NaN });

            //All fields of computedStats (average, max, min) must be
            //Double.NaN (not-a-number), as described in
            
            Assert.True(double.IsNaN(statsComputer.average));
            Assert.True(double.IsNaN(statsComputer.min));
            Assert.True(double.IsNaN(statsComputer.max));
            //https://docs.microsoft.com/en-us/dotnet/api/system.double.nan?view=netcore-3.1
        }
        [Fact]
        public void RaisesAlertsIfMaxIsMoreThanThreshold()
        {
            var emailAlert = new EmailAlert();
            var ledAlert = new LEDAlert();
            IAlerter[] alerters = {emailAlert, ledAlert};

            const double maxThreshold = 10.2;
            var statsAlerter = new StatsAlerter(maxThreshold, alerters);
            statsAlerter.checkAndAlert(new List<double>{0.2, 11.9, 4.3, 8.5});

            Assert.True(emailAlert.emailSent);
            Assert.True(ledAlert.ledGlows);
        }
    }
}
