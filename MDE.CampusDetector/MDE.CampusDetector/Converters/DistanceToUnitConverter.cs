using MDE.CampusDetector.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MDE.CampusDetector.Converters
{
    public class DistanceToUnitConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double distanceKilometer)
            {
                if (distanceKilometer < Constants.Ranges.CloseRange)
                {
                    return "m";
                }
                else
                {
                    return "km";
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
