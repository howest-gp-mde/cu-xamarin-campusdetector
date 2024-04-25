using MDE.CampusDetector.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MDE.CampusDetector.Converters
{
    public class DistanceToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double distanceKilometer)
            {
                if (distanceKilometer < Constants.Ranges.CloseRange)
                {
                    return $"{(distanceKilometer * 1000):N0}";
                }
                else if (distanceKilometer < Constants.Ranges.MediumRange)
                {
                    return $"{distanceKilometer:N1}";
                }
                else
                {
                    return $"{distanceKilometer:N0}";
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
