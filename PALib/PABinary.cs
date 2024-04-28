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
		BinaryData binaryInfo = BinaryInfo.GetBinaryInfo(binaryName);

		double yYears = greenwichDateYear + (PAMacros.CivilDateToJulianDate(greenwichDateDay, greenwichDateMonth, greenwichDateYear) - PAMacros.CivilDateToJulianDate(0, 1, greenwichDateYear)) / 365.242191 - binaryInfo.EpochPeri;
		double mDeg = 360 * yYears / binaryInfo.Period;
		double mRad = (mDeg - 360 * (mDeg / 360).Floor()).ToRadians();
		double eccentricity = binaryInfo.Ecc;
		double trueAnomalyRad = PAMacros.TrueAnomaly(mRad, eccentricity);
		double rArcsec = (1 - eccentricity * PAMacros.EccentricAnomaly(mRad, eccentricity).Cosine()) * binaryInfo.Axis;
		double taPeriRad = trueAnomalyRad + binaryInfo.LongPeri.ToRadians();

		double y = taPeriRad.Sine() * binaryInfo.Incl.ToRadians().Cosine();
		double x = taPeriRad.Cosine();
		double aDeg = PAMacros.Degrees(y.AngleTangent2(x));
		double thetaDeg1 = aDeg + binaryInfo.PANode;
		double thetaDeg2 = thetaDeg1 - 360 * (thetaDeg1 / 360).Floor();
		double rhoArcsec = rArcsec * taPeriRad.Cosine() / (thetaDeg2 - binaryInfo.PANode).ToRadians().Cosine();

		double positionAngleDeg = Math.Round(thetaDeg2, 1);
		double separationArcsec = Math.Round(rhoArcsec, 2);

		return (positionAngleDeg, separationArcsec);
	}
}