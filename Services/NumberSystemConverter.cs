using System;

namespace WebApiTranslationOfNumberSystems.Services
{
    public static class NumberSystemConverter
    {
        public static string Convert(string value, int fromBase, int toBase)
        {
            if (!IsSupportedBase(fromBase) || !IsSupportedBase(toBase))
                throw new ArgumentException("Only bases 2, 8, 10, 16 are supported.");

            int intValue;
            try
            {
                intValue = ConvertToInt(value, fromBase);
            }
            catch
            {
                throw new ArgumentException($"Invalid value '{value}' for base {fromBase}.");
            }

            return ConvertFromInt(intValue, toBase);
        }

        private static bool IsSupportedBase(int numberBase)
        {
            return numberBase == 2 || numberBase == 8 || numberBase == 10 || numberBase == 16;
        }

        private static int ConvertToInt(string value, int fromBase)
        {
            return System.Convert.ToInt32(value, fromBase);
        }

        private static string ConvertFromInt(int value, int toBase)
        {
            return System.Convert.ToString(value, toBase).ToUpper();
        }
    }
} 