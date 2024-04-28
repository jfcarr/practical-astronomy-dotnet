using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Comet calculations.
/// </summary>
public class PAComet
{
	/// <summary>
	/// Calculate position of an elliptical comet.
	/// </summary>
	/// <returns>
	/// cometRAHour -- Right ascension of comet (hour part)
	/// cometRAMin -- Right ascension of comet (minutes part)
	/// cometDecDeg -- Declination of comet (degrees part)
	/// cometDecMin -- Declination of comet (minutes part)
	/// cometDistEarth -- Comet's distance from Earth (AU)
	/// </returns>
	public (double cometRAHour, double cometRAMin, double cometDecDeg, double cometDecMin, double cometDistEarth) PositionOfEllipticalComet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string cometName)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		CometDataElliptical cometInfo = CometInfoElliptical.GetCometEllipticalInfo(cometName);

		double timeSinceEpochYears = (PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) - PAMacros.CivilDateToJulianDate(0.0, 1, greenwichDateYear)) / 365.242191 + greenwichDateYear - cometInfo.epoch_EpochOfPerihelion;
		double mcDeg = 360 * timeSinceEpochYears / cometInfo.period_PeriodOfOrbit;
		double mcRad = (mcDeg - 360 * (mcDeg / 360).Floor()).ToRadians();
		double eccentricity = cometInfo.ecc_EccentricityOfOrbit;
		double trueAnomalyDeg = PAMacros.Degrees(PAMacros.TrueAnomaly(mcRad, eccentricity));
		double lcDeg = trueAnomalyDeg + cometInfo.peri_LongitudeOfPerihelion;
		double rAU = cometInfo.axis_SemiMajorAxisOfOrbit * (1 - eccentricity * eccentricity) / (1 + eccentricity * trueAnomalyDeg.ToRadians().Cosine());
		double lcNodeRad = (lcDeg - cometInfo.node_LongitudeOfAscendingNode).ToRadians();
		double psiRad = (lcNodeRad.Sine() * cometInfo.incl_InclinationOfOrbit.ToRadians().Sine()).ASine();

		double y = lcNodeRad.Sine() * cometInfo.incl_InclinationOfOrbit.ToRadians().Cosine();
		double x = lcNodeRad.Cosine();

		double ldDeg = PAMacros.Degrees(y.AngleTangent2(x)) + cometInfo.node_LongitudeOfAscendingNode;
		double rdAU = rAU * psiRad.Cosine();

		double earthLongitudeLeDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear) + 180.0;
		double earthRadiusVectorAU = PAMacros.SunDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double leLdRad = (earthLongitudeLeDeg - ldDeg).ToRadians();
		double aRad = (rdAU < earthRadiusVectorAU) ? (rdAU * leLdRad.Sine()).AngleTangent2(earthRadiusVectorAU - rdAU * leLdRad.Cosine()) : (earthRadiusVectorAU * (-leLdRad).Sine()).AngleTangent2(rdAU - earthRadiusVectorAU * leLdRad.Cosine());

		double cometLongDeg1 = (rdAU < earthRadiusVectorAU) ? 180.0 + earthLongitudeLeDeg + PAMacros.Degrees(aRad) : PAMacros.Degrees(aRad) + ldDeg;
		double cometLongDeg = cometLongDeg1 - 360 * (cometLongDeg1 / 360).Floor();
		double cometLatDeg = PAMacros.Degrees((rdAU * psiRad.Tangent() * (cometLongDeg1 - ldDeg).ToRadians().Sine() / (earthRadiusVectorAU * (-leLdRad).Sine())).AngleTangent());
		double cometRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(cometLongDeg, 0, 0, cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear));
		double cometDecDeg1 = PAMacros.EcDec(cometLongDeg, 0, 0, cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);
		double cometDistanceAU = (Math.Pow(earthRadiusVectorAU, 2) + Math.Pow(rAU, 2) - 2.0 * earthRadiusVectorAU * rAU * (lcDeg - earthLongitudeLeDeg).ToRadians().Cosine() * psiRad.Cosine()).SquareRoot();

		int cometRAHour = PAMacros.DecimalHoursHour(cometRAHours1 + 0.008333);
		int cometRAMin = PAMacros.DecimalHoursMinute(cometRAHours1 + 0.008333);
		double cometDecDeg = PAMacros.DecimalDegreesDegrees(cometDecDeg1 + 0.008333);
		double cometDecMin = PAMacros.DecimalDegreesMinutes(cometDecDeg1 + 0.008333);
		double cometDistEarth = Math.Round(cometDistanceAU, 2);

		return (cometRAHour, cometRAMin, cometDecDeg, cometDecMin, cometDistEarth);
	}

	/// <summary>
	/// Calculate position of a parabolic comet.
	/// </summary>
	/// <returns>
	/// cometRAHour -- Right ascension of comet (hour part)
	/// cometRAMin -- Right ascension of comet (minutes part)
	/// cometRASec -- Right ascension of comet (seconds part)
	/// cometDecDeg -- Declination of comet (degrees part)
	/// cometDecMin -- Declination of comet (minutes part)
	/// cometDecSec -- Declination of comet (seconds part)
	/// cometDistEarth -- Comet's distance from Earth (AU)
	/// </returns>
	/// <returns></returns>
	public (double cometRAHour, double cometRAMin, double cometRASec, double cometDecDeg, double cometDecMin, double cometDecSec, double cometDistEarth) PositionOfParabolicComet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string cometName)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		CometDataParabolic cometInfo = CometInfoParabolic.GetCometParabolicInfo(cometName);

		double perihelionEpochDay = cometInfo.EpochPeriDay;
		int perihelionEpochMonth = cometInfo.EpochPeriMonth;
		int perihelionEpochYear = cometInfo.EpochPeriYear;
		double qAU = cometInfo.PeriDist;
		double inclinationDeg = cometInfo.Incl;
		double perihelionDeg = cometInfo.ArgPeri;
		double nodeDeg = cometInfo.Node;

		(double cometLongDeg, double cometLatDeg, double cometDistAU) cometLongLatDist = PAMacros.PCometLongLatDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, perihelionEpochDay, perihelionEpochMonth, perihelionEpochYear, qAU, inclinationDeg, perihelionDeg, nodeDeg);

		double cometRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(cometLongLatDist.cometLongDeg, 0, 0, cometLongLatDist.cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear));
		double cometDecDeg1 = PAMacros.EcDec(cometLongLatDist.cometLongDeg, 0, 0, cometLongLatDist.cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);

		int cometRAHour = PAMacros.DecimalHoursHour(cometRAHours);
		int cometRAMin = PAMacros.DecimalHoursMinute(cometRAHours);
		double cometRASec = PAMacros.DecimalHoursSecond(cometRAHours);
		double cometDecDeg = PAMacros.DecimalDegreesDegrees(cometDecDeg1);
		double cometDecMin = PAMacros.DecimalDegreesMinutes(cometDecDeg1);
		double cometDecSec = PAMacros.DecimalDegreesSeconds(cometDecDeg1);
		double cometDistEarth = Math.Round(cometLongLatDist.cometDistAU, 2);

		return (cometRAHour, cometRAMin, cometRASec, cometDecDeg, cometDecMin, cometDecSec, cometDistEarth);
	}
}
