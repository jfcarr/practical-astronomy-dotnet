using System;

namespace PALib
{
	public class PADateTime
	{
		/// <summary>
		/// Gets the date of Easter for the year specified.
		/// </summary>
		/// <param name="inputYear"></param>
		/// <returns>(Month, Day, Year)</returns>
		public (int Month, int Day, int Year) GetDateOfEaster(int inputYear)
		{
			double year = inputYear;

			var a = year % 19;
			var b = Math.Floor(year / 100);
			var c = year % 100;
			var d = Math.Floor(b / 4);
			var e = b % 4;
			var f = Math.Floor((b + 8) / 25);
			var g = Math.Floor((b - f + 1) / 3);
			var h = ((19 * a) + b - d - g + 15) % 30;
			var i = Math.Floor(c / 4);
			var k = c % 4;
			var l = (32 + 2 * (e + i) - h - k) % 7;
			var m = Math.Floor((a + (11 * h) + (22 * l)) / 451);
			var n = Math.Floor((h + l - (7 * m) + 114) / 31);
			var p = (h + l - (7 * m) + 114) % 31;

			var day = p + 1;
			var month = n;

			return ((int)month, (int)day, (int)year);
		}

		/// <summary>
		/// Calculate day number for a date.
		/// </summary>
		/// <param name="month"></param>
		/// <param name="day"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		public int CivilDateToDayNumber(int month, int day, int year)
		{
			if (month <= 2)
			{
				month = month - 1;
				month = (year.IsLeapYear()) ? month * 62 : month * 63;
				month = (int)Math.Floor((double)month / 2);
			}
			else
			{
				month = (int)Math.Floor(((double)month + 1) * 30.6);
				month = (year.IsLeapYear()) ? month - 62 : month - 63;
			}

			return month + day;
		}

		/// <summary>
		/// Convert a Civil Time (hours,minutes,seconds) to Decimal Hours
		/// </summary>
		/// <param name="hours"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public double CivilTimeToDecimalHours(double hours, double minutes, double seconds)
		{
			return PAMacros.HMStoDH(hours, minutes, seconds);
		}

		/// <summary>
		/// Convert Decimal Hours to Civil Time
		/// </summary>
		/// <param name="decimalHours"></param>
		/// <returns>Tuple(hours (double), minutes (double), seconds (double))</returns>
		public (double hours, double minutes, double seconds) DecimalHoursToCivilTime(double decimalHours)
		{
			var hours = PAMacros.DecimalHoursHour(decimalHours);
			var minutes = PAMacros.DecimalHoursMinute(decimalHours);
			var seconds = PAMacros.DecimalHoursSecond(decimalHours);

			return (hours, minutes, seconds);
		}

		/// <summary>
		/// Convert local Civil Time to Universal Time
		/// </summary>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="isDaylightSavings"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <returns>Tuple (int utHours, int utMinutes, int utSeconds, int gwDay, int gwMonth, int gwYear)</returns>
		public (int utHours, int utMinutes, int utSeconds, int gwDay, int gwMonth, int gwYear) LocalCivilTimeToUniversalTime(double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSavings, int zoneCorrection, double localDay, int localMonth, int localYear)
		{
			var lct = CivilTimeToDecimalHours(lctHours, lctMinutes, lctSeconds);

			var daylightSavingsOffset = (isDaylightSavings) ? 1 : 0;

			var utInterim = lct - daylightSavingsOffset - zoneCorrection;
			var gdayInterim = localDay + (utInterim / 24);

			var jd = PAMacros.CivilDateToJulianDate(gdayInterim, localMonth, localYear);

			var gDay = PAMacros.JulianDateDay(jd);
			var gMonth = PAMacros.JulianDateMonth(jd);
			var gYear = PAMacros.JulianDateYear(jd);

			var ut = 24 * (gDay - Math.Floor(gDay));

			return (
				PAMacros.DecimalHoursHour(ut),
				PAMacros.DecimalHoursMinute(ut),
				(int)PAMacros.DecimalHoursSecond(ut),
				(int)Math.Floor(gDay),
				gMonth,
				gYear
			);
		}

		/// <summary>
		/// Convert Universal Time to local Civil Time
		/// </summary>
		/// <param name="utHours"></param>
		/// <param name="utMinutes"></param>
		/// <param name="utSeconds"></param>
		/// <param name="isDaylightSavings"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="gwDay"></param>
		/// <param name="gwMonth"></param>
		/// <param name="gwYear"></param>
		/// <returns>Tuple (int lctHours, int lctMinutes, int lctSeconds, int localDay, int localMonth, int localYear)</returns>
		public (int lctHours, int lctMinutes, int lctSeconds, int localDay, int localMonth, int localYear) UniversalTimeToLocalCivilTime(double utHours, double utMinutes, double utSeconds, bool isDaylightSavings, int zoneCorrection, int gwDay, int gwMonth, int gwYear)
		{
			var dstValue = (isDaylightSavings) ? 1 : 0;
			var ut = PAMacros.HMStoDH(utHours, utMinutes, utSeconds);
			var zoneTime = ut + zoneCorrection;
			var localTime = zoneTime + dstValue;
			var localJDPlusLocalTime = PAMacros.CivilDateToJulianDate(gwDay, gwMonth, gwYear) + (localTime / 24);
			var localDay = PAMacros.JulianDateDay(localJDPlusLocalTime);
			var integerDay = Math.Floor(localDay);
			var localMonth = PAMacros.JulianDateMonth(localJDPlusLocalTime);
			var localYear = PAMacros.JulianDateYear(localJDPlusLocalTime);

			var lct = 24 * (localDay - integerDay);

			return (
				PAMacros.DecimalHoursHour(lct),
				PAMacros.DecimalHoursMinute(lct),
				(int)PAMacros.DecimalHoursSecond(lct),
				(int)integerDay,
				localMonth,
				localYear
			);
		}
	}
}
