using System;
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
	}
}