using static CronParse.CronEnum;

namespace CronParse
{
    public class ValidCron
    {
        public static bool IsValid(string cronExpression)
        {
            string[] parts = cronExpression.Split(' ');
            if (parts.Length != 5)
            {
                return false;
            }

            return IsValidField(parts[0], EnumCronField.Minute) &&
                   IsValidField(parts[1], EnumCronField.Hour) &&
                   IsValidField(parts[2], EnumCronField.DayOfMonth) &&
                   IsValidField(parts[3], EnumCronField.Month) &&
                   IsValidField(parts[4], EnumCronField.DayOfWeek) &&
                   IsValidDay(parts[2], parts[3]);
        }

        private static bool IsValidField(string field, EnumCronField cronField)
        {
            int min, max;
            switch (cronField)
            {
                case EnumCronField.Minute:
                    min = 0; max = 59;
                    break;
                case EnumCronField.Hour:
                    min = 0; max = 23;
                    break;
                case EnumCronField.DayOfMonth:
                    min = 1; max = 31;
                    break;
                case EnumCronField.Month:
                    min = 1; max = 12;
                    break;
                case EnumCronField.DayOfWeek:
                    min = 0; max = 6;
                    break;
                default:
                    throw new ArgumentException("Invalid cron field");
            }

            if (field == "*")
            {
                return true;
            }

            if (field.Contains(','))
            {
                return field.Split(',').All(f => IsValidField(f, cronField));
            }

            if (field.Contains('-'))
            {
                string[] range = field.Split('-');
                if (range.Length != 2) return false;
                return int.TryParse(range[0], out int start) &&
                       int.TryParse(range[1], out int end) &&
                       start >= min && end <= max && start <= end;
            }

            if (field.Contains('/'))
            {
                string[] parts = field.Split('/');
                if (parts.Length != 2) return false;
                return IsValidField(parts[0], cronField) && int.TryParse(parts[1], out _);
            }

            return int.TryParse(field, out int value) && value >= min && value <= max;
        }

        private static bool IsValidDay(string dayOfMonth, string month)
        {
            if (dayOfMonth == "*")
            {
                return true;
            }

            if (!int.TryParse(dayOfMonth, out int monthNumber) || monthNumber < 1 || monthNumber > 12)
            {
                return false;
            }

            if (!int.TryParse(dayOfMonth, out int dayNumber) || dayNumber < 1)
            {
                return false;
            }

            int maxDaysInMonth = 31;

            switch (monthNumber)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    maxDaysInMonth = 30;
                    break;
                case 2:
                    maxDaysInMonth = 29;
                    break;
            }
            return dayNumber <= maxDaysInMonth;
        }
    }
}
