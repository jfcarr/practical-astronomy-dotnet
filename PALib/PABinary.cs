using System;
using PALib.Data;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Binary star calculations.
/// </summary>
public class PABinary
{
	/// <summary>
	/// Calculate orbital data for binary star.
	/// </summary>
	/// <returns>
	/// <para>positionAngleDeg -- Position angle (degrees)</para>
	/// <para>separationArcsec -- Separation of binary members (arcseconds)</para>
	/// </returns>
	public (double positionAngleDeg, double separationArcsec) BinaryStarOrbit(double greenwichDateDay, int greenwichDateMonth, int greenwichDateYear, string binaryName)
	{
		var binaryInfo = BinaryInfo.GetBinaryInfo(binaryName);

		var yYears = (greenwichDateYear + (PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) - PAMacros.CivilDateToJulianDate(0, 1, greenwichDateYear)) / 365.242191) - binaryInfo.EpochPeri;
		var mDeg = 360 * yYears / binaryInfo.Period;
		var mRad = (mDeg - 360 * (mDeg / 360).Floor()).ToRadians();
		var eccentricity = binaryInfo.Ecc;
		var trueAnomalyRad = PAMacros.TrueAnomaly(mRad, eccentricity);
		var rArcsec = (1 - eccentricity * (PAMacros.EccentricAnomaly(mRad, eccentricity)).Cosine()) * binaryInfo.Axis;
		var taPeriRad = trueAnomalyRad + binaryInfo.LongPeri.ToRadians();

		var y = (taPeriRad).Sine() * ((binaryInfo.Incl).ToRadians()).Cosine();
		var x = (taPeriRad).Cosine();
		var aDeg = PAMacros.Degrees(y.AngleTangent2(x));
		var thetaDeg1 = aDeg + binaryInfo.PANode;
		var thetaDeg2 = thetaDeg1 - 360 * (thetaDeg1 / 360).Floor();
		var rhoArcsec = rArcsec * (taPeriRad).Cosine() / ((thetaDeg2 - binaryInfo.PANode).ToRadians()).Cosine();

		var positionAngleDeg = Math.Round(thetaDeg2, 1);
		var separationArcsec = Math.Round(rhoArcsec, 2);

		return (positionAngleDeg, separationArcsec);
	}
}