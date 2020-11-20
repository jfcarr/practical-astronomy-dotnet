using System;

namespace PALib
{
	public class PACoordinates
	{
		/// <summary>
		/// Convert an Angle (degrees, minutes, and seconds) to Decimal Degrees
		/// </summary>
		/// <param name="degrees"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns>Decimal Degrees (double)</returns>
		public double AngleToDecimalDegrees(double degrees, double minutes, double seconds)
		{
			var a = Math.Abs(seconds) / 60;
			var b = (Math.Abs(minutes) + a) / 60;
			var c = Math.Abs(degrees) + b;
			var d = (degrees < 0 || minutes < 0 || seconds < 0) ? -c : c;

			return d;
		}

		/// <summary>
		/// Convert Decimal Degrees to an Angle (degrees, minutes, and seconds)
		/// </summary>
		/// <param name="degrees"></param>
		/// <param name="minutes"></param>
		/// <param name="decimalDegrees"></param>
		/// <returns>Tuple (degrees, minutes, seconds)</returns>
		public (double degrees, double minutes, double seconds) DecimalDegreesToAngle(double decimalDegrees)
		{
			var unsignedDecimal = Math.Abs(decimalDegrees);
			var totalSeconds = unsignedDecimal * 3600;
			var seconds2DP = Math.Round(totalSeconds % 60, 2);
			var correctedSeconds = (seconds2DP == 60) ? 0 : seconds2DP;
			var correctedRemainder = (seconds2DP == 60) ? totalSeconds + 60 : totalSeconds;
			var minutes = Math.Floor(correctedRemainder / 60) % 60;
			var unsignedDegrees = Math.Floor(correctedRemainder / 3600);
			var signedDegrees = (decimalDegrees < 0) ? -1 * unsignedDegrees : unsignedDegrees;

			return (signedDegrees, minutes, Math.Floor(correctedSeconds));
		}

		/// <summary>
		/// Convert Right Ascension to Hour Angle
		/// </summary>
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
		public (double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds) RightAscensionToHourAngle(double raHours, double raMinutes, double raSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSavings, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var daylightSaving = (isDaylightSavings) ? 1 : 0;

			var hourAngle = PAMacros.RightAscensionToHourAngle(raHours, raMinutes, raSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

			var hourAngleHours = PAMacros.DecimalHoursHour(hourAngle);
			var hourAngleMinutes = PAMacros.DecimalHoursMinute(hourAngle);
			var hourAngleSeconds = PAMacros.DecimalHoursSecond(hourAngle);

			return (hourAngleHours, hourAngleMinutes, hourAngleSeconds);
		}

		/// <summary>
		/// Convert Hour Angle to Right Ascension
		/// </summary>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public (double raHours, double raMinutes, double raSeconds) HourAngleToRightAscension(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var rightAscension = PAMacros.HourAngleToRightAscension(hourAngleHours, hourAngleMinutes, hourAngleSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

			var rightAscensionHours = PAMacros.DecimalHoursHour(rightAscension);
			var rightAscensionMinutes = PAMacros.DecimalHoursMinute(rightAscension);
			var rightAscensionSeconds = PAMacros.DecimalHoursSecond(rightAscension);

			return (rightAscensionHours, rightAscensionMinutes, rightAscensionSeconds);
		}

		/// <summary>
		/// Convert Equatorial Coordinates to Horizon Coordinates
		/// </summary>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="declinationDegrees"></param>
		/// <param name="declinationMinutes"></param>
		/// <param name="declinationSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns>Tuple (azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds)</returns>
		public (double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds) EquatorialCoordinatesToHorizonCoordinates(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
		{
			var azimuthInDecimalDegrees = PAMacros.EquatorialCoordinatesToAzimuth(hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds, geographicalLatitude);

			var altitudeInDecimalDegrees = PAMacros.EquatorialCoordinatesToAltitude(hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds, geographicalLatitude);

			var azimuthDegrees = PAMacros.DecimalDegreesDegrees(azimuthInDecimalDegrees);
			var azimuthMinutes = PAMacros.DecimalDegreesMinutes(azimuthInDecimalDegrees);
			var azimuthSeconds = PAMacros.DecimalDegreesSeconds(azimuthInDecimalDegrees);

			var altitudeDegrees = PAMacros.DecimalDegreesDegrees(altitudeInDecimalDegrees);
			var altitudeMinutes = PAMacros.DecimalDegreesMinutes(altitudeInDecimalDegrees);
			var altitudeSeconds = PAMacros.DecimalDegreesSeconds(altitudeInDecimalDegrees);

			return (azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds);
		}

		/// Convert Horizon Coordinates to Equatorial Coordinates
		public (double hour_angle_hours, double hour_angle_minutes, double hour_angle_seconds, double declination_degrees, double declination_minutes, double declination_seconds) HorizonCoordinatesToEquatorialCoordinates(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
		{
			var hourAngleInDecimalDegrees = PAMacros.HorizonCoordinatesToHourAngle(azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds, geographicalLatitude);

			var declinationInDecimalDegrees = PAMacros.HorizonCoordinatesToDeclination(azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds, geographicalLatitude);

			var hourAngleHours = PAMacros.DecimalHoursHour(hourAngleInDecimalDegrees);
			var hourAngleMinutes = PAMacros.DecimalHoursMinute(hourAngleInDecimalDegrees);
			var hourAngleSeconds = PAMacros.DecimalHoursSecond(hourAngleInDecimalDegrees);

			var declinationDegrees = PAMacros.DecimalDegreesDegrees(declinationInDecimalDegrees);
			var declinationMinutes = PAMacros.DecimalDegreesMinutes(declinationInDecimalDegrees);
			var declinationSeconds = PAMacros.DecimalDegreesSeconds(declinationInDecimalDegrees);

			return (hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds);
		}

		/// <summary>
		/// Calculate Mean Obliquity of the Ecliptic for a Greenwich Date
		/// </summary>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public double MeanObliquityOfTheEcliptic(double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var jd = PAMacros.CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
			var mjd = jd - 2451545;
			var t = mjd / 36525;
			var de1 = t * (46.815 + t * (0.0006 - (t * 0.00181)));
			var de2 = de1 / 3600;

			return 23.439292 - de2;
		}

		/// <summary>
		/// Convert Ecliptic Coordinates to Equatorial Coordinates
		/// </summary>
		/// <param name="eclipticLongitudeDegrees"></param>
		/// <param name="eclipticLongitudeMinutes"></param>
		/// <param name="eclipticLongitudeSeconds"></param>
		/// <param name="eclipticLatitudeDegrees"></param>
		/// <param name="eclipticLatitudeMinutes"></param>
		/// <param name="eclipticLatitudeSeconds"></param>
		/// <param name="greenwichDay"></param>
		/// <param name="greenwichMonth"></param>
		/// <param name="greenwichYear"></param>
		/// <returns></returns>
		public (double outRAHours, double outRAMinutes, double outRASeconds, double outDecDegrees, double outDecMinutes, double outDecSeconds) EclipticCoordinateToEquatorialCoordinate(double eclipticLongitudeDegrees, double eclipticLongitudeMinutes, double eclipticLongitudeSeconds, double eclipticLatitudeDegrees, double eclipticLatitudeMinutes, double eclipticLatitudeSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var eclonDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLongitudeDegrees, eclipticLongitudeMinutes, eclipticLongitudeSeconds);
			var eclatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLatitudeDegrees, eclipticLatitudeMinutes, eclipticLatitudeSeconds);
			var eclonRad = eclonDeg.ToRadians();
			var eclatRad = eclatDeg.ToRadians();
			var obliqDeg = PAMacros.Obliq(greenwichDay, greenwichMonth, greenwichYear);
			var obliqRad = obliqDeg.ToRadians();
			var sinDec = Math.Sin(eclatRad) * Math.Cos(obliqRad) + Math.Cos(eclatRad) * Math.Sin(obliqRad) * Math.Sin(eclonRad);
			var decRad = Math.Asin(sinDec);
			var decDeg = PAMacros.Degrees(decRad);
			var y = Math.Sin(eclonRad) * Math.Cos(obliqRad) - Math.Tan(eclatRad) * Math.Sin(obliqRad);
			var x = Math.Cos(eclonRad);
			var raRad = Math.Atan2(y, x);
			var raDeg1 = PAMacros.Degrees(raRad);
			var raDeg2 = raDeg1 - 360 * Math.Floor(raDeg1 / 360);
			var raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg2);

			var outRAHours = PAMacros.DecimalHoursHour(raHours);
			var outRAMinutes = PAMacros.DecimalHoursMinute(raHours);
			var outRASeconds = PAMacros.DecimalHoursSecond(raHours);
			var outDecDegrees = PAMacros.DecimalDegreesDegrees(decDeg);
			var outDecMinutes = PAMacros.DecimalDegreesMinutes(decDeg);
			var outDecSeconds = PAMacros.DecimalDegreesSeconds(decDeg);

			return (outRAHours, outRAMinutes, outRASeconds, outDecDegrees, outDecMinutes, outDecSeconds);
		}

		/// <summary>
		/// Convert Equatorial Coordinates to Ecliptic Coordinates
		/// </summary>
		/// <param name="raHours"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="decDegrees"></param>
		/// <param name="decMinutes"></param>
		/// <param name="decSeconds"></param>
		/// <param name="gwDay"></param>
		/// <param name="gwMonth"></param>
		/// <param name="gwYear"></param>
		/// <returns></returns>
		public (double outEclLongDeg, double outEclLongMin, double outEclLongSec, double outEclLatDeg, double outEclLatMin, double outEclLatSec) EquatorialCoordinateToEclipticCoordinate(double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds, double gwDay, int gwMonth, int gwYear)
		{
			var raDeg = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHours, raMinutes, raSeconds));
			var decDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDegrees, decMinutes, decSeconds);
			var raRad = raDeg.ToRadians();
			var decRad = decDeg.ToRadians();
			var obliqDeg = PAMacros.Obliq(gwDay, gwMonth, gwYear);
			var obliqRad = obliqDeg.ToRadians();
			var sinEclLat = Math.Sin(decRad) * Math.Cos(obliqRad) - Math.Cos(decRad) * Math.Sin(obliqRad) * Math.Sin(raRad);
			var eclLatRad = Math.Asin(sinEclLat);
			var eclLatDeg = PAMacros.Degrees(eclLatRad);
			var y = Math.Sin(raRad) * Math.Cos(obliqRad) + Math.Tan(decRad) * Math.Sin(obliqRad);
			var x = Math.Cos(raRad);
			var eclLongRad = Math.Atan2(y, x);
			var eclLongDeg1 = PAMacros.Degrees(eclLongRad);
			var eclLongDeg2 = eclLongDeg1 - 360 * Math.Floor(eclLongDeg1 / 360.0);

			var outEclLongDeg = PAMacros.DecimalDegreesDegrees(eclLongDeg2);
			var outEclLongMin = PAMacros.DecimalDegreesMinutes(eclLongDeg2);
			var outEclLongSec = PAMacros.DecimalDegreesSeconds(eclLongDeg2);
			var outEclLatDeg = PAMacros.DecimalDegreesDegrees(eclLatDeg);
			var outEclLatMin = PAMacros.DecimalDegreesMinutes(eclLatDeg);
			var outEclLatSec = PAMacros.DecimalDegreesSeconds(eclLatDeg);

			return (outEclLongDeg, outEclLongMin, outEclLongSec, outEclLatDeg, outEclLatMin, outEclLatSec);
		}
	}
}