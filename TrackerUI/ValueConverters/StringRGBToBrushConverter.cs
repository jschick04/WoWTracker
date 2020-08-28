using System;
using System.Globalization;
using System.Windows.Media;

namespace TrackerUI.ValueConverters {

    /// <summary>Converter that takes in an RGB hex value and converts it to a WPF brush</summary>
    public class StringRgbToBrushConverter : BaseValueConverter<StringRgbToBrushConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (SolidColorBrush)new BrushConverter().ConvertFrom($"#{value}");

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }

}