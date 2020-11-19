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
			var e1 = Math.Floor(e);

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

			return Math.Floor(e);
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
			var e = d - (24 * Math.Floor(d / 24));
			var f = HMStoDH(uHours, uMinutes, uSeconds);
			var g = f * 1.002737909;
			var h = e + g;

			return h - (24 * Math.Floor(h / 24));
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

			return c - (24 * Math.Floor(c / 24));
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
			var g = Math.Sin(e) * Math.Sin(f) + Math.Cos(e) * Math.Cos(f) * Math.Cos(c);
			var h = -Math.Cos(e) * Math.Cos(f) * Math.Sin(c);
			var i = Math.Sin(e) - (Math.Sin(f) * g);
			var j = Degrees(Math.Atan2(h, i));

			return j - 360.0 * Math.Floor(j / 360);
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
			var g = Math.Sin(e) * Math.Sin(f) + Math.Cos(e) * Math.Cos(f) * Math.Cos(c);

			return Degrees(Math.Asin(g));
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
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
			var e = (c == 60) ? 60 : b;

			return (decimalDegrees < 0) ? -(Math.Floor(e / 3600)) : Math.Floor(e / 3600);
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
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
			var e = (c == 60) ? b + 60 : b;

			return Math.Floor(e / 60) % 60;
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
			var c = Math.Round(b - 60 * Math.Floor(b / 60), 2);
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
			var f = Math.Sin(d) * Math.Sin(e) + Math.Cos(d) * Math.Cos(e) * Math.Cos(c);

			return Degrees(Math.Asin(f));
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
			var f = Math.Sin(d) * Math.Sin(e) + Math.Cos(d) * Math.Cos(e) * Math.Cos(c);
			var g = -Math.Cos(d) * Math.Cos(e) * Math.Sin(c);
			var h = Math.Sin(d) - Math.Sin(e) * f;
			var i = DecimalDegreesToDegreeHours(Degrees(Math.Atan2(g, h)));

			return i - 24 * Math.Floor(i / 24.0);
		}
	}
}