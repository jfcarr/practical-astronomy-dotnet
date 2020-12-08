using System;
using System.Collections.Generic;
using System.Linq;
using PALib.Data;
using PALib.Helpers;

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
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
			var e = (c == 60) ? b + 60 : b;

			return (decimalHours < 0) ? (int)-((e / 3600).Floor()) : (int)(e / 3600).Floor();
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
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
			var e = (c == 60) ? b + 60 : b;

			return (int)(e / 60).Floor() % 60;
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
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
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
				var a = (y / 100).Floor();
				b = 2 - a + (a / 4).Floor();
			}
			else
			{
				if (fYear == 1582 && fMonth > 10)
				{
					var a = (y / 100).Floor();
					b = 2 - a + (a / 4).Floor();
				}
				else
				{
					if (fYear == 1582 && fMonth == 10 && fDay >= 15)
					{
						var a = (y / 100).Floor();
						b = 2 - a + (a / 4).Floor();
					}
					else
						b = 0;
				}
			}

			var c = (y < 0) ? (((365.25 * y) - 0.75)).Floor() : (365.25 * y).Floor();
			var d = (30.6001 * (m + 1.0)).Floor();

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
			var i = (julianDate + 0.5).Floor();
			var f = julianDate + 0.5 - i;
			var a = ((i - 1867216.25) / 36524.25).Floor();
			var b = (i > 2299160) ? i + 1 + a - (a / 4).Floor() : i;
			var c = b + 1524;
			var d = ((c - 122.1) / 365.25).Floor();
			var e = (365.25 * d).Floor();
			var g = ((c - e) / 30.6001).Floor();

			return c - e + f - (30.6001 * g).Floor();
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
			var i = (julianDate + 0.5).Floor();
			var a = ((i - 1867216.25) / 36524.25).Floor();
			var b = (i > 2299160) ? i + 1 + a - (a / 4).Floor() : i;
			var c = b + 1524;
			var d = ((c - 122.1) / 365.25).Floor();
			var e = (365.25 * d).Floor();
			var g = ((c - e) / 30.6001).Floor();

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
			var i = (julianDate + 0.5).Floor();
			// var _f = julian_date + 0.5 - i;
			var a = ((i - 1867216.25) / 36524.25).Floor();
			var b = (i > 2299160) ? i + 1.0 + a - (a / 4.0).Floor() : i;
			var c = b + 1524;
			var d = ((c - 122.1) / 365.25).Floor();
			var e = (365.25 * d).Floor();
			var g = ((c - e) / 30.6001).Floor();
			var h = (g < 13.5) ? g - 1 : g - 13;

			var returnValue = (h > 2.5) ? d - 4716 : d - 4715;

			return (int)returnValue;
		}

		/// <summary>
		/// Convert Right Ascension to Hour Angle
		/// </summary>
		/// <remarks>
		/// Original macro name: RAHA
		/// </remarks>
		/// <param name="raHours"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="isDaylightSavings"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public static double RightAscensionToHourAngle(double raHours, double raMinutes, double raSeconds, double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var a = LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var b = LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var c = LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var d = LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var e = UniversalTimeToGreenwichSiderealTime(a, 0, 0, b, c, d);
			var f = GreenwichSiderealTimeToLocalSiderealTime(e, 0, 0, geographicalLongitude);
			var g = HMStoDH(raHours, raMinutes, raSeconds);
			var h = f - g;

			return (h < 0) ? 24 + h : h;
		}

		/// <summary>
		/// Convert Hour Angle to Right Ascension
		/// </summary>
		/// <remarks>
		/// Original macro name: HARA
		/// </remarks>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public static double HourAngleToRightAscension(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var a = LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var b = LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var c = LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var d = LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var e = UniversalTimeToGreenwichSiderealTime(a, 0, 0, b, c, d);
			var f = GreenwichSiderealTimeToLocalSiderealTime(e, 0, 00, geographicalLongitude);
			var g = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
			var h = f - g;

			return (h < 0) ? 24 + h : h;
		}

		/// <summary>
		/// Convert Local Civil Time to Universal Time
		/// </summary>
		/// <remarks>
		/// Original macro name: LctUT
		/// </remarks>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <returns></returns>
		public static double LocalCivilTimeToUniversalTime(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
		{
			var a = HMStoDH(lctHours, lctMinutes, lctSeconds);
			var b = a - daylightSaving - zoneCorrection;
			var c = localDay + (b / 24);
			var d = CivilDateToJulianDate(c, localMonth, localYear);
			var e = JulianDateDay(d);
			var e1 = e.Floor();

			return 24 * (e - e1);
		}

		/// <summary>
		/// Convert Universal Time to Local Civil Time
		/// </summary>
		/// <remarks>
		/// Original macro name: UTLct
		/// </remarks>
		/// <param name="uHours"></param>
		/// <param name="uMinutes"></param>
		/// <param name="uSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public static double UniversalTimeToLocalCivilTime(double uHours, double uMinutes, double uSeconds, int daylightSaving, int zoneCorrection, double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var a = HMStoDH(uHours, uMinutes, uSeconds);
			var b = a + zoneCorrection;
			var c = b + daylightSaving;
			var d = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) + (c / 24);
			var e = JulianDateDay(d);
			var e1 = e.Floor();

			return 24 * (e - e1);
		}

		/// <summary>
		/// Determine Greenwich Day for Local Time
		/// </summary>
		/// <remarks>
		/// Original macro name: LctGDay
		/// </remarks>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <returns></returns>
		public static double LocalCivilTimeGreenwichDay(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
		{
			var a = HMStoDH(lctHours, lctMinutes, lctSeconds);
			var b = a - daylightSaving - zoneCorrection;
			var c = localDay + (b / 24);
			var d = CivilDateToJulianDate(c, localMonth, localYear);
			var e = JulianDateDay(d);

			return e.Floor();
		}

		/// <summary>
		/// Determine Greenwich Month for Local Time
		/// </summary>
		/// <remarks>
		/// Original macro name: LctGMonth
		/// </remarks>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <returns></returns>
		public static int LocalCivilTimeGreenwichMonth(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
		{
			var a = HMStoDH(lctHours, lctMinutes, lctSeconds);
			var b = a - daylightSaving - zoneCorrection;
			var c = localDay + (b / 24);
			var d = CivilDateToJulianDate(c, localMonth, localYear);

			return JulianDateMonth(d);
		}

		/// <summary>
		/// Determine Greenwich Year for Local Time
		/// </summary>
		/// <remarks>
		/// Original macro name: LctGYear
		/// </remarks>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="daylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <returns></returns>
		public static int LocalCivilTimeGreenwichYear(double lctHours, double lctMinutes, double lctSeconds, int daylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear)
		{
			var a = HMStoDH(lctHours, lctMinutes, lctSeconds);
			var b = a - daylightSaving - zoneCorrection;
			var c = localDay + (b / 24);
			var d = CivilDateToJulianDate(c, localMonth, localYear);

			return JulianDateYear(d);
		}

		/// <summary>
		/// Convert Universal Time to Greenwich Sidereal Time
		/// </summary>
		/// <remarks>
		/// Original macro name: UTGST
		/// </remarks>
		/// <param name="uHours"></param>
		/// <param name="uMinutes"></param>
		/// <param name="uSeconds"></param>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public static double UniversalTimeToGreenwichSiderealTime(double uHours, double uMinutes, double uSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
			var b = a - 2451545;
			var c = b / 36525;
			var d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
			var e = d - (24 * (d / 24).Floor());
			var f = HMStoDH(uHours, uMinutes, uSeconds);
			var g = f * 1.002737909;
			var h = e + g;

			return h - (24 * (h / 24).Floor());
		}

		/// <summary>
		/// Convert Greenwich Sidereal Time to Local Sidereal Time
		/// </summary>
		/// <remarks>
		/// Original macro name: GSTLST
		/// </remarks>
		/// <param name="greenwichHours"></param>
		/// <param name="greenwichMinutes"></param>
		/// <param name="greenwichSeconds"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public static double GreenwichSiderealTimeToLocalSiderealTime(double greenwichHours, double greenwichMinutes, double greenwichSeconds, double geographicalLongitude)
		{
			var a = HMStoDH(greenwichHours, greenwichMinutes, greenwichSeconds);
			var b = geographicalLongitude / 15;
			var c = a + b;

			return c - (24 * (c / 24).Floor());
		}

		/// <summary>
		/// Convert Equatorial Coordinates to Azimuth (in decimal degrees)
		/// </summary>
		/// <remarks>
		/// Original macro name: EQAz
		/// </remarks>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="declinationDegrees"></param>
		/// <param name="declinationMinutes"></param>
		/// <param name="declinationSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns></returns>
		public static double EquatorialCoordinatesToAzimuth(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
		{
			var a = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
			var b = a * 15;
			var c = b.ToRadians();
			var d = DegreesMinutesSecondsToDecimalDegrees(declinationDegrees, declinationMinutes, declinationSeconds);
			var e = d.ToRadians();
			var f = geographicalLatitude.ToRadians();
			var g = e.Sine() * f.Sine() + e.Cosine() * f.Cosine() * c.Cosine();
			var h = -e.Cosine() * f.Cosine() * c.Sine();
			var i = e.Sine() - (f.Sine() * g);
			var j = Degrees(h.AngleTangent2(i));

			return j - 360.0 * (j / 360).Floor();
		}

		/// <summary>
		/// Convert Equatorial Coordinates to Altitude (in decimal degrees)
		/// </summary>
		/// <remarks>
		/// Original macro name: EQAlt
		/// </remarks>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="declinationDegrees"></param>
		/// <param name="declinationMinutes"></param>
		/// <param name="declinationSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns></returns>
		public static double EquatorialCoordinatesToAltitude(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
		{
			var a = HMStoDH(hourAngleHours, hourAngleMinutes, hourAngleSeconds);
			var b = a * 15;
			var c = b.ToRadians();
			var d = DegreesMinutesSecondsToDecimalDegrees(declinationDegrees, declinationMinutes, declinationSeconds);
			var e = d.ToRadians();
			var f = geographicalLatitude.ToRadians();
			var g = e.Sine() * f.Sine() + e.Cosine() * f.Cosine() * c.Cosine();

			return Degrees(g.ASine());
		}

		/// <summary>
		/// Convert Degrees Minutes Seconds to Decimal Degrees
		/// </summary>
		/// <remarks>
		/// Original macro name: DMSDD
		/// </remarks>
		/// <param name="degrees"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static double DegreesMinutesSecondsToDecimalDegrees(double degrees, double minutes, double seconds)
		{
			var a = Math.Abs(seconds) / 60;
			var b = (Math.Abs(minutes) + a) / 60;
			var c = Math.Abs(degrees) + b;

			return (degrees < 0 || minutes < 0 || seconds < 0) ? -c : c;
		}

		/// <summary>
		/// Convert W to Degrees
		/// </summary>
		/// <remarks>
		/// Original macro name: Degrees
		/// </remarks>
		/// <param name="w"></param>
		/// <returns></returns>
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
		/// <param name="decimalDegrees"></param>
		/// <returns></returns>
		public static double DecimalDegreesDegrees(double decimalDegrees)
		{
			var a = Math.Abs(decimalDegrees);
			var b = a * 3600;
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
			var e = (c == 60) ? 60 : b;

			return (decimalDegrees < 0) ? -((e / 3600).Floor()) : (e / 3600).Floor();
		}

		/// <summary>
		/// Return Minutes part of Decimal Degrees
		/// </summary>
		/// <remarks>
		/// Original macro name: DDMin
		/// </remarks>
		/// <param name="decimalDegrees"></param>
		/// <returns></returns>
		public static double DecimalDegreesMinutes(double decimalDegrees)
		{
			var a = Math.Abs(decimalDegrees);
			var b = a * 3600;
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
			var e = (c == 60) ? b + 60 : b;

			return (e / 60).Floor() % 60;
		}

		/// <summary>
		/// Return Seconds part of Decimal Degrees
		/// </summary>
		/// <remarks>
		/// Original macro name: DDSec
		/// </remarks>
		/// <param name="decimalDegrees"></param>
		/// <returns></returns>
		public static double DecimalDegreesSeconds(double decimalDegrees)
		{
			var a = Math.Abs(decimalDegrees);
			var b = a * 3600;
			var c = Math.Round(b - 60 * (b / 60).Floor(), 2);
			var d = (c == 60) ? 0 : c;

			return d;
		}

		/// <summary>
		/// Convert Decimal Degrees to Degree-Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: DDDH
		/// </remarks>
		/// <param name="decimalDegrees"></param>
		/// <returns></returns>
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
		/// <param name="degreeHours"></param>
		/// <returns></returns>
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
		/// <param name="azimuthDegrees"></param>
		/// <param name="azimuthMinutes"></param>
		/// <param name="azimuthSeconds"></param>
		/// <param name="altitudeDegrees"></param>
		/// <param name="altitudeMinutes"></param>
		/// <param name="altitudeSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns></returns>
		public static double HorizonCoordinatesToDeclination(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
		{
			var a = DegreesMinutesSecondsToDecimalDegrees(azimuthDegrees, azimuthMinutes, azimuthSeconds);
			var b = DegreesMinutesSecondsToDecimalDegrees(altitudeDegrees, altitudeMinutes, altitudeSeconds);
			var c = a.ToRadians();
			var d = b.ToRadians();
			var e = geographicalLatitude.ToRadians();
			var f = d.Sine() * e.Sine() + d.Cosine() * e.Cosine() * c.Cosine();

			return Degrees(f.ASine());
		}

		/// <summary>
		/// Convert Horizon Coordinates to Hour Angle (in decimal degrees)
		/// </summary>
		/// <remarks>
		/// Original macro name: HORHa
		/// </remarks>
		/// <param name="azimuthDegrees"></param>
		/// <param name="azimuthMinutes"></param>
		/// <param name="azimuthSeconds"></param>
		/// <param name="altitudeDegrees"></param>
		/// <param name="altitudeMinutes"></param>
		/// <param name="altitudeSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns></returns>
		public static double HorizonCoordinatesToHourAngle(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
		{
			var a = DegreesMinutesSecondsToDecimalDegrees(azimuthDegrees, azimuthMinutes, azimuthSeconds);
			var b = DegreesMinutesSecondsToDecimalDegrees(altitudeDegrees, altitudeMinutes, altitudeSeconds);
			var c = a.ToRadians();
			var d = b.ToRadians();
			var e = geographicalLatitude.ToRadians();
			var f = d.Sine() * e.Sine() + d.Cosine() * e.Cosine() * c.Cosine();
			var g = -d.Cosine() * e.Cosine() * c.Sine();
			var h = d.Sine() - e.Sine() * f;
			var i = DecimalDegreesToDegreeHours(Degrees(g.AngleTangent2(h)));

			return i - 24 * (i / 24).Floor();
		}

		/// <summary>
		/// Obliquity of the Ecliptic for a Greenwich Date
		/// </summary>
		/// <remarks>
		/// Original macro name: Obliq
		/// </remarks>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public static double Obliq(double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
			var b = a - 2415020;
			var c = (b / 36525) - 1;
			var d = c * (46.815 + c * (0.0006 - (c * 0.00181)));
			var e = d / 3600;

			return 23.43929167 - e + NutatObl(greenwichDay, greenwichMonth, greenwichYear);
		}

		/// <summary>
		/// Nutation amount to be added in ecliptic longitude, in degrees.
		/// </summary>
		/// <remarks>
		/// Original macro name: NutatLong
		/// </remarks>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double NutatLong(double gd, int gm, int gy)
		{
			var dj = CivilDateToJulianDate(gd, gm, gy) - 2415020;
			var t = dj / 36525;
			var t2 = t * t;

			var a = 100.0021358 * t;
			var b = 360 * (a - a.Floor());

			var l1 = 279.6967 + 0.000303 * t2 + b;
			var l2 = 2 * l1.ToRadians();

			a = 1336.855231 * t;
			b = 360 * (a - a.Floor());

			var d1 = 270.4342 - 0.001133 * t2 + b;
			var d2 = 2 * d1.ToRadians();

			a = 99.99736056 * t;
			b = 360 * (a - a.Floor());

			var m1 = 358.4758 - 0.00015 * t2 + b;
			m1 = m1.ToRadians();

			a = 1325.552359 * t;
			b = 360 * (a - a.Floor());

			var m2 = 296.1046 + 0.009192 * t2 + b;
			m2 = m2.ToRadians();

			a = 5.372616667 * t;
			b = 360 * (a - a.Floor());

			var n1 = 259.1833 + 0.002078 * t2 - b;
			n1 = n1.ToRadians();

			var n2 = 2.0 * n1;

			var dp = (-17.2327 - 0.01737 * t) * n1.Sine();
			dp = dp + (-1.2729 - 0.00013 * t) * (l2).Sine() + 0.2088 * (n2).Sine();
			dp = dp - 0.2037 * (d2).Sine() + (0.1261 - 0.00031 * t) * (m1).Sine();
			dp = dp + 0.0675 * (m2).Sine() - (0.0497 - 0.00012 * t) * (l2 + m1).Sine();
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
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public static double NutatObl(double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var dj = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear) - 2415020;
			var t = dj / 36525;
			var t2 = t * t;

			var a = 100.0021358 * t;
			var b = 360 * (a - a.Floor());

			var l1 = 279.6967 + 0.000303 * t2 + b;
			var l2 = 2 * l1.ToRadians();

			a = 1336.855231 * t;
			b = 360 * (a - a.Floor());

			var d1 = 270.4342 - 0.001133 * t2 + b;
			var d2 = 2 * d1.ToRadians();

			a = 99.99736056 * t;
			b = 360 * (a - a.Floor());

			var m1 = (358.4758 - 0.00015 * t2 + b).ToRadians();

			a = 1325.552359 * t;
			b = 360 * (a - a.Floor());

			var m2 = (296.1046 + 0.009192 * t2 + b).ToRadians();

			a = 5.372616667 * t;
			b = 360 * (a - a.Floor());

			var n1 = (259.1833 + 0.002078 * t2 - b).ToRadians();

			var n2 = 2 * n1;

			var ddo = (9.21 + 0.00091 * t) * n1.Cosine();
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
		/// <param name="localHours"></param>
		/// <param name="localMinutes"></param>
		/// <param name="localSeconds"></param>
		/// <param name="longitude"></param>
		/// <returns></returns>
		public static double LocalSiderealTimeToGreenwichSiderealTime(double localHours, double localMinutes, double localSeconds, double longitude)
		{
			var a = HMStoDH(localHours, localMinutes, localSeconds);
			var b = longitude / 15;
			var c = a - b;

			return c - (24 * (c / 24).Floor());
		}

		/// <summary>
		/// Convert Greenwich Sidereal Time to Universal Time
		/// </summary>
		/// <remarks>
		/// Original macro name: GSTUT
		/// </remarks>
		/// <param name="greenwichSiderealHours"></param>
		/// <param name="greenwichSiderealMinutes"></param>
		/// <param name="greenwichSiderealSeconds"></param>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public static double GreenwichSiderealTimeToUniversalTime(double greenwichSiderealHours, double greenwichSiderealMinutes, double greenwichSiderealSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var a = CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
			var b = a - 2451545;
			var c = b / 36525;
			var d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
			var e = d - (24 * (d / 24).Floor());
			var f = HMStoDH(greenwichSiderealHours, greenwichSiderealMinutes, greenwichSiderealSeconds);
			var g = f - e;
			var h = g - (24 * (g / 24).Floor());

			return h * 0.9972695663;
		}

		/// <summary>
		/// Status of conversion of Greenwich Sidereal Time to Universal Time.
		/// </summary>
		/// <remarks>
		/// Original macro name: eGSTUT
		/// </remarks>
		/// <param name="gsh"></param>
		/// <param name="gsm"></param>
		/// <param name="gss"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static string EGstUt(double gsh, double gsm, double gss, double gd, int gm, int gy)
		{
			var a = CivilDateToJulianDate(gd, gm, gy);
			var b = a - 2451545;
			var c = b / 36525;
			var d = 6.697374558 + (2400.051336 * c) + (0.000025862 * c * c);
			var e = d - (24 * (d / 24).Floor());
			var f = HMStoDH(gsh, gsm, gss);
			var g = f - e;
			var h = g - (24 * (g / 24).Floor());

			return ((h * 0.9972695663) < (4.0 / 60.0)) ? "Warning" : "OK";
		}

		/// <summary>
		/// Calculate Sun's ecliptic longitude
		/// </summary>
		/// <remarks>
		/// Original macro name: SunLong
		/// </remarks>
		/// <param name="lch"></param>
		/// <param name="lcm"></param>
		/// <param name="lcs"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <returns></returns>
		public static double SunLong(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
		{
			var aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;
			var t = (dj / 36525) + (ut / 876600);
			var t2 = t * t;
			var a = 100.0021359 * t;
			var b = 360.0 * (a - a.Floor());

			var l = 279.69668 + 0.0003025 * t2 + b;
			a = 99.99736042 * t;
			b = 360 * (a - a.Floor());

			var m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
			var ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

			var am = m1.ToRadians();
			var at = TrueAnomaly(am, ec);

			a = 62.55209472 * t;
			b = 360 * (a - a.Floor());

			var a1 = (153.23 + b).ToRadians();
			a = 125.1041894 * t;
			b = 360 * (a - a.Floor());

			var b1 = (216.57 + b).ToRadians();
			a = 91.56766028 * t;
			b = 360.0 * (a - a.Floor());

			var c1 = (312.69 + b).ToRadians();
			a = 1236.853095 * t;
			b = 360.0 * (a - a.Floor());

			var d1 = (350.74 - 0.00144 * t2 + b).ToRadians();
			var e1 = (231.19 + 20.2 * t).ToRadians();
			a = 183.1353208 * t;
			b = 360.0 * (a - a.Floor());
			var h1 = (353.4 + b).ToRadians();

			var d2 = 0.00134 * a1.Cosine() + 0.00154 * b1.Cosine() + 0.002 * c1.Cosine();
			d2 = d2 + 0.00179 * d1.Sine() + 0.00178 * e1.Sine();
			var d3 = 0.00000543 * a1.Sine() + 0.00001575 * b1.Sine();
			d3 = d3 + 0.00001627 * c1.Sine() + 0.00003076 * d1.Cosine();

			var sr = at + (l - m1 + d2).ToRadians();
			var tp = 6.283185308;

			sr = sr - tp * (sr / tp).Floor();

			return Degrees(sr);
		}

		/// <summary>
		/// Solve Kepler's equation, and return value of the true anomaly in radians
		/// </summary>
		/// <remarks>
		/// Original macro name: TrueAnomaly
		/// </remarks>
		/// <param name="am"></param>
		/// <param name="ec"></param>
		/// <returns></returns>
		public static double TrueAnomaly(double am, double ec)
		{
			var tp = 6.283185308;
			var m = am - tp * (am / tp).Floor();
			var ae = m;

			while (1 == 1)
			{
				var d = ae - (ec * (ae).Sine()) - m;
				if (Math.Abs(d) < 0.000001)
				{
					break;
				}
				d = d / (1.0 - (ec * (ae).Cosine()));
				ae = ae - d;
			}
			var a = ((1 + ec) / (1 - ec)).SquareRoot() * (ae / 2).Tangent();
			var at = 2.0 * a.AngleTangent();

			return at;
		}

		/// <summary>
		/// Solve Kepler's equation, and return value of the eccentric anomaly in radians
		/// </summary>
		/// <remarks>
		/// Original macro name: EccentricAnomaly
		/// </remarks>
		/// <param name="am"></param>
		/// <param name="ec"></param>
		/// <returns></returns>
		public static double EccentricAnomaly(double am, double ec)
		{
			var tp = 6.283185308;
			var m = am - tp * (am / tp).Floor();
			var ae = m;

			while (1 == 1)
			{
				var d = ae - (ec * (ae).Sine()) - m;

				if (Math.Abs(d) < 0.000001)
				{
					break;
				}

				d = d / (1 - (ec * ae.Cosine()));
				ae = ae - d;
			}

			return ae;
		}

		/// <summary>
		/// Calculate effects of refraction
		/// </summary>
		/// <remarks>
		/// Original macro name: Refract
		/// </remarks>
		/// <param name="y2"></param>
		/// <param name="sw"></param>
		/// <param name="pr"></param>
		/// <param name="tr"></param>
		/// <returns></returns>
		public static double Refract(double y2, string sw, double pr, double tr)
		{
			var y = y2.ToRadians();

			var d = (sw.Substring(0, 1).ToLower() == "t") ? -1.0 : 1.0;

			if (d == -1)
			{
				var y3 = y;
				var y1 = y;
				var r1 = 0.0;

				while (1 == 1)
				{
					var yNew = y1 + r1;
					var rfNew = RefractL3035(pr, tr, yNew, d);

					if (y < -0.087)
						return 0;

					var r2 = rfNew;

					if ((r2 == 0) || (Math.Abs(r2 - r1) < 0.000001))
					{
						var qNew = y3;

						return Degrees(qNew + rfNew);
					}

					r1 = r2;
				}
			}

			var rf = RefractL3035(pr, tr, y, d);

			if (y < -0.087)
				return 0;

			var q = y;

			return Degrees(q + rf);
		}

		/// <summary>
		/// Helper function for Refract
		/// </summary>
		/// <param name="pr"></param>
		/// <param name="tr"></param>
		/// <param name="y"></param>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double RefractL3035(double pr, double tr, double y, double d)
		{
			if (y < 0.2617994)
			{
				if (y < -0.087)
					return 0;

				var yd = Degrees(y);
				var a = ((0.00002 * yd + 0.0196) * yd + 0.1594) * pr;
				var b = (273.0 + tr) * ((0.0845 * yd + 0.505) * yd + 1);

				return (-(a / b) * d).ToRadians();
			}

			return -d * 0.00007888888 * pr / ((273.0 + tr) * (y).Tangent());
		}

		/// <summary>
		/// Calculate corrected hour angle in decimal hours
		/// </summary>
		/// <remarks>
		/// Original macro name: ParallaxHA
		/// </remarks>
		/// <param name="hh"></param>
		/// <param name="hm"></param>
		/// <param name="hs"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="sw"></param>
		/// <param name="gp"></param>
		/// <param name="ht"></param>
		/// <param name="hp"></param>
		/// <returns></returns>
		public static double ParallaxHA(double hh, double hm, double hs, double dd, double dm, double ds, string sw, double gp, double ht, double hp)
		{
			var a = gp.ToRadians();
			var c1 = a.Cosine();
			var s1 = a.Sine();

			var u = (0.996647 * s1 / c1).AngleTangent();
			var c2 = u.Cosine();
			var s2 = u.Sine();
			var b = ht / 6378160;

			var rs = (0.996647 * s2) + (b * s1);

			var rc = c2 + (b * c1);
			var tp = 6.283185308;

			var rp = 1.0 / hp.ToRadians().Sine();

			var x = (DegreeHoursToDecimalDegrees(HMStoDH(hh, hm, hs))).ToRadians();
			var x1 = x;
			var y = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var y1 = y;

			var d = (sw.Substring(0, 1).ToLower().Equals("t")) ? 1.0 : -1.0;

			if (d == 1)
			{
				var result = ParallaxHAL2870(x, y, rc, rp, rs, tp);
				return DecimalDegreesToDegreeHours(Degrees(result.p));
			}

			var p1 = 0.0;
			var q1 = 0.0;
			var xLoop = x;
			var yLoop = y;

			while (1 == 1)
			{
				var result = ParallaxHAL2870(xLoop, yLoop, rc, rp, rs, tp);
				var p2 = result.p - xLoop;
				var q2 = result.q - yLoop;

				var aa = Math.Abs(p2 - p1);
				var bb = Math.Abs(q2 - q1);

				if ((aa < 0.000001) && (bb < 0.000001))
				{
					var p = x1 - p2;

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
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="rc"></param>
		/// <param name="rp"></param>
		/// <param name="rs"></param>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static (double p, double q) ParallaxHAL2870(double x, double y, double rc, double rp, double rs, double tp)
		{
			var cx = x.Cosine();
			var sy = y.Sine();
			var cy = y.Cosine();

			var aa = (rc * x.Sine()) / ((rp * cy) - (rc * cx));

			var dx = aa.AngleTangent();
			var p = x + dx;
			var cp = p.Cosine();

			p = p - tp * (p / tp).Floor();
			var q = (cp * (rp * sy - rs) / (rp * cy * cx - rc)).AngleTangent();

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
		/// <param name="hh"></param>
		/// <param name="hm"></param>
		/// <param name="hs"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="sw"></param>
		/// <param name="gp"></param>
		/// <param name="ht"></param>
		/// <param name="hp"></param>
		/// <returns></returns>
		public static double ParallaxDec(double hh, double hm, double hs, double dd, double dm, double ds, string sw, double gp, double ht, double hp)
		{
			var a = gp.ToRadians();
			var c1 = a.Cosine();
			var s1 = a.Sine();

			var u = (0.996647 * s1 / c1).AngleTangent();

			var c2 = u.Cosine();
			var s2 = u.Sine();
			var b = ht / 6378160;
			var rs = (0.996647 * s2) + (b * s1);

			var rc = c2 + (b * c1);
			var tp = 6.283185308;

			var rp = 1.0 / hp.ToRadians().Sine();

			var x = (DegreeHoursToDecimalDegrees(HMStoDH(hh, hm, hs))).ToRadians();
			var x1 = x;

			var y = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var y1 = y;

			var d = (sw.Substring(0, 1).ToLower().Equals("t")) ? 1.0 : -1.0;

			if (d == 1)
			{
				var result = ParallaxDecL2870(x, y, rc, rp, rs, tp);

				return Degrees(result.q);
			}

			var p1 = 0.0;
			var q1 = 0.0;

			var xLoop = x;
			var yLoop = y;

			while (1 == 1)
			{
				var result = ParallaxDecL2870(xLoop, yLoop, rc, rp, rs, tp);
				var p2 = result.p - xLoop;
				var q2 = result.q - yLoop;
				var aa = Math.Abs(p2 - p1);

				if ((aa < 0.000001) && (b < 0.000001))
				{
					var q = y1 - q2;

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
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="rc"></param>
		/// <param name="rp"></param>
		/// <param name="rs"></param>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static (double p, double q) ParallaxDecL2870(double x, double y, double rc, double rp, double rs, double tp)
		{
			var cx = x.Cosine();
			var sy = y.Sine();
			var cy = y.Cosine();

			var aa = (rc * x.Sine()) / ((rp * cy) - (rc * cx));
			var dx = aa.AngleTangent();
			var p = x + dx;
			var cp = p.Cosine();

			p = p - tp * (p / tp).Floor();
			var q = (cp * (rp * sy - rs) / (rp * cy * cx - rc)).AngleTangent();

			return (p, q);
		}

		/// <summary>
		/// Calculate Sun's angular diameter in decimal degrees
		/// </summary>
		/// <remarks>
		/// Original macro name: SunDia
		/// </remarks>
		/// <param name="lch"></param>
		/// <param name="lcm"></param>
		/// <param name="lcs"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <returns></returns>
		public static double SunDia(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
		{
			var a = SunDist(lch, lcm, lcs, ds, zc, ld, lm, ly);

			return 0.533128 / a;
		}

		/// <summary>
		/// Calculate Sun's distance from the Earth in astronomical units
		/// </summary>
		/// <remarks>
		/// Original macro name: SunDist
		/// </remarks>
		/// <param name="lch"></param>
		/// <param name="lcm"></param>
		/// <param name="lcs"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <returns></returns>
		public static double SunDist(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
		{
			var aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;

			var t = (dj / 36525) + (ut / 876600);
			var t2 = t * t;

			var a = 100.0021359 * t;
			var b = 360 * (a - a.Floor());
			a = 99.99736042 * t;
			b = 360 * (a - a.Floor());
			var m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
			var ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

			var am = m1.ToRadians();
			var ae = EccentricAnomaly(am, ec);

			a = 62.55209472 * t;
			b = 360 * (a - a.Floor());
			var a1 = (153.23 + b).ToRadians();
			a = 125.1041894 * t;
			b = 360 * (a - a.Floor());
			var b1 = (216.57 + b).ToRadians();
			a = 91.56766028 * t;
			b = 360 * (a - a.Floor());
			var c1 = (312.69 + b).ToRadians();
			a = 1236.853095 * t;
			b = 360 * (a - a.Floor());
			var d1 = (350.74 - 0.00144 * t2 + b).ToRadians();
			var e1 = (231.19 + 20.2 * t).ToRadians();
			a = 183.1353208 * t;
			b = 360 * (a - a.Floor());
			var h1 = (353.4 + b).ToRadians();

			var d3 = (0.00000543 * a1.Sine() + 0.00001575 * b1.Sine()) + (0.00001627 * c1.Sine() + 0.00003076 * d1.Cosine()) + (0.00000927 * h1.Sine());

			return 1.0000002 * (1 - ec * ae.Cosine()) + d3;
		}

		/// <summary>
		/// Calculate geocentric ecliptic longitude for the Moon
		/// </summary>
		/// <remarks>
		/// Original macro name: MoonLong
		/// </remarks>
		/// <param name="lh"></param>
		/// <param name="lm"></param>
		/// <param name="ls"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="dy"></param>
		/// <param name="mn"></param>
		/// <param name="yr"></param>
		/// <returns></returns>
		public static double MoonLong(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
		{
			var ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
			var t2 = t * t;

			var m1 = 27.32158213;
			var m2 = 365.2596407;
			var m3 = 27.55455094;
			var m4 = 29.53058868;
			var m5 = 27.21222039;
			var m6 = 6798.363307;
			var q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
			m1 = q / m1;
			m2 = q / m2;
			m3 = q / m3;
			m4 = q / m4;
			m5 = q / m5;
			m6 = q / m6;
			m1 = 360 * (m1 - (m1).Floor());
			m2 = 360 * (m2 - (m2).Floor());
			m3 = 360 * (m3 - (m3).Floor());
			m4 = 360 * (m4 - (m4).Floor());
			m5 = 360 * (m5 - (m5).Floor());
			m6 = 360 * (m6 - (m6).Floor());

			var ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
			var ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
			var md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
			var me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
			var mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
			var na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
			var a = (51.2 + 20.2 * t).ToRadians();
			var s1 = a.Sine();
			var s2 = ((na).ToRadians()).Sine();
			var b = 346.56 + (132.87 - 0.0091731 * t) * t;
			var s3 = 0.003964 * ((b).ToRadians()).Sine();
			var c = (na + 275.05 - 2.3 * t).ToRadians();
			var s4 = c.Sine();
			ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
			ms = ms - 0.001778 * s1;
			md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
			mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
			me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
			var e = 1.0 - (0.002495 + 0.00000752 * t) * t;
			var e2 = e * e;
			ml = (ml).ToRadians();
			ms = ms.ToRadians();
			// var _na = na.ToRadians();
			me1 = me1.ToRadians();
			mf = mf.ToRadians();
			md = md.ToRadians();

			var l = 6.28875 * (md).Sine() + 1.274018 * (2.0 * me1 - md).Sine();
			l = l + 0.658309 * (2.0 * me1).Sine() + 0.213616 * (2.0 * md).Sine();
			l = l - e * 0.185596 * (ms).Sine() - 0.114336 * (2.0 * mf).Sine();
			l = l + 0.058793 * (2.0 * (me1 - md)).Sine();
			l = l + 0.057212 * e * (2.0 * me1 - ms - md).Sine() + 0.05332 * (2.0 * me1 + md).Sine();
			l = l + 0.045874 * e * (2.0 * me1 - ms).Sine() + 0.041024 * e * (md - ms).Sine();
			l = l - 0.034718 * (me1).Sine() - e * 0.030465 * (ms + md).Sine();
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
			l = l + e * 0.000761 * (4.0 * me1 - ms - 2.0 * md).Sine();
			l = l + e2 * 0.000704 * (md - 2.0 * (ms + me1)).Sine();
			l = l + e * 0.000693 * (ms - 2.0 * (md - me1)).Sine();
			l = l + e * 0.000598 * (2.0 * (me1 - mf) - ms).Sine();
			l = l + 0.00055 * (md + 4.0 * me1).Sine() + 0.000538 * (4.0 * md).Sine();
			l = l + e * 0.000521 * (4.0 * me1 - ms).Sine() + 0.000486 * (2.0 * md - me1).Sine();
			l = l + e2 * 0.000717 * (md - 2.0 * ms).Sine();
			var mm = Unwind(ml + l.ToRadians());

			return Degrees(mm);
		}

		/// <summary>
		/// Calculate geocentric ecliptic latitude for the Moon
		/// </summary>
		/// <remarks>
		/// Original macro name: MoonLat
		/// </remarks>
		/// <param name="lh"></param>
		/// <param name="lm"></param>
		/// <param name="ls"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="dy"></param>
		/// <param name="mn"></param>
		/// <param name="yr"></param>
		/// <returns></returns>
		public static double MoonLat(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
		{
			var ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
			var t2 = t * t;

			var m1 = 27.32158213;
			var m2 = 365.2596407;
			var m3 = 27.55455094;
			var m4 = 29.53058868;
			var m5 = 27.21222039;
			var m6 = 6798.363307;
			var q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
			m1 = q / m1;
			m2 = q / m2;
			m3 = q / m3;
			m4 = q / m4;
			m5 = q / m5;
			m6 = q / m6;
			m1 = 360 * (m1 - (m1).Floor());
			m2 = 360 * (m2 - (m2).Floor());
			m3 = 360 * (m3 - (m3).Floor());
			m4 = 360 * (m4 - (m4).Floor());
			m5 = 360 * (m5 - (m5).Floor());
			m6 = 360 * (m6 - (m6).Floor());

			var ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
			var ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
			var md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
			var me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
			var mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
			var na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
			var a = (51.2 + 20.2 * t).ToRadians();
			var s1 = (a).Sine();
			var s2 = na.ToRadians().Sine();
			var b = 346.56 + (132.87 - 0.0091731 * t) * t;
			var s3 = 0.003964 * b.ToRadians().Sine();
			var c = (na + 275.05 - 2.3 * t).ToRadians();
			var s4 = (c).Sine();
			ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
			ms = ms - 0.001778 * s1;
			md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
			mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
			me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
			var e = 1.0 - (0.002495 + 0.00000752 * t) * t;
			var e2 = e * e;
			var _ml = (ml).ToRadians();
			ms = (ms).ToRadians();
			na = (na).ToRadians();
			me1 = (me1).ToRadians();
			mf = (mf).ToRadians();
			md = (md).ToRadians();

			var g = 5.128189 * (mf).Sine() + 0.280606 * (md + mf).Sine();
			g = g + 0.277693 * (md - mf).Sine() + 0.173238 * (2.0 * me1 - mf).Sine();
			g = g + 0.055413 * (2.0 * me1 + mf - md).Sine() + 0.046272 * (2.0 * me1 - mf - md).Sine();
			g = g + 0.032573 * (2.0 * me1 + mf).Sine() + 0.017198 * (2.0 * md + mf).Sine();
			g = g + 0.009267 * (2.0 * me1 + md - mf).Sine() + 0.008823 * (2.0 * md - mf).Sine();
			g = g + e * 0.008247 * (2.0 * me1 - ms - mf).Sine() + 0.004323 * (2.0 * (me1 - md) - mf).Sine();
			g = g + 0.0042 * (2.0 * me1 + mf + md).Sine() + e * 0.003372 * (mf - ms - 2.0 * me1).Sine();
			g = g + e * 0.002472 * (2.0 * me1 + mf - ms - md).Sine();
			g = g + e * 0.002222 * (2.0 * me1 + mf - ms).Sine();
			g = g + e * 0.002072 * (2.0 * me1 - mf - ms - md).Sine();
			g = g + e * 0.001877 * (mf - ms + md).Sine() + 0.001828 * (4.0 * me1 - mf - md).Sine();
			g = g - e * 0.001803 * (mf + ms).Sine() - 0.00175 * (3.0 * mf).Sine();
			g = g + e * 0.00157 * (md - ms - mf).Sine() - 0.001487 * (mf + me1).Sine();
			g = g - e * 0.001481 * (mf + ms + md).Sine() + e * 0.001417 * (mf - ms - md).Sine();
			g = g + e * 0.00135 * (mf - ms).Sine() + 0.00133 * (mf - me1).Sine();
			g = g + 0.001106 * (mf + 3.0 * md).Sine() + 0.00102 * (4.0 * me1 - mf).Sine();
			g = g + 0.000833 * (mf + 4.0 * me1 - md).Sine() + 0.000781 * (md - 3.0 * mf).Sine();
			g = g + 0.00067 * (mf + 4.0 * me1 - 2.0 * md).Sine() + 0.000606 * (2.0 * me1 - 3.0 * mf).Sine();
			g = g + 0.000597 * (2.0 * (me1 + md) - mf).Sine();
			g = g + e * 0.000492 * (2.0 * me1 + md - ms - mf).Sine() + 0.00045 * (2.0 * (md - me1) - mf).Sine();
			g = g + 0.000439 * (3.0 * md - mf).Sine() + 0.000423 * (mf + 2.0 * (me1 + md)).Sine();
			g = g + 0.000422 * (2.0 * me1 - mf - 3.0 * md).Sine() - e * 0.000367 * (ms + mf + 2.0 * me1 - md).Sine();
			g = g - e * 0.000353 * (ms + mf + 2.0 * me1).Sine() + 0.000331 * (mf + 4.0 * me1).Sine();
			g = g + e * 0.000317 * (2.0 * me1 + mf - ms + md).Sine();
			g = g + e2 * 0.000306 * (2.0 * (me1 - ms) - mf).Sine() - 0.000283 * (md + 3.0 * mf).Sine();
			var w1 = 0.0004664 * (na).Cosine();
			var w2 = 0.0000754 * (c).Cosine();
			var bm = (g).ToRadians() * (1.0 - w1 - w2);

			return Degrees(bm);
		}

		/// <summary>
		/// Calculate horizontal parallax for the Moon
		/// </summary>
		/// <remarks>
		/// Original macro name: MoonHP
		/// </remarks>
		/// <param name="lh"></param>
		/// <param name="lm"></param>
		/// <param name="ls"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="dy"></param>
		/// <param name="mn"></param>
		/// <param name="yr"></param>
		/// <returns></returns>
		public static double MoonHP(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr)
		{
			var ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525) + (ut / 876600);
			var t2 = t * t;

			var m1 = 27.32158213;
			var m2 = 365.2596407;
			var m3 = 27.55455094;
			var m4 = 29.53058868;
			var m5 = 27.21222039;
			var m6 = 6798.363307;
			var q = CivilDateToJulianDate(gd, gm, gy) - 2415020 + (ut / 24);
			m1 = q / m1;
			m2 = q / m2;
			m3 = q / m3;
			m4 = q / m4;
			m5 = q / m5;
			m6 = q / m6;
			m1 = 360 * (m1 - (m1).Floor());
			m2 = 360 * (m2 - (m2).Floor());
			m3 = 360 * (m3 - (m3).Floor());
			m4 = 360 * (m4 - (m4).Floor());
			m5 = 360 * (m5 - (m5).Floor());
			m6 = 360 * (m6 - (m6).Floor());

			var ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
			var ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
			var md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
			var me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
			var mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
			var na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
			var a = (51.2 + 20.2 * t).ToRadians();
			var s1 = a.Sine();
			var s2 = na.ToRadians().Sine();
			var b = 346.56 + (132.87 - 0.0091731 * t) * t;
			var s3 = 0.003964 * b.ToRadians().Sine();
			var c = (na + 275.05 - 2.3 * t).ToRadians();
			var s4 = c.Sine();
			ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
			ms = ms - 0.001778 * s1;
			md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
			mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
			me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
			var e = 1.0 - (0.002495 + 0.00000752 * t) * t;
			var e2 = e * e;
			ms = (ms).ToRadians();
			me1 = (me1).ToRadians();
			mf = (mf).ToRadians();
			md = (md).ToRadians();

			var pm = 0.950724 + 0.051818 * (md).Cosine() + 0.009531 * (2.0 * me1 - md).Cosine();
			pm = pm + 0.007843 * (2.0 * me1).Cosine() + 0.002824 * (2.0 * md).Cosine();
			pm = pm + 0.000857 * (2.0 * me1 + md).Cosine() + e * 0.000533 * (2.0 * me1 - ms).Cosine();
			pm = pm + e * 0.000401 * (2.0 * me1 - md - ms).Cosine();
			pm = pm + e * 0.00032 * (md - ms).Cosine() - 0.000271 * (me1).Cosine();
			pm = pm - e * 0.000264 * (ms + md).Cosine() - 0.000198 * (2.0 * mf - md).Cosine();
			pm = pm + 0.000173 * (3.0 * md).Cosine() + 0.000167 * (4.0 * me1 - md).Cosine();
			pm = pm - e * 0.000111 * (ms).Cosine() + 0.000103 * (4.0 * me1 - 2.0 * md).Cosine();
			pm = pm - 0.000084 * (2.0 * md - 2.0 * me1).Cosine() - e * 0.000083 * (2.0 * me1 + ms).Cosine();
			pm = pm + 0.000079 * (2.0 * me1 + 2.0 * md).Cosine() + 0.000072 * (4.0 * me1).Cosine();
			pm = pm + e * 0.000064 * (2.0 * me1 - ms + md).Cosine() - e * 0.000063 * (2.0 * me1 + ms - md).Cosine();
			pm = pm + e * 0.000041 * (ms + me1).Cosine() + e * 0.000035 * (2.0 * md - ms).Cosine();
			pm = pm - 0.000033 * (3.0 * md - 2.0 * me1).Cosine() - 0.00003 * (md + me1).Cosine();
			pm = pm - 0.000029 * (2.0 * (mf - me1)).Cosine() - e * 0.000029 * (2.0 * md + ms).Cosine();
			pm = pm + e2 * 0.000026 * (2.0 * (me1 - ms)).Cosine() - 0.000023 * (2.0 * (mf - me1) + md).Cosine();
			pm = pm + e * 0.000019 * (4.0 * me1 - ms - md).Cosine();

			return pm;
		}

		/// <summary>
		/// Convert angle in radians to equivalent angle in degrees.
		/// </summary>
		/// <remarks>
		/// Original macro name: Unwind
		/// </remarks>
		/// <param name="w"></param>
		/// <returns></returns>
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
		/// <param name="w"></param>
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
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double SunELong(double gd, int gm, int gy)
		{
			var t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
			var t2 = t * t;
			var x = 279.6966778 + 36000.76892 * t + 0.0003025 * t2;

			return x - 360 * (x / 360).Floor();
		}

		/// <summary>
		/// Longitude of the Sun at perigee
		/// </summary>
		/// <remarks>
		/// Original macro name: SunPeri
		/// </remarks>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double SunPeri(double gd, int gm, int gy)
		{
			var t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
			var t2 = t * t;
			var x = 281.2208444 + 1.719175 * t + 0.000452778 * t2;

			return x - 360 * (x / 360).Floor();
		}

		/// <summary>
		/// Eccentricity of the Sun-Earth orbit
		/// </summary>
		/// <remarks>
		/// Original macro name: SunEcc
		/// </remarks>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double SunEcc(double gd, int gm, int gy)
		{
			var t = (CivilDateToJulianDate(gd, gm, gy) - 2415020) / 36525;
			var t2 = t * t;

			return 0.01675104 - 0.0000418 * t - 0.000000126 * t2;
		}

		/// <summary>
		/// Ecliptic - Declination (degrees)
		/// </summary>
		/// <remarks>
		/// Original macro name: ECDec
		/// </remarks>
		/// <param name="eld"></param>
		/// <param name="elm"></param>
		/// <param name="els"></param>
		/// <param name="bd"></param>
		/// <param name="bm"></param>
		/// <param name="bs"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double EcDec(double eld, double elm, double els, double bd, double bm, double bs, double gd, int gm, int gy)
		{
			var a = (DegreesMinutesSecondsToDecimalDegrees(eld, elm, els)).ToRadians();
			var b = (DegreesMinutesSecondsToDecimalDegrees(bd, bm, bs)).ToRadians();
			var c = (Obliq(gd, gm, gy)).ToRadians();
			var d = b.Sine() * c.Cosine() + b.Cosine() * c.Sine() * a.Sine();

			return Degrees(d.ASine());
		}

		/// <summary>
		/// Ecliptic - Right Ascension (degrees)
		/// </summary>
		/// <remarks>
		/// Original macro name: ECRA
		/// </remarks>
		/// <param name="eld"></param>
		/// <param name="elm"></param>
		/// <param name="els"></param>
		/// <param name="bd"></param>
		/// <param name="bm"></param>
		/// <param name="bs"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <returns></returns>
		public static double EcRA(double eld, double elm, double els, double bd, double bm, double bs, double gd, int gm, int gy)
		{
			var a = (DegreesMinutesSecondsToDecimalDegrees(eld, elm, els)).ToRadians();
			var b = (DegreesMinutesSecondsToDecimalDegrees(bd, bm, bs)).ToRadians();
			var c = (Obliq(gd, gm, gy)).ToRadians();
			var d = a.Sine() * c.Cosine() - b.Tangent() * c.Sine();
			var e = a.Cosine();
			var f = Degrees(d.AngleTangent2(e));

			return f - 360 * (f / 360).Floor();
		}

		/// <summary>
		/// Calculate Sun's true anomaly, i.e., how much its orbit deviates from a true circle to an ellipse.
		/// </summary>
		/// <remarks>
		/// Original macro name: SunTrueAnomaly
		/// </remarks>
		/// <param name="lch"></param>
		/// <param name="lcm"></param>
		/// <param name="lcs"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <returns></returns>
		public static double SunTrueAnomaly(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
		{
			var aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;

			var t = (dj / 36525) + (ut / 876600);
			var t2 = t * t;

			var a = 99.99736042 * t;
			var b = 360 * (a - a.Floor());

			var m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
			var ec = 0.01675104 - 0.0000418 * t - 0.000000126 * t2;

			var am = m1.ToRadians();

			return Degrees(TrueAnomaly(am, ec));
		}

		/// <summary>
		/// Calculate the Sun's mean anomaly.
		/// </summary>
		/// <remarks>
		/// Original macro name: SunMeanAnomaly
		/// </remarks>
		/// <param name="lch"></param>
		/// <param name="lcm"></param>
		/// <param name="lcs"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <returns></returns>
		public static double SunMeanAnomaly(double lch, double lcm, double lcs, int ds, int zc, double ld, int lm, int ly)
		{
			var aa = LocalCivilTimeGreenwichDay(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var bb = LocalCivilTimeGreenwichMonth(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var cc = LocalCivilTimeGreenwichYear(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var ut = LocalCivilTimeToUniversalTime(lch, lcm, lcs, ds, zc, ld, lm, ly);
			var dj = CivilDateToJulianDate(aa, bb, cc) - 2415020;
			var t = (dj / 36525) + (ut / 876600);
			var t2 = t * t;
			var a = 100.0021359 * t;
			var b = 360 * (a - a.Floor());
			var m1 = 358.47583 - (0.00015 + 0.0000033 * t) * t2 + b;
			var am = Unwind((m1).ToRadians());

			return am;
		}

		/// <summary>
		/// Calculate local civil time of sunrise.
		/// </summary>
		/// <remarks>
		/// Original macro name: SunriseLCT
		/// </remarks>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="gl"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static double SunriseLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
		{
			var di = 0.8333333;
			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = SunriseLCTL3710(gd, gm, gy, sr, di, gp);

			double xx;
			if (!result1.s.Equals("OK"))
			{
				xx = -99.0;
			}
			else
			{
				var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
				var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

				if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
				{
					xx = -99.0;
				}
				else
				{
					sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
					var result2 = SunriseLCTL3710(gd, gm, gy, sr, di, gp);

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
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) SunriseLCTL3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0.0, 0.0, y, 0.0, 0.0, di, gp);

			return (a, x, y, la, s);
		}

		/// Calculate local civil time of sunset.
		///
		/// Original macro name: SunsetLCT
		public static double SunsetLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
		{
			var di = 0.8333333;
			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = SunsetLCTL3710(gd, gm, gy, sr, di, gp);

			double xx;
			if (!result1.s.Equals("OK"))
			{
				xx = -99.0;
			}
			else
			{
				var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
				var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

				if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
				{
					xx = -99.0;
				}
				else
				{
					sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
					var result2 = SunsetLCTL3710(gd, gm, gy, sr, di, gp);

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
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) SunsetLCTL3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0.0, 0.0, 0.0, 0.0, 0.0, gd, gm, gy);
			var y = EcDec(a, 0.0, 0.0, 0.0, 0.0, 0.0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

			return (a, x, y, la, s);
		}

		/// <summary>
		/// Local sidereal time of rise, in hours.
		/// </summary>
		/// <remarks>
		/// Original macro name: RSLSTR
		/// </remarks>
		/// <param name="rah"></param>
		/// <param name="ram"></param>
		/// <param name="ras"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="vd"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public static double RiseSetLocalSiderealTimeRise(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
		{
			var a = HMStoDH(rah, ram, ras);
			var b = (DegreeHoursToDecimalDegrees(a)).ToRadians();
			var c = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var d = (vd).ToRadians();
			var e = (g).ToRadians();
			var f = -((d).Sine() + (e).Sine() * (c).Sine()) / ((e).Cosine() * (c).Cosine());
			var h = (Math.Abs(f) < 1) ? f.ACosine() : 0;
			var i = DecimalDegreesToDegreeHours(Degrees(b - h));

			return i - 24 * (i / 24).Floor();
		}

		/// <summary>
		/// Local sidereal time of setting, in hours.
		/// </summary>
		/// <remarks>
		/// Original macro name: RSLSTS
		/// </remarks>
		/// <param name="rah"></param>
		/// <param name="ram"></param>
		/// <param name="ras"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="vd"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public static double RiseSetLocalSiderealTimeSet(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
		{
			var a = HMStoDH(rah, ram, ras);
			var b = (DegreeHoursToDecimalDegrees(a)).ToRadians();
			var c = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var d = vd.ToRadians();
			var e = g.ToRadians();
			var f = -(d.Sine() + e.Sine() * c.Sine()) / (e.Cosine() * c.Cosine());
			var h = (Math.Abs(f) < 1) ? f.ACosine() : 0;
			var i = DecimalDegreesToDegreeHours(Degrees(b + h));

			return i - 24 * (i / 24).Floor();
		}

		/// <summary>
		/// Azimuth of rising, in degrees.
		/// </summary>
		/// <remarks>
		/// Original macro name: RSAZR
		/// </remarks>
		/// <param name="rah"></param>
		/// <param name="ram"></param>
		/// <param name="ras"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="vd"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public static double RiseSetAzimuthRise(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
		{
			var a = HMStoDH(rah, ram, ras);
			var c = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var d = vd.ToRadians();
			var e = g.ToRadians();
			var f = (c.Sine() + d.Sine() * e.Sine()) / (d.Cosine() * e.Cosine());
			var h = (ERS(rah, ram, ras, dd, dm, ds, vd, g).Equals("OK")) ? f.ACosine() : 0;
			var i = Degrees(h);

			return i - 360 * (i / 360).Floor();
		}

		/// <summary>
		/// Azimuth of setting, in degrees.
		/// </summary>
		/// <remarks>
		/// Original macro name: RSAZS
		/// </remarks>
		/// <param name="rah"></param>
		/// <param name="ram"></param>
		/// <param name="ras"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="vd"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public static double RiseSetAzimuthSet(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
		{
			var a = HMStoDH(rah, ram, ras);
			var _b = (DegreeHoursToDecimalDegrees(a)).ToRadians();
			var c = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var d = vd.ToRadians();
			var e = g.ToRadians();
			var f = (c.Sine() + d.Sine() * e.Sine()) / (d.Cosine() * e.Cosine());
			var h = (ERS(rah, ram, ras, dd, dm, ds, vd, g).Equals("OK")) ? f.ACosine() : 0;
			var i = 360 - Degrees(h);

			return i - 360 * (i / 360).Floor();
		}

		/// <summary>
		/// Rise/Set status
		/// </summary>
		/// <remarks>
		/// <para>Possible values: "OK", "** never rises", "** circumpolar"</para>
		/// <para>Original macro name: eRS</para>
		/// </remarks>
		/// <param name="rah"></param>
		/// <param name="ram"></param>
		/// <param name="ras"></param>
		/// <param name="dd"></param>
		/// <param name="dm"></param>
		/// <param name="ds"></param>
		/// <param name="vd"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public static string ERS(double rah, double ram, double ras, double dd, double dm, double ds, double vd, double g)
		{
			var a = HMStoDH(rah, ram, ras);
			var c = (DegreesMinutesSecondsToDecimalDegrees(dd, dm, ds)).ToRadians();
			var d = vd.ToRadians();
			var e = g.ToRadians();
			var f = -(d.Sine() + e.Sine() * c.Sine()) / (e.Cosine() * c.Cosine());

			var returnValue = "OK";
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
			var di = 0.8333333;
			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = ESunRS_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
			{
				return result1.s;
			}
			else
			{
				var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
				var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
				sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
				var result2 = ESunRS_L3710(gd, gm, gy, sr, di, gp);
				if (!result2.s.Equals("OK"))
				{
					return result2.s;
				}
				else
				{
					x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
					// var _ut = gst_ut(x, 0.0, 0.0, gd, gm, gy);

					if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
					{
						var s = result2.s + " GST to UT conversion warning";

						return s;
					}

					return result2.s;
				}
			}
		}

		/// <summary>
		/// Helper function for e_sun_rs()
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) ESunRS_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

			return (a, x, y, la, s);
		}

		/// Calculate azimuth of sunrise.
		///
		/// Original macro name: SunriseAz
		public static double SunriseAZ(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
		{
			var di = 0.8333333;
			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = SunriseAZ_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
			{
				return -99.0;
			}

			var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
			{
				return -99.0;
			}

			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);
			// var(_a, x, y, _la, s) = sunrise_az_l3710(gd, gm, gy, sr, di, gp);
			var result2 = SunriseAZ_L3710(gd, gm, gy, sr, di, gp);

			if (!result2.s.Equals("OK"))
			{
				return -99.0;
			}

			return RiseSetAzimuthRise(DecimalDegreesToDegreeHours(x), 0, 0, result2.y, 0.0, 0.0, di, gp);
		}

		/// <summary>
		/// Helper function for sunrise_az()
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) SunriseAZ_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

			return (a, x, y, la, s);
		}

		/// <summary>
		/// Calculate azimuth of sunset.
		/// </summary>
		/// <remarks>
		/// Original macro name: SunsetAz
		/// </remarks>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="gl"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static double SunsetAZ(double ld, int lm, int ly, int ds, int zc, double gl, double gp)
		{
			var di = 0.8333333;
			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = SunsetAZ_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
			{
				return -99.0;
			}

			var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
			{
				return -99.0;
			}

			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

			var result2 = SunsetAZ_L3710(gd, gm, gy, sr, di, gp);

			if (!result2.s.Equals("OK"))
			{
				return -99.0;
			}
			return RiseSetAzimuthSet(DecimalDegreesToDegreeHours(x), 0, 0, result2.y, 0, 0, di, gp);
		}

		/// <summary>
		/// Helper function for sunset_az()
		/// </summary>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) SunsetAZ_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

			return (a, x, y, la, s);
		}

		/// <summary>
		/// Calculate morning twilight start, in local time.
		/// </summary>
		/// <remarks>
		/// <para>Twilight type (TT) can be one of "C" (civil), "N" (nautical), or "A" (astronomical)</para>
		/// <para>Original macro name: TwilightAMLCT</para>
		/// </remarks>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="gl"></param>
		/// <param name="gp"></param>
		/// <param name="tt"></param>
		/// <returns></returns>
		public static double TwilightAMLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp, PATwilightType tt)
		{
			var di = (double)tt;

			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = TwilightAMLCT_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
				return -99.0;

			var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
				return -99.0;

			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

			var result2 = TwilightAMLCT_L3710(gd, gm, gy, sr, di, gp);

			if (!result2.s.Equals("OK"))
				return -99.0;

			x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			var xx = UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);

			return xx;
		}

		/// <summary>
		/// Helper function for twilight_am_lct()
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) TwilightAMLCT_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

			return (a, x, y, la, s);
		}

		/// <summary>
		/// Calculate evening twilight end, in local time.
		/// </summary>
		/// <remarks>
		/// <para>Twilight type can be one of "C" (civil), "N" (nautical), or "A" (astronomical)</para>
		/// <para>Original macro name: TwilightPMLCT</para>
		/// </remarks>
		/// <param name="ld"></param>
		/// <param name="lm"></param>
		/// <param name="ly"></param>
		/// <param name="ds"></param>
		/// <param name="zc"></param>
		/// <param name="gl"></param>
		/// <param name="gp"></param>
		/// <param name="tt"></param>
		/// <returns></returns>
		public static double TwilightPMLCT(double ld, int lm, int ly, int ds, int zc, double gl, double gp, PATwilightType tt)
		{
			var di = (double)tt;

			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = TwilightPMLCT_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
				return 0.0;

			var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
				return 0.0;

			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

			var result2 = TwilightPMLCT_L3710(gd, gm, gy, sr, di, gp);

			if (!result2.s.Equals("OK"))
				return 0.0;

			x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);
			ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);

			return UniversalTimeToLocalCivilTime(ut, 0, 0, ds, zc, gd, gm, gy);
		}

		/// <summary>
		/// Helper function for twilight_pm_lct()
		/// </summary>
		/// <param name="a"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="la"></param>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) TwilightPMLCT_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeSet(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

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
			var di = (double)tt;

			var gd = LocalCivilTimeGreenwichDay(12, 0, 0, ds, zc, ld, lm, ly);
			var gm = LocalCivilTimeGreenwichMonth(12, 0, 0, ds, zc, ld, lm, ly);
			var gy = LocalCivilTimeGreenwichYear(12, 0, 0, ds, zc, ld, lm, ly);
			var sr = SunLong(12, 0, 0, ds, zc, ld, lm, ly);

			var result1 = ETwilight_L3710(gd, gm, gy, sr, di, gp);

			if (!result1.s.Equals("OK"))
			{
				return result1.s;
			}

			var x = LocalSiderealTimeToGreenwichSiderealTime(result1.la, 0, 0, gl);
			var ut = GreenwichSiderealTimeToUniversalTime(x, 0, 0, gd, gm, gy);
			sr = SunLong(ut, 0, 0, 0, 0, gd, gm, gy);

			var result2 = ETwilight_L3710(gd, gm, gy, sr, di, gp);

			if (!result2.s.Equals("OK"))
			{
				return result2.s;
			}

			x = LocalSiderealTimeToGreenwichSiderealTime(result2.la, 0, 0, gl);

			if (!EGstUt(x, 0, 0, gd, gm, gy).Equals("OK"))
			{
				result2.s = $"{result2.s} GST to UT conversion warning";

				return result2.s;
			}

			return result2.s;
		}

		/// <summary>
		/// Helper function for e_twilight()
		/// </summary>
		/// <param name="gd"></param>
		/// <param name="gm"></param>
		/// <param name="gy"></param>
		/// <param name="sr"></param>
		/// <param name="di"></param>
		/// <param name="gp"></param>
		/// <returns></returns>
		public static (double a, double x, double y, double la, string s) ETwilight_L3710(double gd, int gm, int gy, double sr, double di, double gp)
		{
			var a = sr + NutatLong(gd, gm, gy) - 0.005694;
			var x = EcRA(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var y = EcDec(a, 0, 0, 0, 0, 0, gd, gm, gy);
			var la = RiseSetLocalSiderealTimeRise(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);
			var s = ERS(DecimalDegreesToDegreeHours(x), 0, 0, y, 0, 0, di, gp);

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
		/// <param name="xx1"></param>
		/// <param name="xm1"></param>
		/// <param name="xs1"></param>
		/// <param name="dd1"></param>
		/// <param name="dm1"></param>
		/// <param name="ds1"></param>
		/// <param name="xx2"></param>
		/// <param name="xm2"></param>
		/// <param name="xs2"></param>
		/// <param name="dd2"></param>
		/// <param name="dm2"></param>
		/// <param name="ds2"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		public static double Angle(double xx1, double xm1, double xs1, double dd1, double dm1, double ds1, double xx2, double xm2, double xs2, double dd2, double dm2, double ds2, PAAngleMeasure s
		)
		{
			var a = (s.Equals(PAAngleMeasure.Hours)) ? DegreeHoursToDecimalDegrees(HMStoDH(xx1, xm1, xs1)) : DegreesMinutesSecondsToDecimalDegrees(xx1, xm1, xs1);
			var b = a.ToRadians();
			var c = DegreesMinutesSecondsToDecimalDegrees(dd1, dm1, ds1);
			var d = c.ToRadians();
			var e = (s.Equals(PAAngleMeasure.Hours)) ? DegreeHoursToDecimalDegrees(HMStoDH(xx2, xm2, xs2)) : DegreesMinutesSecondsToDecimalDegrees(xx2, xm2, xs2);
			var f = e.ToRadians();
			var g = DegreesMinutesSecondsToDecimalDegrees(dd2, dm2, ds2);
			var h = g.ToRadians();
			var i = (d.Sine() * h.Sine() + d.Cosine() * h.Cosine() * (b - f).Cosine()).ACosine();

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
			var a11 = 178.179078;
			var a12 = 415.2057519;
			var a13 = 0.0003011;
			var a14 = 0.0;
			var a21 = 75.899697;
			var a22 = 1.5554889;
			var a23 = 0.0002947;
			var a24 = 0.0;
			var a31 = 0.20561421;
			var a32 = 0.00002046;
			var a33 = -0.00000003;
			var a34 = 0.0;
			var a41 = 7.002881;
			var a42 = 0.0018608;
			var a43 = -0.0000183;
			var a44 = 0.0;
			var a51 = 47.145944;
			var a52 = 1.1852083;
			var a53 = 0.0001739;
			var a54 = 0.0;
			var a61 = 0.3870986;
			var a62 = 6.74;
			var a63 = -0.42;

			var b11 = 342.767053;
			var b12 = 162.5533664;
			var b13 = 0.0003097;
			var b14 = 0.0;
			var b21 = 130.163833;
			var b22 = 1.4080361;
			var b23 = -0.0009764;
			var b24 = 0.0;
			var b31 = 0.00682069;
			var b32 = -0.00004774;
			var b33 = 0.000000091;
			var b34 = 0.0;
			var b41 = 3.393631;
			var b42 = 0.0010058;
			var b43 = -0.000001;
			var b44 = 0.0;
			var b51 = 75.779647;
			var b52 = 0.89985;
			var b53 = 0.00041;
			var b54 = 0.0;
			var b61 = 0.7233316;
			var b62 = 16.92;
			var b63 = -4.4;

			var c11 = 293.737334;
			var c12 = 53.17137642;
			var c13 = 0.0003107;
			var c14 = 0.0;
			var c21 = 334.218203;
			var c22 = 1.8407584;
			var c23 = 0.0001299;
			var c24 = -0.00000119;
			var c31 = 0.0933129;
			var c32 = 0.000092064;
			var c33 = -0.000000077;
			var c34 = 0.0;
			var c41 = 1.850333;
			var c42 = -0.000675;
			var c43 = 0.0000126;
			var c44 = 0.0;
			var c51 = 48.786442;
			var c52 = 0.7709917;
			var c53 = -0.0000014;
			var c54 = -0.00000533;
			var c61 = 1.5236883;
			var c62 = 9.36;
			var c63 = -1.52;

			var d11 = 238.049257;
			var d12 = 8.434172183;
			var d13 = 0.0003347;
			var d14 = -0.00000165;
			var d21 = 12.720972;
			var d22 = 1.6099617;
			var d23 = 0.00105627;
			var d24 = -0.00000343;
			var d31 = 0.04833475;
			var d32 = 0.00016418;
			var d33 = -0.0000004676;
			var d34 = -0.0000000017;
			var d41 = 1.308736;
			var d42 = -0.0056961;
			var d43 = 0.0000039;
			var d44 = 0.0;
			var d51 = 99.443414;
			var d52 = 1.01053;
			var d53 = 0.00035222;
			var d54 = -0.00000851;
			var d61 = 5.202561;
			var d62 = 196.74;
			var d63 = -9.4;

			var e11 = 266.564377;
			var e12 = 3.398638567;
			var e13 = 0.0003245;
			var e14 = -0.0000058;
			var e21 = 91.098214;
			var e22 = 1.9584158;
			var e23 = 0.00082636;
			var e24 = 0.00000461;
			var e31 = 0.05589232;
			var e32 = -0.0003455;
			var e33 = -0.000000728;
			var e34 = 0.00000000074;
			var e41 = 2.492519;
			var e42 = -0.0039189;
			var e43 = -0.00001549;
			var e44 = 0.00000004;
			var e51 = 112.790414;
			var e52 = 0.8731951;
			var e53 = -0.00015218;
			var e54 = -0.00000531;
			var e61 = 9.554747;
			var e62 = 165.6;
			var e63 = -8.88;

			var f11 = 244.19747;
			var f12 = 1.194065406;
			var f13 = 0.000316;
			var f14 = -0.0000006;
			var f21 = 171.548692;
			var f22 = 1.4844328;
			var f23 = 0.0002372;
			var f24 = -0.00000061;
			var f31 = 0.0463444;
			var f32a = -0.00002658;
			var f33 = 0.000000077;
			var f34 = 0.0;
			var f41 = 0.772464;
			var f42 = 0.0006253;
			var f43 = 0.0000395;
			var f44 = 0.0;
			var f51 = 73.477111;
			var f52 = 0.4986678;
			var f53 = 0.0013117;
			var f54 = 0.0;
			var f61 = 19.21814;
			var f62 = 65.8;
			var f63 = -7.19;

			var g11 = 84.457994;
			var g12 = 0.6107942056;
			var g13 = 0.0003205;
			var g14 = -0.0000006;
			var g21 = 46.727364;
			var g22 = 1.4245744;
			var g23 = 0.00039082;
			var g24 = -0.000000605;
			var g31 = 0.00899704;
			var g32 = 0.00000633;
			var g33 = -0.000000002;
			var g34 = 0.0;
			var g41 = 1.779242;
			var g42 = -0.0095436;
			var g43 = -0.0000091;
			var g44 = 0.0;
			var g51 = 130.681389;
			var g52 = 1.098935;
			var g53 = 0.00024987;
			var g54 = -0.000004718;
			var g61 = 30.10957;
			var g62 = 62.2;
			var g63 = -6.87;

			var pl = new List<PlanetDataPrecise>();

			pl.Add(new PlanetDataPrecise() { Name = "", Value1 = 0, Value2 = 0, Value3 = 0, Value4 = 0, Value5 = 0, Value6 = 0, Value7 = 0, Value8 = 0, Value9 = 0 });

			var ip = 0;
			var b = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var a = CivilDateToJulianDate(gd, gm, gy);
			var t = ((a - 2415020.0) / 36525.0) + (b / 876600.0);

			var a0 = a11;
			var a1 = a12;
			var a2 = a13;
			var a3 = a14;
			var b0 = a21;
			var b1 = a22;
			var b2 = a23;
			var b3 = a24;
			var c0 = a31;
			var c1 = a32;
			var c2 = a33;
			var c3 = a34;
			var d0 = a41;
			var d1 = a42;
			var d2 = a43;
			var d3 = a44;
			var e0 = a51;
			var e1 = a52;
			var e2 = a53;
			var e3 = a54;
			var f = a61;
			var g = a62;
			var h = a63;
			var aa = a1 * t;
			b = 360.0 * (aa - aa.Floor());
			var c = a0 + b + (a3 * t + a2) * t * t;

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
			b = 360.0 * (aa - (aa).Floor());
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
			b = 360.0 * (aa - (aa).Floor());
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
			b = 360.0 * (aa - (aa).Floor());
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
			b = 360.0 * (aa - (aa).Floor());
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
			b = 360.0 * (aa - (aa).Floor());
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
			b = 360.0 * (aa - (aa).Floor());
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

			var checkPlanet = pl.Where(x => x.Name.ToLower() == s.ToLower()).Select(x => x).FirstOrDefault();
			if (checkPlanet == null)
				return (Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)), Degrees(Unwind(0)));

			var li = 0.0;
			var ms = SunMeanAnomaly(lh, lm, ls, ds, zc, dy, mn, yr);
			var sr = (SunLong(lh, lm, ls, ds, zc, dy, mn, yr)).ToRadians();
			var re = SunDist(lh, lm, ls, ds, zc, dy, mn, yr);
			var lg = sr + Math.PI;

			var l0 = 0.0;
			var s0 = 0.0;
			var p0 = 0.0;
			var vo = 0.0;
			var lp1 = 0.0;
			var ll = 0.0;
			var rd = 0.0;
			var pd = 0.0;
			var sp = 0.0;
			var ci = 0.0;

			for (int k = 1; k <= 3; k++)
			{
				foreach (var planet in pl)
					planet.APValue = (planet.Value1 - planet.Value3 - li * planet.Value2).ToRadians();

				var qa = 0.0;
				var qb = 0.0;
				var qc = 0.0;
				var qd = 0.0;
				var qe = 0.0;
				var qf = 0.0;
				var qg = 0.0;

				if (s == "Mercury")
					(qa, qb) = PlanetLong_L4685(pl);

				if (s == "Venus")
					(qa, qb, qc, qe) = PlanetLong_L4735(pl, ms, t);

				if (s == "Mars")
				{
					var returnValue = PlanetLong_L4810(pl, ms);

					qc = returnValue.qc;
					qe = returnValue.qe;
					qa = returnValue.qa;
					qb = returnValue.qb;
				}

				var matchPlanet = pl.Where(x => x.Name.ToLower() == s.ToLower()).Select(x => x).FirstOrDefault();

				if (new string[] { "Jupiter", "Saturn", "Uranus", "Neptune" }.Contains(s))
					(qa, qb, qc, qd, qe, qf, qg) = PlanetLong_L4945(t, matchPlanet);

				var ec = matchPlanet.Value4 + qd;
				var am = matchPlanet.APValue + qe;
				var at = TrueAnomaly(am, ec);
				var pvv = (matchPlanet.Value7 + qf) * (1.0 - ec * ec) / (1.0 + ec * (at).Cosine());
				var lp = Degrees(at) + matchPlanet.Value3 + Degrees(qc - qe);
				lp = lp.ToRadians();
				var om = matchPlanet.Value6.ToRadians();
				var lo = lp - om;
				var so = lo.Sine();
				var co = lo.Cosine();
				var inn = matchPlanet.Value5.ToRadians();
				pvv = pvv + qb;
				sp = so * inn.Sine();
				var y = so * inn.Cosine();
				var ps = sp.ASine() + qg;
				sp = ps.Sine();
				pd = y.AngleTangent2(co) + om + (qa).ToRadians();
				pd = Unwind(pd);
				ci = ps.Cosine();
				rd = pvv * ci;
				ll = pd - lg;
				var rh = re * re + pvv * pvv - 2.0 * re * pvv * ci * ll.Cosine();
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

			var l1 = ll.Sine();
			var l2 = ll.Cosine();

			var ep = (ip < 3) ? (-1.0 * rd * l1 / (re - rd * l2)).AngleTangent() + lg + Math.PI : (re * l1 / (rd - re * l2)).AngleTangent() + pd;
			ep = Unwind(ep);

			var bp = (rd * sp * (ep - pd).Sine() / (ci * re * l1)).AngleTangent();

			var planetLongitude = Degrees(Unwind(ep));
			var planetLatitude = Degrees(Unwind(bp));
			var planetDistanceAU = vo;
			var planetHLong1 = Degrees(lp1);
			var planetHLong2 = Degrees(l0);
			var planetHLat = Degrees(s0);
			var planetRVect = p0;

			return (planetLongitude, planetLatitude, planetDistanceAU, planetHLong1, planetHLong2, planetHLat, planetRVect);
		}

		/// <summary>
		/// Helper function for planet_long_lat()
		/// </summary>
		/// <param name="qa"></param>
		/// <param name="pl"></param>
		/// <returns></returns>
		public static (double qa, double qb) PlanetLong_L4685(List<PlanetDataPrecise> pl)
		{
			var qa = 0.00204 * (5.0 * pl[2].APValue - 2.0 * pl[1].APValue + 0.21328).Cosine();
			qa = qa + 0.00103 * (2.0 * pl[2].APValue - pl[1].APValue - 2.8046).Cosine();
			qa = qa + 0.00091 * (2.0 * pl[4].APValue - pl[1].APValue - 0.64582).Cosine();
			qa = qa + 0.00078 * (5.0 * pl[2].APValue - 3.0 * pl[1].APValue + 0.17692).Cosine();

			var qb = 0.000007525 * (2.0 * pl[4].APValue - pl[1].APValue + 0.925251).Cosine();
			qb = qb + 0.000006802 * (5.0 * pl[2].APValue - 3.0 * pl[1].APValue - 4.53642).Cosine();
			qb = qb + 0.000005457 * (2.0 * pl[2].APValue - 2.0 * pl[1].APValue - 1.24246).Cosine();
			qb = qb + 0.000003569 * (5.0 * pl[2].APValue - pl[1].APValue - 1.35699).Cosine();

			return (qa, qb);
		}

		/// <summary>
		/// Helper function for planet_long_lat()
		/// </summary>
		/// <param name="qa"></param>
		/// <param name="qb"></param>
		/// <param name="qc"></param>
		/// <param name="pl"></param>
		/// <param name="ms"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		public static (double qa, double qb, double qc, double qe) PlanetLong_L4735(List<PlanetDataPrecise> pl, double ms, double t)
		{
			var qc = 0.00077 * (4.1406 + t * 2.6227).Sine();
			qc = qc.ToRadians();
			var qe = qc;

			var qa = 0.00313 * (2.0 * ms - 2.0 * pl[2].APValue - 2.587).Cosine();
			qa = qa + 0.00198 * (3.0 * ms - 3.0 * pl[2].APValue + 0.044768).Cosine();
			qa = qa + 0.00136 * (ms - pl[2].APValue - 2.0788).Cosine();
			qa = qa + 0.00096 * (3.0 * ms - 2.0 * pl[2].APValue - 2.3721).Cosine();
			qa = qa + 0.00082 * (pl[4].APValue - pl[2].APValue - 3.6318).Cosine();

			var qb = 0.000022501 * (2.0 * ms - 2.0 * pl[2].APValue - 1.01592).Cosine();
			qb = qb + 0.000019045 * (3.0 * ms - 3.0 * pl[2].APValue + 1.61577).Cosine();
			qb = qb + 0.000006887 * (pl[4].APValue - pl[2].APValue - 2.06106).Cosine();
			qb = qb + 0.000005172 * (ms - pl[2].APValue - 0.508065).Cosine();
			qb = qb + 0.00000362 * (5.0 * ms - 4.0 * pl[2].APValue - 1.81877).Cosine();
			qb = qb + 0.000003283 * (4.0 * ms - 4.0 * pl[2].APValue + 1.10851).Cosine();
			qb = qb + 0.000003074 * (2.0 * pl[4].APValue - 2.0 * pl[2].APValue - 0.962846).Cosine();

			return (qa, qb, qc, qe);
		}

		/// <summary>
		/// Helper function for planet_long_lat()
		/// </summary>
		/// <param name="a"></param>
		/// <param name="sa"></param>
		/// <param name="ca"></param>
		/// <param name="qc"></param>
		/// <param name="qe"></param>
		/// <param name="qa"></param>
		/// <param name="pl"></param>
		/// <param name="ms"></param>
		/// <returns></returns>
		public static (double a, double sa, double ca, double qc, double qe, double qa, double qb) PlanetLong_L4810(List<PlanetDataPrecise> pl, double ms)
		{
			var a = 3.0 * pl[4].APValue - 8.0 * pl[3].APValue + 4.0 * ms;
			var sa = a.Sine();
			var ca = a.Cosine();
			var qc = -(0.01133 * sa + 0.00933 * ca);
			qc = qc.ToRadians();
			var qe = qc;

			var qa = 0.00705 * (pl[4].APValue - pl[3].APValue - 0.85448).Cosine();
			qa = qa + 0.00607 * (2.0 * pl[4].APValue - pl[3].APValue - 3.2873).Cosine();
			qa = qa + 0.00445 * (2.0 * pl[4].APValue - 2.0 * pl[3].APValue - 3.3492).Cosine();
			qa = qa + 0.00388 * (ms - 2.0 * pl[3].APValue + 0.35771).Cosine();
			qa = qa + 0.00238 * (ms - pl[3].APValue + 0.61256).Cosine();
			qa = qa + 0.00204 * (2.0 * ms - 3.0 * pl[3].APValue + 2.7688).Cosine();
			qa = qa + 0.00177 * (3.0 * pl[3].APValue - pl[2].APValue - 1.0053).Cosine();
			qa = qa + 0.00136 * (2.0 * ms - 4.0 * pl[3].APValue + 2.6894).Cosine();
			qa = qa + 0.00104 * (pl[4].APValue + 0.30749).Cosine();

			var qb = 0.000053227 * (pl[4].APValue - pl[3].APValue + 0.717864).Cosine();
			qb = qb + 0.000050989 * (2.0 * pl[4].APValue - 2.0 * pl[3].APValue - 1.77997).Cosine();
			qb = qb + 0.000038278 * (2.0 * pl[4].APValue - pl[3].APValue - 1.71617).Cosine();
			qb = qb + 0.000015996 * (ms - pl[3].APValue - 0.969618).Cosine();
			qb = qb + 0.000014764 * (2.0 * ms - 3.0 * pl[3].APValue + 1.19768).Cosine();
			qb = qb + 0.000008966 * (pl[4].APValue - 2.0 * pl[3].APValue + 0.761225).Cosine();
			qb = qb + 0.000007914 * (3.0 * pl[4].APValue - 2.0 * pl[3].APValue - 2.43887).Cosine();
			qb = qb + 0.000007004 * (2.0 * pl[4].APValue - 3.0 * pl[3].APValue - 1.79573).Cosine();
			qb = qb + 0.00000662 * (ms - 2.0 * pl[3].APValue + 1.97575).Cosine();
			qb = qb + 0.00000493 * (3.0 * pl[4].APValue - 3.0 * pl[3].APValue - 1.33069).Cosine();
			qb = qb + 0.000004693 * (3.0 * ms - 5.0 * pl[3].APValue + 3.32665).Cosine();
			qb = qb + 0.000004571 * (2.0 * ms - 4.0 * pl[3].APValue + 4.27086).Cosine();
			qb = qb + 0.000004409 * (3.0 * pl[4].APValue - pl[3].APValue - 2.02158).Cosine();

			return (a, sa, ca, qc, qe, qa, qb);
		}

		/// <summary>
		/// Helper function for planet_long_lat()
		/// </summary>
		/// <param name="qa"></param>
		/// <param name="qb"></param>
		/// <param name="qc"></param>
		/// <param name="qd"></param>
		/// <param name="qe"></param>
		/// <param name="qf"></param>
		/// <param name="t"></param>
		/// <param name="planet"></param>
		/// <returns></returns>
		public static (double qa, double qb, double qc, double qd, double qe, double qf, double qg) PlanetLong_L4945(double t, PlanetDataPrecise planet)
		{
			var qa = 0.0;
			var qb = 0.0;
			var qc = 0.0;
			var qd = 0.0;
			var qe = 0.0;
			var qf = 0.0;
			var qg = 0.0;
			var vk = 0.0;
			var ja = 0.0;
			var jb = 0.0;
			var jc = 0.0;

			var j1 = t / 5.0 + 0.1;
			var j2 = Unwind(4.14473 + 52.9691 * t);
			var j3 = Unwind(4.641118 + 21.32991 * t);
			var j4 = Unwind(4.250177 + 7.478172 * t);
			var j5 = 5.0 * j3 - 2.0 * j2;
			var j6 = 2.0 * j2 - 6.0 * j3 + 3.0 * j4;

			if (new string[] { "Mercury", "Venus", "Mars" }.Contains(planet.Name))
				return (qa, qb, qc, qd, qe, qf, qg);

			if (new string[] { "Jupiter", "Saturn" }.Contains(planet.Name))
			{
				var j7 = j3 - j2;
				var u1 = (j3).Sine();
				var u2 = (j3).Cosine();
				var u3 = (2.0 * j3).Sine();
				var u4 = (2.0 * j3).Cosine();
				var u5 = (j5).Sine();
				var u6 = (j5).Cosine();
				var u7 = (2.0 * j5).Sine();
				var u8a = (j6).Sine();
				var u9 = (j7).Sine();
				var ua = (j7).Cosine();
				var ub = (2.0 * j7).Sine();
				var uc = (2.0 * j7).Cosine();
				var ud = (3.0 * j7).Sine();
				var ue = (3.0 * j7).Cosine();
				var uf = (4.0 * j7).Sine();
				var ug = (4.0 * j7).Cosine();
				var vh = (5.0 * j7).Cosine();

				if (planet.Name == "Saturn")
				{
					var ui = (3.0 * j3).Sine();
					var uj = (3.0 * j3).Cosine();
					var uk = (4.0 * j3).Sine();
					var ul = (4.0 * j3).Cosine();
					var vi = (2.0 * j5).Cosine();
					var un = (5.0 * j7).Sine();
					var j8 = j4 - j3;
					var uo = (2.0 * j8).Sine();
					var up = (2.0 * j8).Cosine();
					var uq = (3.0 * j8).Sine();
					var ur = (3.0 * j8).Cosine();

					qc = 0.007581 * u7 - 0.007986 * u8a - 0.148811 * u9;
					qc = qc - (0.814181 - (0.01815 - 0.016714 * j1) * j1) * u5;
					qc = qc - (0.010497 - (0.160906 - 0.0041 * j1) * j1) * u6;
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
					qd = qd * 0.0000001;

					vk = (0.077108 + (0.007186 - 0.001533 * j1) * j1) * u5;
					vk = vk - 0.007075 * u9;
					vk = vk + (0.045803 - (0.014766 + 0.000536 * j1) * j1) * u6;
					vk = vk - 0.072586 * u2 - 0.075825 * u9 * u1 - 0.024839 * ub * u1;
					vk = vk - 0.008631 * ud * u1 - 0.150383 * ua * u2;
					vk = vk + 0.026897 * uc * u2 + 0.010053 * ue * u2;
					vk = vk - (0.013597 + 0.001719 * j1) * u9 * u3 + 0.011981 * ub * u4;
					vk = vk - (0.007742 - 0.001517 * j1) * ua * u3;
					vk = vk + (0.013586 - 0.001375 * j1) * uc * u3;
					vk = vk - (0.013667 - 0.001239 * j1) * u9 * u4;
					vk = vk + (0.014861 + 0.001136 * j1) * ua * u4;
					vk = vk - (0.013064 + 0.001628 * j1) * uc * u4;
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
					qf = qf - 427.0 * ue * uj;
					qf = qf * 0.000001;

					qg = 0.000747 * ua * u1 + 0.001069 * ua * u2 + 0.002108 * ub * u3;
					qg = qg + 0.001261 * uc * u3 + 0.001236 * ub * u4 - 0.002075 * uc * u4;
					qg = qg.ToRadians();

					return (qa, qb, qc, qd, qe, qf, qg);
				}

				qc = (0.331364 - (0.010281 + 0.004692 * j1) * j1) * u5;
				qc = qc + (0.003228 - (0.064436 - 0.002075 * j1) * j1) * u6;
				qc = qc - (0.003083 + (0.000275 - 0.000489 * j1) * j1) * u7;
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
				qd = qd * 0.0000001;

				vk = (0.007192 - 0.003147 * j1) * u5 - 0.004344 * u1;
				vk = vk + (j1 * (0.000197 * j1 - 0.000675) - 0.020428) * u6;
				vk = vk + 0.034036 * ua * u1 + (0.007269 + 0.000672 * j1) * u9 * u1;
				vk = vk + 0.005614 * uc * u1 + 0.002964 * ue * u1 + 0.037761 * u9 * u2;
				vk = vk + 0.006158 * ub * u2 - 0.006603 * ua * u2 - 0.005356 * u9 * u3;
				vk = vk + 0.002722 * ub * u3 + 0.004483 * ua * u3;
				vk = vk - 0.002642 * uc * u3 + 0.004403 * u9 * u4;
				vk = vk - 0.002536 * ub * u4 + 0.005547 * ua * u4 - 0.002689 * uc * u4;
				qe = qc - (vk.ToRadians() / planet.Value4);

				qf = 205.0 * ua - 263.0 * u6 + 693.0 * uc + 312.0 * ue + 147.0 * ug + 299.0 * u9 * u1;
				qf = qf + 181.0 * uc * u1 + 204.0 * ub * u2 + 111.0 * ud * u2 - 337.0 * ua * u2;
				qf = qf - 111.0 * uc * u2;
				qf = qf * 0.000001;

				return (qa, qb, qc, qd, qe, qf, qg);
			}

			if (new string[] { "Uranus", "Neptune" }.Contains(planet.Name))
			{
				var j8 = Unwind(1.46205 + 3.81337 * t);
				var j9 = 2.0 * j8 - j4;
				var vj = (j9).Sine();
				var uu = (j9).Cosine();
				var uv = (2.0 * j9).Sine();
				var uw = (2.0 * j9).Cosine();

				if (planet.Name == "Neptune")
				{
					ja = j8 - j2;
					jb = j8 - j3;
					jc = j8 - j4;
					qc = (0.001089 * j1 - 0.589833) * vj;
					qc = qc + (0.004658 * j1 - 0.056094) * uu - 0.024286 * uv;
					qc = qc.ToRadians();

					vk = 0.024039 * vj - 0.025303 * uu + 0.006206 * uv;
					vk = vk - 0.005992 * uw;
					qe = qc - (vk.ToRadians() / planet.Value4);

					qd = 4389.0 * vj + 1129.0 * uv + 4262.0 * uu + 1089.0 * uw;
					qd = qd * 0.0000001;

					qf = 8189.0 * uu - 817.0 * vj + 781.0 * uw;
					qf = qf * 0.000001;

					var vd = (2.0 * jc).Sine();
					var ve = (2.0 * jc).Cosine();
					var vf = (j8).Sine();
					var vg = (j8).Cosine();
					qa = -0.009556 * (ja).Sine() - 0.005178 * (jb).Sine();
					qa = qa + 0.002572 * vd - 0.002972 * ve * vf - 0.002833 * vd * vg;

					qg = 0.000336 * ve * vf + 0.000364 * vd * vg;
					qg = qg.ToRadians();

					qb = -40596.0 + 4992.0 * (ja).Cosine() + 2744.0 * (jb).Cosine();
					qb = qb + 2044.0 * (jc).Cosine() + 1051.0 * ve;
					qb = qb * 0.000001;

					return (qa, qb, qc, qd, qe, qf, qg);
				}

				ja = j4 - j2;
				jb = j4 - j3;
				jc = j8 - j4;
				qc = (0.864319 - 0.001583 * j1) * vj;
				qc = qc + (0.082222 - 0.006833 * j1) * uu + 0.036017 * uv;
				qc = qc - 0.003019 * uw + 0.008122 * (j6).Sine();
				qc = qc.ToRadians();

				vk = 0.120303 * vj + 0.006197 * uv;
				vk = vk + (0.019472 - 0.000947 * j1) * uu;
				qe = qc - (vk.ToRadians() / planet.Value4);

				qd = (163.0 * j1 - 3349.0) * vj + 20981.0 * uu + 1311.0 * uw;
				qd = qd * 0.0000001;

				qf = -0.003825 * uu;

				qa = (-0.038581 + (0.002031 - 0.00191 * j1) * j1) * (j4 + jb).Cosine();
				qa = qa + (0.010122 - 0.000988 * j1) * (j4 + jb).Sine();
				var a = (0.034964 - (0.001038 - 0.000868 * j1) * j1) * (2.0 * j4 + jb).Cosine();
				qa = a + qa + 0.005594 * (j4 + 3.0 * jc).Sine() - 0.014808 * (ja).Sine();
				qa = qa - 0.005794 * (jb).Sine() + 0.002347 * (jb).Cosine();
				qa = qa + 0.009872 * (jc).Sine() + 0.008803 * (2.0 * jc).Sine();
				qa = qa - 0.004308 * (3.0 * jc).Sine();

				var ux = jb.Sine();
				var uy = jb.Cosine();
				var uz = j4.Sine();
				var va = j4.Cosine();
				var vb = (2.0 * j4).Sine();
				var vc = (2.0 * j4).Cosine();
				qg = (0.000458 * ux - 0.000642 * uy - 0.000517 * (4.0 * jc).Cosine()) * uz;
				qg = qg - (0.000347 * ux + 0.000853 * uy + 0.000517 * (4.0 * jb).Sine()) * va;
				qg = qg + 0.000403 * ((2.0 * jc).Cosine() * vb + (2.0 * jc).Sine() * vc);
				qg = qg.ToRadians();

				qb = -25948.0 + 4985.0 * (ja).Cosine() - 1230.0 * va + 3354.0 * uy;
				qb = qb + 904.0 * (2.0 * jc).Cosine() + 894.0 * ((jc).Cosine() - (3.0 * jc).Cosine());
				qb = qb + (5795.0 * va - 1165.0 * uz + 1388.0 * vc) * ux;
				qb = qb + (1351.0 * va + 5702.0 * uz + 1388.0 * vb) * uy;
				qb = qb * 0.000001;

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
		/// <param name="w"></param>
		/// <returns></returns>
		public static double SolveCubic(double w)
		{
			var s = w / 3.0;

			while (1 == 1)
			{
				var s2 = s * s;
				var d = (s2 + 3.0) * s - w;

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
		/// <para>comet_long_deg -- Comet longitude (degrees)</para>
		/// <para>comet_lat_deg -- Comet lat (degrees)</para>
		/// <para>comet_dist_au -- Comet distance from Earth (AU)</para>
		/// </returns>
		public static (double cometLongDeg, double cometLatDeg, double cometDistAU) PCometLongLatDist(double lh, double lm, double ls, int ds, int zc, double dy, int mn, int yr, double td, int tm, int ty, double q, double i, double p, double n)
		{
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var tpe = (ut / 365.242191) + CivilDateToJulianDate(gd, gm, gy) - CivilDateToJulianDate(td, tm, ty);
			var lg = (SunLong(lh, lm, ls, ds, zc, dy, mn, yr) + 180.0).ToRadians();
			var re = SunDist(lh, lm, ls, ds, zc, dy, mn, yr);

			// var _li = 0.0;
			var rh2 = 0.0;
			var rd = 0.0;
			var s3 = 0.0;
			var c3 = 0.0;
			var lc = 0.0;
			var s2 = 0.0;
			var c2 = 0.0;

			for (int k = 1; k < 3; k++)
			{
				var s = SolveCubic(0.0364911624 * tpe / (q * (q).SquareRoot()));
				var nu = 2.0 * s.AngleTangent();
				var r = q * (1.0 + s * s);
				var l = nu + p.ToRadians();
				var s1 = l.Sine();
				var c1 = l.Cosine();
				var i1 = i.ToRadians();
				s2 = s1 * i1.Sine();
				var ps = s2.ASine();
				var y = s1 * i1.Cosine();
				lc = y.AngleTangent2(c1) + n.ToRadians();
				c2 = ps.Cosine();
				rd = r * c2;
				var ll = lc - lg;
				c3 = ll.Cosine();
				s3 = ll.Sine();
				var rh = ((re * re) + (r * r) - (2.0 * re * rd * c3 * (ps).Cosine())).SquareRoot();
				if (k == 1)
				{
					rh2 = ((re * re) + (r * r) - (2.0 * re * r * (ps).Cosine() * (l + (n).ToRadians() - lg).Cosine())).SquareRoot();
				}

				// _li = rh * 0.005775518;
			}

			double ep;

			ep = (rd < re) ? ((-rd * s3) / (re - (rd * c3))).AngleTangent() + lg + 3.141592654 : ((re * s3) / (rd - (re * c3))).AngleTangent() + lc;
			ep = Unwind(ep);

			var tb = (rd * s2 * (ep - lc).Sine()) / (c2 * re * s3);
			var bp = tb.AngleTangent();

			var cometLongDeg = Degrees(ep);
			var cometLatDeg = Degrees(bp);
			var cometDistAU = rh2;

			return (cometLongDeg, cometLatDeg, cometDistAU);
		}

		/// <summary>
		/// Calculate longitude, latitude, and horizontal parallax of the Moon.
		/// </summary>
		/// <remarks>
		/// Original macro names: MoonLong, MoonLat, MoonHP
		/// </remarks>
		/// <param name="moonLongDeg"></param>
		/// <param name="moonLatDeg"></param>
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
			var ut = LocalCivilTimeToUniversalTime(lh, lm, ls, ds, zc, dy, mn, yr);
			var gd = LocalCivilTimeGreenwichDay(lh, lm, ls, ds, zc, dy, mn, yr);
			var gm = LocalCivilTimeGreenwichMonth(lh, lm, ls, ds, zc, dy, mn, yr);
			var gy = LocalCivilTimeGreenwichYear(lh, lm, ls, ds, zc, dy, mn, yr);
			var t = ((CivilDateToJulianDate(gd, gm, gy) - 2415020.0) / 36525.0) + (ut / 876600.0);
			var t2 = t * t;

			var m1 = 27.32158213;
			var m2 = 365.2596407;
			var m3 = 27.55455094;
			var m4 = 29.53058868;
			var m5 = 27.21222039;
			var m6 = 6798.363307;
			var q = CivilDateToJulianDate(gd, gm, gy) - 2415020.0 + (ut / 24.0);
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

			var ml = 270.434164 + m1 - (0.001133 - 0.0000019 * t) * t2;
			var ms = 358.475833 + m2 - (0.00015 + 0.0000033 * t) * t2;
			var md = 296.104608 + m3 + (0.009192 + 0.0000144 * t) * t2;
			var me1 = 350.737486 + m4 - (0.001436 - 0.0000019 * t) * t2;
			var mf = 11.250889 + m5 - (0.003211 + 0.0000003 * t) * t2;
			var na = 259.183275 - m6 + (0.002078 + 0.0000022 * t) * t2;
			var a = (51.2 + 20.2 * t).ToRadians();
			var s1 = a.Sine();
			var s2 = na.ToRadians().Sine();
			var b = 346.56 + (132.87 - 0.0091731 * t) * t;
			var s3 = 0.003964 * b.ToRadians().Sine();
			var c = (na + 275.05 - 2.3 * t).ToRadians();
			var s4 = c.Sine();
			ml = ml + 0.000233 * s1 + s3 + 0.001964 * s2;
			ms = ms - 0.001778 * s1;
			md = md + 0.000817 * s1 + s3 + 0.002541 * s2;
			mf = mf + s3 - 0.024691 * s2 - 0.004328 * s4;
			me1 = me1 + 0.002011 * s1 + s3 + 0.001964 * s2;
			var e = 1.0 - (0.002495 + 0.00000752 * t) * t;
			var e2 = e * e;
			ml = ml.ToRadians();
			ms = ms.ToRadians();
			na = na.ToRadians();
			me1 = me1.ToRadians();
			mf = mf.ToRadians();
			md = md.ToRadians();

			// Longitude-specific
			var l = 6.28875 * md.Sine() + 1.274018 * (2.0 * me1 - md).Sine();
			l = l + 0.658309 * (2.0 * me1).Sine() + 0.213616 * (2.0 * md).Sine();
			l = l - e * 0.185596 * ms.Sine() - 0.114336 * (2.0 * mf).Sine();
			l = l + 0.058793 * (2.0 * (me1 - md)).Sine();
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
			l = l + e * 0.000761 * (4.0 * me1 - ms - 2.0 * md).Sine();
			l = l + e2 * 0.000704 * (md - 2.0 * (ms + me1)).Sine();
			l = l + e * 0.000693 * (ms - 2.0 * (md - me1)).Sine();
			l = l + e * 0.000598 * (2.0 * (me1 - mf) - ms).Sine();
			l = l + 0.00055 * (md + 4.0 * me1).Sine() + 0.000538 * (4.0 * md).Sine();
			l = l + e * 0.000521 * (4.0 * me1 - ms).Sine() + 0.000486 * (2.0 * md - me1).Sine();
			l = l + e2 * 0.000717 * (md - 2.0 * ms).Sine();
			var mm = Unwind(ml + l.ToRadians());

			// Latitude-specific
			var g = 5.128189 * mf.Sine() + 0.280606 * (md + mf).Sine();
			g = g + 0.277693 * (md - mf).Sine() + 0.173238 * (2.0 * me1 - mf).Sine();
			g = g + 0.055413 * (2.0 * me1 + mf - md).Sine() + 0.046272 * (2.0 * me1 - mf - md).Sine();
			g = g + 0.032573 * (2.0 * me1 + mf).Sine() + 0.017198 * (2.0 * md + mf).Sine();
			g = g + 0.009267 * (2.0 * me1 + md - mf).Sine() + 0.008823 * (2.0 * md - mf).Sine();
			g = g + e * 0.008247 * (2.0 * me1 - ms - mf).Sine() + 0.004323 * (2.0 * (me1 - md) - mf).Sine();
			g = g + 0.0042 * (2.0 * me1 + mf + md).Sine() + e * 0.003372 * (mf - ms - 2.0 * me1).Sine();
			g = g + e * 0.002472 * (2.0 * me1 + mf - ms - md).Sine();
			g = g + e * 0.002222 * (2.0 * me1 + mf - ms).Sine();
			g = g + e * 0.002072 * (2.0 * me1 - mf - ms - md).Sine();
			g = g + e * 0.001877 * (mf - ms + md).Sine() + 0.001828 * (4.0 * me1 - mf - md).Sine();
			g = g - e * 0.001803 * (mf + ms).Sine() - 0.00175 * (3.0 * mf).Sine();
			g = g + e * 0.00157 * (md - ms - mf).Sine() - 0.001487 * (mf + me1).Sine();
			g = g - e * 0.001481 * (mf + ms + md).Sine() + e * 0.001417 * (mf - ms - md).Sine();
			g = g + e * 0.00135 * (mf - ms).Sine() + 0.00133 * (mf - me1).Sine();
			g = g + 0.001106 * (mf + 3.0 * md).Sine() + 0.00102 * (4.0 * me1 - mf).Sine();
			g = g + 0.000833 * (mf + 4.0 * me1 - md).Sine() + 0.000781 * (md - 3.0 * mf).Sine();
			g = g + 0.00067 * (mf + 4.0 * me1 - 2.0 * md).Sine() + 0.000606 * (2.0 * me1 - 3.0 * mf).Sine();
			g = g + 0.000597 * (2.0 * (me1 + md) - mf).Sine();
			g = g + e * 0.000492 * (2.0 * me1 + md - ms - mf).Sine() + 0.00045 * (2.0 * (md - me1) - mf).Sine();
			g = g + 0.000439 * (3.0 * md - mf).Sine() + 0.000423 * (mf + 2.0 * (me1 + md)).Sine();
			g = g + 0.000422 * (2.0 * me1 - mf - 3.0 * md).Sine() - e * 0.000367 * (ms + mf + 2.0 * me1 - md).Sine();
			g = g - e * 0.000353 * (ms + mf + 2.0 * me1).Sine() + 0.000331 * (mf + 4.0 * me1).Sine();
			g = g + e * 0.000317 * (2.0 * me1 + mf - ms + md).Sine();
			g = g + e2 * 0.000306 * (2.0 * (me1 - ms) - mf).Sine() - 0.000283 * (md + 3.0 * mf).Sine();
			var w1 = 0.0004664 * na.Cosine();
			var w2 = 0.0000754 * c.Cosine();
			var bm = g.ToRadians() * (1.0 - w1 - w2);

			// Horizontal parallax-specific
			var pm = 0.950724 + 0.051818 * md.Cosine() + 0.009531 * (2.0 * me1 - md).Cosine();
			pm = pm + 0.007843 * (2.0 * me1).Cosine() + 0.002824 * (2.0 * md).Cosine();
			pm = pm + 0.000857 * (2.0 * me1 + md).Cosine() + e * 0.000533 * (2.0 * me1 - ms).Cosine();
			pm = pm + e * 0.000401 * (2.0 * me1 - md - ms).Cosine();
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
			pm = pm + e * 0.000019 * (4.0 * me1 - ms - md).Cosine();

			var moonLongDeg = Degrees(mm);
			var moonLatDeg = Degrees(bm);
			var moonHorPara = pm;

			return (moonLongDeg, moonLatDeg, moonHorPara);
		}
	}
}