﻿using System;

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
	}
}
