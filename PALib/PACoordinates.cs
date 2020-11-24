using System;
using PALib.Helpers;

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
			var minutes = (correctedRemainder / 60).Floor() % 60;
			var unsignedDegrees = (correctedRemainder / 3600).Floor();
			var signedDegrees = (decimalDegrees < 0) ? -1 * unsignedDegrees : unsignedDegrees;

			return (signedDegrees, minutes, correctedSeconds.Floor());
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
		/// <returns>Tuple (hourAngleHours, hourAngleMinutes, hourAngleSeconds)</returns>
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
		/// <returns>Tuple (rightAscensionHours, rightAscensionMinutes, rightAscensionSeconds)</returns>
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

		/// <summary>
		/// Convert Horizon Coordinates to Equatorial Coordinates
		/// </summary>
		/// <param name="azimuthDegrees"></param>
		/// <param name="azimuthMinutes"></param>
		/// <param name="azimuthSeconds"></param>
		/// <param name="altitudeDegrees"></param>
		/// <param name="altitudeMinutes"></param>
		/// <param name="altitudeSeconds"></param>
		/// <param name="geographicalLatitude"></param>
		/// <returns>Tuple (hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds)</returns>
		public (double hour_angle_hours, double hour_angle_minutes, double hour_angle_seconds, double declination_degrees, double declination_minutes, double declinationseconds) HorizonCoordinatesToEquatorialCoordinates(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
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
		/// <returns>Tuple (outRAHours, outRAMinutes, outRASeconds, outDecDegrees, outDecMinutes, outDecSeconds)</returns>
		public (double outRAHours, double outRAMinutes, double outRASeconds, double outDecDegrees, double outDecMinutes, double outDecSeconds) EclipticCoordinateToEquatorialCoordinate(double eclipticLongitudeDegrees, double eclipticLongitudeMinutes, double eclipticLongitudeSeconds, double eclipticLatitudeDegrees, double eclipticLatitudeMinutes, double eclipticLatitudeSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var eclonDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLongitudeDegrees, eclipticLongitudeMinutes, eclipticLongitudeSeconds);
			var eclatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLatitudeDegrees, eclipticLatitudeMinutes, eclipticLatitudeSeconds);
			var eclonRad = eclonDeg.ToRadians();
			var eclatRad = eclatDeg.ToRadians();
			var obliqDeg = PAMacros.Obliq(greenwichDay, greenwichMonth, greenwichYear);
			var obliqRad = obliqDeg.ToRadians();
			var sinDec = eclatRad.Sine() * obliqRad.Cosine() + eclatRad.Cosine() * obliqRad.Sine() * eclonRad.Sine();
			var decRad = sinDec.ASine();
			var decDeg = PAMacros.Degrees(decRad);
			var y = eclonRad.Sine() * obliqRad.Cosine() - eclatRad.Tangent() * obliqRad.Sine();
			var x = eclonRad.Cosine();
			var raRad = y.AngleTangent2(x);
			var raDeg1 = PAMacros.Degrees(raRad);
			var raDeg2 = raDeg1 - 360 * (raDeg1 / 360).Floor();
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
		/// <returns>Tuple (outEclLongDeg, outEclLongMin, outEclLongSec, outEclLatDeg, outEclLatMin, outEclLatSec)</returns>
		public (double outEclLongDeg, double outEclLongMin, double outEclLongSec, double outEclLatDeg, double outEclLatMin, double outEclLatSec) EquatorialCoordinateToEclipticCoordinate(double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds, double gwDay, int gwMonth, int gwYear)
		{
			var raDeg = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHours, raMinutes, raSeconds));
			var decDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDegrees, decMinutes, decSeconds);
			var raRad = raDeg.ToRadians();
			var decRad = decDeg.ToRadians();
			var obliqDeg = PAMacros.Obliq(gwDay, gwMonth, gwYear);
			var obliqRad = obliqDeg.ToRadians();
			var sinEclLat = decRad.Sine() * obliqRad.Cosine() - decRad.Cosine() * obliqRad.Sine() * raRad.Sine();
			var eclLatRad = sinEclLat.ASine();
			var eclLatDeg = PAMacros.Degrees(eclLatRad);
			var y = raRad.Sine() * obliqRad.Cosine() + decRad.Tangent() * obliqRad.Sine();
			var x = raRad.Cosine();
			var eclLongRad = y.AngleTangent2(x);
			var eclLongDeg1 = PAMacros.Degrees(eclLongRad);
			var eclLongDeg2 = eclLongDeg1 - 360 * (eclLongDeg1 / 360).Floor();

			var outEclLongDeg = PAMacros.DecimalDegreesDegrees(eclLongDeg2);
			var outEclLongMin = PAMacros.DecimalDegreesMinutes(eclLongDeg2);
			var outEclLongSec = PAMacros.DecimalDegreesSeconds(eclLongDeg2);
			var outEclLatDeg = PAMacros.DecimalDegreesDegrees(eclLatDeg);
			var outEclLatMin = PAMacros.DecimalDegreesMinutes(eclLatDeg);
			var outEclLatSec = PAMacros.DecimalDegreesSeconds(eclLatDeg);

			return (outEclLongDeg, outEclLongMin, outEclLongSec, outEclLatDeg, outEclLatMin, outEclLatSec);
		}

		/// <summary>
		/// Convert Equatorial Coordinates to Galactic Coordinates
		/// </summary>
		/// <param name="raHours"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="decDegrees"></param>
		/// <param name="decMinutes"></param>
		/// <param name="decSeconds"></param>
		/// <returns>Tuple (galLongDeg, galLongMin, galLongSec, galLatDeg, galLatMin, galLatSec)</returns>
		public (double galLongDeg, double galLongMin, double galLongSec, double galLatDeg, double galLatMin, double galLatSec) EquatorialCoordinateToGalacticCoordinate(double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds)
		{
			var raDeg = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHours, raMinutes, raSeconds));
			var decDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDegrees, decMinutes, decSeconds);
			var raRad = raDeg.ToRadians();
			var decRad = decDeg.ToRadians();
			var sinB = decRad.Cosine() * (27.4).ToRadians().Cosine() * (raRad - (192.25).ToRadians()).Cosine() + decRad.Sine() * (27.4).ToRadians().Sine();
			var bRadians = sinB.ASine();
			var bDeg = PAMacros.Degrees(bRadians);
			var y = decRad.Sine() - sinB * (27.4).ToRadians().Sine();
			var x = decRad.Cosine() * (raRad - (192.25).ToRadians()).Sine() * (27.4).ToRadians().Cosine();
			var longDeg1 = PAMacros.Degrees(y.AngleTangent2(x)) + 33;
			var longDeg2 = longDeg1 - 360 * (longDeg1 / 360).Floor();

			var galLongDeg = PAMacros.DecimalDegreesDegrees(longDeg2);
			var galLongMin = PAMacros.DecimalDegreesMinutes(longDeg2);
			var galLongSec = PAMacros.DecimalDegreesSeconds(longDeg2);
			var galLatDeg = PAMacros.DecimalDegreesDegrees(bDeg);
			var galLatMin = PAMacros.DecimalDegreesMinutes(bDeg);
			var galLatSec = PAMacros.DecimalDegreesSeconds(bDeg);

			return (galLongDeg, galLongMin, galLongSec, galLatDeg, galLatMin, galLatSec);
		}

		/// <summary>
		/// Convert Galactic Coordinates to Equatorial Coordinates
		/// </summary>
		/// <param name="galLongDeg"></param>
		/// <param name="galLongMin"></param>
		/// <param name="galLongSec"></param>
		/// <param name="galLatDeg"></param>
		/// <param name="galLatMin"></param>
		/// <param name="galLatSec"></param>
		/// <returns>Tuple (raHours, raMinutes, raSeconds, decDegrees, decMinutes, decSeconds)</returns>
		public (double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds) GalacticCoordinateToEquatorialCoordinate(double galLongDeg, double galLongMin, double galLongSec, double galLatDeg, double galLatMin, double galLatSec)
		{
			var glongDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(galLongDeg, galLongMin, galLongSec);
			var glatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(galLatDeg, galLatMin, galLatSec);
			var glongRad = glongDeg.ToRadians();
			var glatRad = glatDeg.ToRadians();
			var sinDec = glatRad.Cosine() * (27.4).ToRadians().Cosine() * (glongRad - (33.0).ToRadians()).Sine() + glatRad.Sine() * (27.4).ToRadians().Sine();
			var decRadians = sinDec.ASine();
			var decDeg = PAMacros.Degrees(decRadians);
			var y = glatRad.Cosine() * (glongRad - (33.0).ToRadians()).Cosine();
			var x = glatRad.Sine() * ((27.4).ToRadians()).Cosine() - (glatRad).Cosine() * ((27.4).ToRadians()).Sine() * (glongRad - (33.0).ToRadians()).Sine();

			var raDeg1 = PAMacros.Degrees(y.AngleTangent2(x)) + 192.25;
			var raDeg2 = raDeg1 - 360 * (raDeg1 / 360).Floor();
			var raHours1 = PAMacros.DecimalDegreesToDegreeHours(raDeg2);

			var raHours = PAMacros.DecimalHoursHour(raHours1);
			var raMinutes = PAMacros.DecimalHoursMinute(raHours1);
			var raSeconds = PAMacros.DecimalHoursSecond(raHours1);
			var decDegrees = PAMacros.DecimalDegreesDegrees(decDeg);
			var decMinutes = PAMacros.DecimalDegreesMinutes(decDeg);
			var decSeconds = PAMacros.DecimalDegreesSeconds(decDeg);

			return (raHours, raMinutes, raSeconds, decDegrees, decMinutes, decSeconds);
		}

		/// <summary>
		/// Calculate the angle between two celestial objects
		/// </summary>
		/// <param name="raLong1HourDeg"></param>
		/// <param name="raLong1Min"></param>
		/// <param name="raLong1Sec"></param>
		/// <param name="decLat1Deg"></param>
		/// <param name="decLat1Min"></param>
		/// <param name="decLat1Sec"></param>
		/// <param name="raLong2HourDeg"></param>
		/// <param name="raLong2Min"></param>
		/// <param name="raLong2Sec"></param>
		/// <param name="decLat2Deg"></param>
		/// <param name="decLat2Min"></param>
		/// <param name="decLat2Sec"></param>
		/// <param name="hourOrDegree"></param>
		/// <returns>Tuple (angleDeg, angleMin, angleSec)</returns>
		public (double angleDeg, double angleMin, double angleSec) AngleBetweenTwoObjects(double raLong1HourDeg, double raLong1Min, double raLong1Sec, double decLat1Deg, double decLat1Min, double decLat1Sec, double raLong2HourDeg, double raLong2Min, double raLong2Sec, double decLat2Deg, double decLat2Min, double decLat2Sec, string hourOrDegree)
		{
			var raLong1Decimal = (hourOrDegree.Equals("H")) ? PAMacros.HMStoDH(raLong1HourDeg, raLong1Min, raLong1Sec) : PAMacros.DegreesMinutesSecondsToDecimalDegrees(raLong1HourDeg, raLong1Min, raLong1Sec);
			var raLong1Deg = (hourOrDegree.Equals("H")) ? PAMacros.DegreeHoursToDecimalDegrees(raLong1Decimal) : raLong1Decimal;

			var raLong1Rad = raLong1Deg.ToRadians();
			var decLat1Deg1 = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decLat1Deg, decLat1Min, decLat1Sec);
			var decLat1Rad = decLat1Deg1.ToRadians();

			var raLong2Decimal = (hourOrDegree.Equals("H")) ? PAMacros.HMStoDH(raLong2HourDeg, raLong2Min, raLong2Sec) : PAMacros.DegreesMinutesSecondsToDecimalDegrees(raLong2HourDeg, raLong2Min, raLong2Sec);
			var raLong2Deg = (hourOrDegree.Equals("H")) ? PAMacros.DegreeHoursToDecimalDegrees(raLong2Decimal) : raLong2Decimal;
			var raLong2Rad = raLong2Deg.ToRadians();
			var decLat2Deg1 = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decLat2Deg, decLat2Min, decLat2Sec);
			var decLat2Rad = decLat2Deg1.ToRadians();

			var cosD = decLat1Rad.Sine() * decLat2Rad.Sine() + decLat1Rad.Cosine() * decLat2Rad.Cosine() * (raLong1Rad - raLong2Rad).Cosine();
			var dRad = cosD.ACosine();
			var dDeg = PAMacros.Degrees(dRad);

			var angleDeg = PAMacros.DecimalDegreesDegrees(dDeg);
			var angleMin = PAMacros.DecimalDegreesMinutes(dDeg);
			var angleSec = PAMacros.DecimalDegreesSeconds(dDeg);

			return (angleDeg, angleMin, angleSec);
		}


		/// <summary>
		/// Calculate rising and setting times for an object.
		/// </summary>
		/// <param name="raHours"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="decDeg"></param>
		/// <param name="decMin"></param>
		/// <param name="decSec"></param>
		/// <param name="gwDateDay"></param>
		/// <param name="gwDateMonth"></param>
		/// <param name="gwDateYear"></param>
		/// <param name="geogLongDeg"></param>
		/// <param name="geogLatDeg"></param>
		/// <param name="vertShiftDeg"></param>
		/// <returns>Tuple (riseSetStatus, utRiseHour, utRiseMin, utSetHour, utSetMin, azRise, azSet)</returns>
		public (string riseSetStatus, double utRiseHour, double utRiseMin, double utSetHour, double utSetMin, double azRise, double azSet) RisingAndSetting(double raHours, double raMinutes, double raSeconds, double decDeg, double decMin, double decSec, double gwDateDay, int gwDateMonth, int gwDateYear, double geogLongDeg, double geogLatDeg, double vertShiftDeg)
		{
			var raHours1 = PAMacros.HMStoDH(raHours, raMinutes, raSeconds);
			var decRad = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMin, decSec).ToRadians();
			var verticalDisplRadians = vertShiftDeg.ToRadians();
			var geoLatRadians = geogLatDeg.ToRadians();
			var cosH = -(verticalDisplRadians.Sine() + geoLatRadians.Sine() * decRad.Sine()) / (geoLatRadians.Cosine() * decRad.Cosine());
			var hHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.Degrees(cosH.ACosine()));
			var lstRiseHours = (raHours1 - hHours) - 24 * ((raHours1 - hHours) / 24).Floor();
			var lstSetHours = (raHours1 + hHours) - 24 * ((raHours1 + hHours) / 24).Floor();
			var aDeg = PAMacros.Degrees((((decRad).Sine() + (verticalDisplRadians).Sine() * (geoLatRadians).Sine()) / ((verticalDisplRadians).Cosine() * (geoLatRadians).Cosine())).ACosine());
			var azRiseDeg = aDeg - 360 * (aDeg / 360).Floor();
			var azSetDeg = (360 - aDeg) - 360 * ((360 - aDeg) / 360).Floor();
			var utRiseHours1 = PAMacros.GreenwichSiderealTimeToUniversalTime(PAMacros.LocalSiderealTimeToGreenwichSiderealTime(lstRiseHours, 0, 0, geogLongDeg), 0, 0, gwDateDay, gwDateMonth, gwDateYear);
			var utSetHours1 = PAMacros.GreenwichSiderealTimeToUniversalTime(PAMacros.LocalSiderealTimeToGreenwichSiderealTime(lstSetHours, 0, 0, geogLongDeg), 0, 0, gwDateDay, gwDateMonth, gwDateYear);
			var utRiseAdjustedHours = utRiseHours1 + 0.008333;
			var utSetAdjustedHours = utSetHours1 + 0.008333;

			var riseSetStatus = "OK";
			if (cosH > 1)
				riseSetStatus = "never rises";
			if (cosH < -1)
				riseSetStatus = "circumpolar";

			var utRiseHour = (riseSetStatus == "OK") ? PAMacros.DecimalHoursHour(utRiseAdjustedHours) : 0;
			var utRiseMin = (riseSetStatus == "OK") ? PAMacros.DecimalHoursMinute(utRiseAdjustedHours) : 0;
			var utSetHour = (riseSetStatus == "OK") ? PAMacros.DecimalHoursHour(utSetAdjustedHours) : 0;
			var utSetMin = (riseSetStatus == "OK") ? PAMacros.DecimalHoursMinute(utSetAdjustedHours) : 0;
			var azRise = (riseSetStatus == "OK") ? Math.Round(azRiseDeg, 2) : 0;
			var azSet = (riseSetStatus == "OK") ? Math.Round(azSetDeg, 2) : 0;

			return (riseSetStatus, utRiseHour, utRiseMin, utSetHour, utSetMin, azRise, azSet);
		}

		/// <summary>
		/// Calculate precession (corrected coordinates between two epochs)
		/// </summary>
		/// <param name="raHour"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="decDeg"></param>
		/// <param name="decMinutes"></param>
		/// <param name="decSeconds"></param>
		/// <param name="epoch1Day"></param>
		/// <param name="epoch1Month"></param>
		/// <param name="epoch1Year"></param>
		/// <param name="epoch2Day"></param>
		/// <param name="epoch2Month"></param>
		/// <param name="epoch2Year"></param>
		/// <returns>Tuple (correctedRAHour, correctedRAMinutes, correctedRASeconds, correctedDecDeg, correctedDecMinutes, correctedDecSeconds)</returns>
		public (double correctedRAHour, double correctedRAMinutes, double correctedRASeconds, double correctedDecDeg, double correctedDecMinutes, double correctedDecSeconds) CorrectForPrecession(double raHour, double raMinutes, double raSeconds, double decDeg, double decMinutes, double decSeconds, double epoch1Day, int epoch1Month, int epoch1Year, double epoch2Day, int epoch2Month, int epoch2Year)
		{
			var ra1Rad = (PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHour, raMinutes, raSeconds))).ToRadians();
			var dec1Rad = (PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMinutes, decSeconds)).ToRadians();
			var tCenturies = (PAMacros.CivilDateToJulianDate(epoch1Day, epoch1Month, epoch1Year) - 2415020) / 36525;
			var mSec = 3.07234 + (0.00186 * tCenturies);
			var nArcsec = 20.0468 - (0.0085 * tCenturies);
			var nYears = (PAMacros.CivilDateToJulianDate(epoch2Day, epoch2Month, epoch2Year) - PAMacros.CivilDateToJulianDate(epoch1Day, epoch1Month, epoch1Year)) / 365.25;
			var s1Hours = ((mSec + (nArcsec * (ra1Rad).Sine() * (dec1Rad).Tangent() / 15)) * nYears) / 3600;
			var ra2Hours = PAMacros.HMStoDH(raHour, raMinutes, raSeconds) + s1Hours;
			var s2Deg = (nArcsec * (ra1Rad).Cosine() * nYears) / 3600;
			var dec2Deg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMinutes, decSeconds) + s2Deg;

			var correctedRAHour = PAMacros.DecimalHoursHour(ra2Hours);
			var correctedRAMinutes = PAMacros.DecimalHoursMinute(ra2Hours);
			var correctedRASeconds = PAMacros.DecimalHoursSecond(ra2Hours);
			var correctedDecDeg = PAMacros.DecimalDegreesDegrees(dec2Deg);
			var correctedDecMinutes = PAMacros.DecimalDegreesMinutes(dec2Deg);
			var correctedDecSeconds = PAMacros.DecimalDegreesSeconds(dec2Deg);

			return (correctedRAHour, correctedRAMinutes, correctedRASeconds, correctedDecDeg, correctedDecMinutes, correctedDecSeconds);
		}

		/// <summary>
		/// Calculate nutation for two values: ecliptic longitude and obliquity, for a Greenwich date.
		/// </summary>
		/// <param name="greenwich_day"></param>
		/// <param name="greenwich_month"></param>
		/// <param name="greenwich_year"></param>
		/// <returns>Tuple (nutation in ecliptic longitude (degrees), nutation in obliquity (degrees))</returns>
		public (double nutInLongDeg, double nutInOblDeg) NutationInEclipticLongitudeAndObliquity(double greenwichDay, int greenwichMonth, int greenwichYear)
		{
			var jdDays = PAMacros.CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
			var tCenturies = (jdDays - 2415020) / 36525;
			var aDeg = 100.0021358 * tCenturies;
			var l1Deg = 279.6967 + (0.000303 * tCenturies * tCenturies);
			var lDeg1 = l1Deg + 360 * (aDeg - aDeg.Floor());
			var lDeg2 = lDeg1 - 360 * (lDeg1 / 360).Floor();
			var lRad = lDeg2.ToRadians();
			var bDeg = 5.372617 * tCenturies;
			var nDeg1 = 259.1833 - 360 * (bDeg - bDeg.Floor());
			var nDeg2 = nDeg1 - 360 * ((nDeg1 / 360).Floor());
			var nRad = nDeg2.ToRadians();
			var nutInLongArcsec = -17.2 * nRad.Sine() - 1.3 * (2 * lRad).Sine();
			var nutInOblArcsec = 9.2 * nRad.Cosine() + 0.5 * (2 * lRad).Cosine();

			var nutInLongDeg = nutInLongArcsec / 3600;
			var nutInOblDeg = nutInOblArcsec / 3600;

			return (nutInLongDeg, nutInOblDeg);
		}

		/// <summary>
		/// Correct ecliptic coordinates for the effects of aberration.
		/// </summary>
		/// <param name="utHour"></param>
		/// <param name="utMinutes"></param>
		/// <param name="utSeconds"></param>
		/// <param name="gwDay"></param>
		/// <param name="gwMonth"></param>
		/// <param name="gwYear"></param>
		/// <param name="trueEclLongDeg"></param>
		/// <param name="trueEclLongMin"></param>
		/// <param name="trueEclLongSec"></param>
		/// <param name="trueEclLatDeg"></param>
		/// <param name="trueEclLatMin"></param>
		/// <param name="trueEclLatSec"></param>
		/// <returns>
		/// apparent ecliptic longitude (degrees, minutes, seconds),
		/// apparent ecliptic latitude (degrees, minutes, seconds)
		/// </returns>
		public (double apparentEclLongDeg, double apparentEclLongMin, double apparentEclLongSec, double apparentEclLatDeg, double apparentEclLatMin, double apparentEclLatSec) CorrectForAberration(double utHour, double utMinutes, double utSeconds, double gwDay, int gwMonth, int gwYear, double trueEclLongDeg, double trueEclLongMin, double trueEclLongSec, double trueEclLatDeg, double trueEclLatMin, double trueEclLatSec)
		{
			var trueLongDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(trueEclLongDeg, trueEclLongMin, trueEclLongSec);
			var trueLatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(trueEclLatDeg, trueEclLatMin, trueEclLatSec);
			var sunTrueLongDeg = PAMacros.SunLong(utHour, utMinutes, utSeconds, 0, 0, gwDay, gwMonth, gwYear);
			var dlongArcsec = -20.5 * ((sunTrueLongDeg - trueLongDeg).ToRadians()).Cosine() / ((trueLatDeg).ToRadians()).Cosine();
			var dlatArcsec = -20.5 * ((sunTrueLongDeg - trueLongDeg).ToRadians()).Sine() * ((trueLatDeg).ToRadians()).Sine();
			var apparentLongDeg = trueLongDeg + (dlongArcsec / 3600);
			var apparentLatDeg = trueLatDeg + (dlatArcsec / 3600);

			var apparentEclLongDeg = PAMacros.DecimalDegreesDegrees(apparentLongDeg);
			var apparentEclLongMin = PAMacros.DecimalDegreesMinutes(apparentLongDeg);
			var apparentEclLongSec = PAMacros.DecimalDegreesSeconds(apparentLongDeg);
			var apparentEclLatDeg = PAMacros.DecimalDegreesDegrees(apparentLatDeg);
			var apparentEclLatMin = PAMacros.DecimalDegreesMinutes(apparentLatDeg);
			var apparentEclLatSec = PAMacros.DecimalDegreesSeconds(apparentLatDeg);

			return (apparentEclLongDeg, apparentEclLongMin, apparentEclLongSec, apparentEclLatDeg, apparentEclLatMin, apparentEclLatSec);
		}


		/// <summary>
		/// Calculate corrected RA/Dec, accounting for atmospheric refraction.
		/// </summary>
		/// <remarks>
		/// NOTE: Valid values for coordinate_type are "TRUE" and "APPARENT".
		/// </remarks>
		/// <param name="trueRAHour"></param>
		/// <param name="trueRAMin"></param>
		/// <param name="trueRASec"></param>
		/// <param name="trueDecDeg"></param>
		/// <param name="trueDecMin"></param>
		/// <param name="trueDecSec"></param>
		/// <param name="coordinateType"></param>
		/// <param name="geogLongDeg"></param>
		/// <param name="geogLatDeg"></param>
		/// <param name="daylightSavingHours"></param>
		/// <param name="timezoneHours"></param>
		/// <param name="lcdDay"></param>
		/// <param name="lcdMonth"></param>
		/// <param name="lcdYear"></param>
		/// <param name="lctHour"></param>
		/// <param name="lctMin"></param>
		/// <param name="lctSec"></param>
		/// <param name="atmosphericPressureMbar"></param>
		/// <param name="atmosphericTemperatureCelsius"></param>
		/// <returns>
		/// corrected RA hours,minutes,seconds,
		/// corrected Declination degrees,minutes,seconds
		/// </returns>
		public (double correctedRAHour, double correctedRAMin, double correctedRASec, double correctedDecDeg, double correctedDecMin, double correctedDecSec) AtmosphericRefraction(double trueRAHour, double trueRAMin, double trueRASec, double trueDecDeg, double trueDecMin, double trueDecSec, string coordinateType, double geogLongDeg, double geogLatDeg, int daylightSavingHours, int timezoneHours, double lcdDay, int lcdMonth, int lcdYear, double lctHour, double lctMin, double lctSec, double atmosphericPressureMbar, double atmosphericTemperatureCelsius)
		{
			var haHour = PAMacros.RightAscensionToHourAngle(trueRAHour, trueRAMin, trueRASec, lctHour, lctMin, lctSec, daylightSavingHours, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);
			var azimuthDeg = PAMacros.EquatorialCoordinatesToAzimuth(haHour, 0, 0, trueDecDeg, trueDecMin, trueDecSec, geogLatDeg);
			var altitudeDeg = PAMacros.EquatorialCoordinatesToAltitude(haHour, 0, 0, trueDecDeg, trueDecMin, trueDecSec, geogLatDeg);
			var correctedAltitudeDeg = PAMacros.Refract(altitudeDeg, coordinateType, atmosphericPressureMbar, atmosphericTemperatureCelsius);

			var correctedHAHour = PAMacros.HorizonCoordinatesToHourAngle(azimuthDeg, 0, 0, correctedAltitudeDeg, 0, 0, geogLatDeg);
			var correctedRAHour1 = PAMacros.HourAngleToRightAscension(correctedHAHour, 0, 0, lctHour, lctMin, lctSec, daylightSavingHours, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);
			var correctedDecDeg1 = PAMacros.HorizonCoordinatesToDeclination(azimuthDeg, 0, 0, correctedAltitudeDeg, 0, 0, geogLatDeg);

			var correctedRAHour = PAMacros.DecimalHoursHour(correctedRAHour1);
			var correctedRAMin = PAMacros.DecimalHoursMinute(correctedRAHour1);
			var correctedRASec = PAMacros.DecimalHoursSecond(correctedRAHour1);
			var correctedDecDeg = PAMacros.DecimalDegreesDegrees(correctedDecDeg1);
			var correctedDecMin = PAMacros.DecimalDegreesMinutes(correctedDecDeg1);
			var correctedDecSec = PAMacros.DecimalDegreesSeconds(correctedDecDeg1);

			return (correctedRAHour, correctedRAMin, correctedRASec, correctedDecDeg, correctedDecMin, correctedDecSec);
		}
	}
}