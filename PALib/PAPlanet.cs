using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib
{
	public class PAPlanet
	{
		/// <summary>
		/// Calculate approximate position of a planet.
		/// </summary>
		public (double planetRAHour, double planetRAMin, double planetRASec, double planetDecDeg, double planetDecMin, double planetDecSec) ApproximatePositionOfPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var planetInfo = PlanetInfo.GetPlanetInfo(planetName);

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

			var earthInfo = PlanetInfo.GetPlanetInfo("Earth");

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

		/// <summary>
		/// Calculate precise position of a planet.
		/// </summary>
		public (double planetRAHour, double planetRAMin, double planetRASec, double planetDecDeg, double planetDecMin, double planetDecSec) PrecisePositionOfPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var coordinateResults = PAMacros.PlanetCoordinates(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, planetName);

			var planetRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(coordinateResults.planetLongitude, 0, 0, coordinateResults.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear));
			var planetDecDeg1 = PAMacros.EcDec(coordinateResults.planetLongitude, 0, 0, coordinateResults.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear);

			var planetRAHour = PAMacros.DecimalHoursHour(planetRAHours);
			var planetRAMin = PAMacros.DecimalHoursMinute(planetRAHours);
			var planetRASec = PAMacros.DecimalHoursSecond(planetRAHours);
			var planetDecDeg = PAMacros.DecimalDegreesDegrees(planetDecDeg1);
			var planetDecMin = PAMacros.DecimalDegreesMinutes(planetDecDeg1);
			var planetDecSec = PAMacros.DecimalDegreesSeconds(planetDecDeg1);

			return (planetRAHour, planetRAMin, planetRASec, planetDecDeg, planetDecMin, planetDecSec);
		}

		/// <summary>
		/// Calculate several visual aspects of a planet.
		/// </summary>
		/// <returns>
		/// <para>distance_au -- Planet's distance from Earth, in AU.</para>
		/// <para>ang_dia_arcsec -- Angular diameter of the planet.</para>
		/// <para>phase -- Illuminated fraction of the planet.</para>
		/// <para>light_time_hour -- Light travel time from planet to Earth, hour part.</para>
		/// <para>light_time_minutes -- Light travel time from planet to Earth, minutes part.</para>
		/// <para>light_time_seconds -- Light travel time from planet to Earth, seconds part.</para>
		/// <para>pos_angle_bright_limb_deg -- Position-angle of the bright limb.</para>
		/// <para>approximate_magnitude -- Apparent brightness of the planet.</para>
		/// </returns>
		public (double distanceAU, double angDiaArcsec, double phase, double lightTimeHour, double lightTimeMinutes, double lightTimeSeconds, double posAngleBrightLimbDeg, double approximateMagnitude) VisualAspectsOfAPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

			var planetCoordInfo = PAMacros.PlanetCoordinates(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, planetName);

			var planetRARad = (PAMacros.EcRA(planetCoordInfo.planetLongitude, 0, 0, planetCoordInfo.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear)).ToRadians();
			var planetDecRad = (PAMacros.EcDec(planetCoordInfo.planetLongitude, 0, 0, planetCoordInfo.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear)).ToRadians();

			var lightTravelTimeHours = planetCoordInfo.planetDistanceAU * 0.1386;
			var planetInfo = PlanetInfo.GetPlanetInfo(planetName);
			var angularDiameterArcsec = planetInfo.theta0_AngularDiameter / planetCoordInfo.planetDistanceAU;
			var phase1 = 0.5 * (1.0 + ((planetCoordInfo.planetLongitude - planetCoordInfo.planetHLong1).ToRadians()).Cosine());

			var sunEclLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
			var sunRARad = (PAMacros.EcRA(sunEclLongDeg, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear)).ToRadians();
			var sunDecRad = (PAMacros.EcDec(sunEclLongDeg, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear)).ToRadians();

			var y = (sunDecRad).Cosine() * (sunRARad - planetRARad).Sine();
			var x = (planetDecRad).Cosine() * (sunDecRad).Sine() - (planetDecRad).Sine() * (sunDecRad).Cosine() * (sunRARad - planetRARad).Cosine();

			var chiDeg = PAMacros.Degrees(y.AngleTangent2(x));
			var radiusVectorAU = planetCoordInfo.planetRVect;
			var approximateMagnitude1 = 5.0 * (radiusVectorAU * planetCoordInfo.planetDistanceAU / (phase1).SquareRoot()).Log10() + planetInfo.v0_VisualMagnitude;

			var distanceAU = Math.Round(planetCoordInfo.planetDistanceAU, 5);
			var angDiaArcsec = Math.Round(angularDiameterArcsec, 1);
			var phase = Math.Round(phase1, 2);
			var lightTimeHour = PAMacros.DecimalHoursHour(lightTravelTimeHours);
			var lightTimeMinutes = PAMacros.DecimalHoursMinute(lightTravelTimeHours);
			var lightTimeSeconds = PAMacros.DecimalHoursSecond(lightTravelTimeHours);
			var posAngleBrightLimbDeg = Math.Round(chiDeg, 1);
			var approximateMagnitude = Math.Round(approximateMagnitude1, 1);

			return (distanceAU, angDiaArcsec, phase, lightTimeHour, lightTimeMinutes, lightTimeSeconds, posAngleBrightLimbDeg, approximateMagnitude);
		}
	}
}