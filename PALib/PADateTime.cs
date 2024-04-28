using System;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Date and time calculations.
/// </summary>
public class PADateTime
{
	/// <summary>
	/// Gets the date of Easter for the year specified.
	/// </summary>
	/// <returns>(Month, Day, Year)</returns>
	public (int Month, int Day, int Year) GetDateOfEaster(int inputYear)
	{
		double year = inputYear;

		double a = year % 19;
		double b = (year / 100).Floor();
		double c = year % 100;
		double d = (b / 4).Floor();
		double e = b % 4;
		double f = ((b + 8) / 25).Floor();
		double g = ((b - f + 1) / 3).Floor();
		double h = ((19 * a) + b - d - g + 15) % 30;
		double i = (c / 4).Floor();
		double k = c % 4;
		double l = (32 + 2 * (e + i) - h - k) % 7;
		double m = ((a + (11 * h) + (22 * l)) / 451).Floor();
		double n = ((h + l - (7 * m) + 114) / 31).Floor();
		double p = (h + l - (7 * m) + 114) % 31;

		double day = p + 1;
		double month = n;

		return ((int)month, (int)day, (int)year);
	}

	/// <summary>
	/// Calculate day number for a date.
	/// </summary>
	public int CivilDateToDayNumber(int month, int day, int year)
	{
		if (month <= 2)
		{
			month = month - 1;
			month = year.IsLeapYear() ? month * 62 : month * 63;
			month = (int)((double)month / 2).Floor();
		}
		else
		{
			month = (int)(((double)month + 1) * 30.6).Floor();
			month = year.IsLeapYear() ? month - 62 : month - 63;
		}

		return month + day;
	}

	/// <summary>
	/// Convert a Civil Time (hours,minutes,seconds) to Decimal Hours
	/// </summary>
	public double CivilTimeToDecimalHours(double hours, double minutes, double seconds)
	{
		return PAMacros.HMStoDH(hours, minutes, seconds);
	}

	/// <summary>
	/// Convert Decimal Hours to Civil Time
	/// </summary>
	/// <returns>Tuple(hours (double), minutes (double), seconds (double))</returns>
	public (double hours, double minutes, double seconds) DecimalHoursToCivilTime(double decimalHours)
	{
		int hours = PAMacros.DecimalHoursHour(decimalHours);
		int minutes = PAMacros.DecimalHoursMinute(decimalHours);
		double seconds = PAMacros.DecimalHoursSecond(decimalHours);

		return (hours, minutes, seconds);
	}

	/// <summary>
	/// Convert local Civil Time to Universal Time
	/// </summary>
	/// <returns>Tuple (int utHours, int utMinutes, int utSeconds, int gwDay, int gwMonth, int gwYear)</returns>
	public (int utHours, int utMinutes, int utSeconds, int gwDay, int gwMonth, int gwYear) LocalCivilTimeToUniversalTime(double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSavings, int zoneCorrection, double localDay, int localMonth, int localYear)
	{
		double lct = CivilTimeToDecimalHours(lctHours, lctMinutes, lctSeconds);

		int daylightSavingsOffset = isDaylightSavings ? 1 : 0;

		double utInterim = lct - daylightSavingsOffset - zoneCorrection;
		double gdayInterim = localDay + (utInterim / 24);

		double jd = PAMacros.CivilDateToJulianDate(gdayInterim, localMonth, localYear);

		double gDay = PAMacros.JulianDateDay(jd);
		int gMonth = PAMacros.JulianDateMonth(jd);
		int gYear = PAMacros.JulianDateYear(jd);

		double ut = 24 * (gDay - gDay.Floor());

		return (
			PAMacros.DecimalHoursHour(ut),
			PAMacros.DecimalHoursMinute(ut),
			(int)PAMacros.DecimalHoursSecond(ut),
			(int)gDay.Floor(),
			gMonth,
			gYear
		);
	}

	/// <summary>
	/// Convert Universal Time to local Civil Time
	/// </summary>
	/// <returns>Tuple (int lctHours, int lctMinutes, int lctSeconds, int localDay, int localMonth, int localYear)</returns>
	public (int lctHours, int lctMinutes, int lctSeconds, int localDay, int localMonth, int localYear) UniversalTimeToLocalCivilTime(double utHours, double utMinutes, double utSeconds, bool isDaylightSavings, int zoneCorrection, int gwDay, int gwMonth, int gwYear)
	{
		int dstValue = isDaylightSavings ? 1 : 0;
		double ut = PAMacros.HMStoDH(utHours, utMinutes, utSeconds);
		double zoneTime = ut + zoneCorrection;
		double localTime = zoneTime + dstValue;
		double localJDPlusLocalTime = PAMacros.CivilDateToJulianDate(gwDay, gwMonth, gwYear) + (localTime / 24);
		double localDay = PAMacros.JulianDateDay(localJDPlusLocalTime);
		double integerDay = localDay.Floor();
		int localMonth = PAMacros.JulianDateMonth(localJDPlusLocalTime);
		int localYear = PAMacros.JulianDateYear(localJDPlusLocalTime);

		double lct = 24 * (localDay - integerDay);

		return (
			PAMacros.DecimalHoursHour(lct),
			PAMacros.DecimalHoursMinute(lct),
			(int)PAMacros.DecimalHoursSecond(lct),
			(int)integerDay,
			localMonth,
			localYear
		);
	}

	/// <summary>
	/// Convert Universal Time to Greenwich Sidereal Time
	/// </summary>
	/// <returns>Tuple (int gstHours, int gstMinutes, double gstSeconds)</returns>
	public (int gstHours, int gstMinutes, double gstSeconds) UniversalTimeToGreenwichSiderealTime(double utHours, double utMinutes, double utSeconds, double gwDay, int gwMonth, int gwYear)
	{
		double jd = PAMacros.CivilDateToJulianDate(gwDay, gwMonth, gwYear);
		double s = jd - 2451545;
		double t = s / 36525;
		double t01 = 6.697374558 + (2400.051336 * t) + (0.000025862 * t * t);
		double t02 = t01 - (24.0 * (t01 / 24).Floor());
		double ut = PAMacros.HMStoDH(utHours, utMinutes, utSeconds);
		double a = ut * 1.002737909;
		double gst1 = t02 + a;
		double gst2 = gst1 - (24.0 * (gst1 / 24).Floor());

		int gstHours = PAMacros.DecimalHoursHour(gst2);
		int gstMinutes = PAMacros.DecimalHoursMinute(gst2);
		double gstSeconds = PAMacros.DecimalHoursSecond(gst2);

		return (gstHours, gstMinutes, gstSeconds);
	}

	/// <summary>
	/// Convert Greenwich Sidereal Time to Universal Time
	/// </summary>
	/// <returns>Tuple (int utHours, int utMinutes, double utSeconds, PAWarningFlag warningFlag)</returns>
	public (int utHours, int utMinutes, double utSeconds, PAWarningFlag warningFlag) GreenwichSiderealTimeToUniversalTime(double gstHours, double gstMinutes, double gstSeconds, double gwDay, int gwMonth, int gwYear)
	{
		double jd = PAMacros.CivilDateToJulianDate(gwDay, gwMonth, gwYear);
		double s = jd - 2451545;
		double t = s / 36525;
		double t01 = 6.697374558 + (2400.051336 * t) + (0.000025862 * t * t);
		double t02 = t01 - (24 * (t01 / 24).Floor());
		double gstHours1 = PAMacros.HMStoDH(gstHours, gstMinutes, gstSeconds);

		double a = gstHours1 - t02;
		double b = a - (24 * (a / 24).Floor());
		double ut = b * 0.9972695663;
		int utHours = PAMacros.DecimalHoursHour(ut);
		int utMinutes = PAMacros.DecimalHoursMinute(ut);
		double utSeconds = PAMacros.DecimalHoursSecond(ut);

		PAWarningFlag warningFlag = (ut < 0.065574) ? PAWarningFlag.Warning : PAWarningFlag.OK;

		return (utHours, utMinutes, utSeconds, warningFlag);
	}

	/// <summary>
	/// Convert Greenwich Sidereal Time to Local Sidereal Time
	/// </summary>
	/// <returns>Tuple (int lstHours, int lstMinutes, double lstSeconds)</returns>
	public (int lstHours, int lstMinutes, double lstSeconds) GreenwichSiderealTimeToLocalSiderealTime(double gstHours, double gstMinutes, double gstSeconds, double geographicalLongitude)
	{
		double gst = PAMacros.HMStoDH(gstHours, gstMinutes, gstSeconds);
		double offset = geographicalLongitude / 15;
		double lstHours1 = gst + offset;
		double lstHours2 = lstHours1 - (24 * (lstHours1 / 24).Floor());

		int lstHours = PAMacros.DecimalHoursHour(lstHours2);
		int lstMinutes = PAMacros.DecimalHoursMinute(lstHours2);
		double lstSeconds = PAMacros.DecimalHoursSecond(lstHours2);

		return (lstHours, lstMinutes, lstSeconds);
	}

	/// <summary>
	/// Convert Local Sidereal Time to Greenwich Sidereal Time
	/// </summary>
	/// <returns>Tuple (int gstHours, int gstMinutes, double gstSeconds)</returns>
	public (int gstHours, int gstMinutes, double gstSeconds) LocalSiderealTimeToGreenwichSiderealTime(double lstHours, double lstMinutes, double lstSeconds, double geographicalLongitude)
	{
		double gst = PAMacros.HMStoDH(lstHours, lstMinutes, lstSeconds);
		double longHours = geographicalLongitude / 15;
		double gst1 = gst - longHours;
		double gst2 = gst1 - (24 * (gst1 / 24).Floor());

		int gstHours = PAMacros.DecimalHoursHour(gst2);
		int gstMinutes = PAMacros.DecimalHoursMinute(gst2);
		double gstSeconds = PAMacros.DecimalHoursSecond(gst2);

		return (gstHours, gstMinutes, gstSeconds);
	}
}
