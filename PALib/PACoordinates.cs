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
		/// <returns></returns>
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
			var raRad = y.AngleTangent(x);
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
		/// <returns></returns>
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
			var eclLongRad = y.AngleTangent(x);
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
		/// <returns></returns>
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
			var longDeg1 = PAMacros.Degrees(y.AngleTangent(x)) + 33;
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
		/// <returns></returns>
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

			var raDeg1 = PAMacros.Degrees(y.AngleTangent(x)) + 192.25;
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
		/// <returns></returns>
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
		/// <returns></returns>
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


	}
}