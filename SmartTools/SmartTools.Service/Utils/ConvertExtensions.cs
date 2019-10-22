using System;

namespace SmartTools.Service.Utils
{
    public static class ConvertExtensions
    {
        public static TimeSpan ToTimeSpan(int level)
        {
            TimeSpan time = TimeSpan.Zero;
            switch (level)
            {
                case 1:
                    time = TimeSpan.FromSeconds(2629800); // 1 month
                    break;
                case 2:
                    time = TimeSpan.FromDays(10519200); // 4 month
                    break;
                case 3:
                    time = TimeSpan.FromDays(15778800); // 6 month
                    break;
                case 4:
                    time = TimeSpan.FromDays(31557600); // 1 year
                    break;
            }

            return time;
        }

        public static DateTime ToTimeSpan(int level, DateTime time)
        {
            return time + ConvertExtensions.ToTimeSpan(level);
        }

        public static string ToActivationDate(int level)
        {
            string result = string.Empty;
            switch (level)
            {
                case 1:
                    result = "一个月"; // 1 month
                    break;
                case 2:
                    result = "4个月"; // 4 month
                    break;
                case 3:
                    result = "半年"; // 6 month
                    break;
                case 4:
                    result = "一年"; // 1 year
                    break;
            }

            return result;
        }
    }
}
