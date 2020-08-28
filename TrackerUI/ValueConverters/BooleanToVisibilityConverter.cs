using System;
using System.Globalization;
using System.Windows;

namespace TrackerUI.ValueConverters {

    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            if (parameter == null) {
                return (bool)value ? Visibility.Hidden : Visibility.Visible;
            }

            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }

}