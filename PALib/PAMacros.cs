using System;

namespace PALib
{
	public static class PAMacros
	{
		/// <summary>
		/// Convert a Civil Time (hours,minutes,seconds) to Decimal Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: HMSDH
		/// </remarks>
		/// <param name="hours"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static double HMStoDH(double hours, double minutes, double seconds)
		{
			double fHours = hours;
			double fMinutes = minutes;
			double fSeconds = seconds;

			var a = Math.Abs(fSeconds) / 60;
			var b = (Math.Abs(fMinutes) + a) / 60;
			var c = Math.Abs(fHours) + b;

			return (fHours < 0 || fMinutes < 0 || fSeconds < 0) ? -c : c;
		}

		/// <summary>
		/// Return the hour part of a Decimal Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: DHHour
		/// </remarks>
		/// <param name="decimalHours"></param>
		/// <returns></returns>
		public static int DecimalHoursHour(double decimalHours)
		{
			var a = Math.Abs(decimalHours);
			var b = a * 3600;
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
			var e = (c == 60) ? b + 60 : b;

			return (decimalHours < 0) ? (int)-(Math.Floor(e / 3600)) : (int)Math.Floor(e / 3600);
		}

		/// <summary>
		/// Return the minutes part of a Decimal Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: DHMin
		/// </remarks>
		/// <param name="decimalHours"></param>
		/// <returns></returns>
		public static int DecimalHoursMinute(double decimalHours)
		{
			var a = Math.Abs(decimalHours);
			var b = a * 3600;
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
			var e = (c == 60) ? b + 60 : b;

			return (int)Math.Floor(e / 60) % 60;
		}

		/// <summary>
		/// Return the seconds part of a Decimal Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: DHSec
		/// </remarks>
		/// <param name="decimalHours"></param>
		/// <returns></returns>
		public static double DecimalHoursSecond(double decimalHours)
		{
			var a = Math.Abs(decimalHours);
			var b = a * 3600;
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
			var d = (c == 60) ? 0 : c;

			return d;
		}

		/// <summary>
		/// Convert a Greenwich Date/Civil Date (day,month,year) to Julian Date
		/// </summary>
		/// <remarks>
		/// Original macro name: CDJD
		/// </remarks>
		/// <param name="day"></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		public static double CivilDateToJulianDate(double day, double month, double year)
		{
			var fDay = (double)day;
			var fMonth = (double)month;
			var fYear = (double)year;

			var y = (fMonth < 3) ? fYear - 1 : fYear;
			var m = (fMonth < 3) ? fMonth + 12 : fMonth;

			double b;

			if (fYear > 1582)
			{
				var a = Math.Floor(y / 100);
				b = 2 - a + Math.Floor(a / 4);
			}
			else
			{
				if (fYear == 1582 && fMonth > 10)
				{
					var a = Math.Floor(y / 100);
					b = 2 - a + Math.Floor(a / 4);
				}
				else
				{
					if (fYear == 1582 && fMonth == 10 && fDay >= 15)
					{
						var a = Math.Floor(y / 100);
						b = 2 - a + Math.Floor(a / 4);
					}
					else
						b = 0;
				}
			}

			var c = (y < 0) ? Math.Floor(((365.25 * y) - 0.75)) : Math.Floor(365.25 * y);
			var d = Math.Floor(30.6001 * (m + 1.0));

			return b + c + d + fDay + 1720994.5;
		}

		/// <summary>
		/// Returns the day part of a Julian Date
		/// </summary>
		/// <remarks>
		/// Original macro name: JDCDay
		/// </remarks>
		/// <param name="julianDate"></param>
		/// <returns></returns>
		public static double JulianDateDay(double julianDate)
		{
			var i = Math.Floor(julianDate + 0.5);
			var f = julianDate + 0.5 - i;
			var a = Math.Floor((i - 1867216.25) / 36524.25);
			var b = (i > 2299160) ? i + 1 + a - Math.Floor(a / 4) : i;
			var c = b + 1524;
			var d = Math.Floor((c - 122.1) / 365.25);
			var e = Math.Floor(365.25 * d);
			var g = Math.Floor((c - e) / 30.6001);

			return c - e + f - Math.Floor(30.6001 * g);
		}

		/// <summary>
		/// Returns the month part of a Julian Date
		/// </summary>
		/// <remarks>
		/// Original macro name: JDCMonth
		/// </remarks>
		/// <param name="julianDate"></param>
		/// <returns></returns>
		public static int JulianDateMonth(double julianDate)
		{
			var i = Math.Floor(julianDate + 0.5);
			var a = Math.Floor((i - 1867216.25) / 36524.25);
			var b = (i > 2299160) ? i + 1 + a - Math.Floor(a / 4) : i;
			var c = b + 1524;
			var d = Math.Floor((c - 122.1) / 365.25);
			var e = Math.Floor(365.25 * d);
			var g = Math.Floor((c - e) / 30.6001);

			var returnValue = (g < 13.5) ? g - 1 : g - 13;

			return (int)returnValue;
		}

		/// <summary>
		/// Returns the year part of a Julian Date
		/// </summary>
		/// <remarks>
		/// Original macro name: JDCYear
		/// </remarks>
		/// <param name="julianDate"></param>
		/// <returns></returns>
		public static int JulianDateYear(double julianDate)
		{
			var i = Math.Floor(julianDate + 0.5);
			// var _f = julian_date + 0.5 - i;
			var a = Math.Floor((i - 1867216.25) / 36524.25);
			var b = (i > 2299160) ? i + 1.0 + a - Math.Floor(a / 4.0) : i;
			var c = b + 1524;
			var d = Math.Floor((c - 122.1) / 365.25);
			var e = Math.Floor(365.25 * d);
			var g = Math.Floor((c - e) / 30.6001);
			var h = (g < 13.5) ? g - 1 : g - 13;

			var returnValue = (h > 2.5) ? d - 4716 : d - 4715;

			return (int)returnValue;
		}
	}
}