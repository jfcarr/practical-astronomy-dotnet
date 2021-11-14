using System;

namespace PALib;

/// <summary>
/// Utility methods.
/// </summary>
public static class PAUtils
{
	/// <summary>
	/// Determine if year is a leap year.
	/// </summary>
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
}
