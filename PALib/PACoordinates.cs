using System;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Coordinate system calculations and conversions.
/// </summary>
public class PACoordinates
{
	/// <summary>
	/// Convert an Angle (degrees, minutes, and seconds) to Decimal Degrees
	/// </summary>
	/// <returns>Decimal Degrees (double)</returns>
	public double AngleToDecimalDegrees(double degrees, double minutes, double seconds)
	{
		double a = Math.Abs(seconds) / 60;
		double b = (Math.Abs(minutes) + a) / 60;
		double c = Math.Abs(degrees) + b;
		double d = (degrees < 0 || minutes < 0 || seconds < 0) ? -c : c;

		return d;
	}

	/// <summary>
	/// Convert Decimal Degrees to an Angle (degrees, minutes, and seconds)
	/// </summary>
	/// <returns>Tuple (degrees, minutes, seconds)</returns>
	public (double degrees, double minutes, double seconds) DecimalDegreesToAngle(double decimalDegrees)
	{
		double unsignedDecimal = Math.Abs(decimalDegrees);
		double totalSeconds = unsignedDecimal * 3600;
		double seconds2DP = Math.Round(totalSeconds % 60, 2);
		double correctedSeconds = (seconds2DP == 60) ? 0 : seconds2DP;
		double correctedRemainder = (seconds2DP == 60) ? totalSeconds + 60 : totalSeconds;
		double minutes = (correctedRemainder / 60).Floor() % 60;
		double unsignedDegrees = (correctedRemainder / 3600).Floor();
		double signedDegrees = (decimalDegrees < 0) ? -1 * unsignedDegrees : unsignedDegrees;

		return (signedDegrees, minutes, correctedSeconds.Floor());
	}

	/// <summary>
	/// Convert Right Ascension to Hour Angle
	/// </summary>
	/// <returns>Tuple (hourAngleHours, hourAngleMinutes, hourAngleSeconds)</returns>
	public (double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds) RightAscensionToHourAngle(double raHours, double raMinutes, double raSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSavings, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
	{
		int daylightSaving = isDaylightSavings ? 1 : 0;

		double hourAngle = PAMacros.RightAscensionToHourAngle(raHours, raMinutes, raSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

		int hourAngleHours = PAMacros.DecimalHoursHour(hourAngle);
		int hourAngleMinutes = PAMacros.DecimalHoursMinute(hourAngle);
		double hourAngleSeconds = PAMacros.DecimalHoursSecond(hourAngle);

		return (hourAngleHours, hourAngleMinutes, hourAngleSeconds);
	}

	/// <summary>
	/// Convert Hour Angle to Right Ascension
	/// </summary>
	/// <returns>Tuple (rightAscensionHours, rightAscensionMinutes, rightAscensionSeconds)</returns>
	public (double raHours, double raMinutes, double raSeconds) HourAngleToRightAscension(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double rightAscension = PAMacros.HourAngleToRightAscension(hourAngleHours, hourAngleMinutes, hourAngleSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

		int rightAscensionHours = PAMacros.DecimalHoursHour(rightAscension);
		int rightAscensionMinutes = PAMacros.DecimalHoursMinute(rightAscension);
		double rightAscensionSeconds = PAMacros.DecimalHoursSecond(rightAscension);

		return (rightAscensionHours, rightAscensionMinutes, rightAscensionSeconds);
	}

	/// <summary>
	/// Convert Equatorial Coordinates to Horizon Coordinates
	/// </summary>
	/// <returns>Tuple (azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds)</returns>
	public (double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds) EquatorialCoordinatesToHorizonCoordinates(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double declinationDegrees, double declinationMinutes, double declinationSeconds, double geographicalLatitude)
	{
		double azimuthInDecimalDegrees = PAMacros.EquatorialCoordinatesToAzimuth(hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds, geographicalLatitude);

		double altitudeInDecimalDegrees = PAMacros.EquatorialCoordinatesToAltitude(hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds, geographicalLatitude);

		double azimuthDegrees = PAMacros.DecimalDegreesDegrees(azimuthInDecimalDegrees);
		double azimuthMinutes = PAMacros.DecimalDegreesMinutes(azimuthInDecimalDegrees);
		double azimuthSeconds = PAMacros.DecimalDegreesSeconds(azimuthInDecimalDegrees);

		double altitudeDegrees = PAMacros.DecimalDegreesDegrees(altitudeInDecimalDegrees);
		double altitudeMinutes = PAMacros.DecimalDegreesMinutes(altitudeInDecimalDegrees);
		double altitudeSeconds = PAMacros.DecimalDegreesSeconds(altitudeInDecimalDegrees);

		return (azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds);
	}

	/// <summary>
	/// Convert Horizon Coordinates to Equatorial Coordinates
	/// </summary>
	/// <returns>Tuple (hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds)</returns>
	public (double hour_angle_hours, double hour_angle_minutes, double hour_angle_seconds, double declination_degrees, double declination_minutes, double declinationseconds) HorizonCoordinatesToEquatorialCoordinates(double azimuthDegrees, double azimuthMinutes, double azimuthSeconds, double altitudeDegrees, double altitudeMinutes, double altitudeSeconds, double geographicalLatitude)
	{
		double hourAngleInDecimalDegrees = PAMacros.HorizonCoordinatesToHourAngle(azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds, geographicalLatitude);

		double declinationInDecimalDegrees = PAMacros.HorizonCoordinatesToDeclination(azimuthDegrees, azimuthMinutes, azimuthSeconds, altitudeDegrees, altitudeMinutes, altitudeSeconds, geographicalLatitude);

		int hourAngleHours = PAMacros.DecimalHoursHour(hourAngleInDecimalDegrees);
		int hourAngleMinutes = PAMacros.DecimalHoursMinute(hourAngleInDecimalDegrees);
		double hourAngleSeconds = PAMacros.DecimalHoursSecond(hourAngleInDecimalDegrees);

		double declinationDegrees = PAMacros.DecimalDegreesDegrees(declinationInDecimalDegrees);
		double declinationMinutes = PAMacros.DecimalDegreesMinutes(declinationInDecimalDegrees);
		double declinationSeconds = PAMacros.DecimalDegreesSeconds(declinationInDecimalDegrees);

		return (hourAngleHours, hourAngleMinutes, hourAngleSeconds, declinationDegrees, declinationMinutes, declinationSeconds);
	}

	/// <summary>
	/// Calculate Mean Obliquity of the Ecliptic for a Greenwich Date
	/// </summary>
	public double MeanObliquityOfTheEcliptic(double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double jd = PAMacros.CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
		double mjd = jd - 2451545;
		double t = mjd / 36525;
		double de1 = t * (46.815 + t * (0.0006 - (t * 0.00181)));
		double de2 = de1 / 3600;

		return 23.439292 - de2;
	}

	/// <summary>
	/// Convert Ecliptic Coordinates to Equatorial Coordinates
	/// </summary>
	/// <returns>Tuple (outRAHours, outRAMinutes, outRASeconds, outDecDegrees, outDecMinutes, outDecSeconds)</returns>
	public (double outRAHours, double outRAMinutes, double outRASeconds, double outDecDegrees, double outDecMinutes, double outDecSeconds) EclipticCoordinateToEquatorialCoordinate(double eclipticLongitudeDegrees, double eclipticLongitudeMinutes, double eclipticLongitudeSeconds, double eclipticLatitudeDegrees, double eclipticLatitudeMinutes, double eclipticLatitudeSeconds, double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double eclonDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLongitudeDegrees, eclipticLongitudeMinutes, eclipticLongitudeSeconds);
		double eclatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(eclipticLatitudeDegrees, eclipticLatitudeMinutes, eclipticLatitudeSeconds);
		double eclonRad = eclonDeg.ToRadians();
		double eclatRad = eclatDeg.ToRadians();
		double obliqDeg = PAMacros.Obliq(greenwichDay, greenwichMonth, greenwichYear);
		double obliqRad = obliqDeg.ToRadians();
		double sinDec = eclatRad.Sine() * obliqRad.Cosine() + eclatRad.Cosine() * obliqRad.Sine() * eclonRad.Sine();
		double decRad = sinDec.ASine();
		double decDeg = PAMacros.Degrees(decRad);
		double y = eclonRad.Sine() * obliqRad.Cosine() - eclatRad.Tangent() * obliqRad.Sine();
		double x = eclonRad.Cosine();
		double raRad = y.AngleTangent2(x);
		double raDeg1 = PAMacros.Degrees(raRad);
		double raDeg2 = raDeg1 - 360 * (raDeg1 / 360).Floor();
		double raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg2);

		int outRAHours = PAMacros.DecimalHoursHour(raHours);
		int outRAMinutes = PAMacros.DecimalHoursMinute(raHours);
		double outRASeconds = PAMacros.DecimalHoursSecond(raHours);
		double outDecDegrees = PAMacros.DecimalDegreesDegrees(decDeg);
		double outDecMinutes = PAMacros.DecimalDegreesMinutes(decDeg);
		double outDecSeconds = PAMacros.DecimalDegreesSeconds(decDeg);

		return (outRAHours, outRAMinutes, outRASeconds, outDecDegrees, outDecMinutes, outDecSeconds);
	}

	/// <summary>
	/// Convert Equatorial Coordinates to Ecliptic Coordinates
	/// </summary>
	/// <returns>Tuple (outEclLongDeg, outEclLongMin, outEclLongSec, outEclLatDeg, outEclLatMin, outEclLatSec)</returns>
	public (double outEclLongDeg, double outEclLongMin, double outEclLongSec, double outEclLatDeg, double outEclLatMin, double outEclLatSec) EquatorialCoordinateToEclipticCoordinate(double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds, double gwDay, int gwMonth, int gwYear)
	{
		double raDeg = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHours, raMinutes, raSeconds));
		double decDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDegrees, decMinutes, decSeconds);
		double raRad = raDeg.ToRadians();
		double decRad = decDeg.ToRadians();
		double obliqDeg = PAMacros.Obliq(gwDay, gwMonth, gwYear);
		double obliqRad = obliqDeg.ToRadians();
		double sinEclLat = decRad.Sine() * obliqRad.Cosine() - decRad.Cosine() * obliqRad.Sine() * raRad.Sine();
		double eclLatRad = sinEclLat.ASine();
		double eclLatDeg = PAMacros.Degrees(eclLatRad);
		double y = raRad.Sine() * obliqRad.Cosine() + decRad.Tangent() * obliqRad.Sine();
		double x = raRad.Cosine();
		double eclLongRad = y.AngleTangent2(x);
		double eclLongDeg1 = PAMacros.Degrees(eclLongRad);
		double eclLongDeg2 = eclLongDeg1 - 360 * (eclLongDeg1 / 360).Floor();

		double outEclLongDeg = PAMacros.DecimalDegreesDegrees(eclLongDeg2);
		double outEclLongMin = PAMacros.DecimalDegreesMinutes(eclLongDeg2);
		double outEclLongSec = PAMacros.DecimalDegreesSeconds(eclLongDeg2);
		double outEclLatDeg = PAMacros.DecimalDegreesDegrees(eclLatDeg);
		double outEclLatMin = PAMacros.DecimalDegreesMinutes(eclLatDeg);
		double outEclLatSec = PAMacros.DecimalDegreesSeconds(eclLatDeg);

		return (outEclLongDeg, outEclLongMin, outEclLongSec, outEclLatDeg, outEclLatMin, outEclLatSec);
	}

	/// <summary>
	/// Convert Equatorial Coordinates to Galactic Coordinates
	/// </summary>
	/// <returns>Tuple (galLongDeg, galLongMin, galLongSec, galLatDeg, galLatMin, galLatSec)</returns>
	public (double galLongDeg, double galLongMin, double galLongSec, double galLatDeg, double galLatMin, double galLatSec) EquatorialCoordinateToGalacticCoordinate(double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds)
	{
		double raDeg = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHours, raMinutes, raSeconds));
		double decDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDegrees, decMinutes, decSeconds);
		double raRad = raDeg.ToRadians();
		double decRad = decDeg.ToRadians();
		double sinB = decRad.Cosine() * 27.4.ToRadians().Cosine() * (raRad - 192.25.ToRadians()).Cosine() + decRad.Sine() * 27.4.ToRadians().Sine();
		double bRadians = sinB.ASine();
		double bDeg = PAMacros.Degrees(bRadians);
		double y = decRad.Sine() - sinB * 27.4.ToRadians().Sine();
		double x = decRad.Cosine() * (raRad - 192.25.ToRadians()).Sine() * 27.4.ToRadians().Cosine();
		double longDeg1 = PAMacros.Degrees(y.AngleTangent2(x)) + 33;
		double longDeg2 = longDeg1 - 360 * (longDeg1 / 360).Floor();

		double galLongDeg = PAMacros.DecimalDegreesDegrees(longDeg2);
		double galLongMin = PAMacros.DecimalDegreesMinutes(longDeg2);
		double galLongSec = PAMacros.DecimalDegreesSeconds(longDeg2);
		double galLatDeg = PAMacros.DecimalDegreesDegrees(bDeg);
		double galLatMin = PAMacros.DecimalDegreesMinutes(bDeg);
		double galLatSec = PAMacros.DecimalDegreesSeconds(bDeg);

		return (galLongDeg, galLongMin, galLongSec, galLatDeg, galLatMin, galLatSec);
	}

	/// <summary>
	/// Convert Galactic Coordinates to Equatorial Coordinates
	/// </summary>
	/// <returns>Tuple (raHours, raMinutes, raSeconds, decDegrees, decMinutes, decSeconds)</returns>
	public (double raHours, double raMinutes, double raSeconds, double decDegrees, double decMinutes, double decSeconds) GalacticCoordinateToEquatorialCoordinate(double galLongDeg, double galLongMin, double galLongSec, double galLatDeg, double galLatMin, double galLatSec)
	{
		double glongDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(galLongDeg, galLongMin, galLongSec);
		double glatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(galLatDeg, galLatMin, galLatSec);
		double glongRad = glongDeg.ToRadians();
		double glatRad = glatDeg.ToRadians();
		double sinDec = glatRad.Cosine() * 27.4.ToRadians().Cosine() * (glongRad - 33.0.ToRadians()).Sine() + glatRad.Sine() * 27.4.ToRadians().Sine();
		double decRadians = sinDec.ASine();
		double decDeg = PAMacros.Degrees(decRadians);
		double y = glatRad.Cosine() * (glongRad - 33.0.ToRadians()).Cosine();
		double x = glatRad.Sine() * 27.4.ToRadians().Cosine() - glatRad.Cosine() * 27.4.ToRadians().Sine() * (glongRad - 33.0.ToRadians()).Sine();

		double raDeg1 = PAMacros.Degrees(y.AngleTangent2(x)) + 192.25;
		double raDeg2 = raDeg1 - 360 * (raDeg1 / 360).Floor();
		double raHours1 = PAMacros.DecimalDegreesToDegreeHours(raDeg2);

		int raHours = PAMacros.DecimalHoursHour(raHours1);
		int raMinutes = PAMacros.DecimalHoursMinute(raHours1);
		double raSeconds = PAMacros.DecimalHoursSecond(raHours1);
		double decDegrees = PAMacros.DecimalDegreesDegrees(decDeg);
		double decMinutes = PAMacros.DecimalDegreesMinutes(decDeg);
		double decSeconds = PAMacros.DecimalDegreesSeconds(decDeg);

		return (raHours, raMinutes, raSeconds, decDegrees, decMinutes, decSeconds);
	}

	/// <summary>
	/// Calculate the angle between two celestial objects
	/// </summary>
	/// <returns>Tuple (angleDeg, angleMin, angleSec)</returns>
	public (double angleDeg, double angleMin, double angleSec) AngleBetweenTwoObjects(double raLong1HourDeg, double raLong1Min, double raLong1Sec, double decLat1Deg, double decLat1Min, double decLat1Sec, double raLong2HourDeg, double raLong2Min, double raLong2Sec, double decLat2Deg, double decLat2Min, double decLat2Sec, PAAngleMeasure hourOrDegree)
	{
		double raLong1Decimal = (hourOrDegree == PAAngleMeasure.Hours) ? PAMacros.HMStoDH(raLong1HourDeg, raLong1Min, raLong1Sec) : PAMacros.DegreesMinutesSecondsToDecimalDegrees(raLong1HourDeg, raLong1Min, raLong1Sec);
		double raLong1Deg = (hourOrDegree == PAAngleMeasure.Hours) ? PAMacros.DegreeHoursToDecimalDegrees(raLong1Decimal) : raLong1Decimal;

		double raLong1Rad = raLong1Deg.ToRadians();
		double decLat1Deg1 = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decLat1Deg, decLat1Min, decLat1Sec);
		double decLat1Rad = decLat1Deg1.ToRadians();

		double raLong2Decimal = (hourOrDegree == PAAngleMeasure.Hours) ? PAMacros.HMStoDH(raLong2HourDeg, raLong2Min, raLong2Sec) : PAMacros.DegreesMinutesSecondsToDecimalDegrees(raLong2HourDeg, raLong2Min, raLong2Sec);
		double raLong2Deg = (hourOrDegree == PAAngleMeasure.Hours) ? PAMacros.DegreeHoursToDecimalDegrees(raLong2Decimal) : raLong2Decimal;
		double raLong2Rad = raLong2Deg.ToRadians();
		double decLat2Deg1 = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decLat2Deg, decLat2Min, decLat2Sec);
		double decLat2Rad = decLat2Deg1.ToRadians();

		double cosD = decLat1Rad.Sine() * decLat2Rad.Sine() + decLat1Rad.Cosine() * decLat2Rad.Cosine() * (raLong1Rad - raLong2Rad).Cosine();
		double dRad = cosD.ACosine();
		double dDeg = PAMacros.Degrees(dRad);

		double angleDeg = PAMacros.DecimalDegreesDegrees(dDeg);
		double angleMin = PAMacros.DecimalDegreesMinutes(dDeg);
		double angleSec = PAMacros.DecimalDegreesSeconds(dDeg);

		return (angleDeg, angleMin, angleSec);
	}


	/// <summary>
	/// Calculate rising and setting times for an object.
	/// </summary>
	/// <returns>Tuple (riseSetStatus, utRiseHour, utRiseMin, utSetHour, utSetMin, azRise, azSet)</returns>
	public (string riseSetStatus, double utRiseHour, double utRiseMin, double utSetHour, double utSetMin, double azRise, double azSet) RisingAndSetting(double raHours, double raMinutes, double raSeconds, double decDeg, double decMin, double decSec, double gwDateDay, int gwDateMonth, int gwDateYear, double geogLongDeg, double geogLatDeg, double vertShiftDeg)
	{
		double raHours1 = PAMacros.HMStoDH(raHours, raMinutes, raSeconds);
		double decRad = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMin, decSec).ToRadians();
		double verticalDisplRadians = vertShiftDeg.ToRadians();
		double geoLatRadians = geogLatDeg.ToRadians();
		double cosH = -(verticalDisplRadians.Sine() + geoLatRadians.Sine() * decRad.Sine()) / (geoLatRadians.Cosine() * decRad.Cosine());
		double hHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.Degrees(cosH.ACosine()));
		double lstRiseHours = raHours1 - hHours - 24 * ((raHours1 - hHours) / 24).Floor();
		double lstSetHours = raHours1 + hHours - 24 * ((raHours1 + hHours) / 24).Floor();
		double aDeg = PAMacros.Degrees(((decRad.Sine() + verticalDisplRadians.Sine() * geoLatRadians.Sine()) / (verticalDisplRadians.Cosine() * geoLatRadians.Cosine())).ACosine());
		double azRiseDeg = aDeg - 360 * (aDeg / 360).Floor();
		double azSetDeg = 360 - aDeg - 360 * ((360 - aDeg) / 360).Floor();
		double utRiseHours1 = PAMacros.GreenwichSiderealTimeToUniversalTime(PAMacros.LocalSiderealTimeToGreenwichSiderealTime(lstRiseHours, 0, 0, geogLongDeg), 0, 0, gwDateDay, gwDateMonth, gwDateYear);
		double utSetHours1 = PAMacros.GreenwichSiderealTimeToUniversalTime(PAMacros.LocalSiderealTimeToGreenwichSiderealTime(lstSetHours, 0, 0, geogLongDeg), 0, 0, gwDateDay, gwDateMonth, gwDateYear);
		double utRiseAdjustedHours = utRiseHours1 + 0.008333;
		double utSetAdjustedHours = utSetHours1 + 0.008333;

		string riseSetStatus = "OK";
		if (cosH > 1)
			riseSetStatus = "never rises";
		if (cosH < -1)
			riseSetStatus = "circumpolar";

		int utRiseHour = (riseSetStatus == "OK") ? PAMacros.DecimalHoursHour(utRiseAdjustedHours) : 0;
		int utRiseMin = (riseSetStatus == "OK") ? PAMacros.DecimalHoursMinute(utRiseAdjustedHours) : 0;
		int utSetHour = (riseSetStatus == "OK") ? PAMacros.DecimalHoursHour(utSetAdjustedHours) : 0;
		int utSetMin = (riseSetStatus == "OK") ? PAMacros.DecimalHoursMinute(utSetAdjustedHours) : 0;
		double azRise = (riseSetStatus == "OK") ? Math.Round(azRiseDeg, 2) : 0;
		double azSet = (riseSetStatus == "OK") ? Math.Round(azSetDeg, 2) : 0;

		return (riseSetStatus, utRiseHour, utRiseMin, utSetHour, utSetMin, azRise, azSet);
	}

	/// <summary>
	/// Calculate precession (corrected coordinates between two epochs)
	/// </summary>
	/// <returns>Tuple (correctedRAHour, correctedRAMinutes, correctedRASeconds, correctedDecDeg, correctedDecMinutes, correctedDecSeconds)</returns>
	public (double correctedRAHour, double correctedRAMinutes, double correctedRASeconds, double correctedDecDeg, double correctedDecMinutes, double correctedDecSeconds) CorrectForPrecession(double raHour, double raMinutes, double raSeconds, double decDeg, double decMinutes, double decSeconds, double epoch1Day, int epoch1Month, int epoch1Year, double epoch2Day, int epoch2Month, int epoch2Year)
	{
		double ra1Rad = PAMacros.DegreeHoursToDecimalDegrees(PAMacros.HMStoDH(raHour, raMinutes, raSeconds)).ToRadians();
		double dec1Rad = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMinutes, decSeconds).ToRadians();
		double tCenturies = (PAMacros.CivilDateToJulianDate(epoch1Day, epoch1Month, epoch1Year) - 2415020) / 36525;
		double mSec = 3.07234 + (0.00186 * tCenturies);
		double nArcsec = 20.0468 - (0.0085 * tCenturies);
		double nYears = (PAMacros.CivilDateToJulianDate(epoch2Day, epoch2Month, epoch2Year) - PAMacros.CivilDateToJulianDate(epoch1Day, epoch1Month, epoch1Year)) / 365.25;
		double s1Hours = (mSec + (nArcsec * ra1Rad.Sine() * dec1Rad.Tangent() / 15)) * nYears / 3600;
		double ra2Hours = PAMacros.HMStoDH(raHour, raMinutes, raSeconds) + s1Hours;
		double s2Deg = nArcsec * ra1Rad.Cosine() * nYears / 3600;
		double dec2Deg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(decDeg, decMinutes, decSeconds) + s2Deg;

		int correctedRAHour = PAMacros.DecimalHoursHour(ra2Hours);
		int correctedRAMinutes = PAMacros.DecimalHoursMinute(ra2Hours);
		double correctedRASeconds = PAMacros.DecimalHoursSecond(ra2Hours);
		double correctedDecDeg = PAMacros.DecimalDegreesDegrees(dec2Deg);
		double correctedDecMinutes = PAMacros.DecimalDegreesMinutes(dec2Deg);
		double correctedDecSeconds = PAMacros.DecimalDegreesSeconds(dec2Deg);

		return (correctedRAHour, correctedRAMinutes, correctedRASeconds, correctedDecDeg, correctedDecMinutes, correctedDecSeconds);
	}

	/// <summary>
	/// Calculate nutation for two values: ecliptic longitude and obliquity, for a Greenwich date.
	/// </summary>
	/// <returns>Tuple (nutation in ecliptic longitude (degrees), nutation in obliquity (degrees))</returns>
	public (double nutInLongDeg, double nutInOblDeg) NutationInEclipticLongitudeAndObliquity(double greenwichDay, int greenwichMonth, int greenwichYear)
	{
		double jdDays = PAMacros.CivilDateToJulianDate(greenwichDay, greenwichMonth, greenwichYear);
		double tCenturies = (jdDays - 2415020) / 36525;
		double aDeg = 100.0021358 * tCenturies;
		double l1Deg = 279.6967 + (0.000303 * tCenturies * tCenturies);
		double lDeg1 = l1Deg + 360 * (aDeg - aDeg.Floor());
		double lDeg2 = lDeg1 - 360 * (lDeg1 / 360).Floor();
		double lRad = lDeg2.ToRadians();
		double bDeg = 5.372617 * tCenturies;
		double nDeg1 = 259.1833 - 360 * (bDeg - bDeg.Floor());
		double nDeg2 = nDeg1 - 360 * (nDeg1 / 360).Floor();
		double nRad = nDeg2.ToRadians();
		double nutInLongArcsec = -17.2 * nRad.Sine() - 1.3 * (2 * lRad).Sine();
		double nutInOblArcsec = 9.2 * nRad.Cosine() + 0.5 * (2 * lRad).Cosine();

		double nutInLongDeg = nutInLongArcsec / 3600;
		double nutInOblDeg = nutInOblArcsec / 3600;

		return (nutInLongDeg, nutInOblDeg);
	}

	/// <summary>
	/// Correct ecliptic coordinates for the effects of aberration.
	/// </summary>
	/// <returns>
	/// apparent ecliptic longitude (degrees, minutes, seconds),
	/// apparent ecliptic latitude (degrees, minutes, seconds)
	/// </returns>
	public (double apparentEclLongDeg, double apparentEclLongMin, double apparentEclLongSec, double apparentEclLatDeg, double apparentEclLatMin, double apparentEclLatSec) CorrectForAberration(double utHour, double utMinutes, double utSeconds, double gwDay, int gwMonth, int gwYear, double trueEclLongDeg, double trueEclLongMin, double trueEclLongSec, double trueEclLatDeg, double trueEclLatMin, double trueEclLatSec)
	{
		double trueLongDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(trueEclLongDeg, trueEclLongMin, trueEclLongSec);
		double trueLatDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(trueEclLatDeg, trueEclLatMin, trueEclLatSec);
		double sunTrueLongDeg = PAMacros.SunLong(utHour, utMinutes, utSeconds, 0, 0, gwDay, gwMonth, gwYear);
		double dlongArcsec = -20.5 * (sunTrueLongDeg - trueLongDeg).ToRadians().Cosine() / trueLatDeg.ToRadians().Cosine();
		double dlatArcsec = -20.5 * (sunTrueLongDeg - trueLongDeg).ToRadians().Sine() * trueLatDeg.ToRadians().Sine();
		double apparentLongDeg = trueLongDeg + (dlongArcsec / 3600);
		double apparentLatDeg = trueLatDeg + (dlatArcsec / 3600);

		double apparentEclLongDeg = PAMacros.DecimalDegreesDegrees(apparentLongDeg);
		double apparentEclLongMin = PAMacros.DecimalDegreesMinutes(apparentLongDeg);
		double apparentEclLongSec = PAMacros.DecimalDegreesSeconds(apparentLongDeg);
		double apparentEclLatDeg = PAMacros.DecimalDegreesDegrees(apparentLatDeg);
		double apparentEclLatMin = PAMacros.DecimalDegreesMinutes(apparentLatDeg);
		double apparentEclLatSec = PAMacros.DecimalDegreesSeconds(apparentLatDeg);

		return (apparentEclLongDeg, apparentEclLongMin, apparentEclLongSec, apparentEclLatDeg, apparentEclLatMin, apparentEclLatSec);
	}


	/// <summary>
	/// Calculate corrected RA/Dec, accounting for atmospheric refraction.
	/// </summary>
	/// <remarks>
	/// NOTE: Valid values for coordinate_type are "TRUE" and "APPARENT".
	/// </remarks>
	/// <returns>
	/// <para>corrected RA hours,minutes,seconds</para>
	/// <para>corrected Declination degrees,minutes,seconds</para>
	/// </returns>
	public (double correctedRAHour, double correctedRAMin, double correctedRASec, double correctedDecDeg, double correctedDecMin, double correctedDecSec) AtmosphericRefraction(double trueRAHour, double trueRAMin, double trueRASec, double trueDecDeg, double trueDecMin, double trueDecSec, PACoordinateType coordinateType, double geogLongDeg, double geogLatDeg, int daylightSavingHours, int timezoneHours, double lcdDay, int lcdMonth, int lcdYear, double lctHour, double lctMin, double lctSec, double atmosphericPressureMbar, double atmosphericTemperatureCelsius)
	{
		double haHour = PAMacros.RightAscensionToHourAngle(trueRAHour, trueRAMin, trueRASec, lctHour, lctMin, lctSec, daylightSavingHours, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);
		double azimuthDeg = PAMacros.EquatorialCoordinatesToAzimuth(haHour, 0, 0, trueDecDeg, trueDecMin, trueDecSec, geogLatDeg);
		double altitudeDeg = PAMacros.EquatorialCoordinatesToAltitude(haHour, 0, 0, trueDecDeg, trueDecMin, trueDecSec, geogLatDeg);
		double correctedAltitudeDeg = PAMacros.Refract(altitudeDeg, coordinateType, atmosphericPressureMbar, atmosphericTemperatureCelsius);

		double correctedHAHour = PAMacros.HorizonCoordinatesToHourAngle(azimuthDeg, 0, 0, correctedAltitudeDeg, 0, 0, geogLatDeg);
		double correctedRAHour1 = PAMacros.HourAngleToRightAscension(correctedHAHour, 0, 0, lctHour, lctMin, lctSec, daylightSavingHours, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);
		double correctedDecDeg1 = PAMacros.HorizonCoordinatesToDeclination(azimuthDeg, 0, 0, correctedAltitudeDeg, 0, 0, geogLatDeg);

		int correctedRAHour = PAMacros.DecimalHoursHour(correctedRAHour1);
		int correctedRAMin = PAMacros.DecimalHoursMinute(correctedRAHour1);
		double correctedRASec = PAMacros.DecimalHoursSecond(correctedRAHour1);
		double correctedDecDeg = PAMacros.DecimalDegreesDegrees(correctedDecDeg1);
		double correctedDecMin = PAMacros.DecimalDegreesMinutes(correctedDecDeg1);
		double correctedDecSec = PAMacros.DecimalDegreesSeconds(correctedDecDeg1);

		return (correctedRAHour, correctedRAMin, correctedRASec, correctedDecDeg, correctedDecMin, correctedDecSec);
	}


	/// <summary>
	/// Calculate corrected RA/Dec, accounting for geocentric parallax.
	/// </summary>
	/// <returns>corrected RA hours,minutes,seconds and corrected Declination degrees,minutes,seconds</returns>
	public (double correctedRAHour, double correctedRAMin, double correctedRASec, double correctedDecDeg, double correctedDecMin, double correctedDecSec) CorrectionsForGeocentricParallax(double raHour, double raMin, double raSec, double decDeg, double decMin, double decSec, PACoordinateType coordinateType, double equatorialHorParallaxDeg, double geogLongDeg, double geogLatDeg, double heightM, int daylightSaving, int timezoneHours, double lcdDay, int lcdMonth, int lcdYear, double lctHour, double lctMin, double lctSec)
	{
		double haHours = PAMacros.RightAscensionToHourAngle(raHour, raMin, raSec, lctHour, lctMin, lctSec, daylightSaving, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);

		double correctedHAHours = PAMacros.ParallaxHA(haHours, 0, 0, decDeg, decMin, decSec, coordinateType, geogLatDeg, heightM, equatorialHorParallaxDeg);

		double correctedRAHours = PAMacros.HourAngleToRightAscension(correctedHAHours, 0, 0, lctHour, lctMin, lctSec, daylightSaving, timezoneHours, lcdDay, lcdMonth, lcdYear, geogLongDeg);

		double correctedDecDeg1 = PAMacros.ParallaxDec(haHours, 0, 0, decDeg, decMin, decSec, coordinateType, geogLatDeg, heightM, equatorialHorParallaxDeg);

		int correctedRAHour = PAMacros.DecimalHoursHour(correctedRAHours);
		int correctedRAMin = PAMacros.DecimalHoursMinute(correctedRAHours);
		double correctedRASec = PAMacros.DecimalHoursSecond(correctedRAHours);
		double correctedDecDeg = PAMacros.DecimalDegreesDegrees(correctedDecDeg1);
		double correctedDecMin = PAMacros.DecimalDegreesMinutes(correctedDecDeg1);
		double correctedDecSec = PAMacros.DecimalDegreesSeconds(correctedDecDeg1);

		return (correctedRAHour, correctedRAMin, correctedRASec, correctedDecDeg, correctedDecMin, correctedDecSec);
	}

	/// <summary>
	/// Calculate heliographic coordinates for a given Greenwich date, with a given heliographic position angle and heliographic displacement in arc minutes.
	/// </summary>
	/// <returns>heliographic longitude and heliographic latitude, in degrees</returns>
	public (double helioLongDeg, double helioLatDeg) HeliographicCoordinates(double helioPositionAngleDeg, double helioDisplacementArcmin, double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double julianDateDays = PAMacros.CivilDateToJulianDate(gwdateDay, gwdateMonth, gwdateYear);
		double tCenturies = (julianDateDays - 2415020) / 36525;
		double longAscNodeDeg = PAMacros.DegreesMinutesSecondsToDecimalDegrees(74, 22, 0) + (84 * tCenturies / 60);
		double sunLongDeg = PAMacros.SunLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double y = (longAscNodeDeg - sunLongDeg).ToRadians().Sine() * PAMacros.DegreesMinutesSecondsToDecimalDegrees(7, 15, 0).ToRadians().Cosine();
		double x = -(longAscNodeDeg - sunLongDeg).ToRadians().Cosine();
		double aDeg = PAMacros.Degrees(y.AngleTangent2(x));
		double mDeg1 = 360 - (360 * (julianDateDays - 2398220) / 25.38);
		double mDeg2 = mDeg1 - 360 * (mDeg1 / 360).Floor();
		double l0Deg1 = mDeg2 + aDeg;
		double b0Rad = ((sunLongDeg - longAscNodeDeg).ToRadians().Sine() * PAMacros.DegreesMinutesSecondsToDecimalDegrees(7, 15, 0).ToRadians().Sine()).ASine();
		double theta1Rad = (-sunLongDeg.ToRadians().Cosine() * PAMacros.Obliq(gwdateDay, gwdateMonth, gwdateYear).ToRadians().Tangent()).AngleTangent();
		double theta2Rad = (-(longAscNodeDeg - sunLongDeg).ToRadians().Cosine() * PAMacros.DegreesMinutesSecondsToDecimalDegrees(7, 15, 0).ToRadians().Tangent()).AngleTangent();
		double pDeg = PAMacros.Degrees(theta1Rad + theta2Rad);
		double rho1Deg = helioDisplacementArcmin / 60;
		double rhoRad = (2 * rho1Deg / PAMacros.SunDia(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear)).ASine() - rho1Deg.ToRadians();
		double bRad = (b0Rad.Sine() * rhoRad.Cosine() + b0Rad.Cosine() * rhoRad.Sine() * (pDeg - helioPositionAngleDeg).ToRadians().Cosine()).ASine();
		double bDeg = PAMacros.Degrees(bRad);
		double lDeg1 = PAMacros.Degrees((rhoRad.Sine() * (pDeg - helioPositionAngleDeg).ToRadians().Sine() / bRad.Cosine()).ASine()) + l0Deg1;
		double lDeg2 = lDeg1 - 360 * (lDeg1 / 360).Floor();

		double helioLongDeg = Math.Round(lDeg2, 2);
		double helioLatDeg = Math.Round(bDeg, 2);

		return (helioLongDeg, helioLatDeg);
	}

	/// <summary>
	/// Calculate carrington rotation number for a Greenwich date
	/// </summary>
	/// <returns>carrington rotation number</returns>
	public int CarringtonRotationNumber(double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double julianDateDays = PAMacros.CivilDateToJulianDate(gwdateDay, gwdateMonth, gwdateYear);

		int crn = 1690 + (int)Math.Round((julianDateDays - 2444235.34) / 27.2753, 0);

		return crn;
	}

	/// <summary>
	/// Calculate selenographic (lunar) coordinates (sub-Earth)
	/// </summary>
	/// <returns>sub-earth longitude, sub-earth latitude, and position angle of pole</returns>
	public (double subEarthLongitude, double subEarthLatitude, double positionAngleOfPole) SelenographicCoordinates1(double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double julianDateDays = PAMacros.CivilDateToJulianDate(gwdateDay, gwdateMonth, gwdateYear);
		double tCenturies = (julianDateDays - 2451545) / 36525;
		double longAscNodeDeg = 125.044522 - 1934.136261 * tCenturies;
		double f1 = 93.27191 + 483202.0175 * tCenturies;
		double f2 = f1 - 360 * (f1 / 360).Floor();
		double geocentricMoonLongDeg = PAMacros.MoonLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double geocentricMoonLatRad = PAMacros.MoonLat(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear).ToRadians();
		double inclinationRad = PAMacros.DegreesMinutesSecondsToDecimalDegrees(1, 32, 32.7).ToRadians();
		double nodeLongRad = (longAscNodeDeg - geocentricMoonLongDeg).ToRadians();
		double sinBe = -inclinationRad.Cosine() * geocentricMoonLatRad.Sine() + inclinationRad.Sine() * geocentricMoonLatRad.Cosine() * nodeLongRad.Sine();
		double subEarthLatDeg = PAMacros.Degrees(sinBe.ASine());
		double aRad = (-geocentricMoonLatRad.Sine() * inclinationRad.Sine() - geocentricMoonLatRad.Cosine() * inclinationRad.Cosine() * nodeLongRad.Sine()).AngleTangent2(geocentricMoonLatRad.Cosine() * nodeLongRad.Cosine());
		double aDeg = PAMacros.Degrees(aRad);
		double subEarthLongDeg1 = aDeg - f2;
		double subEarthLongDeg2 = subEarthLongDeg1 - 360 * (subEarthLongDeg1 / 360).Floor();
		double subEarthLongDeg3 = (subEarthLongDeg2 > 180) ? subEarthLongDeg2 - 360 : subEarthLongDeg2;
		double c1Rad = (nodeLongRad.Cosine() * inclinationRad.Sine() / (geocentricMoonLatRad.Cosine() * inclinationRad.Cosine() + geocentricMoonLatRad.Sine() * inclinationRad.Sine() * nodeLongRad.Sine())).AngleTangent();
		double obliquityRad = PAMacros.Obliq(gwdateDay, gwdateMonth, gwdateYear).ToRadians();
		double c2Rad = (obliquityRad.Sine() * geocentricMoonLongDeg.ToRadians().Cosine() / (obliquityRad.Sine() * geocentricMoonLatRad.Sine() * geocentricMoonLongDeg.ToRadians().Sine() - obliquityRad.Cosine() * geocentricMoonLatRad.Cosine())).AngleTangent();
		double cDeg = PAMacros.Degrees(c1Rad + c2Rad);

		double subEarthLongitude = Math.Round(subEarthLongDeg3, 2);
		double subEarthLatitude = Math.Round(subEarthLatDeg, 2);
		double positionAngleOfPole = Math.Round(cDeg, 2);

		return (subEarthLongitude, subEarthLatitude, positionAngleOfPole);
	}

	/// <summary>
	/// Calculate selenographic (lunar) coordinates (sub-Solar)
	/// </summary>
	/// <returns>sub-solar longitude, sub-solar colongitude, and sub-solar latitude</returns>
	public (double subSolarLongitude, double subSolarColongitude, double subSolarLatitude) SelenographicCoordinates2(double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double julianDateDays = PAMacros.CivilDateToJulianDate(gwdateDay, gwdateMonth, gwdateYear);
		double tCenturies = (julianDateDays - 2451545) / 36525;
		double longAscNodeDeg = 125.044522 - 1934.136261 * tCenturies;
		double f1 = 93.27191 + 483202.0175 * tCenturies;
		double f2 = f1 - 360 * (f1 / 360).Floor();
		double sunGeocentricLongDeg = PAMacros.SunLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double moonEquHorParallaxArcMin = PAMacros.MoonHP(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear) * 60;
		double sunEarthDistAU = PAMacros.SunDist(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double geocentricMoonLatRad = PAMacros.MoonLat(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear).ToRadians();
		double geocentricMoonLongDeg = PAMacros.MoonLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double adjustedMoonLongDeg = sunGeocentricLongDeg + 180 + (26.4 * geocentricMoonLatRad.Cosine() * (sunGeocentricLongDeg - geocentricMoonLongDeg).ToRadians().Sine() / (moonEquHorParallaxArcMin * sunEarthDistAU));
		double adjustedMoonLatRad = 0.14666 * geocentricMoonLatRad / (moonEquHorParallaxArcMin * sunEarthDistAU);
		double inclinationRad = PAMacros.DegreesMinutesSecondsToDecimalDegrees(1, 32, 32.7).ToRadians();
		double nodeLongRad = (longAscNodeDeg - adjustedMoonLongDeg).ToRadians();
		double sinBs = -inclinationRad.Cosine() * adjustedMoonLatRad.Sine() + inclinationRad.Sine() * adjustedMoonLatRad.Cosine() * nodeLongRad.Sine();
		double subSolarLatDeg = PAMacros.Degrees(sinBs.ASine());
		double aRad = (-adjustedMoonLatRad.Sine() * inclinationRad.Sine() - adjustedMoonLatRad.Cosine() * inclinationRad.Cosine() * nodeLongRad.Sine()).AngleTangent2(adjustedMoonLatRad.Cosine() * nodeLongRad.Cosine());
		double aDeg = PAMacros.Degrees(aRad);
		double subSolarLongDeg1 = aDeg - f2;
		double subSolarLongDeg2 = subSolarLongDeg1 - 360 * (subSolarLongDeg1 / 360).Floor();
		double subSolarLongDeg3 = (subSolarLongDeg2 > 180) ? subSolarLongDeg2 - 360 : subSolarLongDeg2;
		double subSolarColongDeg = 90 - subSolarLongDeg3;

		double subSolarLongitude = Math.Round(subSolarLongDeg3, 2);
		double subSolarColongitude = Math.Round(subSolarColongDeg, 2);
		double subSolarLatitude = Math.Round(subSolarLatDeg, 2);

		return (subSolarLongitude, subSolarColongitude, subSolarLatitude);
	}
}
