using System;
using PALib.Helpers;

namespace PALib
{
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
			var daylightSaving = (isDaylightSaving == true) ? 1 : 0;

			var greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var utDays = utHours / 24;
			var jdDays = PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) + utDays;
			var dDays = jdDays - PAMacros.CivilDateToJulianDate(0, 1, 2010);
			var nDeg = 360 * dDays / 365.242191;
			var mDeg1 = nDeg + PAMacros.SunELong(0, 1, 2010) - PAMacros.SunPeri(0, 1, 2010);
			var mDeg2 = mDeg1 - 360 * (mDeg1 / 360).Floor();
			var eCDeg = 360 * PAMacros.SunEcc(0, 1, 2010) * mDeg2.ToRadians().Sine() / Math.PI;
			var lSDeg1 = nDeg + eCDeg + PAMacros.SunELong(0, 1, 2010);
			var lSDeg2 = lSDeg1 - 360 * (lSDeg1 / 360).Floor();
			var raDeg = PAMacros.EcRA(lSDeg2, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);
			var raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg);
			var decDeg = PAMacros.EcDec(lSDeg2, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);

			var sunRAHour = PAMacros.DecimalHoursHour(raHours);
			var sunRAMin = PAMacros.DecimalHoursMinute(raHours);
			var sunRASec = PAMacros.DecimalHoursSecond(raHours);
			var sunDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
			var sunDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
			var sunDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

			return (sunRAHour, sunRAMin, sunRASec, sunDecDeg, sunDecMin, sunDecSec);
		}

		/// <summary>
		/// Calculate precise position of the sun for a local date and time.
		/// </summary>
		public (double sunRAHour, double sunRAMin, double sunRASec, double sunDecDeg, double sunDecMin, double sunDecSec) PrecisePositionOfSun(double lctHours, double lctMinutes, double lctSeconds, double localDay, int localMonth, int localYear, bool isDaylightSaving, int zoneCorrection)
		{
			var daylightSaving = (isDaylightSaving == true) ? 1 : 0;

			var gDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var gMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var gYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var sunEclipticLongitudeDeg = PAMacros.SunLong(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var raDeg = PAMacros.EcRA(sunEclipticLongitudeDeg, 0, 0, 0, 0, 0, gDay, gMonth, gYear);
			var raHours = PAMacros.DecimalDegreesToDegreeHours(raDeg);
			var decDeg = PAMacros.EcDec(sunEclipticLongitudeDeg, 0, 0, 0, 0, 0, gDay, gMonth, gYear);

			var sunRAHour = PAMacros.DecimalHoursHour(raHours);
			var sunRAMin = PAMacros.DecimalHoursMinute(raHours);
			var sunRASec = PAMacros.DecimalHoursSecond(raHours);
			var sunDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
			var sunDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
			var sunDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var gDay = PAMacros.LocalCivilTimeGreenwichDay(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var gMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var gYear = PAMacros.LocalCivilTimeGreenwichYear(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var trueAnomalyDeg = PAMacros.SunTrueAnomaly(lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear);
			var trueAnomalyRad = trueAnomalyDeg.ToRadians();
			var eccentricity = PAMacros.SunEcc(gDay, gMonth, gYear);
			var f = (1 + eccentricity * trueAnomalyRad.Cosine()) / (1 - eccentricity * eccentricity);
			var rKm = 149598500 / f;
			var thetaDeg = f * 0.533128;

			var sunDistKm = Math.Round(rKm, 0);
			var sunAngSizeDeg = PAMacros.DecimalDegreesDegrees(thetaDeg);
			var sunAngSizeMin = PAMacros.DecimalDegreesMinutes(thetaDeg);
			var sunAngSizeSec = PAMacros.DecimalDegreesSeconds(thetaDeg);

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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var localSunriseHours = PAMacros.SunriseLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);
			var localSunsetHours = PAMacros.SunsetLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

			var sunRiseSetStatus = PAMacros.ESunRS(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

			var adjustedSunriseHours = localSunriseHours + 0.008333;
			var adjustedSunsetHours = localSunsetHours + 0.008333;

			var azimuthOfSunriseDeg1 = PAMacros.SunriseAZ(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);
			var azimuthOfSunsetDeg1 = PAMacros.SunsetAZ(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg);

			var localSunriseHour = (sunRiseSetStatus.Equals("OK")) ? PAMacros.DecimalHoursHour(adjustedSunriseHours) : 0;
			var localSunriseMinute = (sunRiseSetStatus.Equals("OK")) ? PAMacros.DecimalHoursMinute(adjustedSunriseHours) : 0;

			var localSunsetHour = (sunRiseSetStatus.Equals("OK")) ? PAMacros.DecimalHoursHour(adjustedSunsetHours) : 0;
			var localSunsetMinute = (sunRiseSetStatus.Equals("OK")) ? PAMacros.DecimalHoursMinute(adjustedSunsetHours) : 0;

			var azimuthOfSunriseDeg = (sunRiseSetStatus.Equals("OK")) ? Math.Round(azimuthOfSunriseDeg1, 2) : 0;
			var azimuthOfSunsetDeg = (sunRiseSetStatus.Equals("OK")) ? Math.Round(azimuthOfSunsetDeg1, 2) : 0;

			var status = sunRiseSetStatus;

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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var startOfAMTwilightHours = PAMacros.TwilightAMLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

			var endOfPMTwilightHours = PAMacros.TwilightPMLCT(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

			var twilightStatus = PAMacros.ETwilight(localDay, localMonth, localYear, daylightSaving, zoneCorrection, geographicalLongDeg, geographicalLatDeg, twilightType);

			var adjustedAMStartTime = startOfAMTwilightHours + 0.008333;
			var adjustedPMStartTime = endOfPMTwilightHours + 0.008333;

			double amTwilightBeginsHour = (twilightStatus.Equals("OK")) ? PAMacros.DecimalHoursHour(adjustedAMStartTime) : -99;
			double amTwilightBeginsMin = (twilightStatus.Equals("OK")) ? PAMacros.DecimalHoursMinute(adjustedAMStartTime) : -99;

			double pmTwilightEndsHour = (twilightStatus.Equals("OK")) ? PAMacros.DecimalHoursHour(adjustedPMStartTime) : -99;
			double pmTwilightEndsMin = (twilightStatus.Equals("OK")) ? PAMacros.DecimalHoursMinute(adjustedPMStartTime) : -99;

			var status = twilightStatus;

			return (amTwilightBeginsHour, amTwilightBeginsMin, pmTwilightEndsHour, pmTwilightEndsMin, status);
		}

		/// <summary>
		/// Calculate the equation of time. (The difference between the real Sun time and the mean Sun time.)
		/// </summary>
		/// <param name="gwdate_day">Greenwich date (day part)</param>
		/// <param name="gwdate_month">Greenwich date (month part)</param>
		/// <param name="gwdate_year">Greenwich date (year part)</param>
		/// <returns>
		/// <para>equation_of_time_min -- equation of time (minute part)</para>
		/// <para>equation_of_time_sec -- equation of time (seconds part)</para>
		/// </returns>
		public (double equationOfTimeMin, double equationOfTimeSec) EquationOfTime(double gwdateDay, int gwdateMonth, int gwdateYear)
		{
			var sunLongitudeDeg = PAMacros.SunLong(12, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
			var sunRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear));
			var equivalentUTHours = PAMacros.GreenwichSiderealTimeToUniversalTime(sunRAHours, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
			var equationOfTimeHours = equivalentUTHours - 12;

			var equationOfTimeMin = PAMacros.DecimalHoursMinute(equationOfTimeHours);
			var equationOfTimeSec = PAMacros.DecimalHoursSecond(equationOfTimeHours);

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
			var sunLongitudeDeg = PAMacros.SunLong(0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
			var sunRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear));
			var sunDecDeg = PAMacros.EcDec(sunLongitudeDeg, 0, 0, 0, 0, 0, gwdateDay, gwdateMonth, gwdateYear);
			var solarElongationDeg = PAMacros.Angle(sunRAHours, 0, 0, sunDecDeg, 0, 0, raHour, raMin, raSec, decDeg, decMin, decSec, PAAngleMeasure.Hours);

			return Math.Round(solarElongationDeg, 2);
		}
	}
}