using System;
using System.Globalization;

namespace Tracker.Api.Library.Helpers {

    public class ApiException : Exception {

        public ApiException() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args) :
            base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

    }

}