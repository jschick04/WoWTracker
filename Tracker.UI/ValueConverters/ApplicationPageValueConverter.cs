using System;
using System.Diagnostics;
using System.Globalization;
using Tracker.UI.Core.Helpers;
using Tracker.UI.Views;

namespace Tracker.UI.ValueConverters {

    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) { return null; }

            switch ((ApplicationPage)value) {
                case ApplicationPage.Login :
                    return new LoginView();
                case ApplicationPage.Summary :
                    return new SummaryView();
                default :
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }

}