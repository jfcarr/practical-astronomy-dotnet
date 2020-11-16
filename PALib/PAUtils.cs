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
	}
}