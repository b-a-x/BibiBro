using System;

namespace BibiBro.Client.Telegram.Extensions
{
    public static class StringExtension
    {
        public static DateTime UnixTimeToDateTime(this string str)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (long.TryParse(str, out long value))
            {
                // миллисекунды нужно убрать
                if (str.Length == 13) 
                    value /= 1000;
                
                return origin.AddSeconds(value).ToLocalTime();
            }
            
            return origin;
        }
    }
}
