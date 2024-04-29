using System;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Moon calculations.
/// </summary>
public class PAMoon
{
	/// <summary>
	/// Calculate approximate position of the Moon.
	/// </summary>
	/// <returns>
	/// <para>moon_ra_hour -- Right ascension of Moon (hour part)</para>
	/// <para>moon_ra_min -- Right ascension of Moon (minutes part)</para>
	/// <para>moon_ra_sec -- Right ascension of Moon (seconds part)</para>
	/// <para>moon_dec_deg -- Declination of Moon (degrees part)</para>
	/// <para>moon_dec_min -- Declination of Moon (minutes part)</para>
	/// <para>moon_dec_sec -- Declination of Moon (seconds part)</para>
	/// </returns>
	public (double moonRAHour, double moonRAMin, double moonRASec, double moonDecDeg, double moonDecMin, double moonDecSec) ApproximatePositionOfMoon(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double l0 = 91.9293359879052;
		double p0 = 130.143076320618;
		double n0 = 291.682546643194;
		double i = 5.145396;

		double gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double dDays = PAMacros.CivilDateToJulianDate(gdateDay, gdateMonth, gdateYear) - PAMacros.CivilDateToJulianDate(0.0, 1, 2010) + utHours / 24;
		double sunLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double sunMeanAnomalyRad = PAMacros.SunMeanAnomaly(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double lmDeg = PAMacros.UnwindDeg(13.1763966 * dDays + l0);
		double mmDeg = PAMacros.UnwindDeg(lmDeg - 0.1114041 * dDays - p0);
		double nDeg = PAMacros.UnwindDeg(n0 - (0.0529539 * dDays));
		double evDeg = 1.2739 * (2.0 * (lmDeg - sunLongDeg) - mmDeg).ToRadians().Sine();
		double aeDeg = 0.1858 * sunMeanAnomalyRad.Sine();
		double a3Deg = 0.37 * sunMeanAnomalyRad.Sine();
		double mmdDeg = mmDeg + evDeg - aeDeg - a3Deg;
		double ecDeg = 6.2886 * mmdDeg.ToRadians().Sine();
		double a4Deg = 0.214 * (2.0 * mmdDeg.ToRadians()).Sine();
		double ldDeg = lmDeg + evDeg + ecDeg - aeDeg + a4Deg;
		double vDeg = 0.6583 * (2.0 * (ldDeg - sunLongDeg).ToRadians()).Sine();
		double lddDeg = ldDeg + vDeg;
		double ndDeg = nDeg - 0.16 * sunMeanAnomalyRad.Sine();
		double y = (lddDeg - ndDeg).ToRadians().Sine() * i.ToRadians().Cosine();
		double x = (lddDeg - ndDeg).ToRadians().Cosine();

		double moonLongDeg = PAMacros.UnwindDeg(PAMacros.Degrees(y.AngleTangent2(x)) + ndDeg);
		double moonLatDeg = PAMacros.Degrees(((lddDeg - ndDeg).ToRadians().Sine() * i.ToRadians().Sine()).ASine());
		double moonRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(moonLongDeg, 0, 0, moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
		double moonDecDeg1 = PAMacros.EcDec(moonLongDeg, 0, 0, moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

		int moonRAHour = PAMacros.DecimalHoursHour(moonRAHours1);
		int moonRAMin = PAMacros.DecimalHoursMinute(moonRAHours1);
		double moonRASec = PAMacros.DecimalHoursSecond(moonRAHours1);
		double moonDecDeg = PAMacros.DecimalDegreesDegrees(moonDecDeg1);
		double moonDecMin = PAMacros.DecimalDegreesMinutes(moonDecDeg1);
		double moonDecSec = PAMacros.DecimalDegreesSeconds(moonDecDeg1);

		return (moonRAHour, moonRAMin, moonRASec, moonDecDeg, moonDecMin, moonDecSec);
	}

	/// <summary>
	/// Calculate precise position of the Moon.
	/// </summary>
	/// <returns>
	/// <para>moonRAHour -- Right ascension of Moon (hour part)</para>
	/// <para>moonRAMin -- Right ascension of Moon (minutes part)</para>
	/// <para>moonRASec -- Right ascension of Moon (seconds part)</para>
	/// <para>moonDecDeg -- Declination of Moon (degrees part)</para>
	/// <para>moonDecMin -- Declination of Moon (minutes part)</para>
	/// <para>moonDecSec -- Declination of Moon (seconds part)</para>
	/// <para>earthMoonDistKM -- Distance from Earth to Moon (km)</para>
	/// <para>moonHorParallaxDeg -- Horizontal parallax of Moon (degrees)</para>
	/// </returns>
	public (double moonRAHour, double moonRAMin, double moonRASec, double moonDecDeg, double moonDecMin, double moonDecSec, double earthMoonDistKM, double moonHorParallaxDeg) PrecisePositionOfMoon(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		(double moonLongDeg, double moonLatDeg, double moonHorPara) moonResult = PAMacros.MoonLongLatHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double nutationInLongitudeDeg = PAMacros.NutatLong(gdateDay, gdateMonth, gdateYear);
		double correctedLongDeg = moonResult.moonLongDeg + nutationInLongitudeDeg;
		double earthMoonDistanceKM = 6378.14 / moonResult.moonHorPara.ToRadians().Sine();
		double moonRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(correctedLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
		double moonDecDeg1 = PAMacros.EcDec(correctedLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

		int moonRAHour = PAMacros.DecimalHoursHour(moonRAHours1);
		int moonRAMin = PAMacros.DecimalHoursMinute(moonRAHours1);
		double moonRASec = PAMacros.DecimalHoursSecond(moonRAHours1);
		double moonDecDeg = PAMacros.DecimalDegreesDegrees(moonDecDeg1);
		double moonDecMin = PAMacros.DecimalDegreesMinutes(moonDecDeg1);
		double moonDecSec = PAMacros.DecimalDegreesSeconds(moonDecDeg1);
		double earthMoonDistKM = Math.Round(earthMoonDistanceKM, 0);
		double moonHorParallaxDeg = Math.Round(moonResult.moonHorPara, 6);

		return (moonRAHour, moonRAMin, moonRASec, moonDecDeg, moonDecMin, moonDecSec, earthMoonDistKM, moonHorParallaxDeg);
	}

	/// <summary>
	/// Calculate Moon phase and position angle of bright limb.
	/// </summary>
	/// <returns>
	/// <para>moonPhase -- Phase of Moon, between 0 and 1, where 0 is New and 1 is Full.</para>
	/// <para>paBrightLimbDeg -- Position angle of the bright limb (degrees)</para>
	/// </returns>
	public (double moonPhase, double paBrightLimbDeg) MoonPhase(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, PAAccuracyLevel accuracyLevel)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double sunLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		(double moonLongDeg, double moonLatDeg, double moonHorPara) moonResult = PAMacros.MoonLongLatHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double dRad = (moonResult.moonLongDeg - sunLongDeg).ToRadians();

		double moonPhase1 = (accuracyLevel == PAAccuracyLevel.Precise) ? PAMacros.MoonPhase(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear) : (1.0 - dRad.Cosine()) / 2.0;

		double sunRARad = PAMacros.EcRA(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		double moonRARad = PAMacros.EcRA(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		double sunDecRad = PAMacros.EcDec(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		double moonDecRad = PAMacros.EcDec(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();

		double y = sunDecRad.Cosine() * (sunRARad - moonRARad).Sine();
		double x = moonDecRad.Cosine() * sunDecRad.Sine() - moonDecRad.Sine() * sunDecRad.Cosine() * (sunRARad - moonRARad).Cosine();

		double chiDeg = PAMacros.Degrees(y.AngleTangent2(x));

		double moonPhase = Math.Round(moonPhase1, 2);
		double paBrightLimbDeg = Math.Round(chiDeg, 2);

		return (moonPhase, paBrightLimbDeg);
	}

	/// <summary>
	/// Calculate new moon and full moon instances.
	/// </summary>
	/// <returns>
	/// <para>nmLocalTimeHour -- new Moon instant - local time (hour)</para>
	/// <para>nmLocalTimeMin -- new Moon instant - local time (minutes)</para>
	/// <para>nmLocalDateDay -- new Moon instance - local date (day)</para>
	/// <para>nmLocalDateMonth -- new Moon instance - local date (month)</para>
	/// <para>nmLocalDateYear -- new Moon instance - local date (year)</para>
	/// <para>fmLocalTimeHour -- full Moon instant - local time (hour)</para>
	/// <para>fmLocalTimeMin -- full Moon instant - local time (minutes)</para>
	/// <para>fmLocalDateDay -- full Moon instance - local date (day)</para>
	/// <para>fmLocalDateMonth -- full Moon instance - local date (month)</para>
	/// <para>fmLocalDateYear -- full Moon instance - local date (year)</para>
	/// </returns>
	public (double nmLocalTimeHour, double nmLocalTimeMin, double nmLocalDateDay, int nmLocalDateMonth, int nmLocalDateYear, double fmLocalTimeHour, double fmLocalTimeMin, double fmLocalDateDay, int fmLocalDateMonth, int fmLocalDateYear) TimesOfNewMoonAndFullMoon(bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double jdOfNewMoonDays = PAMacros.NewMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double jdOfFullMoonDays = PAMacros.FullMoon(3, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double gDateOfNewMoonDay = PAMacros.JulianDateDay(jdOfNewMoonDays);
		double integerDay1 = gDateOfNewMoonDay.Floor();
		int gDateOfNewMoonMonth = PAMacros.JulianDateMonth(jdOfNewMoonDays);
		int gDateOfNewMoonYear = PAMacros.JulianDateYear(jdOfNewMoonDays);

		double gDateOfFullMoonDay = PAMacros.JulianDateDay(jdOfFullMoonDays);
		double integerDay2 = gDateOfFullMoonDay.Floor();
		int gDateOfFullMoonMonth = PAMacros.JulianDateMonth(jdOfFullMoonDays);
		int gDateOfFullMoonYear = PAMacros.JulianDateYear(jdOfFullMoonDays);

		double utOfNewMoonHours = 24.0 * (gDateOfNewMoonDay - integerDay1);
		double utOfFullMoonHours = 24.0 * (gDateOfFullMoonDay - integerDay2);
		double lctOfNewMoonHours = PAMacros.UniversalTimeToLocalCivilTime(utOfNewMoonHours + 0.008333, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		double lctOfFullMoonHours = PAMacros.UniversalTimeToLocalCivilTime(utOfFullMoonHours + 0.008333, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);

		int nmLocalTimeHour = PAMacros.DecimalHoursHour(lctOfNewMoonHours);
		int nmLocalTimeMin = PAMacros.DecimalHoursMinute(lctOfNewMoonHours);
		double nmLocalDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int nmLocalDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int nmLocalDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int fmLocalTimeHour = PAMacros.DecimalHoursHour(lctOfFullMoonHours);
		int fmLocalTimeMin = PAMacros.DecimalHoursMinute(lctOfFullMoonHours);
		double fmLocalDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int fmLocalDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int fmLocalDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);

		return (nmLocalTimeHour, nmLocalTimeMin, nmLocalDateDay, nmLocalDateMonth, nmLocalDateYear, fmLocalTimeHour, fmLocalTimeMin, fmLocalDateDay, fmLocalDateMonth, fmLocalDateYear);
	}

	/// <summary>
	/// Calculate Moon's distance, angular diameter, and horizontal parallax.
	/// </summary>
	/// <returns>
	/// <para>earth_moon_dist -- Earth-Moon distance (km)</para>
	/// <para>ang_diameter_deg -- Angular diameter (degrees part)</para>
	/// <para>ang_diameter_min -- Angular diameter (minutes part)</para>
	/// <para>hor_parallax_deg -- Horizontal parallax (degrees part)</para>
	/// <para>hor_parallax_min -- Horizontal parallax (minutes part)</para>
	/// <para>hor_parallax_sec -- Horizontal parallax (seconds part)</para>
	/// </returns>
	public (double earthMoonDist, double angDiameterDeg, double angDiameterMin, double horParallaxDeg, double horParallaxMin, double horParallaxSec) MoonDistAngDiamHorParallax(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double moonDistance = PAMacros.MoonDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double moonAngularDiameter = PAMacros.MoonSize(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double moonHorizontalParallax = PAMacros.MoonHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double earthMoonDist = Math.Round(moonDistance, 0);
		double angDiameterDeg = PAMacros.DecimalDegreesDegrees(moonAngularDiameter + 0.008333);
		double angDiameterMin = PAMacros.DecimalDegreesMinutes(moonAngularDiameter + 0.008333);
		double horParallaxDeg = PAMacros.DecimalDegreesDegrees(moonHorizontalParallax);
		double horParallaxMin = PAMacros.DecimalDegreesMinutes(moonHorizontalParallax);
		double horParallaxSec = PAMacros.DecimalDegreesSeconds(moonHorizontalParallax);

		return (earthMoonDist, angDiameterDeg, angDiameterMin, horParallaxDeg, horParallaxMin, horParallaxSec);
	}

	/// <summary>
	/// Calculate date/time of local moonrise and moonset.
	/// </summary>
	/// <returns>
	/// <para>mrLTHour -- Moonrise, local time (hour part)</para>
	/// <para>mrLTMin -- Moonrise, local time (minutes part)</para>
	/// <para>mrLocalDateDay -- Moonrise, local date (day)</para>
	/// <para>mrLocalDateMonth -- Moonrise, local date (month)</para>
	/// <para>mrLocalDateYear -- Moonrise, local date (year)</para>
	/// <para>mrAzimuthDeg -- Moonrise, azimuth (degrees)</para>
	/// <para>msLTHour -- Moonset, local time (hour part)</para>
	/// <para>msLTMin -- Moonset, local time (minutes part)</para>
	/// <para>msLocalDateDay -- Moonset, local date (day)</para>
	/// <para>msLocalDateMonth -- Moonset, local date (month)</para>
	/// <para>msLocalDateYear -- Moonset, local date (year)</para>
	/// <para>msAzimuthDeg -- Moonset, azimuth (degrees)</para>
	/// </returns>
	public (double mrLTHour, double mrLTMin, double mrLocalDateDay, int mrLocalDateMonth, int mrLocalDateYear, double mrAzimuthDeg, double msLTHour, double msLTMin, double msLocalDateDay, int msLocalDateMonth, int msLocalDateYear, double msAzimuthDeg) MoonriseAndMoonset(double localDateDay, int localDateMonth, int localDateYear, bool isDaylightSaving, int zoneCorrectionHours, double geogLongDeg, double geogLatDeg)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double localTimeOfMoonriseHours = PAMacros.MoonRiseLCT(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		(double dy1, int mn1, int yr1) moonRiseLCResult = PAMacros.MoonRiseLcDMY(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		double localAzimuthDeg1 = PAMacros.MoonRiseAz(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);

		double localTimeOfMoonsetHours = PAMacros.MoonSetLCT(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		(double dy1, int mn1, int yr1) moonSetLCResult = PAMacros.MoonSetLcDMY(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		double localAzimuthDeg2 = PAMacros.MoonSetAz(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);

		int mrLTHour = PAMacros.DecimalHoursHour(localTimeOfMoonriseHours + 0.008333);
		int mrLTMin = PAMacros.DecimalHoursMinute(localTimeOfMoonriseHours + 0.008333);
		double mrLocalDateDay = moonRiseLCResult.dy1;
		int mrLocalDateMonth = moonRiseLCResult.mn1;
		int mrLocalDateYear = moonRiseLCResult.yr1;
		double mrAzimuthDeg = Math.Round(localAzimuthDeg1, 2);
		int msLTHour = PAMacros.DecimalHoursHour(localTimeOfMoonsetHours + 0.008333);
		int msLTMin = PAMacros.DecimalHoursMinute(localTimeOfMoonsetHours + 0.008333);
		double msLocalDateDay = moonSetLCResult.dy1;
		int msLocalDateMonth = moonSetLCResult.mn1;
		int msLocalDateYear = moonSetLCResult.yr1;
		double msAzimuthDeg = Math.Round(localAzimuthDeg2, 2);

		return (mrLTHour, mrLTMin, mrLocalDateDay, mrLocalDateMonth, mrLocalDateYear, mrAzimuthDeg, msLTHour, msLTMin, msLocalDateDay, msLocalDateMonth, msLocalDateYear, msAzimuthDeg);
	}
}
