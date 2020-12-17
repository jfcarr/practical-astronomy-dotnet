using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib
{
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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var cometInfo = CometInfoElliptical.GetCometEllipticalInfo(cometName);

			var timeSinceEpochYears = (PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) - PAMacros.CivilDateToJulianDate(0.0, 1, greenwichDateYear)) / 365.242191 + greenwichDateYear - cometInfo.epoch_EpochOfPerihelion;
			var mcDeg = 360 * timeSinceEpochYears / cometInfo.period_PeriodOfOrbit;
			var mcRad = (mcDeg - 360 * (mcDeg / 360).Floor()).ToRadians();
			var eccentricity = cometInfo.ecc_EccentricityOfOrbit;
			var trueAnomalyDeg = PAMacros.Degrees(PAMacros.TrueAnomaly(mcRad, eccentricity));
			var lcDeg = trueAnomalyDeg + cometInfo.peri_LongitudeOfPerihelion;
			var rAU = cometInfo.axis_SemiMajorAxisOfOrbit * (1 - eccentricity * eccentricity) / (1 + eccentricity * ((trueAnomalyDeg).ToRadians()).Cosine());
			var lcNodeRad = (lcDeg - cometInfo.node_LongitudeOfAscendingNode).ToRadians();
			var psiRad = ((lcNodeRad).Sine() * ((cometInfo.incl_InclinationOfOrbit).ToRadians()).Sine()).ASine();

			var y = (lcNodeRad).Sine() * ((cometInfo.incl_InclinationOfOrbit).ToRadians()).Cosine();
			var x = (lcNodeRad).Cosine();

			var ldDeg = PAMacros.Degrees(y.AngleTangent2(x)) + cometInfo.node_LongitudeOfAscendingNode;
			var rdAU = rAU * (psiRad).Cosine();

			var earthLongitudeLeDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear) + 180.0;
			var earthRadiusVectorAU = PAMacros.SunDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var leLdRad = (earthLongitudeLeDeg - ldDeg).ToRadians();
			var aRad = (rdAU < earthRadiusVectorAU) ? (rdAU * (leLdRad).Sine()).AngleTangent2(earthRadiusVectorAU - rdAU * (leLdRad).Cosine()) : (earthRadiusVectorAU * (-leLdRad).Sine()).AngleTangent2(rdAU - earthRadiusVectorAU * (leLdRad).Cosine());

			var cometLongDeg1 = (rdAU < earthRadiusVectorAU) ? 180.0 + earthLongitudeLeDeg + PAMacros.Degrees(aRad) : PAMacros.Degrees(aRad) + ldDeg;
			var cometLongDeg = cometLongDeg1 - 360 * (cometLongDeg1 / 360).Floor();
			var cometLatDeg = PAMacros.Degrees((rdAU * (psiRad).Tangent() * ((cometLongDeg1 - ldDeg).ToRadians()).Sine() / (earthRadiusVectorAU * (-leLdRad).Sine())).AngleTangent());
			var cometRAHours1 = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(cometLongDeg, 0, 0, cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear));
			var cometDecDeg1 = PAMacros.EcDec(cometLongDeg, 0, 0, cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);
			var cometDistanceAU = (Math.Pow(earthRadiusVectorAU, 2) + Math.Pow(rAU, 2) - 2.0 * earthRadiusVectorAU * rAU * ((lcDeg - earthLongitudeLeDeg).ToRadians()).Cosine() * (psiRad).Cosine()).SquareRoot();

			var cometRAHour = PAMacros.DecimalHoursHour(cometRAHours1 + 0.008333);
			var cometRAMin = PAMacros.DecimalHoursMinute(cometRAHours1 + 0.008333);
			var cometDecDeg = PAMacros.DecimalDegreesDegrees(cometDecDeg1 + 0.008333);
			var cometDecMin = PAMacros.DecimalDegreesMinutes(cometDecDeg1 + 0.008333);
			var cometDistEarth = Math.Round(cometDistanceAU, 2);

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
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var cometInfo = CometInfoParabolic.GetCometParabolicInfo(cometName);

			var perihelionEpochDay = cometInfo.EpochPeriDay;
			var perihelionEpochMonth = cometInfo.EpochPeriMonth;
			var perihelionEpochYear = cometInfo.EpochPeriYear;
			var qAU = cometInfo.PeriDist;
			var inclinationDeg = cometInfo.Incl;
			var perihelionDeg = cometInfo.ArgPeri;
			var nodeDeg = cometInfo.Node;

			var cometLongLatDist = PAMacros.PCometLongLatDist(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, perihelionEpochDay, perihelionEpochMonth, perihelionEpochYear, qAU, inclinationDeg, perihelionDeg, nodeDeg);

			var cometRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(cometLongLatDist.cometLongDeg, 0, 0, cometLongLatDist.cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear));
			var cometDecDeg1 = PAMacros.EcDec(cometLongLatDist.cometLongDeg, 0, 0, cometLongLatDist.cometLatDeg, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear);

			var cometRAHour = PAMacros.DecimalHoursHour(cometRAHours);
			var cometRAMin = PAMacros.DecimalHoursMinute(cometRAHours);
			var cometRASec = PAMacros.DecimalHoursSecond(cometRAHours);
			var cometDecDeg = PAMacros.DecimalDegreesDegrees(cometDecDeg1);
			var cometDecMin = PAMacros.DecimalDegreesMinutes(cometDecDeg1);
			var cometDecSec = PAMacros.DecimalDegreesSeconds(cometDecDeg1);
			var cometDistEarth = Math.Round(cometLongLatDist.cometDistAU, 2);

			return (cometRAHour, cometRAMin, cometRASec, cometDecDeg, cometDecMin, cometDecSec, cometDistEarth);
		}
	}
}