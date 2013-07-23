using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using My;

namespace mRemoteNC
{
    public class SupportedCultures : Dictionary<string, string>
    {
        private SupportedCultures()
        {
            foreach (var cultureName in Settings.Default.SupportedUICultures.Split(','))
            {
                try
                {
                    var cultureInfo = new CultureInfo(cultureName.Trim());
                    Add(cultureInfo.Name, cultureInfo.TextInfo.ToTitleCase(cultureInfo.NativeName));
                }
                catch (Exception ex)
                {
                    Debug.Print("An exception occurred while adding the culture \'{0}\' to the list of supported cultures. {1}", cultureName, ex);
                }
            }
        }

        private static SupportedCultures _instance = new SupportedCultures();

        public static void InstantiateSingleton()
        {
            if (_instance == null)
            {
                _instance = new SupportedCultures();
            }
        }

        public static bool IsNameSupported(string cultureName)
        {
            return _instance.ContainsKey(cultureName);
        }

        public static bool IsNativeNameSupported(string cultureNativeName)
        {
            return _instance.ContainsValue(cultureNativeName);
        }

        public static string CultureName(string cultureNativeName)
        {
            var names = new string[_instance.Count + 1];
            var nativeNames = new string[_instance.Count + 1];

            _instance.Keys.CopyTo(names, 0);
            _instance.Values.CopyTo(nativeNames, 0);

            for (var index = 0; index <= _instance.Count; index++)
            {
                if (nativeNames[index] == cultureNativeName)
                {
                    return names[index];
                }
            }

            throw (new KeyNotFoundException());
        }

        public static string CultureNativeName(string cultureName)
        {
            return _instance[cultureName];
        }

        public static IEnumerable<string> CultureNativeNames
        {
            get
            {
                return _instance.Values.ToList();
            }
        }
    }
}