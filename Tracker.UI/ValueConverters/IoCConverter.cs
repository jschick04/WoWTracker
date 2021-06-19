using System;
using System.Diagnostics;
using System.Globalization;
using Tracker.UI.Core;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.ValueConverters {

    public class IoCConverter : BaseValueConverter<IoCConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) { return null; }

            switch ((string)value) {
                case nameof(ApplicationViewModel) :
                    return IoC.Get<ApplicationViewModel>();
                default :
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();

    }

}