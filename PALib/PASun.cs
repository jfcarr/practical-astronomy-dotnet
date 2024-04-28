using System;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Sun calculations.
/// </summary>
public class PASun
{
	/// <summary>
	/// Calculate approximate position of the sun for a local date and time.
	/// </summary>
	/// <param name="lctHours">Local civil time, in hours.</param>
	/// <param name="lctMinutes">Local civil time, in minutes.</param>
	/// <param name="lctSeconds">Local civil time, in seconds.</param>
	/// <param name="localDay">Local day, day part.</param>
	/// <param name="localMonth">Local day, month part.</param>
	/// <param name="localYear">Local day, year part.</param>
	/// <param name="isDaylightSaving">Is daylight savings in effect?</param>
	/// <param name="zoneCorrection">Time zone correction, in hours.</param>
	/// <returns>
	/// <para>sunRAHour -- Right Ascension of Sun, hour part</para>
	/// <para>sunRAMin -- Right Ascension of Sun, minutes part</para>
	/// <para>sunRASec -- Right Ascension of Sun, seconds part</para>
	/// <para>sunDecDeg -- Declination of Sun, degrees part</para>
	/// <para>sunDecMin -- Declination of Sun, minutes part</para>
	/// <para>sunDecSec -- Declination of Sun, seconds part</para>
	/// </returns>
	public (double sunRAHour, double sunRAMin, double sunRASec, double sunDecDeg, double sunDecMin, double sunDecSec) ApproximatePositionOfSun(double lctHours, double lctMinutes, double lctSeconds, double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection)
	{
		int daylightSaving = (isDaylightSaving == true) ? 1 : 0;

		double greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double utDays = utHours / 24;
		double jdDays = PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) + utDays;
		double dDays = jdDays - PAMacros.CivilDateToJulianDate(0, 1, 2010);
		double nDeg = 360 * dDays / 365.242191;
		double mDeg1 = nDeg + PAMacros.SunELong(0, 1, 2010) - PAMacros.SunPeri(0, 1, 2010);
		double mDeg2 = mDeg1 - 360 * (mDeg1 / 360).Floor();
		double eCDeg = 360 * PAMacros.SunEcc(0, 1, 2010) * mDeg2.ToRadians().Sine() / Math.PI;
		double lSDeg1 = nDeg + eCDeg + PAMacros.SunELong(0, 1, 2010);
		double lSDeg2 = lSDeg1 - 360 * (lSDeg1 / 360).Floor();
		double raDeg = PAMacros.EcRA(lSDeg2, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);
		double raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg);
		double decDeg = PAMacros.EcDec(lSDeg2, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);

		int sunRAHour = PAMacros.DecimalHoursHour(raHours);
		int sunRAMin = PAMacros.DecimalHoursMinute(raHours);
		double sunRASec = PAMacros.DecimalHoursSecond(raHours);
		double sunDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
		double sunDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
		double sunDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

		return (sunRAHour, sunRAMin, sunRASec, sunDecDeg, sunDecMin, sunDecSec);
	}

	/// <summary>
	/// Calculate precise position of the sun for a local date and time.
	/// </summary>
	public (double sunRAHour, double sunRAMin, double sunRASec, double sunDecDeg, double sunDecMin, double sunDecSec) PrecisePositionOfSun(double lctHours, double lctMinutes, double lctSeconds, double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection)
	{
		int daylightSaving = (isDaylightSaving == true) ? 1 : 0;

		double gDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int gMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int gYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double sunEclipticLongitudeDeg = PAMacros.SunLong(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double raDeg = PAMacros.EcRA(sunEclipticLongitudeDeg, 0, 0, 0, 0, 0, gDay, gMonth, gYear);
		double raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg);
		double decDeg = PAMacros.EcDec(sunEclipticLongitudeDeg, 0, 0, 0, 0, 0, gDay, gMonth, gYear);

		int sunRAHour = PAMacros.DecimalHoursHour(raHours);
		int sunRAMin = PAMacros.DecimalHoursMinute(raHours);
		double sunRASec = PAMacros.DecimalHoursSecond(raHours);
		double sunDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
		double sunDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
		double sunDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

		return (sunRAHour, sunRAMin, sunRASec, sunDecDeg, sunDecMin, sunDecSec);
	}

	/// <summary>
	/// Calculate distance to the Sun (in km), and angular size.
	/// </summary>
	/// <returns>
	/// <para>sunDistKm -- Sun's distance, in kilometers</para>
	/// <para>sunAngSizeDeg -- Sun's angular size (degrees part)</para>
	/// <para>sunAngSizeMin -- Sun's angular size (minutes part)</para>
	/// <para>sunAngSizeSec -- Sun's angular size (seconds part)</para>
	/// </returns>
	public (double sunDistKm, double sunAngSizeDeg, double sunAngSizeMin, double sunAngSizeSec) SunDistanceAndAngularSize(double lctHours, double lctMinutes, double lctSeconds, double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double gDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int gMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		int gYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double trueAnomalyDeg = PAMacros.SunTrueAnomaly(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
		double trueAnomalyRad = trueAnomalyDeg.ToRadians();
		double eccentricity = PAMacros.SunEcc(gDay, gMonth, gYear);
		double f = (1 + eccentricity * trueAnomalyRad.Cosine()) / (1 - eccentricity * eccentricity);
		double rKm = 149598500 / f;
		double thetaDeg = f * 0.533128;

		double sunDistKm = Math.Round(rKm, 0);
		double sunAngSizeDeg = PAMacros.DecimalDegreesDegrees(thetaDeg);
		double sunAngSizeMin = PAMacros.DecimalDegreesMinutes(thetaDeg);
		double sunAngSizeSec = PAMacros.DecimalDegreesSeconds(thetaDeg);

		return (sunDistKm, sunAngSizeDeg, sunAngSizeMin, sunAngSizeSec);
	}

	/// <summary>
	/// Calculate local sunrise and sunset.
	/// </summary>
	/// <returns>
	/// <para>localSunriseHour -- Local sunrise, hour part</para>
	/// <para>localSunriseMinute -- Local sunrise, minutes part</para>
	/// <para>localSunsetHour -- Local sunset, hour part</para>
	/// <para>localSunsetMinute -- Local sunset, minutes part</para>
	/// <para>azimuthOfSunriseDeg -- Azimuth (horizon direction) of sunrise, in degrees</para>
	/// <para>azimuthOfSunsetDeg -- Azimuth (horizon direction) of sunset, in degrees</para>
	/// <para>status -- Calculation status</para>
	/// </returns>
	public (double localSunriseHour, double localSunriseMinute, double localSunsetHour, double localSunsetMinute, double azimuthOfSunriseDeg, double azimuthOfSunsetDeg, string status) SunriseAndSunset(double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection, double geographicalLongDeg, double geographicalLatDeg)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double localSunriseHours = PAMacros.SunriseLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);
		double localSunsetHours = PAMacros.SunsetLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

		string sunRiseSetStatus = PAMacros.ESunRS(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

		double adjustedSunriseHours = localSunriseHours + 0.008333;
		double adjustedSunsetHours = localSunsetHours + 0.008333;

		double azimuthOfSunriseDeg1 = PAMacros.SunriseAZ(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);
		double azimuthOfSunsetDeg1 = PAMacros.SunsetAZ(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

		int localSunriseHour = sunRiseSetStatus.Equals("OK") ? PAMacros.DecimalHoursHour(adjustedSunriseHours) : 0;
		int localSunriseMinute = sunRiseSetStatus.Equals("OK") ? PAMacros.DecimalHoursMinute(adjustedSunriseHours) : 0;

		int localSunsetHour = sunRiseSetStatus.Equals("OK") ? PAMacros.DecimalHoursHour(adjustedSunsetHours) : 0;
		int localSunsetMinute = sunRiseSetStatus.Equals("OK") ? PAMacros.DecimalHoursMinute(adjustedSunsetHours) : 0;

		double azimuthOfSunriseDeg = sunRiseSetStatus.Equals("OK") ? Math.Round(azimuthOfSunriseDeg1, 2) : 0;
		double azimuthOfSunsetDeg = sunRiseSetStatus.Equals("OK") ? Math.Round(azimuthOfSunsetDeg1, 2) : 0;

		string status = sunRiseSetStatus;

		return (localSunriseHour, localSunriseMinute, localSunsetHour, localSunsetMinute, azimuthOfSunriseDeg, azimuthOfSunsetDeg, status);
	}

	/// <summary>
	/// Calculate times of morning and evening twilight.
	/// </summary>
	/// <param name="localDay">Local date, day part.</param>
	/// <param name="localMonth">Local date, month part.</param>
	/// <param name="localYear">Local date, year part.</param>
	/// <param name="isDaylightSaving">Is daylight savings in effect?</param>
	/// <param name="zoneCorrection">Time zone correction, in hours.</param>
	/// <param name="geographicalLongDeg">Geographical longitude, in degrees.</param>
	/// <param name="geographicalLatDeg">Geographical latitude, in degrees.</param>
	/// <param name="twilightType">"C" (civil), "N" (nautical), or "A" (astronomical)</param>
	/// <returns>
	/// <para>amTwilightBeginsHour -- Beginning of AM twilight (hour part)</para>
	/// <para>amTwilightBeginsMin -- Beginning of AM twilight (minutes part)</para>
	/// <para>pmTwilightEndsHour -- Ending of PM twilight (hour part)</para>
	/// <para>pmTwilightEndsMin -- Ending of PM twilight (minutes part)</para>
	/// <para>status -- Calculation status</para>
	/// </returns>
	public (double amTwilightBeginsHour, double amTwilightBeginsMin, double pmTwilightEndsHour, double pmTwilightEndsMin, string status) MorningAndEveningTwilight(double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection, double geographicalLongDeg, double geographicalLatDeg, PATwilightType twilightType)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double startOfAMTwilightHours = PAMacros.TwilightAMLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

		double endOfPMTwilightHours = PAMacros.TwilightPMLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

		string twilightStatus = PAMacros.ETwilight(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

		double adjustedAMStartTime = startOfAMTwilightHours + 0.008333;
		double adjustedPMStartTime = endOfPMTwilightHours + 0.008333;

		double amTwilightBeginsHour = twilightStatus.Equals("OK") ? PAMacros.DecimalHoursHour(adjustedAMStartTime) : -99;
		double amTwilightBeginsMin = twilightStatus.Equals("OK") ? PAMacros.DecimalHoursMinute(adjustedAMStartTime) : -99;

		double pmTwilightEndsHour = twilightStatus.Equals("OK") ? PAMacros.DecimalHoursHour(adjustedPMStartTime) : -99;
		double pmTwilightEndsMin = twilightStatus.Equals("OK") ? PAMacros.DecimalHoursMinute(adjustedPMStartTime) : -99;

		string status = twilightStatus;

		return (amTwilightBeginsHour, amTwilightBeginsMin, pmTwilightEndsHour, pmTwilightEndsMin, status);
	}

	/// <summary>
	/// Calculate the equation of time. (The difference between the real Sun time and the mean Sun time.)
	/// </summary>
	/// <param name="gwdateDay">Greenwich date (day part)</param>
	/// <param name="gwdateMonth">Greenwich date (month part)</param>
	/// <param name="gwdateYear">Greenwich date (year part)</param>
	/// <returns>
	/// <para>equation_of_time_min -- equation of time (minute part)</para>
	/// <para>equation_of_time_sec -- equation of time (seconds part)</para>
	/// </returns>
	public (double equationOfTimeMin, double equationOfTimeSec) EquationOfTime(double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double sunLongitudeDeg = PAMacros.SunLong(12, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double sunRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear));
		double equivalentUTHours = PAMacros.GreenwichSiderealTimeToUniversalTime(sunRAHours, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double equationOfTimeHours = equivalentUTHours - 12;

		int equationOfTimeMin = PAMacros.DecimalHoursMinute(equationOfTimeHours);
		double equationOfTimeSec = PAMacros.DecimalHoursSecond(equationOfTimeHours);

		return (equationOfTimeMin, equationOfTimeSec);
	}

	/// <summary>
	/// Calculate solar elongation for a celestial body.
	/// </summary>
	/// <remarks>
	/// Solar elongation is the angle between the lines of sight from the Earth to the Sun and from the Earth to the celestial body.
	/// </remarks>
	/// <returns>solarElongationDeg -- Solar elongation, in degrees</returns>
	public double SolarElongation(double raHour, double raMin, double raSec, double decDeg, double decMin, double decSec, double gwdateDay, int gwdateMonth, int gwdateYear)
	{
		double sunLongitudeDeg = PAMacros.SunLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double sunRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear));
		double sunDecDeg = PAMacros.EcDec(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
		double solarElongationDeg = PAMacros.Angle(sunRAHours, 0, 0, sunDecDeg, 0, 0, raHour, raMin, raSec, decDeg, decMin, decSec, PAAngleMeasure.Hours);

		return Math.Round(solarElongationDeg, 2);
	}
}
