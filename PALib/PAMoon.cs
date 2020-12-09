using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib
{
	public class PAMoon
	{
		/// <summary>
		/// Calculate approximate position of the Moon.
		/// </summary>
		/// <param name="moonRAHour"></param>
		/// <param name="moonRAMin"></param>
		/// <param name="moonRASec"></param>
		/// <param name="moonDecDeg"></param>
		/// <param name="moonDecMin"></param>
		/// <param name="lctHour"></param>
		/// <param name="lctMin"></param>
		/// <param name="lctSec"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrectionHours"></param>
		/// <param name="localDateDay"></param>
		/// <param name="localDateMonth"></param>
		/// <param name="localDateYear"></param>
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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

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
			var evDeg = 1.2739 * ((2.0 * (lmDeg - sunLongDeg) - mmDeg).ToRadians()).Sine();
			var aeDeg = 0.1858 * (sunMeanAnomalyRad).Sine();
			var a3Deg = 0.37 * (sunMeanAnomalyRad).Sine();
			var mmdDeg = mmDeg + evDeg - aeDeg - a3Deg;
			var ecDeg = 6.2886 * mmdDeg.ToRadians().Sine();
			var a4Deg = 0.214 * (2.0 * (mmdDeg).ToRadians()).Sine();
			var ldDeg = lmDeg + evDeg + ecDeg - aeDeg + a4Deg;
			var vDeg = 0.6583 * (2.0 * (ldDeg - sunLongDeg).ToRadians()).Sine();
			var lddDeg = ldDeg + vDeg;
			var ndDeg = nDeg - 0.16 * (sunMeanAnomalyRad).Sine();
			var y = ((lddDeg - ndDeg).ToRadians()).Sine() * i.ToRadians().Cosine();
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
		/// <param name="moonRAHour"></param>
		/// <param name="moonRAMin"></param>
		/// <param name="moonRASec"></param>
		/// <param name="moonDecDeg"></param>
		/// <param name="moonDecMin"></param>
		/// <param name="moonDecSec"></param>
		/// <param name="earthMoonDistKM"></param>
		/// <param name="lctHour"></param>
		/// <param name="lctMin"></param>
		/// <param name="lctSec"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrectionHours"></param>
		/// <param name="localDateDay"></param>
		/// <param name="localDateMonth"></param>
		/// <param name="localDateYear"></param>
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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

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
		/// <param name="moonPhase"></param>
		/// <param name="lctHour"></param>
		/// <param name="lctMin"></param>
		/// <param name="lctSec"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrectionHours"></param>
		/// <param name="localDateDay"></param>
		/// <param name="localDateMonth"></param>
		/// <param name="localDateYear"></param>
		/// <param name="accuracyLevel"></param>
		/// <returns>
		/// <para>moonPhase -- Phase of Moon, between 0 and 1, where 0 is New and 1 is Full.</para>
		/// <para>paBrightLimbDeg -- Position angle of the bright limb (degrees)</para>
		/// </returns>
		public (double moonPhase, double paBrightLimbDeg) MoonPhase(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, PAAccuracyLevel accuracyLevel)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var sunLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var moonResult = PAMacros.MoonLongLatHP(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var dRad = (moonResult.moonLongDeg - sunLongDeg).ToRadians();

			var moonPhase1 = (accuracyLevel == PAAccuracyLevel.Precise) ? PAMacros.MoonPhase(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear) : (1.0 - (dRad).Cosine()) / 2.0;

			var sunRARad = (PAMacros.EcRA(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear)).ToRadians();
			var moonRARad = (PAMacros.EcRA(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear)).ToRadians();
			var sunDecRad = (PAMacros.EcDec(sunLongDeg, 0, 0, 0, 0, 0, gdateDay, gdateMonth, gdateYear)).ToRadians();
			var moonDecRad = (PAMacros.EcDec(moonResult.moonLongDeg, 0, 0, moonResult.moonLatDeg, 0, 0, gdateDay, gdateMonth, gdateYear)).ToRadians();

			var y = (sunDecRad).Cosine() * (sunRARad - moonRARad).Sine();
			var x = (moonDecRad).Cosine() * (sunDecRad).Sine() - (moonDecRad).Sine() * (sunDecRad).Cosine() * (sunRARad - moonRARad).Cosine();

			var chiDeg = PAMacros.Degrees(y.AngleTangent2(x));

			var moonPhase = Math.Round(moonPhase1, 2);
			var paBrightLimbDeg = Math.Round(chiDeg, 2);

			return (moonPhase, paBrightLimbDeg);
		}

		/// <summary>
		/// Calculate new moon and full moon instances.
		/// </summary>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrectionHours"></param>
		/// <param name="localDateDay"></param>
		/// <param name="localDateMonth"></param>
		/// <param name="localDateYear"></param>
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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

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
	}
}