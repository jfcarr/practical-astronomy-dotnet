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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var l0 = 91.9293359879052;
		var p0 = 130.143076320618;
		var n0 = 291.682546643194;
		var i = 5.145396;

		var gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var dDays = PAMacros.CivilDateToJulianDate(gdateDay, gdateMonth, gdateYear) - PAMacros.CivilDateToJulianDate(0.0, 1, 2010) + utHours / 24;
		var sunLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var sunMeanAnomalyRad = PAMacros.SunMeanAnomaly(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var lmDeg = PAMacros.UnwindDeg(13.1763966 * dDays + l0);
		var mmDeg = PAMacros.UnwindDeg(lmDeg - 0.1114041 * dDays - p0);
		var nDeg = PAMacros.UnwindDeg(n0 - (0.0529539 * dDays));
		var evDeg = 1.2739 * (2.0 * (lmDeg - sunLongDeg) - mmDeg).ToRadians().Sine();
		var aeDeg = 0.1858 * sunMeanAnomalyRad.Sine();
		var a3Deg = 0.37 * sunMeanAnomalyRad.Sine();
		var mmdDeg = mmDeg + evDeg - aeDeg - a3Deg;
		var ecDeg = 6.2886 * mmdDeg.ToRadians().Sine();
		var a4Deg = 0.214 * (2.0 * mmdDeg.ToRadians()).Sine();
		var ldDeg = lmDeg + evDeg + ecDeg - aeDeg + a4Deg;
		var vDeg = 0.6583 * (2.0 * (ldDeg - sunLongDeg).ToRadians()).Sine();
		var lddDeg = ldDeg + vDeg;
		var ndDeg = nDeg - 0.16 * sunMeanAnomalyRad.Sine();
		var y = (lddDeg - ndDeg).ToRadians().Sine() * i.ToRadians().Cosine();
		var x = (lddDeg - ndDeg).ToRadians().Cosine();

		var moonLongDeg = PAMacros.UnwindDeg(PAMacros.Degrees(y.AngleTangent2(x)) + ndDeg);
		var moonLatDeg = PAMacros.Degrees(((lddDeg - ndDeg).ToRadians().Sine() * i.ToRadians().Sine()).ASine());
		var moonRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(moonLongDeg, 0, 0, moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
		var moonDecDeg1 = PAMacros.EcDec(moonLongDeg, 0, 0, moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

		var moonRAHour = PAMacros.DecimalHoursHour(moonRAHours1);
		var moonRAMin = PAMacros.DecimalHoursMinute(moonRAHours1);
		var moonRASec = PAMacros.DecimalHoursSecond(moonRAHours1);
		var moonDecDeg = PAMacros.DecimalDegreesDegrees(moonDecDeg1);
		var moonDecMin = PAMacros.DecimalDegreesMinutes(moonDecDeg1);
		var moonDecSec = PAMacros.DecimalDegreesSeconds(moonDecDeg1);

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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var moonResult = PAMacros.MoonLongLatHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var nutationInLongitudeDeg = PAMacros.NutatLong(gdateDay, gdateMonth, gdateYear);
		var correctedLongDeg = moonResult.moonLongDeg + nutationInLongitudeDeg;
		var earthMoonDistanceKM = 6378.14 / moonResult.moonHorPara.ToRadians().Sine();
		var moonRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(correctedLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
		var moonDecDeg1 = PAMacros.EcDec(correctedLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

		var moonRAHour = PAMacros.DecimalHoursHour(moonRAHours1);
		var moonRAMin = PAMacros.DecimalHoursMinute(moonRAHours1);
		var moonRASec = PAMacros.DecimalHoursSecond(moonRAHours1);
		var moonDecDeg = PAMacros.DecimalDegreesDegrees(moonDecDeg1);
		var moonDecMin = PAMacros.DecimalDegreesMinutes(moonDecDeg1);
		var moonDecSec = PAMacros.DecimalDegreesSeconds(moonDecDeg1);
		var earthMoonDistKM = Math.Round(earthMoonDistanceKM, 0);
		var moonHorParallaxDeg = Math.Round(moonResult.moonHorPara, 6);

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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var sunLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var moonResult = PAMacros.MoonLongLatHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var dRad = (moonResult.moonLongDeg - sunLongDeg).ToRadians();

		var moonPhase1 = (accuracyLevel == PAAccuracyLevel.Precise) ? PAMacros.MoonPhase(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear) : (1.0 - dRad.Cosine()) / 2.0;

		var sunRARad = PAMacros.EcRA(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		var moonRARad = PAMacros.EcRA(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		var sunDecRad = PAMacros.EcDec(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();
		var moonDecRad = PAMacros.EcDec(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear).ToRadians();

		var y = sunDecRad.Cosine() * (sunRARad - moonRARad).Sine();
		var x = moonDecRad.Cosine() * sunDecRad.Sine() - moonDecRad.Sine() * sunDecRad.Cosine() * (sunRARad - moonRARad).Cosine();

		var chiDeg = PAMacros.Degrees(y.AngleTangent2(x));

		var moonPhase = Math.Round(moonPhase1, 2);
		var paBrightLimbDeg = Math.Round(chiDeg, 2);

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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var jdOfNewMoonDays = PAMacros.NewMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var jdOfFullMoonDays = PAMacros.FullMoon(3, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var gDateOfNewMoonDay = PAMacros.JulianDateDay(jdOfNewMoonDays);
		var integerDay1 = gDateOfNewMoonDay.Floor();
		var gDateOfNewMoonMonth = PAMacros.JulianDateMonth(jdOfNewMoonDays);
		var gDateOfNewMoonYear = PAMacros.JulianDateYear(jdOfNewMoonDays);

		var gDateOfFullMoonDay = PAMacros.JulianDateDay(jdOfFullMoonDays);
		var integerDay2 = gDateOfFullMoonDay.Floor();
		var gDateOfFullMoonMonth = PAMacros.JulianDateMonth(jdOfFullMoonDays);
		var gDateOfFullMoonYear = PAMacros.JulianDateYear(jdOfFullMoonDays);

		var utOfNewMoonHours = 24.0 * (gDateOfNewMoonDay - integerDay1);
		var utOfFullMoonHours = 24.0 * (gDateOfFullMoonDay - integerDay2);
		var lctOfNewMoonHours = PAMacros.UniversalTimeToLocalCivilTime(utOfNewMoonHours + 0.008333, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		var lctOfFullMoonHours = PAMacros.UniversalTimeToLocalCivilTime(utOfFullMoonHours + 0.008333, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);

		var nmLocalTimeHour = PAMacros.DecimalHoursHour(lctOfNewMoonHours);
		var nmLocalTimeMin = PAMacros.DecimalHoursMinute(lctOfNewMoonHours);
		var nmLocalDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		var nmLocalDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		var nmLocalDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfNewMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay1, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		var fmLocalTimeHour = PAMacros.DecimalHoursHour(lctOfFullMoonHours);
		var fmLocalTimeMin = PAMacros.DecimalHoursMinute(lctOfFullMoonHours);
		var fmLocalDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		var fmLocalDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		var fmLocalDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfFullMoonHours, 0, 0, daylightSaving, zoneCorrectionHours, integerDay2, gDateOfFullMoonMonth, gDateOfFullMoonYear);

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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var moonDistance = PAMacros.MoonDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var moonAngularDiameter = PAMacros.MoonSize(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		var moonHorizontalParallax = PAMacros.MoonHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		var earthMoonDist = Math.Round(moonDistance, 0);
		var angDiameterDeg = PAMacros.DecimalDegreesDegrees(moonAngularDiameter + 0.008333);
		var angDiameterMin = PAMacros.DecimalDegreesMinutes(moonAngularDiameter + 0.008333);
		var horParallaxDeg = PAMacros.DecimalDegreesDegrees(moonHorizontalParallax);
		var horParallaxMin = PAMacros.DecimalDegreesMinutes(moonHorizontalParallax);
		var horParallaxSec = PAMacros.DecimalDegreesSeconds(moonHorizontalParallax);

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
		var daylightSaving = isDaylightSaving ? 1 : 0;

		var localTimeOfMoonriseHours = PAMacros.MoonRiseLCT(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		var moonRiseLCResult = PAMacros.MoonRiseLcDMY(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		var localAzimuthDeg1 = PAMacros.MoonRiseAz(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);

		var localTimeOfMoonsetHours = PAMacros.MoonSetLCT(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		var moonSetLCResult = PAMacros.MoonSetLcDMY(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);
		var localAzimuthDeg2 = PAMacros.MoonSetAz(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongDeg, geogLatDeg);

		var mrLTHour = PAMacros.DecimalHoursHour(localTimeOfMoonriseHours + 0.008333);
		var mrLTMin = PAMacros.DecimalHoursMinute(localTimeOfMoonriseHours + 0.008333);
		var mrLocalDateDay = moonRiseLCResult.dy1;
		var mrLocalDateMonth = moonRiseLCResult.mn1;
		var mrLocalDateYear = moonRiseLCResult.yr1;
		var mrAzimuthDeg = Math.Round(localAzimuthDeg1, 2);
		var msLTHour = PAMacros.DecimalHoursHour(localTimeOfMoonsetHours + 0.008333);
		var msLTMin = PAMacros.DecimalHoursMinute(localTimeOfMoonsetHours + 0.008333);
		var msLocalDateDay = moonSetLCResult.dy1;
		var msLocalDateMonth = moonSetLCResult.mn1;
		var msLocalDateYear = moonSetLCResult.yr1;
		var msAzimuthDeg = Math.Round(localAzimuthDeg2, 2);

		return (mrLTHour, mrLTMin, mrLocalDateDay, mrLocalDateMonth, mrLocalDateYear, mrAzimuthDeg, msLTHour, msLTMin, msLocalDateDay, msLocalDateMonth, msLocalDateYear, msAzimuthDeg);
	}
}
