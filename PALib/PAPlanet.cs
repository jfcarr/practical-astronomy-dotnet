using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Planet calculations.
/// </summary>
public class PAPlanet
{
	/// <summary>
	/// Calculate approximate position of a planet.
	/// </summary>
	public (double planetRAHour, double planetRAMin, double planetRASec, double planetDecDeg, double planetDecMin, double planetDecSec) ApproximatePositionOfPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		PlanetData planetInfo = PlanetInfo.GetPlanetInfo(planetName);

		double gdateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int gdateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double utHours = PAMacros.LocalCivilTimeToUniversalTime(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double dDays = PAMacros.CivilDateToJulianDate(gdateDay + (utHours / 24), gdateMonth, gdateYear) - PAMacros.CivilDateToJulianDate(0, 1, 2010);
		double npDeg1 = 360 * dDays / (365.242191 * planetInfo.tp_PeriodOrbit);
		double npDeg2 = npDeg1 - 360 * (npDeg1 / 360).Floor();
		double mpDeg = npDeg2 + planetInfo.long_LongitudeEpoch - planetInfo.peri_LongitudePerihelion;
		double lpDeg1 = npDeg2 + (360 * planetInfo.ecc_EccentricityOrbit * mpDeg.ToRadians().Sine() / Math.PI) + planetInfo.long_LongitudeEpoch;
		double lpDeg2 = lpDeg1 - 360 * (lpDeg1 / 360).Floor();
		double planetTrueAnomalyDeg = lpDeg2 - planetInfo.peri_LongitudePerihelion;
		double rAU = planetInfo.axis_AxisOrbit * (1 - Math.Pow(planetInfo.ecc_EccentricityOrbit, 2)) / (1 + planetInfo.ecc_EccentricityOrbit * planetTrueAnomalyDeg.ToRadians().Cosine());

		PlanetData earthInfo = PlanetInfo.GetPlanetInfo("Earth");

		double neDeg1 = 360 * dDays / (365.242191 * earthInfo.tp_PeriodOrbit);
		double neDeg2 = neDeg1 - 360 * (neDeg1 / 360).Floor();
		double meDeg = neDeg2 + earthInfo.long_LongitudeEpoch - earthInfo.peri_LongitudePerihelion;
		double leDeg1 = neDeg2 + earthInfo.long_LongitudeEpoch + 360 * earthInfo.ecc_EccentricityOrbit * meDeg.ToRadians().Sine() / Math.PI;
		double leDeg2 = leDeg1 - 360 * (leDeg1 / 360).Floor();
		double earthTrueAnomalyDeg = leDeg2 - earthInfo.peri_LongitudePerihelion;
		double rAU2 = earthInfo.axis_AxisOrbit * (1 - Math.Pow(earthInfo.ecc_EccentricityOrbit, 2)) / (1 + earthInfo.ecc_EccentricityOrbit * earthTrueAnomalyDeg.ToRadians().Cosine());
		double lpNodeRad = (lpDeg2 - planetInfo.node_LongitudeAscendingNode).ToRadians();
		double psiRad = (lpNodeRad.Sine() * planetInfo.incl_OrbitalInclination.ToRadians().Sine()).ASine();
		double y = lpNodeRad.Sine() * planetInfo.incl_OrbitalInclination.ToRadians().Cosine();
		double x = lpNodeRad.Cosine();
		double ldDeg = PAMacros.Degrees(y.AngleTangent2(x)) + planetInfo.node_LongitudeAscendingNode;
		double rdAU = rAU * psiRad.Cosine();
		double leLdRad = (leDeg2 - ldDeg).ToRadians();
		double atan2Type1 = (rdAU * leLdRad.Sine()).AngleTangent2(rAU2 - rdAU * leLdRad.Cosine());
		double atan2Type2 = (rAU2 * (-leLdRad).Sine()).AngleTangent2(rdAU - rAU2 * leLdRad.Cosine());
		double aRad = (rdAU < 1) ? atan2Type1 : atan2Type2;
		double lamdaDeg1 = (rdAU < 1) ? 180 + leDeg2 + PAMacros.Degrees(aRad) : PAMacros.Degrees(aRad) + ldDeg;
		double lamdaDeg2 = lamdaDeg1 - 360 * (lamdaDeg1 / 360).Floor();
		double betaDeg = PAMacros.Degrees((rdAU * psiRad.Tangent() * (lamdaDeg2 - ldDeg).ToRadians().Sine() / (rAU2 * (-leLdRad).Sine())).AngleTangent());
		double raHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(lamdaDeg2, 0, 0, betaDeg, 0, 0, gdateDay, gdateMonth, gdateYear));
		double decDeg = PAMacros.EcDec(lamdaDeg2, 0, 0, betaDeg, 0, 0, gdateDay, gdateMonth, gdateYear);

		int planetRAHour = PAMacros.DecimalHoursHour(raHours);
		int planetRAMin = PAMacros.DecimalHoursMinute(raHours);
		double planetRASec = PAMacros.DecimalHoursSecond(raHours);
		double planetDecDeg = PAMacros.DecimalDegreesDegrees(decDeg);
		double planetDecMin = PAMacros.DecimalDegreesMinutes(decDeg);
		double planetDecSec = PAMacros.DecimalDegreesSeconds(decDeg);

		return (planetRAHour, planetRAMin, planetRASec, planetDecDeg, planetDecMin, planetDecSec);
	}

	/// <summary>
	/// Calculate precise position of a planet.
	/// </summary>
	public (double planetRAHour, double planetRAMin, double planetRASec, double planetDecDeg, double planetDecMin, double planetDecSec) PrecisePositionOfPlanet(double lctHour, double lctMin, double lctSec, bool isDaylightSaving, int zoneCorrectionHours, double localDateDay, int localDateMonth, int localDateYear, string planetName)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		(double planetLongitude, double planetLatitude, double planetDistanceAU, double planetHLong1, double planetHLong2, double planetHLat, double planetRVect) coordinateResults = PAMacros.PlanetCoordinates(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, planetName);

		double planetRAHours = PAMacros.DecimalDegreesToDegreeHours(PAMacros.EcRA(coordinateResults.planetLongitude, 0, 0, coordinateResults.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear));
		double planetDecDeg1 = PAMacros.EcDec(coordinateResults.planetLongitude, 0, 0, coordinateResults.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear);

		int planetRAHour = PAMacros.DecimalHoursHour(planetRAHours);
		int planetRAMin = PAMacros.DecimalHoursMinute(planetRAHours);
		double planetRASec = PAMacros.DecimalHoursSecond(planetRAHours);
		double planetDecDeg = PAMacros.DecimalDegreesDegrees(planetDecDeg1);
		double planetDecMin = PAMacros.DecimalDegreesMinutes(planetDecDeg1);
		double planetDecSec = PAMacros.DecimalDegreesSeconds(planetDecDeg1);

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
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double greenwichDateDay = PAMacros.LocalCivilTimeGreenwichDay(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateMonth = PAMacros.LocalCivilTimeGreenwichMonth(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		int greenwichDateYear = PAMacros.LocalCivilTimeGreenwichYear(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		(double planetLongitude, double planetLatitude, double planetDistanceAU, double planetHLong1, double planetHLong2, double planetHLat, double planetRVect) planetCoordInfo = PAMacros.PlanetCoordinates(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear, planetName);

		double planetRARad = PAMacros.EcRA(planetCoordInfo.planetLongitude, 0, 0, planetCoordInfo.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear).ToRadians();
		double planetDecRad = PAMacros.EcDec(planetCoordInfo.planetLongitude, 0, 0, planetCoordInfo.planetLatitude, 0, 0, localDateDay, localDateMonth, localDateYear).ToRadians();

		double lightTravelTimeHours = planetCoordInfo.planetDistanceAU * 0.1386;
		PlanetData planetInfo = PlanetInfo.GetPlanetInfo(planetName);
		double angularDiameterArcsec = planetInfo.theta0_AngularDiameter / planetCoordInfo.planetDistanceAU;
		double phase1 = 0.5 * (1.0 + (planetCoordInfo.planetLongitude - planetCoordInfo.planetHLong1).ToRadians().Cosine());

		double sunEclLongDeg = PAMacros.SunLong(lctHour, lctMin, lctSec, daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double sunRARad = PAMacros.EcRA(sunEclLongDeg, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear).ToRadians();
		double sunDecRad = PAMacros.EcDec(sunEclLongDeg, 0, 0, 0, 0, 0, greenwichDateDay, greenwichDateMonth, greenwichDateYear).ToRadians();

		double y = sunDecRad.Cosine() * (sunRARad - planetRARad).Sine();
		double x = planetDecRad.Cosine() * sunDecRad.Sine() - planetDecRad.Sine() * sunDecRad.Cosine() * (sunRARad - planetRARad).Cosine();

		double chiDeg = PAMacros.Degrees(y.AngleTangent2(x));
		double radiusVectorAU = planetCoordInfo.planetRVect;
		double approximateMagnitude1 = 5.0 * (radiusVectorAU * planetCoordInfo.planetDistanceAU / phase1.SquareRoot()).Log10() + planetInfo.v0_VisualMagnitude;

		double distanceAU = Math.Round(planetCoordInfo.planetDistanceAU, 5);
		double angDiaArcsec = Math.Round(angularDiameterArcsec, 1);
		double phase = Math.Round(phase1, 2);
		int lightTimeHour = PAMacros.DecimalHoursHour(lightTravelTimeHours);
		int lightTimeMinutes = PAMacros.DecimalHoursMinute(lightTravelTimeHours);
		double lightTimeSeconds = PAMacros.DecimalHoursSecond(lightTravelTimeHours);
		double posAngleBrightLimbDeg = Math.Round(chiDeg, 1);
		double approximateMagnitude = Math.Round(approximateMagnitude1, 1);

		return (distanceAU, angDiaArcsec, phase, lightTimeHour, lightTimeMinutes, lightTimeSeconds, posAngleBrightLimbDeg, approximateMagnitude);
	}
}
