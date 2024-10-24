using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CronParse.CronEnum;

namespace CronParse
{
    public class Cron
    {
        public static DateTime NextRunTime(string cron, DateTime from_time)
        {
            DateTime q = new DateTime();

            var year = from_time.Year;
            var month = from_time.Month;
            var day = from_time.Day;
            var hour = from_time.Hour;
            var minute = from_time.Minute;

            var cronList = cron.Split(" ");

            return new DateTime(year, month, day, hour, minute, 0);
        }

    }
}
