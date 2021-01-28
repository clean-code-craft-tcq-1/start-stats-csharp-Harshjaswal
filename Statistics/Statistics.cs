using System;
using System.Collections.Generic;
using System.Linq;

namespace Statistics
{
    public class StatsComputer
    {
        public double average = double.NaN;
        public double max = double.NaN;
        public double min = double.NaN;

        public void CalculateStatistics(List<double> numbers) {

            if (numbers.Contains(double.NaN))
            {
                return;
            }
            average = numbers.Average();
            max = numbers.Max();
            min = numbers.Min();
        }
    }

    public class StatsAlerter
    {
        private double _maxThreshold;
        readonly EmailAlert _emailAlert;
        readonly LEDAlert _ledAlert;

        public StatsAlerter(double maxThreshold, IAlerter[] alerters)
        {
            this._maxThreshold = maxThreshold;
            this._emailAlert = (EmailAlert)alerters[0];
            this._ledAlert = (LEDAlert)alerters[1];
        }

        public void checkAndAlert(List<double> doubles)
        {
            if (!doubles.Any(number => number > _maxThreshold))
            {
                return;
            }
            _emailAlert.emailSent = true;
            _ledAlert.ledGlows = true;
        }
    }
}
