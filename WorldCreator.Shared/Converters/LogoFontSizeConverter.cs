using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace WorldCreator.Converters
{
    public class LogoFontSizeConverter : IValueConverter
    {
        private const double SmallSize = 28;
        private const double BigSize = 36;
        private const double DefaultSize = 18;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double width = 0;
            if (double.TryParse(value.ToString(), out width))
            {
                if (width < 250)
                {
                    return SmallSize;
                }
                else
                {
                    return BigSize;
                }
            }

            return DefaultSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
