using System;
namespace PALib
{
	public static class PAUtils
	{
		/// <summary>
		/// Determine if year is a leap year.
		/// </summary>
		/// <param name="inputYear"></param>
		/// <returns></returns>
		public static bool IsLeapYear(this int inputYear)
		{
			double year = inputYear;

			if (year % 4 == 0)
			{
				if (year % 100 == 0)
					return (year % 400 == 0) ? true : false;
				else
					return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Convert degrees to radians.
		/// </summary>
		/// <param name="degrees"></param>
		/// <returns></returns>
		public static double ToRadians(this double degrees)
		{
			return degrees * (Math.PI / 180);
		}

		/// <summary>
		/// Convert radians to degrees.
		/// </summary>
		/// <param name="radians"></param>
		/// <returns></returns>
		public static double ToDegrees(this double radians)
		{
			return radians * Math.PI / 180;
		}
	}
}