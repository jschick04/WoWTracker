using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TrackerUI.Core.Helpers {

    public static class SecureStringHelpers {

        public static string Unsecure(this SecureString secureString) {
            if (secureString == null) {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;

            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

    }

}