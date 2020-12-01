using System;
using PALib.Helpers;

namespace PALib
{
	public class PAPlanet
	{
		/// <summary>
		/// Calculate approximate position of a planet.
		/// </summary>
		/// <param name="lctHour"></param>
		/// <param name="lctMin"></param>
		/// <param name="lctSec"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrectionHours"></param>
		/// <param name="localDateDay"></param>
		/// <param name="localDateMonth"></param>
		/// <param name="localDateYear"></param>
		/// <param name="planetName"></param>
		/// <returns></returns>
		public (double planetRAHour, double planetRAMin, double planetRASec, double planetDecDeg, double planetDecMin, double planetDecSec) ApproximatePositionOfPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var planetInfo = PAPlanetInfo.GetPlanetInfo(planetName);

			var gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var dDays = PAMacros.CivilDateToJulianDate(gdateDay + (utHours / 24), gdateMonth, gdateYear) - PAMacros.CivilDateToJulianDate(0, 1, 2010);
			var npDeg1 = 360 * dDays / (365.242191 * planetInfo.tp_PeriodOrbit);
			var npDeg2 = npDeg1 - 360 * (npDeg1 / 360).Floor();
			var mpDeg = npDeg2 + planetInfo.long_LongitudeEpoch - planetInfo.peri_LongitudePerihelion;
			var lpDeg1 = npDeg2 + (360 * planetInfo.ecc_EccentricityOrbit * mpDeg.ToRadians().Sine() / Math.PI) + planetInfo.long_LongitudeEpoch;
			var lpDeg2 = lpDeg1 - 360 * (lpDeg1 / 360).Floor();
			var planetTrueAnomalyDeg = lpDeg2 - planetInfo.peri_LongitudePerihelion;
			var rAU = planetInfo.axis_AxisOrbit * (1 - Math.Pow(planetInfo.ecc_EccentricityOrbit, 2)) / (1 + planetInfo.ecc_EccentricityOrbit * planetTrueAnomalyDeg.ToRadians().Cosine());

			var earthInfo = PAPlanetInfo.GetPlanetInfo("Earth");

			var neDeg1 = 360 * dDays / (365.242191 * earthInfo.tp_PeriodOrbit);
			var neDeg2 = neDeg1 - 360 * (neDeg1 / 360).Floor();
			var meDeg = neDeg2 + earthInfo.long_LongitudeEpoch - earthInfo.peri_LongitudePerihelion;
			var leDeg1 = neDeg2 + earthInfo.long_LongitudeEpoch + 360 * earthInfo.ecc_EccentricityOrbit * meDeg.ToRadians().Sine() / Math.PI;
			var leDeg2 = leDeg1 - 360 * (leDeg1 / 360).Floor();
			var earthTrueAnomalyDeg = leDeg2 - earthInfo.peri_LongitudePerihelion;
			var rAU2 = earthInfo.axis_AxisOrbit * (1 - Math.Pow(earthInfo.ecc_EccentricityOrbit, 2)) / (1 + earthInfo.ecc_EccentricityOrbit * earthTrueAnomalyDeg.ToRadians().Cosine());
			var lpNodeRad = (lpDeg2 - planetInfo.node_LongitudeAscendingNode).ToRadians();
			var psiRad = ((lpNodeRad).Sine() * planetInfo.incl_OrbitalInclination.ToRadians().Sine()).ASine();
			var y = lpNodeRad.Sine() * planetInfo.incl_OrbitalInclination.ToRadians().Cosine();
			var x = lpNodeRad.Cosine();
			var ldDeg = PAMacros.Degrees(y.AngleTangent2(x)) + planetInfo.node_LongitudeAscendingNode;
			var rdAU = rAU * psiRad.Cosine();
			var leLdRad = (leDeg2 - ldDeg).ToRadians();
			var atan2Type1 = (rdAU * leLdRad.Sine()).AngleTangent2(rAU2 - rdAU * leLdRad.Cosine());
			var atan2Type2 = (rAU2 * (-leLdRad).Sine()).AngleTangent2(rdAU - rAU2 * leLdRad.Cosine());
			var aRad = (rdAU < 1) ? atan2Type1 : atan2Type2;
			var lamdaDeg1 = (rdAU < 1) ? 180 + leDeg2 + PAMacros.Degrees(aRad) : PAMacros.Degrees(aRad) + ldDeg;
			var lamdaDeg2 = lamdaDeg1 - 360 * (lamdaDeg1 / 360).Floor();
			var betaDeg = PAMacros.Degrees((rdAU * psiRad.Tangent() * ((lamdaDeg2 - ldDeg).ToRadians()).Sine() / (rAU2 * (-leLdRad).Sine())).AngleTangent());
			var raHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(lamdaDeg2, 0, 0, betaDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
			var decDeg = PAMacros.EcDec(lamdaDeg2, 0, 0, betaDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

			var planetRAHour = PAMacros.DecimalHoursHour(raHours);
			var planetRAMin = PAMacros.DecimalHoursMinute(raHours);
			var planetRASec = PAMacros.DecimalHoursSecond(raHours);
			var planetDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
			var planetDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
			var planetDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

			return (planetRAHour, planetRAMin, planetRASec, planetDecDeg, planetDecMin, planetDecSec);
		}
	}
}