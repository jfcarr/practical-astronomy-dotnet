using System;
using System.Collections.Generic;
using System.Linq;
using PALib.Data;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Miscellaneous macro functions supporting the other classes.
/// </summary>
public static class PAMacros
{
	/// <summary>
	/// Convert a Civil Time (hours,minutes,seconds) to Decimal Hours
	/// </summary>
	/// <remarks>
	/// Original macro name: HMSDH
	/// </remarks>
	public static double HMStoDH(double hours, double minutes, double seconds)
	{
		double fHours = hours;
		double fMinutes = minutes;
		double fSeconds = seconds;

		double a = Math.Abs(fSeconds) / 60;
		double b = (Math.Abs(fMinutes) + a) / 60;
		double c = Math.Abs(fHours) + b;

		return (fHours < 0 || fMinutes < 0 || fSeconds < 0) ? -c : c;
	}

	/// <summary>
	/// Return the hour part of a Decimal Hours
	/// </summary>
	/// <remarks>
	/// Original macro name: DHHour
	/// </remarks>
	public static int DecimalHoursHour(double decimalHours)
	{
		double a = Math.Abs(decimalHours);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double e = (c == 60) ? b + 60 : b;

		return (decimalHours < 0) ? (int)-(e / 3600).Floor() : (int)(e / 3600).Floor();
	}

	/// <summary>
	/// Return the minutes part of a Decimal Hours
	/// </summary>
	/// <remarks>
	/// Original macro name: DHMin
	/// </remarks>
	public static int DecimalHoursMinute(double decimalHours)
	{
		double a = Math.Abs(decimalHours);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double e = (c == 60) ? b + 60 : b;

		return (int)(e / 60).Floor() % 60;
	}

	/// <summary>
	/// Return the seconds part of a Decimal Hours
	/// </summary>
	/// <remarks>
	/// Original macro name: DHSec
	/// </remarks>
	public static double DecimalHoursSecond(double decimalHours)
	{
		double a = Math.Abs(decimalHours);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double d = (c == 60) ? 0 : c;

		return d;
	}

	/// <summary>
	/// Convert a Greenwich Date/Civil Date (day,month,year) to Julian Date
	/// </summary>
	/// <remarks>
	/// Original macro name: CDJD
	/// </remarks>
	public static double CivilDateToJulianDate(double day, double month, double year)
	{
		double fDay = (double)day;
		double fMonth = (double)month;
		double fYear = (double)year;

		double y = (fMonth < 3) ? fYear - 1 : fYear;
		double m = (fMonth < 3) ? fMonth + 12 : fMonth;

		double b;

		if (fYear > 1582)
		{
			double a = (y / 100).Floor();
			b = 2 - a + (a / 4).Floor();
		}
		else
		{
			if (fYear == 1582 && fMonth > 10)
			{
				double a = (y / 100).Floor();
				b = 2 - a + (a / 4).Floor();
			}
			else
			{
				if (fYear == 1582 && fMonth == 10 && fDay >= 15)
				{
					double a = (y / 100).Floor();
					b = 2 - a + (a / 4).Floor();
				}
				else
					b = 0;
			}
		}

		double c = (y < 0) ? ((365.25 * y) - 0.75).Floor() : (365.25 * y).Floor();
		double d = (30.6001 * (m + 1.0)).Floor();

		return b + c + d + fDay + 1720994.5;
	}

	/// <summary>
	/// Returns the day part of a Julian Date
	/// </summary>
	/// <remarks>
	/// Original macro name: JDCDay
	/// </remarks>
	public static double JulianDateDay(double julianDate)
	{
		double i = (julianDate + 0.5).Floor();
		double f = julianDate + 0.5 - i;
		double a = ((i - 1867216.25) / 36524.25).Floor();
		double b = (i > 2299160) ? i + 1 + a - (a / 4).Floor() : i;
		double c = b + 1524;
		double d = ((c - 122.1) / 365.25).Floor();
		double e = (365.25 * d).Floor();
		double g = ((c - e) / 30.6001).Floor();

		return c - e + f - (30.6001 * g).Floor();
	}

	/// <summary>
	/// Returns the month part of a Julian Date
	/// </summary>
	/// <remarks>
	/// Original macro name: JDCMonth
	/// </remarks>
	public static int JulianDateMonth(double julianDate)
	{
		double i = (julianDate + 0.5).Floor();
		double a = ((i - 1867216.25) / 36524.25).Floor();
		double b = (i > 2299160) ? i + 1 + a - (a / 4).Floor() : i;
		double c = b + 1524;
		double d = ((c - 122.1) / 365.25).Floor();
		double e = (365.25 * d).Floor();
		double g = ((c - e) / 30.6001).Floor();

		double returnValue = (g < 13.5) ? g - 1 : g - 13;

		return (int)returnValue;
	}

	/// <summary>
	/// Returns the year part of a Julian Date
	/// </summary>
	/// <remarks>
	/// Original macro name: JDCYear
	/// </remarks>
	public static int JulianDateYear(double julianDate)
	{
		double i = (julianDate + 0.5).Floor();
		double a = ((i - 1867216.25) / 36524.25).Floor();
		double b = (i > 2299160) ? i + 1.0 + a - (a / 4.0).Floor() : i;
		double c = b + 1524;
		double d = ((c - 122.1) / 365.25).Floor();
		double e = (365.25 * d).Floor();
		double g = ((c - e) / 30.6001).Floor();
		double h = (g < 13.5) ? g - 1 : g - 13;

		double returnValue = (h > 2.5) ? d - 4716 : d - 4715;

		return (int)returnValue;
	}

	/// <summary>
	/// Convert Right Ascension to Hour Angle
	/// </summary>
	/// <remarks>
	/// Original macro name: RAHA
	/// </remarks>
	public static double RightAscensionToHourAngle(double raHours, double raMinutes, double raSeconds, double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
	{
		double a = LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double b = LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int c = LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int d = LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double e = UniversalTimeToGreenwichSiderealTime(a, 0, 0, b, c, d);
		double f = GreenwichSiderealTimeToLocalSiderealTime(e, 0, 0, geographicalLongitude);
		double g = HMStoDH(raHours, raMinutes, raSeconds);
		double h = f - g;

		return (h < 0) ? 24 + h : h;
	}

	/// <summary>
	/// Convert Hour Angle to Right Ascension
	/// </summary>
	/// <remarks>
	/// Original macro name: HARA
	/// </remarks>
	public static double HourAngleToRightAscension(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
	{
		double a = LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double b = LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int c = LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int d = LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double e = UniversalTimeToGreenwichSiderealTime(a, 0, 0, b, c, d);
		double f = GreenwichSiderealTimeToLocalSiderealTime(e, 0, 00, geographicalLongitude);
		double g = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
		double h = f - g;

		return (h < 0) ? 24 + h : h;
	}

	/// <summary>
	/// Convert Local Civil Time to Universal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: LctUT
	/// </remarks>
	public static double LocalCivilTimeToUniversalTime(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
	{
		double a = HMStoDH(lctHours, lctMinutes, lctSeconds);
		double b = a - daylightSaving - zoneCorrection;
		double c = localDay + (b / 24);
		double d = CivilDateToJulianDate(c, localMonth, localYear);
		double e = JulianDateDay(d);
		double e1 = e.Floor();

		return 24 * (e - e1);
	}

	/// <summary>
	/// Convert Universal Time to Local Civil Time
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLct
	/// </remarks>
	public static double UniversalTimeToLocalCivilTime(double uHours, double uMinutes, double uSeconds, int daylightSaving, int zoneCorrection, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = HMStoDH(uHours, uMinutes, uSeconds);
		double b = a + zoneCorrection;
		double c = b + daylightSaving;
		double d = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) + (c / 24);
		double e = JulianDateDay(d);
		double e1 = e.Floor();

		return 24 * (e - e1);
	}

	/// <summary>
	/// Get Local Civil Day for Universal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLcDay
	/// </remarks>
	public static double UniversalTime_LocalCivilDay(double uHours, double uMinutes, double uSeconds, int daylightSaving, int zoneCorrection, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = HMStoDH(uHours, uMinutes, uSeconds);
		double b = a + zoneCorrection;
		double c = b + daylightSaving;
		double d = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) + (c / 24.0);
		double e = JulianDateDay(d);
		double e1 = e.Floor();

		return e1;
	}

	/// <summary>
	/// Get Local Civil Month for Universal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLcMonth
	/// </remarks>
	public static int UniversalTime_LocalCivilMonth(double uHours, double uMinutes, double uSeconds, int daylightSaving, int zoneCorrection, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = HMStoDH(uHours, uMinutes, uSeconds);
		double b = a + zoneCorrection;
		double c = b + daylightSaving;
		double d = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) + (c / 24.0);

		return JulianDateMonth(d);
	}

	/// <summary>
	/// Get Local Civil Year for Universal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLcYear
	/// </remarks>
	public static int UniversalTime_LocalCivilYear(double uHours, double uMinutes, double uSeconds, int daylightSaving, int zoneCorrection, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = HMStoDH(uHours, uMinutes, uSeconds);
		double b = a + zoneCorrection;
		double c = b + daylightSaving;
		double d = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) + (c / 24.0);

		return JulianDateYear(d);
	}

	/// <summary>
	/// Determine Greenwich Day for Local Time
	/// </summary>
	/// <remarks>
	/// Original macro name: LctGDay
	/// </remarks>
	public static double LocalCivilTimeGreenwichDay(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
	{
		double a = HMStoDH(lctHours, lctMinutes, lctSeconds);
		double b = a - daylightSaving - zoneCorrection;
		double c = localDay + (b / 24);
		double d = CivilDateToJulianDate(c, localMonth, localYear);
		double e = JulianDateDay(d);

		return e.Floor();
	}

	/// <summary>
	/// Determine Greenwich Month for Local Time
	/// </summary>
	/// <remarks>
	/// Original macro name: LctGMonth
	/// </remarks>
	public static int LocalCivilTimeGreenwichMonth(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
	{
		double a = HMStoDH(lctHours, lctMinutes, lctSeconds);
		double b = a - daylightSaving - zoneCorrection;
		double c = localDay + (b / 24);
		double d = CivilDateToJulianDate(c, localMonth, localYear);

		return JulianDateMonth(d);
	}

	/// <summary>
	/// Determine Greenwich Year for Local Time
	/// </summary>
	/// <remarks>
	/// Original macro name: LctGYear
	/// </remarks>
	public static int LocalCivilTimeGreenwichYear(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
	{
		double a = HMStoDH(lctHours, lctMinutes, lctSeconds);
		double b = a - daylightSaving - zoneCorrection;
		double c = localDay + (b / 24);
		double d = CivilDateToJulianDate(c, localMonth, localYear);

		return JulianDateYear(d);
	}

	/// <summary>
	/// Convert Universal Time to Greenwich Sidereal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: UTGST
	/// </remarks>
	public static double UniversalTimeToGreenwichSiderealTime(double uHours, double uMinutes, double uSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
		double b = a - 2451545;
		double c = b / 36525;
		double d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
		double e = d - (24 * (d / 24).Floor());
		double f = HMStoDH(uHours, uMinutes, uSeconds);
		double g = f * 1.002737909;
		double h = e + g;

		return h - (24 * (h / 24).Floor());
	}

	/// <summary>
	/// Convert Greenwich Sidereal Time to Local Sidereal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: GSTLST
	/// </remarks>
	public static double GreenwichSiderealTimeToLocalSiderealTime(double greenwichHours, double greenwichMinutes, double greenwichSeconds, double geographicalLongitude)
	{
		double a = HMStoDH(greenwichHours, greenwichMinutes, greenwichSeconds);
		double b = geographicalLongitude / 15;
		double c = a + b;

		return c - (24 * (c / 24).Floor());
	}

	/// <summary>
	/// Convert Equatorial Coordinates to Azimuth (in decimal degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: EQAz
	/// </remarks>
	public static double EquatorialCoordinatesToAzimuth(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
	{
		double a = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
		double b = a * 15;
		double c = b.ToRadians();
		double d = DegreesMinutesSecondsToDecimalDegrees(declinationDegrees, declinationMinutes, declinationSeconds);
		double e = d.ToRadians();
		double f = geographicalLatitude.ToRadians();
		double g = e.Sine() * f.Sine() + e.Cosine() * f.Cosine() * c.Cosine();
		double h = -e.Cosine() * f.Cosine() * c.Sine();
		double i = e.Sine() - (f.Sine() * g);
		double j = Degrees(h.AngleTangent2(i));

		return j - 360.0 * (j / 360).Floor();
	}

	/// <summary>
	/// Convert Equatorial Coordinates to Altitude (in decimal degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: EQAlt
	/// </remarks>
	public static double EquatorialCoordinatesToAltitude(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
	{
		double a = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
		double b = a * 15;
		double c = b.ToRadians();
		double d = DegreesMinutesSecondsToDecimalDegrees(declinationDegrees, declinationMinutes, declinationSeconds);
		double e = d.ToRadians();
		double f = geographicalLatitude.ToRadians();
		double g = e.Sine() * f.Sine() + e.Cosine() * f.Cosine() * c.Cosine();

		return Degrees(g.ASine());
	}

	/// <summary>
	/// Convert Degrees Minutes Seconds to Decimal Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: DMSDD
	/// </remarks>
	public static double DegreesMinutesSecondsToDecimalDegrees(double degrees, double minutes, double seconds)
	{
		double a = Math.Abs(seconds) / 60;
		double b = (Math.Abs(minutes) + a) / 60;
		double c = Math.Abs(degrees) + b;

		return (degrees < 0 || minutes < 0 || seconds < 0) ? -c : c;
	}

	/// <summary>
	/// Convert W to Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: Degrees
	/// </remarks>
	public static double Degrees(double w)
	{
		return w * 57.29577951;
	}

	/// <summary>
	/// Return Degrees part of Decimal Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: DDDeg
	/// </remarks>
	public static double DecimalDegreesDegrees(double decimalDegrees)
	{
		double a = Math.Abs(decimalDegrees);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double e = (c == 60) ? 60 : b;

		return (decimalDegrees < 0) ? -(e / 3600).Floor() : (e / 3600).Floor();
	}

	/// <summary>
	/// Return Minutes part of Decimal Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: DDMin
	/// </remarks>
	public static double DecimalDegreesMinutes(double decimalDegrees)
	{
		double a = Math.Abs(decimalDegrees);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double e = (c == 60) ? b + 60 : b;

		return (e / 60).Floor() % 60;
	}

	/// <summary>
	/// Return Seconds part of Decimal Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: DDSec
	/// </remarks>
	public static double DecimalDegreesSeconds(double decimalDegrees)
	{
		double a = Math.Abs(decimalDegrees);
		double b = a * 3600;
		double c = Math.Round(b - 60 * (b / 60).Floor(), 2);
		double d = (c == 60) ? 0 : c;

		return d;
	}

	/// <summary>
	/// Convert Decimal Degrees to Degree-Hours
	/// </summary>
	/// <remarks>
	/// Original macro name: DDDH
	/// </remarks>
	public static double DecimalDegreesToDegreeHours(double decimalDegrees)
	{
		return decimalDegrees / 15;
	}

	/// <summary>
	/// Convert Degree-Hours to Decimal Degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: DHDD
	/// </remarks>
	public static double DegreeHoursToDecimalDegrees(double degreeHours)
	{
		return degreeHours * 15;
	}

	/// <summary>
	/// Convert Horizon Coordinates to Declination (in decimal degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: HORDec
	/// </remarks>
	public static double HorizonCoordinatesToDeclination(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
	{
		double a = DegreesMinutesSecondsToDecimalDegrees(azimuthDegrees, azimuthMinutes, azimuthSeconds);
		double b = DegreesMinutesSecondsToDecimalDegrees(altitudeDegrees, altitudeMinutes, altitudeSeconds);
		double c = a.ToRadians();
		double d = b.ToRadians();
		double e = geographicalLatitude.ToRadians();
		double f = d.Sine() * e.Sine() + d.Cosine() * e.Cosine() * c.Cosine();

		return Degrees(f.ASine());
	}

	/// <summary>
	/// Convert Horizon Coordinates to Hour Angle (in decimal degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: HORHa
	/// </remarks>
	public static double HorizonCoordinatesToHourAngle(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
	{
		double a = DegreesMinutesSecondsToDecimalDegrees(azimuthDegrees, azimuthMinutes, azimuthSeconds);
		double b = DegreesMinutesSecondsToDecimalDegrees(altitudeDegrees, altitudeMinutes, altitudeSeconds);
		double c = a.ToRadians();
		double d = b.ToRadians();
		double e = geographicalLatitude.ToRadians();
		double f = d.Sine() * e.Sine() + d.Cosine() * e.Cosine() * c.Cosine();
		double g = -d.Cosine() * e.Cosine() * c.Sine();
		double h = d.Sine() - e.Sine() * f;
		double i = DecimalDegreesToDegreeHours(Degrees(g.AngleTangent2(h)));

		return i - 24 * (i / 24).Floor();
	}

	/// <summary>
	/// Obliquity of the Ecliptic for a Greenwich Date
	/// </summary>
	/// <remarks>
	/// Original macro name: Obliq
	/// </remarks>
	public static double Obliq(double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
		double b = a - 2415020;
		double c = (b / 36525) - 1;
		double d = c * (46.815 + c * (0.0006 - (c * 0.00181)));
		double e = d / 3600;

		return 23.43929167 - e + NutatObl(greenwichDay, greenwichMonth, greenwichYear);
	}

	/// <summary>
	/// Nutation amount to be added in ecliptic longitude, in degrees.
	/// </summary>
	/// <remarks>
	/// Original macro name: NutatLong
	/// </remarks>
	public static double NutatLong(double gd, int gm, int gy)
	{
		double dj = CivilDateToJulianDate(gd, gm, gy) - 2415020;
		double t = dj / 36525;
		double t2 = t * t;

		double a = 100.0021358 * t;
		double b = 360 * (a - a.Floor());

		double l1 = 279.6967 + 0.000303 * t2 + b;
		double l2 = 2 * l1.ToRadians();

		a = 1336.855231 * t;
		b = 360 * (a - a.Floor());

		double d1 = 270.4342 - 0.001133 * t2 + b;
		double d2 = 2 * d1.ToRadians();

		a = 99.99736056 * t;
		b = 360 * (a - a.Floor());

		double m1 = 358.4758 - 0.00015 * t2 + b;
		m1 = m1.ToRadians();

		a = 1325.552359 * t;
		b = 360 * (a - a.Floor());

		double m2 = 296.1046 + 0.009192 * t2 + b;
		m2 = m2.ToRadians();

		a = 5.372616667 * t;
		b = 360 * (a - a.Floor());

		double n1 = 259.1833 + 0.002078 * t2 - b;
		n1 = n1.ToRadians();

		double n2 = 2.0 * n1;

		double dp = (-17.2327 - 0.01737 * t) * n1.Sine();
		dp = dp + (-1.2729 - 0.00013 * t) * l2.Sine() + 0.2088 * n2.Sine();
		dp = dp - 0.2037 * d2.Sine() + (0.1261 - 0.00031 * t) * m1.Sine();
		dp = dp + 0.0675 * m2.Sine() - (0.0497 - 0.00012 * t) * (l2 + m1).Sine();
		dp = dp - 0.0342 * (d2 - n1).Sine() - 0.0261 * (d2 + m2).Sine();
		dp = dp + 0.0214 * (l2 - m1).Sine() - 0.0149 * (l2 - d2 + m2).Sine();
		dp = dp + 0.0124 * (l2 - n1).Sine() + 0.0114 * (d2 - m2).Sine();

		return dp / 3600;
	}

	/// <summary>
	/// Nutation of Obliquity
	/// </summary>
	/// <remarks>
	/// Original macro name: NutatObl
	/// </remarks>
	public static double NutatObl(double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double dj = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) - 2415020;
		double t = dj / 36525;
		double t2 = t * t;

		double a = 100.0021358 * t;
		double b = 360 * (a - a.Floor());

		double l1 = 279.6967 + 0.000303 * t2 + b;
		double l2 = 2 * l1.ToRadians();

		a = 1336.855231 * t;
		b = 360 * (a - a.Floor());

		double d1 = 270.4342 - 0.001133 * t2 + b;
		double d2 = 2 * d1.ToRadians();

		a = 99.99736056 * t;
		b = 360 * (a - a.Floor());

		double m1 = (358.4758 - 0.00015 * t2 + b).ToRadians();

		a = 1325.552359 * t;
		b = 360 * (a - a.Floor());

		double m2 = (296.1046 + 0.009192 * t2 + b).ToRadians();

		a = 5.372616667 * t;
		b = 360 * (a - a.Floor());

		double n1 = (259.1833 + 0.002078 * t2 - b).ToRadians();

		double n2 = 2 * n1;

		double ddo = (9.21 + 0.00091 * t) * n1.Cosine();
		ddo = ddo + (0.5522 - 0.00029 * t) * l2.Cosine() - 0.0904 * n2.Cosine();
		ddo = ddo + 0.0884 * d2.Cosine() + 0.0216 * (l2 + m1).Cosine();
		ddo = ddo + 0.0183 * (d2 - n1).Cosine() + 0.0113 * (d2 + m2).Cosine();
		ddo = ddo - 0.0093 * (l2 - m1).Cosine() - 0.0066 * (l2 - n1).Cosine();

		return ddo / 3600;
	}

	/// <summary>
	/// Convert Local Sidereal Time to Greenwich Sidereal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: LSTGST
	/// </remarks>
	public static double LocalSiderealTimeToGreenwichSiderealTime(double localHours, double localMinutes, double localSeconds, double longitude)
	{
		double a = HMStoDH(localHours, localMinutes, localSeconds);
		double b = longitude / 15;
		double c = a - b;

		return c - (24 * (c / 24).Floor());
	}

	/// <summary>
	/// Convert Greenwich Sidereal Time to Universal Time
	/// </summary>
	/// <remarks>
	/// Original macro name: GSTUT
	/// </remarks>
	public static double GreenwichSiderealTimeToUniversalTime(double greenwichSiderealHours, double greenwichSiderealMinutes, double greenwichSiderealSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
		double b = a - 2451545;
		double c = b / 36525;
		double d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
		double e = d - (24 * (d / 24).Floor());
		double f = HMStoDH(greenwichSiderealHours, greenwichSiderealMinutes, greenwichSiderealSeconds);
		double g = f - e;
		double h = g - (24 * (g / 24).Floor());

		return h * 0.9972695663;
	}

	/// <summary>
	/// Status of conversion of Greenwich Sidereal Time to Universal Time.
	/// </summary>
	/// <remarks>
	/// Original macro name: eGSTUT
	/// </remarks>
	public static PAWarningFlag EGstUt(double gsh, double gsm, double gss, double gd, int gm, int gy)
	{
		double a = CivilDateToJulianDate(gd, gm, gy);
		double b = a - 2451545;
		double c = b / 36525;
		double d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
		double e = d - (24 * (d / 24).Floor());
		double f = HMStoDH(gsh, gsm, gss);
		double g = f - e;
		double h = g - (24 * (g / 24).Floor());

		return ((h * 0.9972695663) < (4.0 / 60.0)) ? PAWarningFlag.Warning : PAWarningFlag.OK;
	}

	/// <summary>
	/// Calculate Sun's ecliptic longitude
	/// </summary>
	/// <remarks>
	/// Original macro name: SunLong
	/// </remarks>
	public static double SunLong(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
	{
		double aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;
		double t = (dj / 36525) + (ut / 876600);
		double t2 = t * t;
		double a = 100.0021359 * t;
		double b = 360.0 * (a - a.Floor());

		double l = 279.69668 + 0.0003025 * t2 + b;
		a = 99.99736042 * t;
		b = 360 * (a - a.Floor());

		double m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
		double ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

		double am = m1.ToRadians();
		double at = TrueAnomaly(am, ec);

		a = 62.55209472 * t;
		b = 360 * (a - a.Floor());

		double a1 = (153.23 + b).ToRadians();
		a = 125.1041894 * t;
		b = 360 * (a - a.Floor());

		double b1 = (216.57 + b).ToRadians();
		a = 91.56766028 * t;
		b = 360.0 * (a - a.Floor());

		double c1 = (312.69 + b).ToRadians();
		a = 1236.853095 * t;
		b = 360.0 * (a - a.Floor());

		double d1 = (350.74 - 0.00144 * t2 + b).ToRadians();
		double e1 = (231.19 + 20.2 * t).ToRadians();
		a = 183.1353208 * t;
		b = 360.0 * (a - a.Floor());
		double h1 = (353.4 + b).ToRadians();

		double d2 = 0.00134 * a1.Cosine() + 0.00154 * b1.Cosine() + 0.002 * c1.Cosine();
		d2 = d2 + 0.00179 * d1.Sine() + 0.00178 * e1.Sine();
		double d3 = 0.00000543 * a1.Sine() + 0.00001575 * b1.Sine();
		d3 = d3 + 0.00001627 * c1.Sine() + 0.00003076 * d1.Cosine();

		double sr = at + (l - m1 + d2).ToRadians();
		double tp = 6.283185308;

		sr -= tp * (sr / tp).Floor();

		return Degrees(sr);
	}

	/// <summary>
	/// Solve Kepler's equation, and return value of the true anomaly in radians
	/// </summary>
	/// <remarks>
	/// Original macro name: TrueAnomaly
	/// </remarks>
	public static double TrueAnomaly(double am, double ec)
	{
		double tp = 6.283185308;
		double m = am - tp * (am / tp).Floor();
		double ae = m;

		while (1 == 1)
		{
			double d = ae - (ec * ae.Sine()) - m;
			if (Math.Abs(d) < 0.000001)
			{
				break;
			}
			d /= (1.0 - (ec * ae.Cosine()));
			ae -= d;
		}
		double a = ((1 + ec) / (1 - ec)).SquareRoot() * (ae / 2).Tangent();
		double at = 2.0 * a.AngleTangent();

		return at;
	}

	/// <summary>
	/// Solve Kepler's equation, and return value of the eccentric anomaly in radians
	/// </summary>
	/// <remarks>
	/// Original macro name: EccentricAnomaly
	/// </remarks>
	public static double EccentricAnomaly(double am, double ec)
	{
		double tp = 6.283185308;
		double m = am - tp * (am / tp).Floor();
		double ae = m;

		while (1 == 1)
		{
			double d = ae - (ec * ae.Sine()) - m;

			if (Math.Abs(d) < 0.000001)
			{
				break;
			}

			d /= (1 - (ec * ae.Cosine()));
			ae -= d;
		}

		return ae;
	}

	/// <summary>
	/// Calculate effects of refraction
	/// </summary>
	/// <remarks>
	/// Original macro name: Refract
	/// </remarks>
	public static double Refract(double y2, PACoordinateType sw, double pr, double tr)
	{
		double y = y2.ToRadians();

		double d = (sw == PACoordinateType.True) ? -1.0 : 1.0;

		if (d == -1)
		{
			double y3 = y;
			double y1 = y;
			double r1 = 0.0;

			while (1 == 1)
			{
				double yNew = y1 + r1;
				double rfNew = RefractL3035(pr, tr, yNew, d);

				if (y < -0.087)
					return 0;

				double r2 = rfNew;

				if ((r2 == 0) || (Math.Abs(r2 - r1) < 0.000001))
				{
					double qNew = y3;

					return Degrees(qNew + rfNew);
				}

				r1 = r2;
			}
		}

		double rf = RefractL3035(pr, tr, y, d);

		if (y < -0.087)
			return 0;

		double q = y;

		return Degrees(q + rf);
	}

	/// <summary>
	/// Helper function for Refract
	/// </summary>
	public static double RefractL3035(double pr, double tr, double y, double d)
	{
		if (y < 0.2617994)
		{
			if (y < -0.087)
				return 0;

			double yd = Degrees(y);
			double a = ((0.00002 * yd + 0.0196) * yd + 0.1594) * pr;
			double b = (273.0 + tr) * ((0.0845 * yd + 0.505) * yd + 1);

			return (-(a / b) * d).ToRadians();
		}

		return -d * 0.00007888888 * pr / ((273.0 + tr) * y.Tangent());
	}

	/// <summary>
	/// Calculate corrected hour angle in decimal hours
	/// </summary>
	/// <remarks>
	/// Original macro name: ParallaxHA
	/// </remarks>
	public static double ParallaxHA(double hh, double hm, double hs, double dd, double dm, double ds, PACoordinateType sw, double gp, double ht, double hp)
	{
		double a = gp.ToRadians();
		double c1 = a.Cosine();
		double s1 = a.Sine();

		double u = (0.996647 * s1 / c1).AngleTangent();
		double c2 = u.Cosine();
		double s2 = u.Sine();
		double b = ht / 6378160;

		double rs = (0.996647 * s2) + (b * s1);

		double rc = c2 + (b * c1);
		double tp = 6.283185308;

		double rp = 1.0 / hp.ToRadians().Sine();

		double x = DegreeHoursToDecimalDegrees(HMStoDH(hh, hm, hs)).ToRadians();
		double x1 = x;
		double y = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double y1 = y;

		double d = sw.Equals(PACoordinateType.True) ? 1.0 : -1.0;

		if (d == 1)
		{
			(double p, double q) result = ParallaxHAL2870(x, y, rc, rp, rs, tp);
			return DecimalDegreesToDegreeHours(Degrees(result.p));
		}

		double p1 = 0.0;
		double q1 = 0.0;
		double xLoop = x;
		double yLoop = y;

		while (1 == 1)
		{
			(double p, double q) result = ParallaxHAL2870(xLoop, yLoop, rc, rp, rs, tp);
			double p2 = result.p - xLoop;
			double q2 = result.q - yLoop;

			double aa = Math.Abs(p2 - p1);
			double bb = Math.Abs(q2 - q1);

			if ((aa < 0.000001) && (bb < 0.000001))
			{
				double p = x1 - p2;

				return DecimalDegreesToDegreeHours(Degrees(p));
			}

			xLoop = x1 - p2;
			yLoop = y1 - q2;
			p1 = p2;
			q1 = q2;
		}

		// return DecimalDegreesToDegreeHours(Degrees(0));
	}

	/// <summary>
	/// Helper function for parallax_ha
	/// </summary>
	public static (double p, double q) ParallaxHAL2870(double x, double y, double rc, double rp, double rs, double tp)
	{
		double cx = x.Cosine();
		double sy = y.Sine();
		double cy = y.Cosine();

		double aa = rc * x.Sine() / ((rp * cy) - (rc * cx));

		double dx = aa.AngleTangent();
		double p = x + dx;
		double cp = p.Cosine();

		p -= tp * (p / tp).Floor();
		double q = (cp * (rp * sy - rs) / (rp * cy * cx - rc)).AngleTangent();

		return (p, q);
	}

	/// <summary>
	/// Calculate corrected declination in decimal degrees
	/// </summary>
	/// <remarks>
	/// <para>
	/// Original macro name: ParallaxDec
	/// </para>
	/// <para>
	/// HH,HM,HS,DD,DM,DS,SW,GP,HT,HP
	/// </para>
	/// </remarks>
	public static double ParallaxDec(double hh, double hm, double hs, double dd, double dm, double ds, PACoordinateType sw, double gp, double ht, double hp)
	{
		double a = gp.ToRadians();
		double c1 = a.Cosine();
		double s1 = a.Sine();

		double u = (0.996647 * s1 / c1).AngleTangent();

		double c2 = u.Cosine();
		double s2 = u.Sine();
		double b = ht / 6378160;
		double rs = (0.996647 * s2) + (b * s1);

		double rc = c2 + (b * c1);
		double tp = 6.283185308;

		double rp = 1.0 / hp.ToRadians().Sine();

		double x = DegreeHoursToDecimalDegrees(HMStoDH(hh, hm, hs)).ToRadians();
		double x1 = x;

		double y = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double y1 = y;

		double d = sw.Equals(PACoordinateType.True) ? 1.0 : -1.0;

		if (d == 1)
		{
			(double p, double q) result = ParallaxDecL2870(x, y, rc, rp, rs, tp);

			return Degrees(result.q);
		}

		double p1 = 0.0;
		double q1 = 0.0;

		double xLoop = x;
		double yLoop = y;

		while (1 == 1)
		{
			(double p, double q) result = ParallaxDecL2870(xLoop, yLoop, rc, rp, rs, tp);
			double p2 = result.p - xLoop;
			double q2 = result.q - yLoop;
			double aa = Math.Abs(p2 - p1);

			if ((aa < 0.000001) && (b < 0.000001))
			{
				double q = y1 - q2;

				return Degrees(q);
			}
			xLoop = x1 - p2;
			yLoop = y1 - q2;
			p1 = p2;
			q1 = q2;
		}

		// return Degrees(0.0);
	}

	/// <summary>
	/// Helper function for parallax_dec
	/// </summary>
	public static (double p, double q) ParallaxDecL2870(double x, double y, double rc, double rp, double rs, double tp)
	{
		double cx = x.Cosine();
		double sy = y.Sine();
		double cy = y.Cosine();

		double aa = rc * x.Sine() / ((rp * cy) - (rc * cx));
		double dx = aa.AngleTangent();
		double p = x + dx;
		double cp = p.Cosine();

		p -= tp * (p / tp).Floor();
		double q = (cp * (rp * sy - rs) / (rp * cy * cx - rc)).AngleTangent();

		return (p, q);
	}

	/// <summary>
	/// Calculate Sun's angular diameter in decimal degrees
	/// </summary>
	/// <remarks>
	/// Original macro name: SunDia
	/// </remarks>
	public static double SunDia(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
	{
		double a = SunDist(lch, lcm, lcs, ds, zc, ld, lm, ly);

		return 0.533128 / a;
	}

	/// <summary>
	/// Calculate Sun's distance from the Earth in astronomical units
	/// </summary>
	/// <remarks>
	/// Original macro name: SunDist
	/// </remarks>
	public static double SunDist(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
	{
		double aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;

		double t = (dj / 36525) + (ut / 876600);
		double t2 = t * t;

		double a = 100.0021359 * t;
		double b = 360 * (a - a.Floor());
		a = 99.99736042 * t;
		b = 360 * (a - a.Floor());
		double m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
		double ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

		double am = m1.ToRadians();
		double ae = EccentricAnomaly(am, ec);

		a = 62.55209472 * t;
		b = 360 * (a - a.Floor());
		double a1 = (153.23 + b).ToRadians();
		a = 125.1041894 * t;
		b = 360 * (a - a.Floor());
		double b1 = (216.57 + b).ToRadians();
		a = 91.56766028 * t;
		b = 360 * (a - a.Floor());
		double c1 = (312.69 + b).ToRadians();
		a = 1236.853095 * t;
		b = 360 * (a - a.Floor());
		double d1 = (350.74 - 0.00144 * t2 + b).ToRadians();
		double e1 = (231.19 + 20.2 * t).ToRadians();
		a = 183.1353208 * t;
		b = 360 * (a - a.Floor());
		double h1 = (353.4 + b).ToRadians();

		double d3 = 0.00000543 * a1.Sine() + 0.00001575 * b1.Sine() + (0.00001627 * c1.Sine() + 0.00003076 * d1.Cosine()) + (0.00000927 * h1.Sine());

		return 1.0000002 * (1 - ec * ae.Cosine()) + d3;
	}

	/// <summary>
	/// Calculate geocentric ecliptic longitude for the Moon
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonLong
	/// </remarks>
	public static double MoonLong(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
		double t2 = t * t;

		double m1 = 27.32158213;
		double m2 = 365.2596407;
		double m3 = 27.55455094;
		double m4 = 29.53058868;
		double m5 = 27.21222039;
		double m6 = 6798.363307;
		double q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
		m1 = q / m1;
		m2 = q / m2;
		m3 = q / m3;
		m4 = q / m4;
		m5 = q / m5;
		m6 = q / m6;
		m1 = 360 * (m1 - m1.Floor());
		m2 = 360 * (m2 - m2.Floor());
		m3 = 360 * (m3 - m3.Floor());
		m4 = 360 * (m4 - m4.Floor());
		m5 = 360 * (m5 - m5.Floor());
		m6 = 360 * (m6 - m6.Floor());

		double ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
		double ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
		double md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
		double me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
		double mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
		double na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
		double a = (51.2 + 20.2 * t).ToRadians();
		double s1 = a.Sine();
		double s2 = na.ToRadians().Sine();
		double b = 346.56 + (132.87 - 0.0091731 * t) * t;
		double s3 = 0.003964 * b.ToRadians().Sine();
		double c = (na + 275.05 - 2.3 * t).ToRadians();
		double s4 = c.Sine();
		ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
		ms -= 0.001778 * s1;
		md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
		mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
		me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
		double e = 1.0 - (0.002495 + 0.00000752 * t) * t;
		double e2 = e * e;
		ml = ml.ToRadians();
		ms = ms.ToRadians();
		me1 = me1.ToRadians();
		mf = mf.ToRadians();
		md = md.ToRadians();

		double l = 6.28875 * md.Sine() + 1.274018 * (2.0 * me1 - md).Sine();
		l = l + 0.658309 * (2.0 * me1).Sine() + 0.213616 * (2.0 * md).Sine();
		l = l - e * 0.185596 * ms.Sine() - 0.114336 * (2.0 * mf).Sine();
		l += 0.058793 * (2.0 * (me1 - md)).Sine();
		l = l + 0.057212 * e * (2.0 * me1 - ms - md).Sine() + 0.05332 * (2.0 * me1 + md).Sine();
		l = l + 0.045874 * e * (2.0 * me1 - ms).Sine() + 0.041024 * e * (md - ms).Sine();
		l = l - 0.034718 * me1.Sine() - e * 0.030465 * (ms + md).Sine();
		l = l + 0.015326 * (2.0 * (me1 - mf)).Sine() - 0.012528 * (2.0 * mf + md).Sine();
		l = l - 0.01098 * (2.0 * mf - md).Sine() + 0.010674 * (4.0 * me1 - md).Sine();
		l = l + 0.010034 * (3.0 * md).Sine() + 0.008548 * (4.0 * me1 - 2.0 * md).Sine();
		l = l - e * 0.00791 * (ms - md + 2.0 * me1).Sine() - e * 0.006783 * (2.0 * me1 + ms).Sine();
		l = l + 0.005162 * (md - me1).Sine() + e * 0.005 * (ms + me1).Sine();
		l = l + 0.003862 * (4.0 * me1).Sine() + e * 0.004049 * (md - ms + 2.0 * me1).Sine();
		l = l + 0.003996 * (2.0 * (md + me1)).Sine() + 0.003665 * (2.0 * me1 - 3.0 * md).Sine();
		l = l + e * 0.002695 * (2.0 * md - ms).Sine() + 0.002602 * (md - 2.0 * (mf + me1)).Sine();
		l = l + e * 0.002396 * (2.0 * (me1 - md) - ms).Sine() - 0.002349 * (md + me1).Sine();
		l = l + e2 * 0.002249 * (2.0 * (me1 - ms)).Sine() - e * 0.002125 * (2.0 * md + ms).Sine();
		l = l - e2 * 0.002079 * (2.0 * ms).Sine() + e2 * 0.002059 * (2.0 * (me1 - ms) - md).Sine();
		l = l - 0.001773 * (md + 2.0 * (me1 - mf)).Sine() - 0.001595 * (2.0 * (mf + me1)).Sine();
		l = l + e * 0.00122 * (4.0 * me1 - ms - md).Sine() - 0.00111 * (2.0 * (md + mf)).Sine();
		l = l + 0.000892 * (md - 3.0 * me1).Sine() - e * 0.000811 * (ms + md + 2.0 * me1).Sine();
		l += e * 0.000761 * (4.0 * me1 - ms - 2.0 * md).Sine();
		l += e2 * 0.000704 * (md - 2.0 * (ms + me1)).Sine();
		l += e * 0.000693 * (ms - 2.0 * (md - me1)).Sine();
		l += e * 0.000598 * (2.0 * (me1 - mf) - ms).Sine();
		l = l + 0.00055 * (md + 4.0 * me1).Sine() + 0.000538 * (4.0 * md).Sine();
		l = l + e * 0.000521 * (4.0 * me1 - ms).Sine() + 0.000486 * (2.0 * md - me1).Sine();
		l += e2 * 0.000717 * (md - 2.0 * ms).Sine();
		double mm = Unwind(ml + l.ToRadians());

		return Degrees(mm);
	}

	/// <summary>
	/// Calculate geocentric ecliptic latitude for the Moon
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonLat
	/// </remarks>
	public static double MoonLat(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
		double t2 = t * t;

		double m1 = 27.32158213;
		double m2 = 365.2596407;
		double m3 = 27.55455094;
		double m4 = 29.53058868;
		double m5 = 27.21222039;
		double m6 = 6798.363307;
		double q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
		m1 = q / m1;
		m2 = q / m2;
		m3 = q / m3;
		m4 = q / m4;
		m5 = q / m5;
		m6 = q / m6;
		m1 = 360 * (m1 - m1.Floor());
		m2 = 360 * (m2 - m2.Floor());
		m3 = 360 * (m3 - m3.Floor());
		m4 = 360 * (m4 - m4.Floor());
		m5 = 360 * (m5 - m5.Floor());
		m6 = 360 * (m6 - m6.Floor());

		double ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
		double ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
		double md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
		double me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
		double mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
		double na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
		double a = (51.2 + 20.2 * t).ToRadians();
		double s1 = a.Sine();
		double s2 = na.ToRadians().Sine();
		double b = 346.56 + (132.87 - 0.0091731 * t) * t;
		double s3 = 0.003964 * b.ToRadians().Sine();
		double c = (na + 275.05 - 2.3 * t).ToRadians();
		double s4 = c.Sine();
		ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
		ms -= 0.001778 * s1;
		md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
		mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
		me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
		double e = 1.0 - (0.002495 + 0.00000752 * t) * t;
		double e2 = e * e;
		ms = ms.ToRadians();
		na = na.ToRadians();
		me1 = me1.ToRadians();
		mf = mf.ToRadians();
		md = md.ToRadians();

		double g = 5.128189 * mf.Sine() + 0.280606 * (md + mf).Sine();
		g = g + 0.277693 * (md - mf).Sine() + 0.173238 * (2.0 * me1 - mf).Sine();
		g = g + 0.055413 * (2.0 * me1 + mf - md).Sine() + 0.046272 * (2.0 * me1 - mf - md).Sine();
		g = g + 0.032573 * (2.0 * me1 + mf).Sine() + 0.017198 * (2.0 * md + mf).Sine();
		g = g + 0.009267 * (2.0 * me1 + md - mf).Sine() + 0.008823 * (2.0 * md - mf).Sine();
		g = g + e * 0.008247 * (2.0 * me1 - ms - mf).Sine() + 0.004323 * (2.0 * (me1 - md) - mf).Sine();
		g = g + 0.0042 * (2.0 * me1 + mf + md).Sine() + e * 0.003372 * (mf - ms - 2.0 * me1).Sine();
		g += e * 0.002472 * (2.0 * me1 + mf - ms - md).Sine();
		g += e * 0.002222 * (2.0 * me1 + mf - ms).Sine();
		g += e * 0.002072 * (2.0 * me1 - mf - ms - md).Sine();
		g = g + e * 0.001877 * (mf - ms + md).Sine() + 0.001828 * (4.0 * me1 - mf - md).Sine();
		g = g - e * 0.001803 * (mf + ms).Sine() - 0.00175 * (3.0 * mf).Sine();
		g = g + e * 0.00157 * (md - ms - mf).Sine() - 0.001487 * (mf + me1).Sine();
		g = g - e * 0.001481 * (mf + ms + md).Sine() + e * 0.001417 * (mf - ms - md).Sine();
		g = g + e * 0.00135 * (mf - ms).Sine() + 0.00133 * (mf - me1).Sine();
		g = g + 0.001106 * (mf + 3.0 * md).Sine() + 0.00102 * (4.0 * me1 - mf).Sine();
		g = g + 0.000833 * (mf + 4.0 * me1 - md).Sine() + 0.000781 * (md - 3.0 * mf).Sine();
		g = g + 0.00067 * (mf + 4.0 * me1 - 2.0 * md).Sine() + 0.000606 * (2.0 * me1 - 3.0 * mf).Sine();
		g += 0.000597 * (2.0 * (me1 + md) - mf).Sine();
		g = g + e * 0.000492 * (2.0 * me1 + md - ms - mf).Sine() + 0.00045 * (2.0 * (md - me1) - mf).Sine();
		g = g + 0.000439 * (3.0 * md - mf).Sine() + 0.000423 * (mf + 2.0 * (me1 + md)).Sine();
		g = g + 0.000422 * (2.0 * me1 - mf - 3.0 * md).Sine() - e * 0.000367 * (ms + mf + 2.0 * me1 - md).Sine();
		g = g - e * 0.000353 * (ms + mf + 2.0 * me1).Sine() + 0.000331 * (mf + 4.0 * me1).Sine();
		g += e * 0.000317 * (2.0 * me1 + mf - ms + md).Sine();
		g = g + e2 * 0.000306 * (2.0 * (me1 - ms) - mf).Sine() - 0.000283 * (md + 3.0 * mf).Sine();
		double w1 = 0.0004664 * na.Cosine();
		double w2 = 0.0000754 * c.Cosine();
		double bm = g.ToRadians() * (1.0 - w1 - w2);

		return Degrees(bm);
	}

	/// <summary>
	/// Calculate horizontal parallax for the Moon
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonHP
	/// </remarks>
	public static double MoonHP(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
		double t2 = t * t;

		double m1 = 27.32158213;
		double m2 = 365.2596407;
		double m3 = 27.55455094;
		double m4 = 29.53058868;
		double m5 = 27.21222039;
		double m6 = 6798.363307;
		double q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
		m1 = q / m1;
		m2 = q / m2;
		m3 = q / m3;
		m4 = q / m4;
		m5 = q / m5;
		m6 = q / m6;
		m1 = 360 * (m1 - m1.Floor());
		m2 = 360 * (m2 - m2.Floor());
		m3 = 360 * (m3 - m3.Floor());
		m4 = 360 * (m4 - m4.Floor());
		m5 = 360 * (m5 - m5.Floor());
		m6 = 360 * (m6 - m6.Floor());

		double ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
		double ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
		double md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
		double me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
		double mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
		double na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
		double a = (51.2 + 20.2 * t).ToRadians();
		double s1 = a.Sine();
		double s2 = na.ToRadians().Sine();
		double b = 346.56 + (132.87 - 0.0091731 * t) * t;
		double s3 = 0.003964 * b.ToRadians().Sine();
		double c = (na + 275.05 - 2.3 * t).ToRadians();
		double s4 = c.Sine();
		ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
		ms -= 0.001778 * s1;
		md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
		mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
		me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
		double e = 1.0 - (0.002495 + 0.00000752 * t) * t;
		double e2 = e * e;
		ms = ms.ToRadians();
		me1 = me1.ToRadians();
		mf = mf.ToRadians();
		md = md.ToRadians();

		double pm = 0.950724 + 0.051818 * md.Cosine() + 0.009531 * (2.0 * me1 - md).Cosine();
		pm = pm + 0.007843 * (2.0 * me1).Cosine() + 0.002824 * (2.0 * md).Cosine();
		pm = pm + 0.000857 * (2.0 * me1 + md).Cosine() + e * 0.000533 * (2.0 * me1 - ms).Cosine();
		pm += e * 0.000401 * (2.0 * me1 - md - ms).Cosine();
		pm = pm + e * 0.00032 * (md - ms).Cosine() - 0.000271 * me1.Cosine();
		pm = pm - e * 0.000264 * (ms + md).Cosine() - 0.000198 * (2.0 * mf - md).Cosine();
		pm = pm + 0.000173 * (3.0 * md).Cosine() + 0.000167 * (4.0 * me1 - md).Cosine();
		pm = pm - e * 0.000111 * ms.Cosine() + 0.000103 * (4.0 * me1 - 2.0 * md).Cosine();
		pm = pm - 0.000084 * (2.0 * md - 2.0 * me1).Cosine() - e * 0.000083 * (2.0 * me1 + ms).Cosine();
		pm = pm + 0.000079 * (2.0 * me1 + 2.0 * md).Cosine() + 0.000072 * (4.0 * me1).Cosine();
		pm = pm + e * 0.000064 * (2.0 * me1 - ms + md).Cosine() - e * 0.000063 * (2.0 * me1 + ms - md).Cosine();
		pm = pm + e * 0.000041 * (ms + me1).Cosine() + e * 0.000035 * (2.0 * md - ms).Cosine();
		pm = pm - 0.000033 * (3.0 * md - 2.0 * me1).Cosine() - 0.00003 * (md + me1).Cosine();
		pm = pm - 0.000029 * (2.0 * (mf - me1)).Cosine() - e * 0.000029 * (2.0 * md + ms).Cosine();
		pm = pm + e2 * 0.000026 * (2.0 * (me1 - ms)).Cosine() - 0.000023 * (2.0 * (mf - me1) + md).Cosine();
		pm += e * 0.000019 * (4.0 * me1 - ms - md).Cosine();

		return pm;
	}

	/// <summary>
	/// Calculate distance from the Earth to the Moon (km)
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonDist
	/// </remarks>
	public static double MoonDist(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double hp = MoonHP(lh, lm, ls, ds, zc, dy, mn, yr).ToRadians();
		double r = 6378.14 / hp.Sine();

		return r;
	}

	/// <summary>
	/// Calculate the Moon's angular diameter (degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonSize
	/// </remarks>
	public static double MoonSize(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double hp = MoonHP(lh, lm, ls, ds, zc, dy, mn, yr).ToRadians();
		double r = 6378.14 / hp.Sine();
		double th = 384401.0 * 0.5181 / r;

		return th;
	}

	/// <summary>
	/// Convert angle in radians to equivalent angle in degrees.
	/// </summary>
	/// <remarks>
	/// Original macro name: Unwind
	/// </remarks>
	public static double Unwind(double w)
	{
		return w - 6.283185308 * (w / 6.283185308).Floor();
	}

	/// <summary>
	/// Convert angle in degrees to equivalent angle in the range 0 to 360 degrees.
	/// </summary>
	/// <remarks>
	/// Original macro name: UnwindDeg
	/// </remarks>
	public static double UnwindDeg(double w)
	{
		return w - 360 * (w / 360).Floor();
	}

	/// <summary>
	/// Mean ecliptic longitude of the Sun at the epoch
	/// </summary>
	/// <remarks>
	/// Original macro name: SunElong
	/// </remarks>
	public static double SunELong(double gd, int gm, int gy)
	{
		double t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
		double t2 = t * t;
		double x = 279.6966778 + 36000.76892 * t + 0.0003025 * t2;

		return x - 360 * (x / 360).Floor();
	}

	/// <summary>
	/// Longitude of the Sun at perigee
	/// </summary>
	/// <remarks>
	/// Original macro name: SunPeri
	/// </remarks>
	public static double SunPeri(double gd, int gm, int gy)
	{
		double t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
		double t2 = t * t;
		double x = 281.2208444 + 1.719175 * t + 0.000452778 * t2;

		return x - 360 * (x / 360).Floor();
	}

	/// <summary>
	/// Eccentricity of the Sun-Earth orbit
	/// </summary>
	/// <remarks>
	/// Original macro name: SunEcc
	/// </remarks>
	public static double SunEcc(double gd, int gm, int gy)
	{
		double t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
		double t2 = t * t;

		return 0.01675104 - 0.0000418 * t - 0.000000126 * t2;
	}

	/// <summary>
	/// Ecliptic - Declination (degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: ECDec
	/// </remarks>
	public static double EcDec(double eld, double elm, double els, double bd, double bm, double bs, double gd, int gm, int gy)
	{
		double a = DegreesMinutesSecondsToDecimalDegrees(eld, elm, els).ToRadians();
		double b = DegreesMinutesSecondsToDecimalDegrees(bd, bm, bs).ToRadians();
		double c = Obliq(gd, gm, gy).ToRadians();
		double d = b.Sine() * c.Cosine() + b.Cosine() * c.Sine() * a.Sine();

		return Degrees(d.ASine());
	}

	/// <summary>
	/// Ecliptic - Right Ascension (degrees)
	/// </summary>
	/// <remarks>
	/// Original macro name: ECRA
	/// </remarks>
	public static double EcRA(double eld, double elm, double els, double bd, double bm, double bs, double gd, int gm, int gy)
	{
		double a = DegreesMinutesSecondsToDecimalDegrees(eld, elm, els).ToRadians();
		double b = DegreesMinutesSecondsToDecimalDegrees(bd, bm, bs).ToRadians();
		double c = Obliq(gd, gm, gy).ToRadians();
		double d = a.Sine() * c.Cosine() - b.Tangent() * c.Sine();
		double e = a.Cosine();
		double f = Degrees(d.AngleTangent2(e));

		return f - 360 * (f / 360).Floor();
	}

	/// <summary>
	/// Calculate Sun's true anomaly, i.e., how much its orbit deviates from a true circle to an ellipse.
	/// </summary>
	/// <remarks>
	/// Original macro name: SunTrueAnomaly
	/// </remarks>
	public static double SunTrueAnomaly(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
	{
		double aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;

		double t = (dj / 36525) + (ut / 876600);
		double t2 = t * t;

		double a = 99.99736042 * t;
		double b = 360 * (a - a.Floor());

		double m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
		double ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

		double am = m1.ToRadians();

		return Degrees(TrueAnomaly(am, ec));
	}

	/// <summary>
	/// Calculate the Sun's mean anomaly.
	/// </summary>
	/// <remarks>
	/// Original macro name: SunMeanAnomaly
	/// </remarks>
	public static double SunMeanAnomaly(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
	{
		double aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
		int cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
		double dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;
		double t = (dj / 36525) + (ut / 876600);
		double t2 = t * t;
		double a = 100.0021359 * t;
		double b = 360 * (a - a.Floor());
		double m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
		double am = Unwind(m1.ToRadians());

		return am;
	}

	/// <summary>
	/// Calculate local civil time of sunrise.
	/// </summary>
	/// <remarks>
	/// Original macro name: SunriseLCT
	/// </remarks>
	public static double SunriseLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
	{
		double di = 0.8333333;
		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = SunriseLCTL3710(gd, gm, gy, sr, di, gp);

		double xx;
		if (!result1.s.Equals("OK"))
		{
			xx = -99.0;
		}
		else
		{
			double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
			{
				xx = -99.0;
			}
			else
			{
				sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
				(double a, double x, double y, double la, string s) result2 = SunriseLCTL3710(gd, gm, gy, sr, di, gp);

				if (!result2.s.Equals("OK"))
				{
					xx = -99.0;
				}
				else
				{
					x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
					ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
					xx = UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);
				}
			}
		}

		return xx;
	}

	/// <summary>
	/// Helper function for sunrise_lct()
	/// </summary>
	public static (double a, double x, double y, double la, string s) SunriseLCTL3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0.0, 0.0, y, 0.0, 0.0, di, gp);

		return (a, x, y, la, s);
	}

	/// Calculate local civil time of sunset.
	///
	/// Original macro name: SunsetLCT
	public static double SunsetLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
	{
		double di = 0.8333333;
		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = SunsetLCTL3710(gd, gm, gy, sr, di, gp);

		double xx;
		if (!result1.s.Equals("OK"))
		{
			xx = -99.0;
		}
		else
		{
			double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
			{
				xx = -99.0;
			}
			else
			{
				sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
				(double a, double x, double y, double la, string s) result2 = SunsetLCTL3710(gd, gm, gy, sr, di, gp);

				if (!result2.s.Equals("OK"))
				{
					xx = -99;
				}
				else
				{
					x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
					ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
					xx = UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);
				}
			}
		}
		return xx;
	}

	/// <summary>
	/// Helper function for sunset_lct().
	/// </summary>
	public static (double a, double x, double y, double la, string s) SunsetLCTL3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0.0, 0.0, 0.0, 0.0, 0.0, gd, gm, gy);
		double y = EcDec(a, 0.0, 0.0, 0.0, 0.0, 0.0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// <summary>
	/// Local sidereal time of rise, in hours.
	/// </summary>
	/// <remarks>
	/// Original macro name: RSLSTR
	/// </remarks>
	public static double RiseSetLocalSiderealTimeRise(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
	{
		double a = HMStoDH(rah, ram, ras);
		double b = DegreeHoursToDecimalDegrees(a).ToRadians();
		double c = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double d = vd.ToRadians();
		double e = g.ToRadians();
		double f = -(d.Sine() + e.Sine() * c.Sine()) / (e.Cosine() * c.Cosine());
		double h = (Math.Abs(f) < 1) ? f.ACosine() : 0;
		double i = DecimalDegreesToDegreeHours(Degrees(b - h));

		return i - 24 * (i / 24).Floor();
	}

	/// <summary>
	/// Local sidereal time of setting, in hours.
	/// </summary>
	/// <remarks>
	/// Original macro name: RSLSTS
	/// </remarks>
	public static double RiseSetLocalSiderealTimeSet(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
	{
		double a = HMStoDH(rah, ram, ras);
		double b = DegreeHoursToDecimalDegrees(a).ToRadians();
		double c = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double d = vd.ToRadians();
		double e = g.ToRadians();
		double f = -(d.Sine() + e.Sine() * c.Sine()) / (e.Cosine() * c.Cosine());
		double h = (Math.Abs(f) < 1) ? f.ACosine() : 0;
		double i = DecimalDegreesToDegreeHours(Degrees(b + h));

		return i - 24 * (i / 24).Floor();
	}

	/// <summary>
	/// Azimuth of rising, in degrees.
	/// </summary>
	/// <remarks>
	/// Original macro name: RSAZR
	/// </remarks>
	public static double RiseSetAzimuthRise(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
	{
		double a = HMStoDH(rah, ram, ras);
		double c = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double d = vd.ToRadians();
		double e = g.ToRadians();
		double f = (c.Sine() + d.Sine() * e.Sine()) / (d.Cosine() * e.Cosine());
		double h = ERS(rah, ram, ras, dd, dm, ds, vd, g).Equals("OK") ? f.ACosine() : 0;
		double i = Degrees(h);

		return i - 360 * (i / 360).Floor();
	}

	/// <summary>
	/// Azimuth of setting, in degrees.
	/// </summary>
	/// <remarks>
	/// Original macro name: RSAZS
	/// </remarks>
	public static double RiseSetAzimuthSet(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
	{
		double a = HMStoDH(rah, ram, ras);
		double c = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double d = vd.ToRadians();
		double e = g.ToRadians();
		double f = (c.Sine() + d.Sine() * e.Sine()) / (d.Cosine() * e.Cosine());
		double h = ERS(rah, ram, ras, dd, dm, ds, vd, g).Equals("OK") ? f.ACosine() : 0;
		double i = 360 - Degrees(h);

		return i - 360 * (i / 360).Floor();
	}

	/// <summary>
	/// Rise/Set status
	/// </summary>
	/// <remarks>
	/// <para>Possible values: "OK", "** never rises", "** circumpolar"</para>
	/// <para>Original macro name: eRS</para>
	/// </remarks>
	public static string ERS(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
	{
		double a = HMStoDH(rah, ram, ras);
		double c = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double d = vd.ToRadians();
		double e = g.ToRadians();
		double f = -(d.Sine() + e.Sine() * c.Sine()) / (e.Cosine() * c.Cosine());

		string returnValue = "OK";
		if (f >= 1)
			returnValue = "** never rises";
		if (f <= -1)
			returnValue = "** circumpolar";

		return returnValue;
	}


	/// Sunrise/Sunset calculation status.
	///
	/// Original macro name: eSunRS
	public static string ESunRS(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
	{
		double di = 0.8333333;
		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = ESunRS_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
		{
			return result1.s;
		}
		else
		{
			double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
			(double a, double x, double y, double la, string s) result2 = ESunRS_L3710(gd, gm, gy, sr, di, gp);
			if (!result2.s.Equals("OK"))
			{
				return result2.s;
			}
			else
			{
				x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);

				if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
				{
					string s = result2.s + " GST to UT conversion warning";

					return s;
				}

				return result2.s;
			}
		}
	}

	/// <summary>
	/// Helper function for e_sun_rs()
	/// </summary>
	public static (double a, double x, double y, double la, string s) ESunRS_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// Calculate azimuth of sunrise.
	///
	/// Original macro name: SunriseAz
	public static double SunriseAZ(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
	{
		double di = 0.8333333;
		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = SunriseAZ_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
		{
			return -99.0;
		}

		double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
		double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
		{
			return -99.0;
		}

		sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
		(double a, double x, double y, double la, string s) result2 = SunriseAZ_L3710(gd, gm, gy, sr, di, gp);

		if (!result2.s.Equals("OK"))
		{
			return -99.0;
		}

		return RiseSetAzimuthRise(DecimalDegreesToDegreeHours(x), 0, 0, result2.y, 0.0, 0.0, di, gp);
	}

	/// <summary>
	/// Helper function for sunrise_az()
	/// </summary>
	public static (double a, double x, double y, double la, string s) SunriseAZ_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// <summary>
	/// Calculate azimuth of sunset.
	/// </summary>
	/// <remarks>
	/// Original macro name: SunsetAz
	/// </remarks>
	public static double SunsetAZ(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
	{
		double di = 0.8333333;
		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = SunsetAZ_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
		{
			return -99.0;
		}

		double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
		double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
		{
			return -99.0;
		}

		sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

		(double a, double x, double y, double la, string s) result2 = SunsetAZ_L3710(gd, gm, gy, sr, di, gp);

		if (!result2.s.Equals("OK"))
		{
			return -99.0;
		}
		return RiseSetAzimuthSet(DecimalDegreesToDegreeHours(x), 0, 0, result2.y, 0, 0, di, gp);
	}

	/// <summary>
	/// Helper function for sunset_az()
	/// </summary>
	public static (double a, double x, double y, double la, string s) SunsetAZ_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// <summary>
	/// Calculate morning twilight start, in local time.
	/// </summary>
	/// <remarks>
	/// <para>Twilight type (TT) can be one of "C" (civil), "N" (nautical), or "A" (astronomical)</para>
	/// <para>Original macro name: TwilightAMLCT</para>
	/// </remarks>
	public static double TwilightAMLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp, PATwilightType tt)
	{
		double di = (double)tt;

		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = TwilightAMLCT_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
			return -99.0;

		double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
		double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
			return -99.0;

		sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

		(double a, double x, double y, double la, string s) result2 = TwilightAMLCT_L3710(gd, gm, gy, sr, di, gp);

		if (!result2.s.Equals("OK"))
			return -99.0;

		x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		double xx = UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);

		return xx;
	}

	/// <summary>
	/// Helper function for twilight_am_lct()
	/// </summary>
	public static (double a, double x, double y, double la, string s) TwilightAMLCT_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// <summary>
	/// Calculate evening twilight end, in local time.
	/// </summary>
	/// <remarks>
	/// <para>Twilight type can be one of "C" (civil), "N" (nautical), or "A" (astronomical)</para>
	/// <para>Original macro name: TwilightPMLCT</para>
	/// </remarks>
	public static double TwilightPMLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp, PATwilightType tt)
	{
		double di = (double)tt;

		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = TwilightPMLCT_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
			return 0.0;

		double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
		double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
			return 0.0;

		sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

		(double a, double x, double y, double la, string s) result2 = TwilightPMLCT_L3710(gd, gm, gy, sr, di, gp);

		if (!result2.s.Equals("OK"))
			return 0.0;

		x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

		return UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);
	}

	/// <summary>
	/// Helper function for twilight_pm_lct()
	/// </summary>
	public static (double a, double x, double y, double la, string s) TwilightPMLCT_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		return (a, x, y, la, s);
	}

	/// Twilight calculation status.
	///
	/// Twilight type can be one of "C" (civil), "N" (nautical), or "A" (astronomical)
	///
	/// Original macro name: eTwilight
	///
	/// ## Returns
	/// One of: "OK", "** lasts all night", or "** Sun too far below horizon"
	public static string ETwilight(double ld, int lm, int ly, int ds, int zc, double gl, double gp, PATwilightType tt)
	{
		double di = (double)tt;

		double gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
		int gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
		int gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
		double sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

		(double a, double x, double y, double la, string s) result1 = ETwilight_L3710(gd, gm, gy, sr, di, gp);

		if (!result1.s.Equals("OK"))
		{
			return result1.s;
		}

		double x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
		double ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
		sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

		(double a, double x, double y, double la, string s) result2 = ETwilight_L3710(gd, gm, gy, sr, di, gp);

		if (!result2.s.Equals("OK"))
		{
			return result2.s;
		}

		x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);

		if (!EGstUt(x, 0, 0, gd, gm, gy).Equals(PAWarningFlag.OK))
		{
			result2.s = $"{result2.s} GST to UT conversion warning";

			return result2.s;
		}

		return result2.s;
	}

	/// <summary>
	/// Helper function for e_twilight()
	/// </summary>
	public static (double a, double x, double y, double la, string s) ETwilight_L3710(double gd, int gm, int gy, double sr, double di, double gp)
	{
		double a = sr + NutatLong(gd, gm, gy) - 0.005694;
		double x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
		double la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
		string s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

		if (s.Length > 2)
		{
			if (s.Substring(0, 3).Equals("** c"))
			{
				s = "** lasts all night";
			}
			else
			{
				if (s.Substring(0, 3).Equals("** n"))
				{
					s = "** Sun too far below horizon";
				}
			}
		}

		return (a, x, y, la, s);
	}

	/// <summary>
	/// Calculate the angle between two celestial objects
	/// </summary>
	/// <remarks>
	/// Original macro name: Angle
	/// </remarks>
	public static double Angle(double xx1, double xm1, double xs1, double dd1, double dm1, double ds1, double xx2, double xm2, double xs2, double dd2, double dm2, double ds2, PAAngleMeasure s
	)
	{
		double a = s.Equals(PAAngleMeasure.Hours) ? DegreeHoursToDecimalDegrees(HMStoDH(xx1, xm1, xs1)) : DegreesMinutesSecondsToDecimalDegrees(xx1, xm1, xs1);
		double b = a.ToRadians();
		double c = DegreesMinutesSecondsToDecimalDegrees(dd1, dm1, ds1);
		double d = c.ToRadians();
		double e = s.Equals(PAAngleMeasure.Hours) ? DegreeHoursToDecimalDegrees(HMStoDH(xx2, xm2, xs2)) : DegreesMinutesSecondsToDecimalDegrees(xx2, xm2, xs2);
		double f = e.ToRadians();
		double g = DegreesMinutesSecondsToDecimalDegrees(dd2, dm2, ds2);
		double h = g.ToRadians();
		double i = (d.Sine() * h.Sine() + d.Cosine() * h.Cosine() * (b - f).Cosine()).ACosine();

		return Degrees(i);
	}

	/// <summary>
	/// Calculate several planetary properties.
	/// </summary>
	/// <remarks>
	/// Original macro names: PlanetLong, PlanetLat, PlanetDist, PlanetHLong1, PlanetHLong2, PlanetHLat, PlanetRVect
	/// </remarks>
	/// <param name="lh">Local civil time, hour part.</param>
	/// <param name="lm">Local civil time, minutes part.</param>
	/// <param name="ls">Local civil time, seconds part.</param>
	/// <param name="ds">Daylight Savings offset.</param>
	/// <param name="zc">Time zone correction, in hours.</param>
	/// <param name="dy">Local date, day part.</param>
	/// <param name="mn">Local date, month part.</param>
	/// <param name="yr">Local date, year part.</param>
	/// <param name="s">Planet name.</param>
	/// <returns>
	/// <para>planetLongitude -- Ecliptic longitude, in degrees.</para>
	/// <para>planetLatitude -- Ecliptic latitude, in degrees.</para>
	/// <para>planetDistanceAU -- Earth-planet distance, in AU.</para>
	/// <para>planetHLong1 -- Heliocentric orbital longitude, in degrees.</para>
	/// <para>planetHLong2 -- NOT USED</para>
	/// <para>planetHLat -- NOT USED</para>
	/// <para>planetRVect -- Sun-planet distance (length of radius vector), in AU.</para>
	/// </returns>
	public static (double planetLongitude, double planetLatitude, double planetDistanceAU, double planetHLong1, double planetHLong2, double planetHLat, double planetRVect) PlanetCoordinates(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr, string s)
	{
		double a11 = 178.179078;
		double a12 = 415.2057519;
		double a13 = 0.0003011;
		double a14 = 0.0;
		double a21 = 75.899697;
		double a22 = 1.5554889;
		double a23 = 0.0002947;
		double a24 = 0.0;
		double a31 = 0.20561421;
		double a32 = 0.00002046;
		double a33 = -0.00000003;
		double a34 = 0.0;
		double a41 = 7.002881;
		double a42 = 0.0018608;
		double a43 = -0.0000183;
		double a44 = 0.0;
		double a51 = 47.145944;
		double a52 = 1.1852083;
		double a53 = 0.0001739;
		double a54 = 0.0;
		double a61 = 0.3870986;
		double a62 = 6.74;
		double a63 = -0.42;

		double b11 = 342.767053;
		double b12 = 162.5533664;
		double b13 = 0.0003097;
		double b14 = 0.0;
		double b21 = 130.163833;
		double b22 = 1.4080361;
		double b23 = -0.0009764;
		double b24 = 0.0;
		double b31 = 0.00682069;
		double b32 = -0.00004774;
		double b33 = 0.000000091;
		double b34 = 0.0;
		double b41 = 3.393631;
		double b42 = 0.0010058;
		double b43 = -0.000001;
		double b44 = 0.0;
		double b51 = 75.779647;
		double b52 = 0.89985;
		double b53 = 0.00041;
		double b54 = 0.0;
		double b61 = 0.7233316;
		double b62 = 16.92;
		double b63 = -4.4;

		double c11 = 293.737334;
		double c12 = 53.17137642;
		double c13 = 0.0003107;
		double c14 = 0.0;
		double c21 = 334.218203;
		double c22 = 1.8407584;
		double c23 = 0.0001299;
		double c24 = -0.00000119;
		double c31 = 0.0933129;
		double c32 = 0.000092064;
		double c33 = -0.000000077;
		double c34 = 0.0;
		double c41 = 1.850333;
		double c42 = -0.000675;
		double c43 = 0.0000126;
		double c44 = 0.0;
		double c51 = 48.786442;
		double c52 = 0.7709917;
		double c53 = -0.0000014;
		double c54 = -0.00000533;
		double c61 = 1.5236883;
		double c62 = 9.36;
		double c63 = -1.52;

		double d11 = 238.049257;
		double d12 = 8.434172183;
		double d13 = 0.0003347;
		double d14 = -0.00000165;
		double d21 = 12.720972;
		double d22 = 1.6099617;
		double d23 = 0.00105627;
		double d24 = -0.00000343;
		double d31 = 0.04833475;
		double d32 = 0.00016418;
		double d33 = -0.0000004676;
		double d34 = -0.0000000017;
		double d41 = 1.308736;
		double d42 = -0.0056961;
		double d43 = 0.0000039;
		double d44 = 0.0;
		double d51 = 99.443414;
		double d52 = 1.01053;
		double d53 = 0.00035222;
		double d54 = -0.00000851;
		double d61 = 5.202561;
		double d62 = 196.74;
		double d63 = -9.4;

		double e11 = 266.564377;
		double e12 = 3.398638567;
		double e13 = 0.0003245;
		double e14 = -0.0000058;
		double e21 = 91.098214;
		double e22 = 1.9584158;
		double e23 = 0.00082636;
		double e24 = 0.00000461;
		double e31 = 0.05589232;
		double e32 = -0.0003455;
		double e33 = -0.000000728;
		double e34 = 0.00000000074;
		double e41 = 2.492519;
		double e42 = -0.0039189;
		double e43 = -0.00001549;
		double e44 = 0.00000004;
		double e51 = 112.790414;
		double e52 = 0.8731951;
		double e53 = -0.00015218;
		double e54 = -0.00000531;
		double e61 = 9.554747;
		double e62 = 165.6;
		double e63 = -8.88;

		double f11 = 244.19747;
		double f12 = 1.194065406;
		double f13 = 0.000316;
		double f14 = -0.0000006;
		double f21 = 171.548692;
		double f22 = 1.4844328;
		double f23 = 0.0002372;
		double f24 = -0.00000061;
		double f31 = 0.0463444;
		double f32a = -0.00002658;
		double f33 = 0.000000077;
		double f34 = 0.0;
		double f41 = 0.772464;
		double f42 = 0.0006253;
		double f43 = 0.0000395;
		double f44 = 0.0;
		double f51 = 73.477111;
		double f52 = 0.4986678;
		double f53 = 0.0013117;
		double f54 = 0.0;
		double f61 = 19.21814;
		double f62 = 65.8;
		double f63 = -7.19;

		double g11 = 84.457994;
		double g12 = 0.6107942056;
		double g13 = 0.0003205;
		double g14 = -0.0000006;
		double g21 = 46.727364;
		double g22 = 1.4245744;
		double g23 = 0.00039082;
		double g24 = -0.000000605;
		double g31 = 0.00899704;
		double g32 = 0.00000633;
		double g33 = -0.000000002;
		double g34 = 0.0;
		double g41 = 1.779242;
		double g42 = -0.0095436;
		double g43 = -0.0000091;
		double g44 = 0.0;
		double g51 = 130.681389;
		double g52 = 1.098935;
		double g53 = 0.00024987;
		double g54 = -0.000004718;
		double g61 = 30.10957;
		double g62 = 62.2;
		double g63 = -6.87;

		List<PlanetDataPrecise> pl = new List<PlanetDataPrecise>();

		pl.Add(new PlanetDataPrecise() { Name = "", Value1 = 0, Value2 = 0, Value3 = 0, Value4 = 0, Value5 = 0, Value6 = 0, Value7 = 0, Value8 = 0, Value9 = 0 });

		int ip = 0;
		double b = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double a = CivilDateToJulianDate(gd, gm, gy);
		double t = ((a - 2415020.0) / 36525.0) + (b / 876600.0);

		double a0 = a11;
		double a1 = a12;
		double a2 = a13;
		double a3 = a14;
		double b0 = a21;
		double b1 = a22;
		double b2 = a23;
		double b3 = a24;
		double c0 = a31;
		double c1 = a32;
		double c2 = a33;
		double c3 = a34;
		double d0 = a41;
		double d1 = a42;
		double d2 = a43;
		double d3 = a44;
		double e0 = a51;
		double e1 = a52;
		double e2 = a53;
		double e3 = a54;
		double f = a61;
		double g = a62;
		double h = a63;
		double aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		double c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Mercury",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = b11;
		a1 = b12;
		a2 = b13;
		a3 = b14;
		b0 = b21;
		b1 = b22;
		b2 = b23;
		b3 = b24;
		c0 = b31;
		c1 = b32;
		c2 = b33;
		c3 = b34;
		d0 = b41;
		d1 = b42;
		d2 = b43;
		d3 = b44;
		e0 = b51;
		e1 = b52;
		e2 = b53;
		e3 = b54;
		f = b61;
		g = b62;
		h = b63;
		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Venus",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = c11;
		a1 = c12;
		a2 = c13;
		a3 = c14;
		b0 = c21;
		b1 = c22;
		b2 = c23;
		b3 = c24;
		c0 = c31;
		c1 = c32;
		c2 = c33;
		c3 = c34;
		d0 = c41;
		d1 = c42;
		d2 = c43;
		d3 = c44;
		e0 = c51;
		e1 = c52;
		e2 = c53;
		e3 = c54;
		f = c61;
		g = c62;
		h = c63;

		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Mars",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = d11;
		a1 = d12;
		a2 = d13;
		a3 = d14;
		b0 = d21;
		b1 = d22;
		b2 = d23;
		b3 = d24;
		c0 = d31;
		c1 = d32;
		c2 = d33;
		c3 = d34;
		d0 = d41;
		d1 = d42;
		d2 = d43;
		d3 = d44;
		e0 = d51;
		e1 = d52;
		e2 = d53;
		e3 = d54;
		f = d61;
		g = d62;
		h = d63;

		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Jupiter",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = e11;
		a1 = e12;
		a2 = e13;
		a3 = e14;
		b0 = e21;
		b1 = e22;
		b2 = e23;
		b3 = e24;
		c0 = e31;
		c1 = e32;
		c2 = e33;
		c3 = e34;
		d0 = e41;
		d1 = e42;
		d2 = e43;
		d3 = e44;
		e0 = e51;
		e1 = e52;
		e2 = e53;
		e3 = e54;
		f = e61;
		g = e62;
		h = e63;

		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Saturn",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = f11;
		a1 = f12;
		a2 = f13;
		a3 = f14;
		b0 = f21;
		b1 = f22;
		b2 = f23;
		b3 = f24;
		c0 = f31;
		c1 = f32a;
		c2 = f33;
		c3 = f34;
		d0 = f41;
		d1 = f42;
		d2 = f43;
		d3 = f44;
		e0 = f51;
		e1 = f52;
		e2 = f53;
		e3 = f54;
		f = f61;
		g = f62;
		h = f63;

		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Uranus",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		a0 = g11;
		a1 = g12;
		a2 = g13;
		a3 = g14;
		b0 = g21;
		b1 = g22;
		b2 = g23;
		b3 = g24;
		c0 = g31;
		c1 = g32;
		c2 = g33;
		c3 = g34;
		d0 = g41;
		d1 = g42;
		d2 = g43;
		d3 = g44;
		e0 = g51;
		e1 = g52;
		e2 = g53;
		e3 = g54;
		f = g61;
		g = g62;
		h = g63;

		aa = a1 * t;
		b = 360.0 * (aa - aa.Floor());
		c = a0 + b + (a3 * t + a2) * t * t;

		pl.Add(new PlanetDataPrecise()
		{
			Name = "Neptune",
			Value1 = c - 360.0 * (c / 360.0).Floor(),
			Value2 = (a1 * 0.009856263) + (a2 + a3) / 36525.0,
			Value3 = ((b3 * t + b2) * t + b1) * t + b0,
			Value4 = ((c3 * t + c2) * t + c1) * t + c0,
			Value5 = ((d3 * t + d2) * t + d1) * t + d0,
			Value6 = ((e3 * t + e2) * t + e1) * t + e0,
			Value7 = f,
			Value8 = g,
			Value9 = h
		});

		PlanetDataPrecise checkPlanet = pl.Where(x => x.Name.ToLower() == s.ToLower()).Select(x => x).FirstOrDefault();
		if (checkPlanet == null)
			return (Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)));

		double li = 0.0;
		double ms = SunMeanAnomaly(lh, lm, ls, ds, zc, dy, mn, yr);
		double sr = SunLong(lh, lm, ls, ds, zc, dy, mn, yr).ToRadians();
		double re = SunDist(lh, lm, ls, ds, zc, dy, mn, yr);
		double lg = sr + Math.PI;

		double l0 = 0.0;
		double s0 = 0.0;
		double p0 = 0.0;
		double vo = 0.0;
		double lp1 = 0.0;
		double ll = 0.0;
		double rd = 0.0;
		double pd = 0.0;
		double sp = 0.0;
		double ci = 0.0;

		for (int k = 1; k <= 3; k++)
		{
			foreach (PlanetDataPrecise planet in pl)
				planet.APValue = (planet.Value1 - planet.Value3 - li * planet.Value2).ToRadians();

			double qa = 0.0;
			double qb = 0.0;
			double qc = 0.0;
			double qd = 0.0;
			double qe = 0.0;
			double qf = 0.0;
			double qg = 0.0;

			if (s == "Mercury")
				(qa, qb) = PlanetLong_L4685(pl);

			if (s == "Venus")
				(qa, qb, qc, qe) = PlanetLong_L4735(pl, ms, t);

			if (s == "Mars")
			{
				(double a, double sa, double ca, double qc, double qe, double qa, double qb) returnValue = PlanetLong_L4810(pl, ms);

				qc = returnValue.qc;
				qe = returnValue.qe;
				qa = returnValue.qa;
				qb = returnValue.qb;
			}

			PlanetDataPrecise matchPlanet = pl.Where(x => x.Name.ToLower() == s.ToLower()).Select(x => x).FirstOrDefault();

			if (new string[] { "Jupiter", "Saturn", "Uranus", "Neptune" }.Contains(s))
				(qa, qb, qc, qd, qe, qf, qg) = PlanetLong_L4945(t, matchPlanet);

			double ec = matchPlanet.Value4 + qd;
			double am = matchPlanet.APValue + qe;
			double at = TrueAnomaly(am, ec);
			double pvv = (matchPlanet.Value7 + qf) * (1.0 - ec * ec) / (1.0 + ec * at.Cosine());
			double lp = Degrees(at) + matchPlanet.Value3 + Degrees(qc - qe);
			lp = lp.ToRadians();
			double om = matchPlanet.Value6.ToRadians();
			double lo = lp - om;
			double so = lo.Sine();
			double co = lo.Cosine();
			double inn = matchPlanet.Value5.ToRadians();
			pvv += qb;
			sp = so * inn.Sine();
			double y = so * inn.Cosine();
			double ps = sp.ASine() + qg;
			sp = ps.Sine();
			pd = y.AngleTangent2(co) + om + qa.ToRadians();
			pd = Unwind(pd);
			ci = ps.Cosine();
			rd = pvv * ci;
			ll = pd - lg;
			double rh = re * re + pvv * pvv - 2.0 * re * pvv * ci * ll.Cosine();
			rh = rh.SquareRoot();
			li = rh * 0.005775518;

			if (k == 1)
			{
				l0 = pd;
				s0 = ps;
				p0 = pvv;
				vo = rh;
				lp1 = lp;
			}
		}

		double l1 = ll.Sine();
		double l2 = ll.Cosine();

		double ep = (ip < 3) ? (-1.0 * rd * l1 / (re - rd * l2)).AngleTangent() + lg + Math.PI : (re * l1 / (rd - re * l2)).AngleTangent() + pd;
		ep = Unwind(ep);

		double bp = (rd * sp * (ep - pd).Sine() / (ci * re * l1)).AngleTangent();

		double planetLongitude = Degrees(Unwind(ep));
		double planetLatitude = Degrees(Unwind(bp));
		double planetDistanceAU = vo;
		double planetHLong1 = Degrees(lp1);
		double planetHLong2 = Degrees(l0);
		double planetHLat = Degrees(s0);
		double planetRVect = p0;

		return (planetLongitude, planetLatitude, planetDistanceAU, planetHLong1, planetHLong2, planetHLat, planetRVect);
	}

	/// <summary>
	/// Helper function for planet_long_lat()
	/// </summary>
	public static (double qa, double qb) PlanetLong_L4685(List<PlanetDataPrecise> pl)
	{
		double qa = 0.00204 * (5.0 * pl[2].APValue - 2.0 * pl[1].APValue + 0.21328).Cosine();
		qa += 0.00103 * (2.0 * pl[2].APValue - pl[1].APValue - 2.8046).Cosine();
		qa += 0.00091 * (2.0 * pl[4].APValue - pl[1].APValue - 0.64582).Cosine();
		qa += 0.00078 * (5.0 * pl[2].APValue - 3.0 * pl[1].APValue + 0.17692).Cosine();

		double qb = 0.000007525 * (2.0 * pl[4].APValue - pl[1].APValue + 0.925251).Cosine();
		qb += 0.000006802 * (5.0 * pl[2].APValue - 3.0 * pl[1].APValue - 4.53642).Cosine();
		qb += 0.000005457 * (2.0 * pl[2].APValue - 2.0 * pl[1].APValue - 1.24246).Cosine();
		qb += 0.000003569 * (5.0 * pl[2].APValue - pl[1].APValue - 1.35699).Cosine();

		return (qa, qb);
	}

	/// <summary>
	/// Helper function for planet_long_lat()
	/// </summary>
	public static (double qa, double qb, double qc, double qe) PlanetLong_L4735(List<PlanetDataPrecise> pl, double ms, double t)
	{
		double qc = 0.00077 * (4.1406 + t * 2.6227).Sine();
		qc = qc.ToRadians();
		double qe = qc;

		double qa = 0.00313 * (2.0 * ms - 2.0 * pl[2].APValue - 2.587).Cosine();
		qa += 0.00198 * (3.0 * ms - 3.0 * pl[2].APValue + 0.044768).Cosine();
		qa += 0.00136 * (ms - pl[2].APValue - 2.0788).Cosine();
		qa += 0.00096 * (3.0 * ms - 2.0 * pl[2].APValue - 2.3721).Cosine();
		qa += 0.00082 * (pl[4].APValue - pl[2].APValue - 3.6318).Cosine();

		double qb = 0.000022501 * (2.0 * ms - 2.0 * pl[2].APValue - 1.01592).Cosine();
		qb += 0.000019045 * (3.0 * ms - 3.0 * pl[2].APValue + 1.61577).Cosine();
		qb += 0.000006887 * (pl[4].APValue - pl[2].APValue - 2.06106).Cosine();
		qb += 0.000005172 * (ms - pl[2].APValue - 0.508065).Cosine();
		qb += 0.00000362 * (5.0 * ms - 4.0 * pl[2].APValue - 1.81877).Cosine();
		qb += 0.000003283 * (4.0 * ms - 4.0 * pl[2].APValue + 1.10851).Cosine();
		qb += 0.000003074 * (2.0 * pl[4].APValue - 2.0 * pl[2].APValue - 0.962846).Cosine();

		return (qa, qb, qc, qe);
	}

	/// <summary>
	/// Helper function for planet_long_lat()
	/// </summary>
	public static (double a, double sa, double ca, double qc, double qe, double qa, double qb) PlanetLong_L4810(List<PlanetDataPrecise> pl, double ms)
	{
		double a = 3.0 * pl[4].APValue - 8.0 * pl[3].APValue + 4.0 * ms;
		double sa = a.Sine();
		double ca = a.Cosine();
		double qc = -(0.01133 * sa + 0.00933 * ca);
		qc = qc.ToRadians();
		double qe = qc;

		double qa = 0.00705 * (pl[4].APValue - pl[3].APValue - 0.85448).Cosine();
		qa += 0.00607 * (2.0 * pl[4].APValue - pl[3].APValue - 3.2873).Cosine();
		qa += 0.00445 * (2.0 * pl[4].APValue - 2.0 * pl[3].APValue - 3.3492).Cosine();
		qa += 0.00388 * (ms - 2.0 * pl[3].APValue + 0.35771).Cosine();
		qa += 0.00238 * (ms - pl[3].APValue + 0.61256).Cosine();
		qa += 0.00204 * (2.0 * ms - 3.0 * pl[3].APValue + 2.7688).Cosine();
		qa += 0.00177 * (3.0 * pl[3].APValue - pl[2].APValue - 1.0053).Cosine();
		qa += 0.00136 * (2.0 * ms - 4.0 * pl[3].APValue + 2.6894).Cosine();
		qa += 0.00104 * (pl[4].APValue + 0.30749).Cosine();

		double qb = 0.000053227 * (pl[4].APValue - pl[3].APValue + 0.717864).Cosine();
		qb += 0.000050989 * (2.0 * pl[4].APValue - 2.0 * pl[3].APValue - 1.77997).Cosine();
		qb += 0.000038278 * (2.0 * pl[4].APValue - pl[3].APValue - 1.71617).Cosine();
		qb += 0.000015996 * (ms - pl[3].APValue - 0.969618).Cosine();
		qb += 0.000014764 * (2.0 * ms - 3.0 * pl[3].APValue + 1.19768).Cosine();
		qb += 0.000008966 * (pl[4].APValue - 2.0 * pl[3].APValue + 0.761225).Cosine();
		qb += 0.000007914 * (3.0 * pl[4].APValue - 2.0 * pl[3].APValue - 2.43887).Cosine();
		qb += 0.000007004 * (2.0 * pl[4].APValue - 3.0 * pl[3].APValue - 1.79573).Cosine();
		qb += 0.00000662 * (ms - 2.0 * pl[3].APValue + 1.97575).Cosine();
		qb += 0.00000493 * (3.0 * pl[4].APValue - 3.0 * pl[3].APValue - 1.33069).Cosine();
		qb += 0.000004693 * (3.0 * ms - 5.0 * pl[3].APValue + 3.32665).Cosine();
		qb += 0.000004571 * (2.0 * ms - 4.0 * pl[3].APValue + 4.27086).Cosine();
		qb += 0.000004409 * (3.0 * pl[4].APValue - pl[3].APValue - 2.02158).Cosine();

		return (a, sa, ca, qc, qe, qa, qb);
	}

	/// <summary>
	/// Helper function for planet_long_lat()
	/// </summary>
	public static (double qa, double qb, double qc, double qd, double qe, double qf, double qg) PlanetLong_L4945(double t, PlanetDataPrecise planet)
	{
		double qa = 0.0;
		double qb = 0.0;
		double qc = 0.0;
		double qd = 0.0;
		double qe = 0.0;
		double qf = 0.0;
		double qg = 0.0;
		double vk = 0.0;
		double ja = 0.0;
		double jb = 0.0;
		double jc = 0.0;

		double j1 = t / 5.0 + 0.1;
		double j2 = Unwind(4.14473 + 52.9691 * t);
		double j3 = Unwind(4.641118 + 21.32991 * t);
		double j4 = Unwind(4.250177 + 7.478172 * t);
		double j5 = 5.0 * j3 - 2.0 * j2;
		double j6 = 2.0 * j2 - 6.0 * j3 + 3.0 * j4;

		if (new string[] { "Mercury", "Venus", "Mars" }.Contains(planet.Name))
			return (qa, qb, qc, qd, qe, qf, qg);

		if (new string[] { "Jupiter", "Saturn" }.Contains(planet.Name))
		{
			double j7 = j3 - j2;
			double u1 = j3.Sine();
			double u2 = j3.Cosine();
			double u3 = (2.0 * j3).Sine();
			double u4 = (2.0 * j3).Cosine();
			double u5 = j5.Sine();
			double u6 = j5.Cosine();
			double u7 = (2.0 * j5).Sine();
			double u8a = j6.Sine();
			double u9 = j7.Sine();
			double ua = j7.Cosine();
			double ub = (2.0 * j7).Sine();
			double uc = (2.0 * j7).Cosine();
			double ud = (3.0 * j7).Sine();
			double ue = (3.0 * j7).Cosine();
			double uf = (4.0 * j7).Sine();
			double ug = (4.0 * j7).Cosine();
			double vh = (5.0 * j7).Cosine();

			if (planet.Name == "Saturn")
			{
				double ui = (3.0 * j3).Sine();
				double uj = (3.0 * j3).Cosine();
				double uk = (4.0 * j3).Sine();
				double ul = (4.0 * j3).Cosine();
				double vi = (2.0 * j5).Cosine();
				double un = (5.0 * j7).Sine();
				double j8 = j4 - j3;
				double uo = (2.0 * j8).Sine();
				double up = (2.0 * j8).Cosine();
				double uq = (3.0 * j8).Sine();
				double ur = (3.0 * j8).Cosine();

				qc = 0.007581 * u7 - 0.007986 * u8a - 0.148811 * u9;
				qc -= (0.814181 - (0.01815 - 0.016714 * j1) * j1) * u5;
				qc -= (0.010497 - (0.160906 - 0.0041 * j1) * j1) * u6;
				qc = qc - 0.015208 * ud - 0.006339 * uf - 0.006244 * u1;
				qc = qc - 0.0165 * ub * u1 - 0.040786 * ub;
				qc = qc + (0.008931 + 0.002728 * j1) * u9 * u1 - 0.005775 * ud * u1;
				qc = qc + (0.081344 + 0.003206 * j1) * ua * u1 + 0.015019 * uc * u1;
				qc = qc + (0.085581 + 0.002494 * j1) * u9 * u2 + 0.014394 * uc * u2;
				qc = qc + (0.025328 - 0.003117 * j1) * ua * u2 + 0.006319 * ue * u2;
				qc = qc + 0.006369 * u9 * u3 + 0.009156 * ub * u3 + 0.007525 * uq * u3;
				qc = qc - 0.005236 * ua * u4 - 0.007736 * uc * u4 - 0.007528 * ur * u4;
				qc = qc.ToRadians();

				qd = (-7927.0 + (2548.0 + 91.0 * j1) * j1) * u5;
				qd = qd + (13381.0 + (1226.0 - 253.0 * j1) * j1) * u6 + (248.0 - 121.0 * j1) * u7;
				qd = qd - (305.0 + 91.0 * j1) * vi + 412.0 * ub + 12415.0 * u1;
				qd = qd + (390.0 - 617.0 * j1) * u9 * u1 + (165.0 - 204.0 * j1) * ub * u1;
				qd = qd + 26599.0 * ua * u1 - 4687.0 * uc * u1 - 1870.0 * ue * u1 - 821.0 * ug * u1;
				qd = qd - 377.0 * vh * u1 + 497.0 * up * u1 + (163.0 - 611.0 * j1) * u2;
				qd = qd - 12696.0 * u9 * u2 - 4200.0 * ub * u2 - 1503.0 * ud * u2 - 619.0 * uf * u2;
				qd = qd - 268.0 * un * u2 - (282.0 + 1306.0 * j1) * ua * u2;
				qd = qd + (-86.0 + 230.0 * j1) * uc * u2 + 461.0 * uo * u2 - 350.0 * u3;
				qd = qd + (2211.0 - 286.0 * j1) * u9 * u3 - 2208.0 * ub * u3 - 568.0 * ud * u3;
				qd = qd - 346.0 * uf * u3 - (2780.0 + 222.0 * j1) * ua * u3;
				qd = qd + (2022.0 + 263.0 * j1) * uc * u3 + 248.0 * ue * u3 + 242.0 * uq * u3;
				qd = qd + 467.0 * ur * u3 - 490.0 * u4 - (2842.0 + 279.0 * j1) * u9 * u4;
				qd = qd + (128.0 + 226.0 * j1) * ub * u4 + 224.0 * ud * u4;
				qd = qd + (-1594.0 + 282.0 * j1) * ua * u4 + (2162.0 - 207.0 * j1) * uc * u4;
				qd = qd + 561.0 * ue * u4 + 343.0 * ug * u4 + 469.0 * uq * u4 - 242.0 * ur * u4;
				qd = qd - 205.0 * u9 * ui + 262.0 * ud * ui + 208.0 * ua * uj - 271.0 * ue * uj;
				qd = qd - 382.0 * ue * uk - 376.0 * ud * ul;
				qd *= 0.0000001;

				vk = (0.077108 + (0.007186 - 0.001533 * j1) * j1) * u5;
				vk -= 0.007075 * u9;
				vk += (0.045803 - (0.014766 + 0.000536 * j1) * j1) * u6;
				vk = vk - 0.072586 * u2 - 0.075825 * u9 * u1 - 0.024839 * ub * u1;
				vk = vk - 0.008631 * ud * u1 - 0.150383 * ua * u2;
				vk = vk + 0.026897 * uc * u2 + 0.010053 * ue * u2;
				vk = vk - (0.013597 + 0.001719 * j1) * u9 * u3 + 0.011981 * ub * u4;
				vk -= (0.007742 - 0.001517 * j1) * ua * u3;
				vk += (0.013586 - 0.001375 * j1) * uc * u3;
				vk -= (0.013667 - 0.001239 * j1) * u9 * u4;
				vk += (0.014861 + 0.001136 * j1) * ua * u4;
				vk -= (0.013064 + 0.001628 * j1) * uc * u4;
				qe = qc - (vk.ToRadians() / planet.Value4);

				qf = 572.0 * u5 - 1590.0 * ub * u2 + 2933.0 * u6 - 647.0 * ud * u2;
				qf = qf + 33629.0 * ua - 344.0 * uf * u2 - 3081.0 * uc + 2885.0 * ua * u2;
				qf = qf - 1423.0 * ue + (2172.0 + 102.0 * j1) * uc * u2 - 671.0 * ug;
				qf = qf + 296.0 * ue * u2 - 320.0 * vh - 267.0 * ub * u3 + 1098.0 * u1;
				qf = qf - 778.0 * ua * u3 - 2812.0 * u9 * u1 + 495.0 * uc * u3 + 688.0 * ub * u1;
				qf = qf + 250.0 * ue * u3 - 393.0 * ud * u1 - 856.0 * u9 * u4 - 228.0 * uf * u1;
				qf = qf + 441.0 * ub * u4 + 2138.0 * ua * u1 + 296.0 * uc * u4 - 999.0 * uc * u1;
				qf = qf + 211.0 * ue * u4 - 642.0 * ue * u1 - 427.0 * u9 * ui - 325.0 * ug * u1;
				qf = qf + 398.0 * ud * ui - 890.0 * u2 + 344.0 * ua * uj + 2206.0 * u9 * u2;
				qf -= 427.0 * ue * uj;
				qf *= 0.000001;

				qg = 0.000747 * ua * u1 + 0.001069 * ua * u2 + 0.002108 * ub * u3;
				qg = qg + 0.001261 * uc * u3 + 0.001236 * ub * u4 - 0.002075 * uc * u4;
				qg = qg.ToRadians();

				return (qa, qb, qc, qd, qe, qf, qg);
			}

			qc = (0.331364 - (0.010281 + 0.004692 * j1) * j1) * u5;
			qc += (0.003228 - (0.064436 - 0.002075 * j1) * j1) * u6;
			qc -= (0.003083 + (0.000275 - 0.000489 * j1) * j1) * u7;
			qc = qc + 0.002472 * u8a + 0.013619 * u9 + 0.018472 * ub;
			qc = qc + 0.006717 * ud + 0.002775 * uf + 0.006417 * ub * u1;
			qc = qc + (0.007275 - 0.001253 * j1) * u9 * u1 + 0.002439 * ud * u1;
			qc = qc - (0.035681 + 0.001208 * j1) * u9 * u2 - 0.003767 * uc * u1;
			qc = qc - (0.033839 + 0.001125 * j1) * ua * u1 - 0.004261 * ub * u2;
			qc = qc + (0.001161 * j1 - 0.006333) * ua * u2 + 0.002178 * u2;
			qc = qc - 0.006675 * uc * u2 - 0.002664 * ue * u2 - 0.002572 * u9 * u3;
			qc = qc - 0.003567 * ub * u3 + 0.002094 * ua * u4 + 0.003342 * uc * u4;
			qc = qc.ToRadians();

			qd = (3606.0 + (130.0 - 43.0 * j1) * j1) * u5 + (1289.0 - 580.0 * j1) * u6;
			qd = qd - 6764.0 * u9 * u1 - 1110.0 * ub * u1 - 224.0 * ud * u1 - 204.0 * u1;
			qd = qd + (1284.0 + 116.0 * j1) * ua * u1 + 188.0 * uc * u1;
			qd = qd + (1460.0 + 130.0 * j1) * u9 * u2 + 224.0 * ub * u2 - 817.0 * u2;
			qd = qd + 6074.0 * u2 * ua + 992.0 * uc * u2 + 508.0 * ue * u2 + 230.0 * ug * u2;
			qd = qd + 108.0 * vh * u2 - (956.0 + 73.0 * j1) * u9 * u3 + 448.0 * ub * u3;
			qd = qd + 137.0 * ud * u3 + (108.0 * j1 - 997.0) * ua * u3 + 480.0 * uc * u3;
			qd = qd + 148.0 * ue * u3 + (99.0 * j1 - 956.0) * u9 * u4 + 490.0 * ub * u4;
			qd = qd + 158.0 * ud * u4 + 179.0 * u4 + (1024.0 + 75.0 * j1) * ua * u4;
			qd = qd - 437.0 * uc * u4 - 132.0 * ue * u4;
			qd *= 0.0000001;

			vk = (0.007192 - 0.003147 * j1) * u5 - 0.004344 * u1;
			vk += (j1 * (0.000197 * j1 - 0.000675) - 0.020428) * u6;
			vk = vk + 0.034036 * ua * u1 + (0.007269 + 0.000672 * j1) * u9 * u1;
			vk = vk + 0.005614 * uc * u1 + 0.002964 * ue * u1 + 0.037761 * u9 * u2;
			vk = vk + 0.006158 * ub * u2 - 0.006603 * ua * u2 - 0.005356 * u9 * u3;
			vk = vk + 0.002722 * ub * u3 + 0.004483 * ua * u3;
			vk = vk - 0.002642 * uc * u3 + 0.004403 * u9 * u4;
			vk = vk - 0.002536 * ub * u4 + 0.005547 * ua * u4 - 0.002689 * uc * u4;
			qe = qc - (vk.ToRadians() / planet.Value4);

			qf = 205.0 * ua - 263.0 * u6 + 693.0 * uc + 312.0 * ue + 147.0 * ug + 299.0 * u9 * u1;
			qf = qf + 181.0 * uc * u1 + 204.0 * ub * u2 + 111.0 * ud * u2 - 337.0 * ua * u2;
			qf -= 111.0 * uc * u2;
			qf *= 0.000001;

			return (qa, qb, qc, qd, qe, qf, qg);
		}

		if (new string[] { "Uranus", "Neptune" }.Contains(planet.Name))
		{
			double j8 = Unwind(1.46205 + 3.81337 * t);
			double j9 = 2.0 * j8 - j4;
			double vj = j9.Sine();
			double uu = j9.Cosine();
			double uv = (2.0 * j9).Sine();
			double uw = (2.0 * j9).Cosine();

			if (planet.Name == "Neptune")
			{
				ja = j8 - j2;
				jb = j8 - j3;
				jc = j8 - j4;
				qc = (0.001089 * j1 - 0.589833) * vj;
				qc = qc + (0.004658 * j1 - 0.056094) * uu - 0.024286 * uv;
				qc = qc.ToRadians();

				vk = 0.024039 * vj - 0.025303 * uu + 0.006206 * uv;
				vk -= 0.005992 * uw;
				qe = qc - (vk.ToRadians() / planet.Value4);

				qd = 4389.0 * vj + 1129.0 * uv + 4262.0 * uu + 1089.0 * uw;
				qd *= 0.0000001;

				qf = 8189.0 * uu - 817.0 * vj + 781.0 * uw;
				qf *= 0.000001;

				double vd = (2.0 * jc).Sine();
				double ve = (2.0 * jc).Cosine();
				double vf = j8.Sine();
				double vg = j8.Cosine();
				qa = -0.009556 * ja.Sine() - 0.005178 * jb.Sine();
				qa = qa + 0.002572 * vd - 0.002972 * ve * vf - 0.002833 * vd * vg;

				qg = 0.000336 * ve * vf + 0.000364 * vd * vg;
				qg = qg.ToRadians();

				qb = -40596.0 + 4992.0 * ja.Cosine() + 2744.0 * jb.Cosine();
				qb = qb + 2044.0 * jc.Cosine() + 1051.0 * ve;
				qb *= 0.000001;

				return (qa, qb, qc, qd, qe, qf, qg);
			}

			ja = j4 - j2;
			jb = j4 - j3;
			jc = j8 - j4;
			qc = (0.864319 - 0.001583 * j1) * vj;
			qc = qc + (0.082222 - 0.006833 * j1) * uu + 0.036017 * uv;
			qc = qc - 0.003019 * uw + 0.008122 * j6.Sine();
			qc = qc.ToRadians();

			vk = 0.120303 * vj + 0.006197 * uv;
			vk += (0.019472 - 0.000947 * j1) * uu;
			qe = qc - (vk.ToRadians() / planet.Value4);

			qd = (163.0 * j1 - 3349.0) * vj + 20981.0 * uu + 1311.0 * uw;
			qd *= 0.0000001;

			qf = -0.003825 * uu;

			qa = (-0.038581 + (0.002031 - 0.00191 * j1) * j1) * (j4 + jb).Cosine();
			qa += (0.010122 - 0.000988 * j1) * (j4 + jb).Sine();
			double a = (0.034964 - (0.001038 - 0.000868 * j1) * j1) * (2.0 * j4 + jb).Cosine();
			qa = a + qa + 0.005594 * (j4 + 3.0 * jc).Sine() - 0.014808 * ja.Sine();
			qa = qa - 0.005794 * jb.Sine() + 0.002347 * jb.Cosine();
			qa = qa + 0.009872 * jc.Sine() + 0.008803 * (2.0 * jc).Sine();
			qa -= 0.004308 * (3.0 * jc).Sine();

			double ux = jb.Sine();
			double uy = jb.Cosine();
			double uz = j4.Sine();
			double va = j4.Cosine();
			double vb = (2.0 * j4).Sine();
			double vc = (2.0 * j4).Cosine();
			qg = (0.000458 * ux - 0.000642 * uy - 0.000517 * (4.0 * jc).Cosine()) * uz;
			qg -= (0.000347 * ux + 0.000853 * uy + 0.000517 * (4.0 * jb).Sine()) * va;
			qg += 0.000403 * ((2.0 * jc).Cosine() * vb + (2.0 * jc).Sine() * vc);
			qg = qg.ToRadians();

			qb = -25948.0 + 4985.0 * ja.Cosine() - 1230.0 * va + 3354.0 * uy;
			qb = qb + 904.0 * (2.0 * jc).Cosine() + 894.0 * (jc.Cosine() - (3.0 * jc).Cosine());
			qb += (5795.0 * va - 1165.0 * uz + 1388.0 * vc) * ux;
			qb += (1351.0 * va + 5702.0 * uz + 1388.0 * vb) * uy;
			qb *= 0.000001;

			return (qa, qb, qc, qd, qe, qf, qg);
		}

		return (qa, qb, qc, qd, qe, qf, qg);
	}

	/// <summary>
	/// For W, in radians, return S, also in radians.
	/// </summary>
	/// <remarks>
	/// Original macro name: SolveCubic
	/// </remarks>
	public static double SolveCubic(double w)
	{
		double s = w / 3.0;

		while (1 == 1)
		{
			double s2 = s * s;
			double d = (s2 + 3.0) * s - w;

			if (Math.Abs(d) < 0.000001)
			{
				return s;
			}

			s = ((2.0 * s * s2) + w) / (3.0 * (s2 + 1.0));
		}
	}

	/// <summary>
	/// Calculate longitude, latitude, and distance of parabolic-orbit comet.
	/// </summary>
	/// <remarks>
	/// Original macro names: PcometLong, PcometLat, PcometDist
	/// </remarks>
	/// <param name="lh">Local civil time, hour part.</param>
	/// <param name="lm">Local civil time, minutes part.</param>
	/// <param name="ls">Local civil time, seconds part.</param>
	/// <param name="ds">Daylight Savings offset.</param>
	/// <param name="zc">Time zone correction, in hours.</param>
	/// <param name="dy">Local date, day part.</param>
	/// <param name="mn">Local date, month part.</param>
	/// <param name="yr">Local date, year part.</param>
	/// <param name="td">Perihelion epoch (day)</param>
	/// <param name="tm">Perihelion epoch (month)</param>
	/// <param name="ty">Perihelion epoch (year)</param>
	/// <param name="q">a (AU)</param>
	/// <param name="i">Inclination (degrees)</param>
	/// <param name="p">Perihelion (degrees)</param>
	/// <param name="n">Node (degrees)</param>
	/// <returns>
	/// <para>cometLongDeg -- Comet longitude (degrees)</para>
	/// <para>cometLatDeg -- Comet lat (degrees)</para>
	/// <para>cometDistAU -- Comet distance from Earth (AU)</para>
	/// </returns>
	public static (double cometLongDeg, double cometLatDeg, double cometDistAU) PCometLongLatDist(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr, double td, int tm, int ty, double q, double i, double p, double n)
	{
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double tpe = (ut / 365.242191) + CivilDateToJulianDate(gd, gm, gy) - CivilDateToJulianDate(td, tm, ty);
		double lg = (SunLong(lh, lm, ls, ds, zc, dy, mn, yr) + 180.0).ToRadians();
		double re = SunDist(lh, lm, ls, ds, zc, dy, mn, yr);

		double rh2 = 0.0;
		double rd = 0.0;
		double s3 = 0.0;
		double c3 = 0.0;
		double lc = 0.0;
		double s2 = 0.0;
		double c2 = 0.0;

		for (int k = 1; k < 3; k++)
		{
			double s = SolveCubic(0.0364911624 * tpe / (q * q.SquareRoot()));
			double nu = 2.0 * s.AngleTangent();
			double r = q * (1.0 + s * s);
			double l = nu + p.ToRadians();
			double s1 = l.Sine();
			double c1 = l.Cosine();
			double i1 = i.ToRadians();
			s2 = s1 * i1.Sine();
			double ps = s2.ASine();
			double y = s1 * i1.Cosine();
			lc = y.AngleTangent2(c1) + n.ToRadians();
			c2 = ps.Cosine();
			rd = r * c2;
			double ll = lc - lg;
			c3 = ll.Cosine();
			s3 = ll.Sine();
			double rh = ((re * re) + (r * r) - (2.0 * re * rd * c3 * ps.Cosine())).SquareRoot();
			if (k == 1)
			{
				rh2 = ((re * re) + (r * r) - (2.0 * re * r * ps.Cosine() * (l + n.ToRadians() - lg).Cosine())).SquareRoot();
			}
		}

		double ep;

		ep = (rd < re) ? (-rd * s3 / (re - (rd * c3))).AngleTangent() + lg + 3.141592654 : (re * s3 / (rd - (re * c3))).AngleTangent() + lc;
		ep = Unwind(ep);

		double tb = rd * s2 * (ep - lc).Sine() / (c2 * re * s3);
		double bp = tb.AngleTangent();

		double cometLongDeg = Degrees(ep);
		double cometLatDeg = Degrees(bp);
		double cometDistAU = rh2;

		return (cometLongDeg, cometLatDeg, cometDistAU);
	}

	/// <summary>
	/// Calculate longitude, latitude, and horizontal parallax of the Moon.
	/// </summary>
	/// <remarks>
	/// Original macro names: MoonLong, MoonLat, MoonHP
	/// </remarks>
	/// <param name="lh">Local civil time, hour part.</param>
	/// <param name="lm">Local civil time, minutes part.</param>
	/// <param name="ls">Local civil time, seconds part.</param>
	/// <param name="ds">Daylight Savings offset.</param>
	/// <param name="zc">Time zone correction, in hours.</param>
	/// <param name="dy">Local date, day part.</param>
	/// <param name="mn">Local date, month part.</param>
	/// <param name="yr">Local date, year part.</param>
	/// <returns>
	/// <para>moonLongDeg -- Moon longitude (degrees)</para>
	/// <para>moonLatDeg -- Moon latitude (degrees)</para>
	/// <para>moonHorPara -- Moon horizontal parallax (degrees)</para>
	/// </returns>
	public static (double moonLongDeg, double moonLatDeg, double moonHorPara) MoonLongLatHP(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020.0) / 36525.0) + (ut / 876600.0);
		double t2 = t * t;

		double m1 = 27.32158213;
		double m2 = 365.2596407;
		double m3 = 27.55455094;
		double m4 = 29.53058868;
		double m5 = 27.21222039;
		double m6 = 6798.363307;
		double q = CivilDateToJulianDate(gd, gm, gy) - 2415020.0 + (ut / 24.0);
		m1 = q / m1;
		m2 = q / m2;
		m3 = q / m3;
		m4 = q / m4;
		m5 = q / m5;
		m6 = q / m6;
		m1 = 360.0 * (m1 - m1.Floor());
		m2 = 360.0 * (m2 - m2.Floor());
		m3 = 360.0 * (m3 - m3.Floor());
		m4 = 360.0 * (m4 - m4.Floor());
		m5 = 360.0 * (m5 - m5.Floor());
		m6 = 360.0 * (m6 - m6.Floor());

		double ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
		double ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
		double md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
		double me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
		double mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
		double na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
		double a = (51.2 + 20.2 * t).ToRadians();
		double s1 = a.Sine();
		double s2 = na.ToRadians().Sine();
		double b = 346.56 + (132.87 - 0.0091731 * t) * t;
		double s3 = 0.003964 * b.ToRadians().Sine();
		double c = (na + 275.05 - 2.3 * t).ToRadians();
		double s4 = c.Sine();
		ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
		ms -= 0.001778 * s1;
		md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
		mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
		me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
		double e = 1.0 - (0.002495 + 0.00000752 * t) * t;
		double e2 = e * e;
		ml = ml.ToRadians();
		ms = ms.ToRadians();
		na = na.ToRadians();
		me1 = me1.ToRadians();
		mf = mf.ToRadians();
		md = md.ToRadians();

		// Longitude-specific
		double l = 6.28875 * md.Sine() + 1.274018 * (2.0 * me1 - md).Sine();
		l = l + 0.658309 * (2.0 * me1).Sine() + 0.213616 * (2.0 * md).Sine();
		l = l - e * 0.185596 * ms.Sine() - 0.114336 * (2.0 * mf).Sine();
		l += 0.058793 * (2.0 * (me1 - md)).Sine();
		l = l + 0.057212 * e * (2.0 * me1 - ms - md).Sine() + 0.05332 * (2.0 * me1 + md).Sine();
		l = l + 0.045874 * e * (2.0 * me1 - ms).Sine() + 0.041024 * e * (md - ms).Sine();
		l = l - 0.034718 * me1.Sine() - e * 0.030465 * (ms + md).Sine();
		l = l + 0.015326 * (2.0 * (me1 - mf)).Sine() - 0.012528 * (2.0 * mf + md).Sine();
		l = l - 0.01098 * (2.0 * mf - md).Sine() + 0.010674 * (4.0 * me1 - md).Sine();
		l = l + 0.010034 * (3.0 * md).Sine() + 0.008548 * (4.0 * me1 - 2.0 * md).Sine();
		l = l - e * 0.00791 * (ms - md + 2.0 * me1).Sine() - e * 0.006783 * (2.0 * me1 + ms).Sine();
		l = l + 0.005162 * (md - me1).Sine() + e * 0.005 * (ms + me1).Sine();
		l = l + 0.003862 * (4.0 * me1).Sine() + e * 0.004049 * (md - ms + 2.0 * me1).Sine();
		l = l + 0.003996 * (2.0 * (md + me1)).Sine() + 0.003665 * (2.0 * me1 - 3.0 * md).Sine();
		l = l + e * 0.002695 * (2.0 * md - ms).Sine() + 0.002602 * (md - 2.0 * (mf + me1)).Sine();
		l = l + e * 0.002396 * (2.0 * (me1 - md) - ms).Sine() - 0.002349 * (md + me1).Sine();
		l = l + e2 * 0.002249 * (2.0 * (me1 - ms)).Sine() - e * 0.002125 * (2.0 * md + ms).Sine();
		l = l - e2 * 0.002079 * (2.0 * ms).Sine() + e2 * 0.002059 * (2.0 * (me1 - ms) - md).Sine();
		l = l - 0.001773 * (md + 2.0 * (me1 - mf)).Sine() - 0.001595 * (2.0 * (mf + me1)).Sine();
		l = l + e * 0.00122 * (4.0 * me1 - ms - md).Sine() - 0.00111 * (2.0 * (md + mf)).Sine();
		l = l + 0.000892 * (md - 3.0 * me1).Sine() - e * 0.000811 * (ms + md + 2.0 * me1).Sine();
		l += e * 0.000761 * (4.0 * me1 - ms - 2.0 * md).Sine();
		l += e2 * 0.000704 * (md - 2.0 * (ms + me1)).Sine();
		l += e * 0.000693 * (ms - 2.0 * (md - me1)).Sine();
		l += e * 0.000598 * (2.0 * (me1 - mf) - ms).Sine();
		l = l + 0.00055 * (md + 4.0 * me1).Sine() + 0.000538 * (4.0 * md).Sine();
		l = l + e * 0.000521 * (4.0 * me1 - ms).Sine() + 0.000486 * (2.0 * md - me1).Sine();
		l += e2 * 0.000717 * (md - 2.0 * ms).Sine();
		double mm = Unwind(ml + l.ToRadians());

		// Latitude-specific
		double g = 5.128189 * mf.Sine() + 0.280606 * (md + mf).Sine();
		g = g + 0.277693 * (md - mf).Sine() + 0.173238 * (2.0 * me1 - mf).Sine();
		g = g + 0.055413 * (2.0 * me1 + mf - md).Sine() + 0.046272 * (2.0 * me1 - mf - md).Sine();
		g = g + 0.032573 * (2.0 * me1 + mf).Sine() + 0.017198 * (2.0 * md + mf).Sine();
		g = g + 0.009267 * (2.0 * me1 + md - mf).Sine() + 0.008823 * (2.0 * md - mf).Sine();
		g = g + e * 0.008247 * (2.0 * me1 - ms - mf).Sine() + 0.004323 * (2.0 * (me1 - md) - mf).Sine();
		g = g + 0.0042 * (2.0 * me1 + mf + md).Sine() + e * 0.003372 * (mf - ms - 2.0 * me1).Sine();
		g += e * 0.002472 * (2.0 * me1 + mf - ms - md).Sine();
		g += e * 0.002222 * (2.0 * me1 + mf - ms).Sine();
		g += e * 0.002072 * (2.0 * me1 - mf - ms - md).Sine();
		g = g + e * 0.001877 * (mf - ms + md).Sine() + 0.001828 * (4.0 * me1 - mf - md).Sine();
		g = g - e * 0.001803 * (mf + ms).Sine() - 0.00175 * (3.0 * mf).Sine();
		g = g + e * 0.00157 * (md - ms - mf).Sine() - 0.001487 * (mf + me1).Sine();
		g = g - e * 0.001481 * (mf + ms + md).Sine() + e * 0.001417 * (mf - ms - md).Sine();
		g = g + e * 0.00135 * (mf - ms).Sine() + 0.00133 * (mf - me1).Sine();
		g = g + 0.001106 * (mf + 3.0 * md).Sine() + 0.00102 * (4.0 * me1 - mf).Sine();
		g = g + 0.000833 * (mf + 4.0 * me1 - md).Sine() + 0.000781 * (md - 3.0 * mf).Sine();
		g = g + 0.00067 * (mf + 4.0 * me1 - 2.0 * md).Sine() + 0.000606 * (2.0 * me1 - 3.0 * mf).Sine();
		g += 0.000597 * (2.0 * (me1 + md) - mf).Sine();
		g = g + e * 0.000492 * (2.0 * me1 + md - ms - mf).Sine() + 0.00045 * (2.0 * (md - me1) - mf).Sine();
		g = g + 0.000439 * (3.0 * md - mf).Sine() + 0.000423 * (mf + 2.0 * (me1 + md)).Sine();
		g = g + 0.000422 * (2.0 * me1 - mf - 3.0 * md).Sine() - e * 0.000367 * (ms + mf + 2.0 * me1 - md).Sine();
		g = g - e * 0.000353 * (ms + mf + 2.0 * me1).Sine() + 0.000331 * (mf + 4.0 * me1).Sine();
		g += e * 0.000317 * (2.0 * me1 + mf - ms + md).Sine();
		g = g + e2 * 0.000306 * (2.0 * (me1 - ms) - mf).Sine() - 0.000283 * (md + 3.0 * mf).Sine();
		double w1 = 0.0004664 * na.Cosine();
		double w2 = 0.0000754 * c.Cosine();
		double bm = g.ToRadians() * (1.0 - w1 - w2);

		// Horizontal parallax-specific
		double pm = 0.950724 + 0.051818 * md.Cosine() + 0.009531 * (2.0 * me1 - md).Cosine();
		pm = pm + 0.007843 * (2.0 * me1).Cosine() + 0.002824 * (2.0 * md).Cosine();
		pm = pm + 0.000857 * (2.0 * me1 + md).Cosine() + e * 0.000533 * (2.0 * me1 - ms).Cosine();
		pm += e * 0.000401 * (2.0 * me1 - md - ms).Cosine();
		pm = pm + e * 0.00032 * (md - ms).Cosine() - 0.000271 * me1.Cosine();
		pm = pm - e * 0.000264 * (ms + md).Cosine() - 0.000198 * (2.0 * mf - md).Cosine();
		pm = pm + 0.000173 * (3.0 * md).Cosine() + 0.000167 * (4.0 * me1 - md).Cosine();
		pm = pm - e * 0.000111 * ms.Cosine() + 0.000103 * (4.0 * me1 - 2.0 * md).Cosine();
		pm = pm - 0.000084 * (2.0 * md - 2.0 * me1).Cosine() - e * 0.000083 * (2.0 * me1 + ms).Cosine();
		pm = pm + 0.000079 * (2.0 * me1 + 2.0 * md).Cosine() + 0.000072 * (4.0 * me1).Cosine();
		pm = pm + e * 0.000064 * (2.0 * me1 - ms + md).Cosine() - e * 0.000063 * (2.0 * me1 + ms - md).Cosine();
		pm = pm + e * 0.000041 * (ms + me1).Cosine() + e * 0.000035 * (2.0 * md - ms).Cosine();
		pm = pm - 0.000033 * (3.0 * md - 2.0 * me1).Cosine() - 0.00003 * (md + me1).Cosine();
		pm = pm - 0.000029 * (2.0 * (mf - me1)).Cosine() - e * 0.000029 * (2.0 * md + ms).Cosine();
		pm = pm + e2 * 0.000026 * (2.0 * (me1 - ms)).Cosine() - 0.000023 * (2.0 * (mf - me1) + md).Cosine();
		pm += e * 0.000019 * (4.0 * me1 - ms - md).Cosine();

		double moonLongDeg = Degrees(mm);
		double moonLatDeg = Degrees(bm);
		double moonHorPara = pm;

		return (moonLongDeg, moonLatDeg, moonHorPara);
	}

	/// <summary>
	/// Calculate current phase of Moon.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonPhase
	/// </remarks>
	public static double MoonPhase(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		(double moonLongDeg, double moonLatDeg, double moonHorPara) moonResult = MoonLongLatHP(lh, lm, ls, ds, zc, dy, mn, yr);

		double cd = (moonResult.moonLongDeg - SunLong(lh, lm, ls, ds, zc, dy, mn, yr)).ToRadians().Cosine() * moonResult.moonLatDeg.ToRadians().Cosine();
		double d = cd.ACosine();
		double sd = d.Sine();
		double i = 0.1468 * sd * (1.0 - 0.0549 * MoonMeanAnomaly(lh, lm, ls, ds, zc, dy, mn, yr).Sine());
		i /= (1.0 - 0.0167 * SunMeanAnomaly(lh, lm, ls, ds, zc, dy, mn, yr).Sine());
		i = 3.141592654 - d - i.ToRadians();
		double k = (1.0 + i.Cosine()) / 2.0;

		return Math.Round(k, 2);
	}

	/// <summary>
	/// Calculate the Moon's mean anomaly.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonMeanAnomaly
	/// </remarks>
	public static double MoonMeanAnomaly(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
	{
		double ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
		double gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
		int gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
		int gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
		double t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020.0) / 36525.0) + (ut / 876600.0);
		double t2 = t * t;

		double m1 = 27.32158213;
		double m2 = 365.2596407;
		double m3 = 27.55455094;
		double m4 = 29.53058868;
		double m5 = 27.21222039;
		double m6 = 6798.363307;
		double q = CivilDateToJulianDate(gd, gm, gy) - 2415020.0 + (ut / 24.0);
		m1 = q / m1;
		m2 = q / m2;
		m3 = q / m3;
		m4 = q / m4;
		m5 = q / m5;
		m6 = q / m6;
		m1 = 360.0 * (m1 - m1.Floor());
		m2 = 360.0 * (m2 - m2.Floor());
		m3 = 360.0 * (m3 - m3.Floor());
		m4 = 360.0 * (m4 - m4.Floor());
		m5 = 360.0 * (m5 - m5.Floor());
		m6 = 360.0 * (m6 - m6.Floor());

		double ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
		double ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
		double md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
		double na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
		double a = (51.2 + 20.2 * t).ToRadians();
		double s1 = a.Sine();
		double s2 = na.ToRadians().Sine();
		double b = 346.56 + (132.87 - 0.0091731 * t) * t;
		double s3 = 0.003964 * b.ToRadians().Sine();
		double c = (na + 275.05 - 2.3 * t).ToRadians();
		md = md + 0.000817 * s1 + s3 + 0.002541 * s2;

		return md.ToRadians();
	}

	/// <summary>
	/// Calculate Julian date of New Moon.
	/// </summary>
	/// <remarks>
	/// Original macro name: NewMoon
	/// </remarks>
	/// <param name="ds">Daylight Savings offset.</param>
	/// <param name="zc">Time zone correction, in hours.</param>
	/// <param name="dy">Local date, day part.</param>
	/// <param name="mn">Local date, month part.</param>
	/// <param name="yr">Local date, year part.</param>
	public static double NewMoon(int ds, int zc, double dy, int mn, int yr)
	{
		double d0 = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int m0 = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int y0 = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);

		double j0 = CivilDateToJulianDate(0.0, 1, y0) - 2415020.0;
		double dj = CivilDateToJulianDate(d0, m0, y0) - 2415020.0;
		double k = Lint(((y0 - 1900.0 + ((dj - j0) / 365.0)) * 12.3685) + 0.5);
		double tn = k / 1236.85;
		double tf = (k + 0.5) / 1236.85;
		double t = tn;
		(double a, double b, double f) nmfmResult1 = NewMoonFullMoon_L6855(k, t);
		double ni = nmfmResult1.a;
		double nf = nmfmResult1.b;
		t = tf;
		k += 0.5;
		(double a, double b, double f) nmfmResult2 = NewMoonFullMoon_L6855(k, t);

		return ni + 2415020.0 + nf;
	}

	/// <summary>
	/// Calculate Julian date of Full Moon.
	/// </summary>
	/// <remarks>
	/// Original macro name: FullMoon
	/// </remarks>
	/// <param name="ds">Daylight Savings offset.</param>
	/// <param name="zc">Time zone correction, in hours.</param>
	/// <param name="dy">Local date, day part.</param>
	/// <param name="mn">Local date, month part.</param>
	/// <param name="yr">Local date, year part.</param>
	public static double FullMoon(int ds, int zc, double dy, int mn, int yr)
	{
		double d0 = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int m0 = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int y0 = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);

		double j0 = CivilDateToJulianDate(0.0, 1, y0) - 2415020.0;
		double dj = CivilDateToJulianDate(d0, m0, y0) - 2415020.0;
		double k = Lint(((y0 - 1900.0 + ((dj - j0) / 365.0)) * 12.3685) + 0.5);
		double tn = k / 1236.85;
		double tf = (k + 0.5) / 1236.85;
		double t = tn;
		(double a, double b, double f) nmfnResult1 = NewMoonFullMoon_L6855(k, t);
		t = tf;
		k += 0.5;
		(double a, double b, double f) nmfnResult2 = NewMoonFullMoon_L6855(k, t);
		double fi = nmfnResult2.a;
		double ff = nmfnResult2.b;

		return fi + 2415020.0 + ff;
	}

	/// <summary>
	/// Helper function for new_moon() and full_moon() """
	/// </summary>
	public static (double a, double b, double f) NewMoonFullMoon_L6855(double k, double t)
	{
		double t2 = t * t;
		double e = 29.53 * k;
		double c = 166.56 + (132.87 - 0.009173 * t) * t;
		c = c.ToRadians();
		double b = 0.00058868 * k + (0.0001178 - 0.000000155 * t) * t2;
		b = b + 0.00033 * c.Sine() + 0.75933;
		double a = k / 12.36886;
		double a1 = 359.2242 + 360.0 * Fract(a) - (0.0000333 + 0.00000347 * t) * t2;
		double a2 = 306.0253 + 360.0 * Fract(k / 0.9330851);
		a2 += (0.0107306 + 0.00001236 * t) * t2;
		a = k / 0.9214926;
		double f = 21.2964 + 360.0 * Fract(a) - (0.0016528 + 0.00000239 * t) * t2;
		a1 = UnwindDeg(a1);
		a2 = UnwindDeg(a2);
		f = UnwindDeg(f);
		a1 = a1.ToRadians();
		a2 = a2.ToRadians();
		f = f.ToRadians();

		double dd = (0.1734 - 0.000393 * t) * a1.Sine() + 0.0021 * (2.0 * a1).Sine();
		dd = dd - 0.4068 * a2.Sine() + 0.0161 * (2.0 * a2).Sine() - 0.0004 * (3.0 * a2).Sine();
		dd = dd + 0.0104 * (2.0 * f).Sine() - 0.0051 * (a1 + a2).Sine();
		dd = dd - 0.0074 * (a1 - a2).Sine() + 0.0004 * (2.0 * f + a1).Sine();
		dd = dd - 0.0004 * (2.0 * f - a1).Sine() - 0.0006 * (2.0 * f + a2).Sine() + 0.001 * (2.0 * f - a2).Sine();
		dd += 0.0005 * (a1 + 2.0 * a2).Sine();
		double e1 = e.Floor();
		b = b + dd + (e - e1);
		double b1 = b.Floor();
		a = e1 + b1;
		b -= b1;

		return (a, b, f);
	}

	/// <summary>
	/// Original macro name: FRACT
	/// </summary>
	public static double Fract(double w)
	{
		return w - Lint(w);
	}

	/// <summary>
	/// Original macro name: LINT
	/// </summary>
	public static double Lint(double w)
	{
		return IInt(w) + IInt(((1.0 * Sign(w)) - 1.0) / 2.0);
	}

	/// <summary>
	/// Original macro name: IINT
	/// </summary>
	public static double IInt(double w)
	{
		return Sign(w) * Math.Abs(w).Floor();
	}

	/// <summary>
	/// Calculate sign of number.
	/// </summary>
	/// <param name="numberToCheck">Number to calculate the sign of.</param>
	/// <returns>signValue -- Sign value: -1, 0, or 1</returns>
	public static double Sign(double numberToCheck)
	{
		double signValue = 0.0;

		if (numberToCheck < 0.0)
			signValue = -1.0;

		if (numberToCheck > 0.0)
			signValue = 1.0;

		return signValue;
	}

	/// <summary>
	/// Original macro name: UTDayAdjust
	/// </summary>
	public static double UTDayAdjust(double ut, double g1)
	{
		double returnValue = ut;

		if ((ut - g1) < -6.0)
			returnValue = ut + 24.0;

		if ((ut - g1) > 6.0)
			returnValue = ut - 24.0;

		return returnValue;
	}

	/// <summary>
	/// Original macro name: Fpart
	/// </summary>
	public static double FPart(double w)
	{
		return w - Lint(w);
	}

	/// <summary>
	/// Original macro name: EQElat
	/// </summary>
	public static double EQELat(double rah, double ram, double ras, double dd, double dm, double ds, double gd, int gm, int gy)
	{
		double a = DegreeHoursToDecimalDegrees(HMStoDH(rah, ram, ras)).ToRadians();
		double b = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double c = Obliq(gd, gm, gy).ToRadians();
		double d = b.Sine() * c.Cosine() - b.Cosine() * c.Sine() * a.Sine();

		return Degrees(d.ASine());
	}

	/// <summary>
	/// Original macro name: EQElong
	/// </summary>
	public static double EQELong(double rah, double ram, double ras, double dd, double dm, double ds, double gd, int gm, int gy)
	{
		double a = DegreeHoursToDecimalDegrees(HMStoDH(rah, ram, ras)).ToRadians();
		double b = DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds).ToRadians();
		double c = Obliq(gd, gm, gy).ToRadians();
		double d = a.Sine() * c.Cosine() + b.Tangent() * c.Sine();
		double e = a.Cosine();
		double f = Degrees(d.AngleTangent2(e));

		return f - 360.0 * (f / 360.0).Floor();
	}

	/// <summary>
	/// Local time of moonrise.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonRiseLCT
	/// </remarks>
	/// <returns>hours</returns>
	public static double MoonRiseLCT(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result1 = MoonRiseLCT_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = lct6700result1.lu;
		lct = lct6700result1.lct;

		if (lct == -99.0)
			return lct;

		double la = lu;

		double x;
		double ut;
		double g1 = 0.0;
		double gu = 0.0;

		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) lct6680result = MoonRiseLCT_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = lct6680result.lct;
			dy1 = lct6680result.dy1;
			mn1 = lct6680result.mn1;
			yr1 = lct6680result.yr1;
			gdy = lct6680result.gdy;
			gmn = lct6680result.gmn;
			gyr = lct6680result.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result2 = MoonRiseLCT_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
			lu = lct6700result2.lu;
			lct = lct6700result2.lct;

			if (lct == -99.0)
				return lct;

			la = lu;
		}

		x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);

		return lct;
	}

	/// <summary>
	/// Helper function for MoonRiseLCT
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonRiseLCT_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for MoonRiseLCT
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) MoonRiseLCT_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeRise(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		if (!ERS(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat).Equals("OK"))
			lct = -99.0;

		return (mm, bm, pm, dp, th, di, p, q, lu, lct);
	}

	/// <summary>
	/// Local date of moonrise.
	/// </summary>
	/// <remarks>
	/// Original macro names: MoonRiseLcDay, MoonRiseLcMonth, MoonRiseLcYear
	/// </remarks>
	/// <returns>
	/// <para>Local date (day)</para>
	/// <para>Local date (month)</para>
	/// <para>Local date (year)</para>
	/// </returns>
	public static (double dy1, int mn1, int yr1) MoonRiseLcDMY(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result1 = MoonRiseLcDMY_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = lct6700result1.lu;
		lct = lct6700result1.lct;

		if (lct == -99.0)
			return (lct, (int)lct, (int)lct);

		double la = lu;

		double x;
		double ut;
		double g1 = 0.0;
		double gu = 0.0;
		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) lct6680result1 = MoonRiseLcDMY_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = lct6680result1.lct;
			dy1 = lct6680result1.dy1;
			mn1 = lct6680result1.mn1;
			yr1 = lct6680result1.yr1;
			gdy = lct6680result1.gdy;
			gmn = lct6680result1.gmn;
			gyr = lct6680result1.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result2 = MoonRiseLcDMY_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);

			lu = lct6700result2.lu;
			lct = lct6700result2.lct;

			if (lct == -99.0)
				return (lct, (int)lct, (int)lct);

			la = lu;
		}

		x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);

		return (dy1, mn1, yr1);
	}

	/// <summary>
	/// Helper function for MoonRiseLcDMY
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonRiseLcDMY_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for MoonRiseLcDMY
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) MoonRiseLcDMY_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeRise(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		return (mm, bm, pm, dp, th, di, p, q, lu, lct);
	}

	/// <summary>
	/// Local azimuth of moonrise.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonRiseAz
	/// </remarks>
	/// <returns>degrees</returns>
	public static double MoonRiseAz(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) az6700result1 = MoonRiseAz_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = az6700result1.lu;
		lct = az6700result1.lct;
		double au;

		if (lct == -99.0)
			return lct;

		double la = lu;

		double x;
		double ut;
		double g1;
		double gu = 0.0;
		double aa = 0.0;
		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) az6680result1 = MoonRiseAz_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = az6680result1.lct;
			dy1 = az6680result1.dy1;
			mn1 = az6680result1.mn1;
			yr1 = az6680result1.yr1;
			gdy = az6680result1.gdy;
			gmn = az6680result1.gmn;
			gyr = az6680result1.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) az6700result2 = MoonRiseAz_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
			lu = az6700result2.lu;
			lct = az6700result2.lct;
			au = az6700result2.au;

			if (lct == -99.0)
				return lct;

			la = lu;
			aa = au;
		}

		au = aa;

		return au;
	}

	/// <summary>
	/// Helper function for MoonRiseAz
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonRiseAz_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for MoonRiseAz
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) MoonRiseAz_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeRise(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);
		double au = RiseSetAzimuthRise(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		return (mm, bm, pm, dp, th, di, p, q, lu, lct, au);
	}

	/// <summary>
	/// Local time of moonset.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonSetLCT
	/// </remarks>
	/// <returns>hours</returns>
	public static double MoonSetLCT(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result1 = MoonSetLCT_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = lct6700result1.lu;
		lct = lct6700result1.lct;

		if (lct == -99.0)
			return lct;

		double la = lu;

		double x;
		double ut;
		double g1 = 0.0;
		double gu = 0.0;
		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) lct6680result1 = MoonSetLCT_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = lct6680result1.lct;
			dy1 = lct6680result1.dy1;
			mn1 = lct6680result1.mn1;
			yr1 = lct6680result1.yr1;
			gdy = lct6680result1.gdy;
			gmn = lct6680result1.gmn;
			gyr = lct6680result1.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) lct6700result2 = MoonSetLCT_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
			lu = lct6700result2.lu;
			lct = lct6700result2.lct;

			if (lct == -99.0)
				return lct;

			la = lu;
		}

		x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);

		return lct;
	}

	/// <summary>
	/// Helper function for MoonSetLCT
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonSetLCT_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for MoonSetLCT
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) MoonSetLCT_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeSet(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		if (!ERS(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat).Equals("OK"))
			lct = -99.0;

		return (mm, bm, pm, dp, th, di, p, q, lu, lct);
	}

	/// <summary>
	/// Local date of moonset.
	/// </summary>
	/// <remarks>
	/// Original macro names: MoonSetLcDay, MoonSetLcMonth, MoonSetLcYear
	/// </remarks>
	/// <returns>
	/// <para>Local date (day)</para>
	/// <para>Local date (month)</para>
	/// <para>Local date (year)</para>
	/// </returns>
	public static (double dy1, int mn1, int yr1) MoonSetLcDMY(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) dmy6700result1 = MoonSetLcDMY_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = dmy6700result1.lu;
		lct = dmy6700result1.lct;

		if (lct == -99.0)
			return (lct, (int)lct, (int)lct);

		double la = lu;

		double x;
		double ut;
		double g1 = 0.0;
		double gu = 0.0;
		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) dmy6680result1 = MoonSetLcDMY_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = dmy6680result1.lct;
			dy1 = dmy6680result1.dy1;
			mn1 = dmy6680result1.mn1;
			yr1 = dmy6680result1.yr1;
			gdy = dmy6680result1.gdy;
			gmn = dmy6680result1.gmn;
			gyr = dmy6680result1.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) dmy6700result2 = MoonSetLcDMY_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
			lu = dmy6700result2.lu;
			lct = dmy6700result2.lct;

			if (lct == -99.0)
				return (lct, (int)lct, (int)lct);

			la = lu;
		}

		x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
		ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);

		return (dy1, mn1, yr1);
	}

	/// <summary>
	/// Helper function for MoonSetLcDMY
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonSetLcDMY_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for MoonSetLcDMY
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct) MoonSetLcDMY_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeSet(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		return (mm, bm, pm, dp, th, di, p, q, lu, lct);
	}

	/// <summary>
	/// Local azimuth of moonset.
	/// </summary>
	/// <remarks>
	/// Original macro name: MoonSetAz
	/// </remarks>
	/// <returns>degrees</returns>
	public static double MoonSetAz(double dy, int mn, int yr, int ds, int zc, double gLong, double gLat)
	{
		double gdy = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gmn = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int gyr = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		double lct = 12.0;
		double dy1 = dy;
		int mn1 = mn;
		int yr1 = yr;

		(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) az6700result1 = MoonSetAz_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
		double lu = az6700result1.lu;
		lct = az6700result1.lct;

		double au;

		if (lct == -99.0)
			return lct;

		double la = lu;

		double x;
		double ut;
		double g1;
		double gu = 0.0;
		double aa = 0.0;
		for (int k = 1; k < 9; k++)
		{
			x = LocalSiderealTimeToGreenwichSiderealTime(la, 0.0, 0.0, gLong);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0.0, 0.0, gdy, gmn, gyr);

			g1 = (k == 1) ? ut : gu;

			gu = ut;
			ut = gu;

			(double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) az6680result1 = MoonSetAz_L6680(x, ds, zc, gdy, gmn, gyr, g1, ut);
			lct = az6680result1.lct;
			dy1 = az6680result1.dy1;
			mn1 = az6680result1.mn1;
			yr1 = az6680result1.yr1;
			gdy = az6680result1.gdy;
			gmn = az6680result1.gmn;
			gyr = az6680result1.gyr;

			(double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) az6700result2 = MoonSetAz_L6700(lct, ds, zc, dy1, mn1, yr1, gdy, gmn, gyr, gLat);
			lu = az6700result2.lu;
			lct = az6700result2.lct;
			au = az6700result2.au;

			if (lct == -99.0)
				return lct;

			la = lu;
			aa = au;
		}

		au = aa;

		return au;
	}

	/// <summary>
	/// Helper function for moon_set_az
	/// </summary>
	public static (double ut, double lct, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr) MoonSetAz_L6680(double x, int ds, int zc, double gdy, int gmn, int gyr, double g1, double ut)
	{
		if (!EGstUt(x, 0.0, 0.0, gdy, gmn, gyr).Equals(PAWarningFlag.OK))
			if (Math.Abs(g1 - ut) > 0.5)
				ut += 23.93447;

		ut = UTDayAdjust(ut, g1);
		double lct = UniversalTimeToLocalCivilTime(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		double dy1 = UniversalTime_LocalCivilDay(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int mn1 = UniversalTime_LocalCivilMonth(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		int yr1 = UniversalTime_LocalCivilYear(ut, 0.0, 0.0, ds, zc, gdy, gmn, gyr);
		gdy = LocalCivilTimeGreenwichDay(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gmn = LocalCivilTimeGreenwichMonth(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		gyr = LocalCivilTimeGreenwichYear(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		ut -= 24.0 * (ut / 24.0).Floor();

		return (ut, lct, dy1, mn1, yr1, gdy, gmn, gyr);
	}

	/// <summary>
	/// Helper function for moon_set_az
	/// </summary>
	public static (double mm, double bm, double pm, double dp, double th, double di, double p, double q, double lu, double lct, double au) MoonSetAz_L6700(double lct, int ds, int zc, double dy1, int mn1, int yr1, double gdy, int gmn, int gyr, double gLat)
	{
		double mm = MoonLong(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double bm = MoonLat(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1);
		double pm = MoonHP(lct, 0.0, 0.0, ds, zc, dy1, mn1, yr1).ToRadians();
		double dp = NutatLong(gdy, gmn, gyr);
		double th = 0.27249 * pm.Sine();
		double di = th + 0.0098902 - pm;
		double p = DecimalDegreesToDegreeHours(EcRA(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr));
		double q = EcDec(mm + dp, 0.0, 0.0, bm, 0.0, 0.0, gdy, gmn, gyr);
		double lu = RiseSetLocalSiderealTimeSet(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);
		double au = RiseSetAzimuthSet(p, 0.0, 0.0, q, 0.0, 0.0, Degrees(di), gLat);

		return (mm, bm, pm, dp, th, di, p, q, lu, lct, au);
	}

	/// <summary>
	/// Determine if a lunar eclipse is likely to occur.
	/// </summary>
	/// <remarks>
	/// Original macro name: LEOccurrence
	/// </remarks>
	public static string LunarEclipseOccurrence(int ds, int zc, double dy, int mn, int yr)
	{
		double d0 = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int m0 = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int y0 = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);

		double j0 = CivilDateToJulianDate(0.0, 1, y0);
		double dj = CivilDateToJulianDate(d0, m0, y0);
		double k = (y0 - 1900.0 + ((dj - j0) * 1.0 / 365.0)) * 12.3685;
		k = Lint(k + 0.5);
		double tn = k / 1236.85;
		double tf = (k + 0.5) / 1236.85;
		double t = tn;
		(double f, double dd, double e1, double b1, double a, double b) l6855result1 = LunarEclipseOccurrence_L6855(t, k);
		t = tf;
		k += 0.5;
		(double f, double dd, double e1, double b1, double a, double b) l6855result2 = LunarEclipseOccurrence_L6855(t, k);
		double fb = l6855result2.f;

		double df = Math.Abs(fb - 3.141592654 * Lint(fb / 3.141592654));

		if (df > 0.37)
			df = 3.141592654 - df;

		string s = "Lunar eclipse certain";
		if (df >= 0.242600766)
		{
			s = "Lunar eclipse possible";

			if (df > 0.37)
				s = "No lunar eclipse";
		}

		return s;
	}

	/// <summary>
	/// Helper function for lunar_eclipse_occurrence
	/// </summary>
	public static (double f, double dd, double e1, double b1, double a, double b) LunarEclipseOccurrence_L6855(double t, double k)
	{
		double t2 = t * t;
		double e = 29.53 * k;
		double c = 166.56 + (132.87 - 0.009173 * t) * t;
		c = c.ToRadians();
		double b = 0.00058868 * k + (0.0001178 - 0.000000155 * t) * t2;
		b = b + 0.00033 * c.Sine() + 0.75933;
		double a = k / 12.36886;
		double a1 = 359.2242 + 360.0 * FPart(a) - (0.0000333 + 0.00000347 * t) * t2;
		double a2 = 306.0253 + 360.0 * FPart(k / 0.9330851);
		a2 += (0.0107306 + 0.00001236 * t) * t2;
		a = k / 0.9214926;
		double f = 21.2964 + 360.0 * FPart(a) - (0.0016528 + 0.00000239 * t) * t2;
		a1 = UnwindDeg(a1);
		a2 = UnwindDeg(a2);
		f = UnwindDeg(f);
		a1 = a1.ToRadians();
		a2 = a2.ToRadians();
		f = f.ToRadians();

		double dd = (0.1734 - 0.000393 * t) * a1.Sine() + 0.0021 * (2.0 * a1).Sine();
		dd = dd - 0.4068 * a2.Sine() + 0.0161 * (2.0 * a2).Sine() - 0.0004 * (3.0 * a2).Sine();
		dd = dd + 0.0104 * (2.0 * f).Sine() - 0.0051 * (a1 + a2).Sine();
		dd = dd - 0.0074 * (a1 - a2).Sine() + 0.0004 * (2.0 * f + a1).Sine();
		dd = dd - 0.0004 * (2.0 * f - a1).Sine() - 0.0006 * (2.0 * f + a2).Sine() + 0.001 * (2.0 * f - a2).Sine();
		dd += 0.0005 * (a1 + 2.0 * a2).Sine();
		double e1 = e.Floor();
		b = b + dd + (e - e1);
		double b1 = b.Floor();
		a = e1 + b1;
		b -= b1;

		return (f, dd, e1, b1, a, b);
	}

	/// <summary>
	/// Calculate time of maximum shadow for lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTMaxLunarEclipse
	/// </remarks>
	public static double UTMaxLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double rp = (hd + rn + ps) * 1.02;
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		return z1;
	}

	/// <summary>
	/// Calculate time of first shadow contact for lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTFirstContactLunarEclipse
	/// </remarks>
	public static double UTFirstContactLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double rp = (hd + rn + ps) * 1.02;
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		if (z6 < 0.0)
			z6 += 24.0;

		return z6;
	}

	/// <summary>
	/// Calculate time of last shadow contact for lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLastContactLunarEclipse
	/// </remarks>
	public static double UTLastContactLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double rp = (hd + rn + ps) * 1.02;
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z7 = z1 + zd - Lint((z1 + zd) / 24.0) * 24.0;

		return z7;
	}

	/// <summary>
	/// Calculate start time of umbra phase of lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTStartUmbraLunarEclipse
	/// </remarks>
	public static double UTStartUmbraLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double ru = (hd - rn + ps) * 1.02;
		double rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		r = rm + ru;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double z8 = z1 - zd;

		if (z8 < 0.0)
			z8 += 24.0;

		return z8;
	}

	/// <summary>
	/// Calculate end time of umbra phase of lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTEndUmbraLunarEclipse
	/// </remarks>
	public static double UTEndUmbraLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double ru = (hd - rn + ps) * 1.02;
		double rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		r = rm + ru;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double z9 = z1 + zd - Lint((z1 + zd) / 24.0) * 24.0;

		return z9;
	}

	/// <summary>
	/// Calculate start time of total phase of lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTStartTotalLunarEclipse
	/// </remarks>
	public static double UTStartTotalLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double ru = (hd - rn + ps) * 1.02;
		double rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		r = rm + ru;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double z8 = z1 - zd;

		r = ru - rm;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double zcc = z1 - zd;

		if (zcc < 0.0)
			zcc = zc + 24.0;

		return zcc;
	}

	/// <summary>
	/// Calculate end time of total phase of lunar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTEndTotalLunarEclipse
	/// </remarks>
	public static double UTEndTotalLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double ru = (hd - rn + ps) * 1.02;
		double rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		r = rm + ru;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double z8 = z1 - zd;

		r = ru - rm;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		zd = dd.SquareRoot();
		double zb = z1 + zd - Lint((z1 + zd) / 24.0) * 24.0;

		return zb;
	}

	/// <summary>
	/// Calculate magnitude of lunar eclipse.
	/// </summary>
	/// <remarks>
	/// Original macro name: MagLunarEclipse
	/// </remarks>
	public static double MagLunarEclipse(double dy, int mn, int yr, int ds, int zc)
	{
		double tp = 2.0 * Math.PI;

		if (LunarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No lunar eclipse"))
			return -99.0;

		double dj = FullMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utfm = xi * 24.0;
		double ut = utfm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utfm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utfm;
		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double q = 0.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		sr = sr + Math.PI - Lint((sr + Math.PI) / tp) * tp;
		by -= q;
		bz -= q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double ru = (hd - rn + ps) * 1.02;
		double rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rp;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		r = rm + ru;
		dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);
		double mg = (rm + rp - pj) / (2.0 * rm);

		if (dd < 0.0)
			return mg;

		zd = dd.SquareRoot();
		double z8 = z1 - zd;


		r = ru - rm;
		dd = z1 - x0;
		mg = (rm + ru - pj) / (2.0 * rm);

		return mg;
	}

	/// <summary>
	/// Determine if a solar eclipse is likely to occur.
	/// </summary>
	/// <remarks>
	/// Original macro name: SEOccurrence
	/// </remarks>
	public static string SolarEclipseOccurrence(int ds, int zc, double dy, int mn, int yr)
	{
		double d0 = LocalCivilTimeGreenwichDay(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int m0 = LocalCivilTimeGreenwichMonth(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);
		int y0 = LocalCivilTimeGreenwichYear(12.0, 0.0, 0.0, ds, zc, dy, mn, yr);

		double j0 = CivilDateToJulianDate(0.0, 1, y0);
		double dj = CivilDateToJulianDate(d0, m0, y0);
		double k = (y0 - 1900.0 + ((dj - j0) * 1.0 / 365.0)) * 12.3685;
		k = Lint(k + 0.5);
		double tn = k / 1236.85;
		double tf = (k + 0.5) / 1236.85;
		double t = tn;
		(double f, double dd, double e1, double b1, double a, double b) l6855result1 = SolarEclipseOccurrence_L6855(t, k);
		double nb = l6855result1.f;
		t = tf;
		k += 0.5;
		(double f, double dd, double e1, double b1, double a, double b) l6855result2 = SolarEclipseOccurrence_L6855(t, k);

		double df = Math.Abs(nb - 3.141592654 * Lint(nb / 3.141592654));

		if (df > 0.37)
			df = 3.141592654 - df;

		string s = "Solar eclipse certain";
		if (df >= 0.242600766)
		{
			s = "Solar eclipse possible";
			if (df > 0.37)
				s = "No solar eclipse";
		}

		return s;
	}

	/// <summary>
	/// Helper function for SolarEclipseOccurrence
	/// </summary>
	public static (double f, double dd, double e1, double b1, double a, double b) SolarEclipseOccurrence_L6855(double t, double k)
	{
		double t2 = t * t;
		double e = 29.53 * k;
		double c = 166.56 + (132.87 - 0.009173 * t) * t;
		c = c.ToRadians();
		double b = 0.00058868 * k + (0.0001178 - 0.000000155 * t) * t2;
		b = b + 0.00033 * c.Sine() + 0.75933;
		double a = k / 12.36886;
		double a1 = 359.2242 + 360.0 * FPart(a) - (0.0000333 + 0.00000347 * t) * t2;
		double a2 = 306.0253 + 360.0 * FPart(k / 0.9330851);
		a2 += (0.0107306 + 0.00001236 * t) * t2;
		a = k / 0.9214926;
		double f = 21.2964 + 360.0 * FPart(a) - (0.0016528 + 0.00000239 * t) * t2;
		a1 = UnwindDeg(a1);
		a2 = UnwindDeg(a2);
		f = UnwindDeg(f);
		a1 = a1.ToRadians();
		a2 = a2.ToRadians();
		f = f.ToRadians();

		double dd = (0.1734 - 0.000393 * t) * a1.Sine() + 0.0021 * (2.0 * a1).Sine();
		dd = dd - 0.4068 * a2.Sine() + 0.0161 * (2.0 * a2).Sine() - 0.0004 * (3.0 * a2).Sine();
		dd = dd + 0.0104 * (2.0 * f).Sine() - 0.0051 * (a1 + a2).Sine();
		dd = dd - 0.0074 * (a1 - a2).Sine() + 0.0004 * (2.0 * f + a1).Sine();
		dd = dd - 0.0004 * (2.0 * f - a1).Sine() - 0.0006 * (2.0 * f + a2).Sine() + 0.001 * (2.0 * f - a2).Sine();
		dd += 0.0005 * (a1 + 2.0 * a2).Sine();
		double e1 = e.Floor();
		b = b + dd + (e - e1);
		double b1 = b.Floor();
		a = e1 + b1;
		b -= b1;

		return (f, dd, e1, b1, a, b);
	}

	/// <summary>
	/// Calculate time of maximum shadow for solar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTMaxSolarEclipse
	/// </remarks>
	public static double UTMaxSolarEclipse(double dy, int mn, int yr, int ds, int zc, double glong, double glat)
	{
		double tp = 2.0 * Math.PI;

		if (SolarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No solar eclipse"))
			return -99.0;

		double dj = NewMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utnm = xi * 24.0;
		double ut = utnm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utnm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utnm;
		double x = my;
		double y = by;
		double tm = xh - 1.0;
		double hp = hy;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result1 = UTMaxSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		my = l7390result1.p;
		by = l7390result1.q;
		x = mz;
		y = bz;
		tm = xh + 1.0;
		hp = hz;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result2 = UTMaxSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		mz = l7390result2.p;
		bz = l7390result2.q;

		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		x = sr;
		y = 0.0;
		tm = ut;
		hp = 0.00004263452 / rr;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result3 = UTMaxSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		// let(_paa, _qaa, _xaa, _pbb, _qbb, _xbb, p, q) =
		sr = l7390result3.p;
		by -= l7390result3.q;
		bz -= l7390result3.q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double _ru = (hd - rn + ps) * 1.02;
		double _rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rn;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();

		return z1;
	}

	/// <summary>
	/// Helper function for ut_max_solar_eclipse
	/// </summary>
	public static (double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) UTMaxSolarEclipse_L7390(double x, double y, double igday, int gmonth, int gyear, double tm, double glong, double glat, double hp)
	{
		double paa = EcRA(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double qaa = EcDec(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double xaa = RightAscensionToHourAngle(DecimalDegreesToDegreeHours(paa), 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double pbb = ParallaxHA(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double qbb = ParallaxDec(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double xbb = HourAngleToRightAscension(pbb, 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double p = EQELong(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();
		double q = EQELat(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();

		return (paa, qaa, xaa, pbb, qbb, xbb, p, q);
	}

	/// <summary>
	/// Calculate time of first contact for solar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTFirstContactSolarEclipse
	/// </remarks>
	public static double UTFirstContactSolarEclipse(double dy, int mn, int yr, int ds, int zc, double glong, double glat)
	{
		double tp = 2.0 * Math.PI;

		if (SolarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No solar eclipse"))
			return -99.0;

		double dj = NewMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utnm = xi * 24.0;
		double ut = utnm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utnm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utnm;
		double x = my;
		double y = by;
		double tm = xh - 1.0;
		double hp = hy;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result1 = UTFirstContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		my = l7390result1.p;
		by = l7390result1.q;
		x = mz;
		y = bz;
		tm = xh + 1.0;
		hp = hz;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result2 = UTFirstContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		mz = l7390result2.p;
		bz = l7390result2.q;

		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		x = sr;
		y = 0.0;
		tm = ut;
		hp = 0.00004263452 / rr;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result3 = UTFirstContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		sr = l7390result3.p;
		by -= l7390result3.q;
		bz -= l7390result3.q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double _ru = (hd - rn + ps) * 1.02;
		double _rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rn;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z6 = z1 - zd;

		if (z6 < 0.0)
			z6 += 24.0;

		return z6;
	}

	/// <summary>
	/// Helper function for UTFirstContactSolarEclipse
	/// </summary>
	public static (double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) UTFirstContactSolarEclipse_L7390(double x, double y, double igday, int gmonth, int gyear, double tm, double glong, double glat, double hp)
	{
		double paa = EcRA(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double qaa = EcDec(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double xaa = RightAscensionToHourAngle(DecimalDegreesToDegreeHours(paa), 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double pbb = ParallaxHA(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double qbb = ParallaxDec(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double xbb = HourAngleToRightAscension(pbb, 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double p = EQELong(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();
		double q = EQELat(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();

		return (paa, qaa, xaa, pbb, qbb, xbb, p, q);
	}

	/// <summary>
	/// Calculate time of last contact for solar eclipse (UT)
	/// </summary>
	/// <remarks>
	/// Original macro name: UTLastContactSolarEclipse
	/// </remarks>
	public static double UTLastContactSolarEclipse(double dy, int mn, int yr, int ds, int zc, double glong, double glat)
	{
		double tp = 2.0 * Math.PI;

		if (SolarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No solar eclipse"))
			return -99.0;

		double dj = NewMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utnm = xi * 24.0;
		double ut = utnm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utnm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utnm;
		double x = my;
		double y = by;
		double tm = xh - 1.0;
		double hp = hy;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result1 = UTLastContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		my = l7390result1.p;
		by = l7390result1.q;
		x = mz;
		y = bz;
		tm = xh + 1.0;
		hp = hz;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result2 = UTLastContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		mz = l7390result2.p;
		bz = l7390result2.q;

		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		x = sr;
		y = 0.0;
		tm = ut;
		hp = 0.00004263452 / rr;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result3 = UTLastContactSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		sr = l7390result3.p;
		by -= l7390result3.q;
		bz -= l7390result3.q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double _ru = (hd - rn + ps) * 1.02;
		double _rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rn;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();
		double z7 = z1 + zd - Lint((z1 + zd) / 24.0) * 24.0;

		return z7;
	}

	/// <summary>
	/// Helper function for ut_last_contact_solar_eclipse
	/// </summary>
	public static (double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) UTLastContactSolarEclipse_L7390(double x, double y, double igday, int gmonth, int gyear, double tm, double glong, double glat, double hp)
	{
		double paa = EcRA(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double qaa = EcDec(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double xaa = RightAscensionToHourAngle(DecimalDegreesToDegreeHours(paa), 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double pbb = ParallaxHA(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double qbb = ParallaxDec(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double xbb = HourAngleToRightAscension(pbb, 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double p = EQELong(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();
		double q = EQELat(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();

		return (paa, qaa, xaa, pbb, qbb, xbb, p, q);
	}

	/// <summary>
	/// Calculate magnitude of solar eclipse.
	/// </summary>
	/// <remarks>
	/// Original macro name: MagSolarEclipse
	/// </remarks>
	public static double MagSolarEclipse(double dy, int mn, int yr, int ds, int zc, double glong, double glat)
	{
		double tp = 2.0 * Math.PI;

		if (SolarEclipseOccurrence(ds, zc, dy, mn, yr).Equals("No solar eclipse"))
			return -99.0;

		double dj = NewMoon(ds, zc, dy, mn, yr);
		double gday = JulianDateDay(dj);
		int gmonth = JulianDateMonth(dj);
		int gyear = JulianDateYear(dj);
		double igday = gday.Floor();
		double xi = gday - igday;
		double utnm = xi * 24.0;
		double ut = utnm - 1.0;
		double ly = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double my = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double by = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hy = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		ut = utnm + 1.0;
		double sb = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians() - ly;
		double mz = MoonLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double bz = MoonLat(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		double hz = MoonHP(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();

		if (sb < 0.0)
			sb += tp;

		double xh = utnm;
		double x = my;
		double y = by;
		double tm = xh - 1.0;
		double hp = hy;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result1 = MagSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		my = l7390result1.p;
		by = l7390result1.q;
		x = mz;
		y = bz;
		tm = xh + 1.0;
		hp = hz;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result2 = MagSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		mz = l7390result2.p;
		bz = l7390result2.q;

		double x0 = xh + 1.0 - (2.0 * bz / (bz - by));
		double dm = mz - my;

		if (dm < 0.0)
			dm += tp;

		double lj = (dm - sb) / 2.0;
		double mr = my + (dm * (x0 - xh + 1.0) / 2.0);
		ut = x0 - 0.13851852;
		double rr = SunDist(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear);
		double sr = SunLong(ut, 0.0, 0.0, 0, 0, igday, gmonth, gyear).ToRadians();
		sr += (NutatLong(igday, gmonth, gyear) - 0.00569).ToRadians();
		x = sr;
		y = 0.0;
		tm = ut;
		hp = 0.00004263452 / rr;
		(double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) l7390result3 = MagSolarEclipse_L7390(x, y, igday, gmonth, gyear, tm, glong, glat, hp);
		sr = l7390result3.p;
		by -= l7390result3.q;
		bz -= l7390result3.q;
		double p3 = 0.00004263;
		double zh = (sr - mr) / lj;
		double tc = x0 + zh;
		double sh = (((bz - by) * (tc - xh - 1.0) / 2.0) + bz) / lj;
		double s2 = sh * sh;
		double z2 = zh * zh;
		double ps = p3 / (rr * lj);
		double z1 = (zh * z2 / (z2 + s2)) + x0;
		double h0 = (hy + hz) / (2.0 * lj);
		double rm = 0.272446 * h0;
		double rn = 0.00465242 / (lj * rr);
		double hd = h0 * 0.99834;
		double _ru = (hd - rn + ps) * 1.02;
		double _rp = (hd + rn + ps) * 1.02;
		double pj = Math.Abs(sh * zh / (s2 + z2).SquareRoot());
		double r = rm + rn;
		double dd = z1 - x0;
		dd = dd * dd - ((z2 - (r * r)) * dd / zh);

		if (dd < 0.0)
			return -99.0;

		double zd = dd.SquareRoot();

		double mg = (rm + rn - pj) / (2.0 * rn);

		return mg;
	}

	/// <summary>
	/// Helper function for mag_solar_eclipse
	/// </summary>
	public static (double paa, double qaa, double xaa, double pbb, double qbb, double xbb, double p, double q) MagSolarEclipse_L7390(double x, double y, double igday, int gmonth, int gyear, double tm, double glong, double glat, double hp)
	{
		double paa = EcRA(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double qaa = EcDec(Degrees(x), 0.0, 0.0, Degrees(y), 0.0, 0.0, igday, gmonth, gyear);
		double xaa = RightAscensionToHourAngle(DecimalDegreesToDegreeHours(paa), 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double pbb = ParallaxHA(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double qbb = ParallaxDec(xaa, 0.0, 0.0, qaa, 0.0, 0.0, PACoordinateType.True, glat, 0.0, Degrees(hp));
		double xbb = HourAngleToRightAscension(pbb, 0.0, 0.0, tm, 0.0, 0.0, 0, 0, igday, gmonth, gyear, glong);
		double p = EQELong(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();
		double q = EQELat(xbb, 0.0, 0.0, qbb, 0.0, 0.0, igday, gmonth, gyear).ToRadians();

		return (paa, qaa, xaa, pbb, qbb, xbb, p, q);
	}
}
