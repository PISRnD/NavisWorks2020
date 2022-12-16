using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PivdcSupportModule
{
    /// <summary>
    /// Boolean to visibility converter negate means if in default true returns visible, therefore here true is returning hidden.
    /// Currently using it in RevitFabricationHanger user interface as static resource.
    /// </summary>
    public sealed class BooleanToVisibilityConverterNegate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? flag2 = (bool?)value;
                flag = (flag2.HasValue && flag2.Value);
            }

            return (flag) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }

            return false;
        }
    }

    /// <summary>
    /// Used WPF window form to negate the boolean value.
    /// </summary>
    public sealed class NegateTheBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool) && ((bool)value))
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool) && !((bool)value))
            {
                return true;
            }
            return false;
        }
    }
}
