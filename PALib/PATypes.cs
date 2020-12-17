using System;
namespace PALib
{
	public enum PACoordinateType
	{
		Apparent,
		True
	}

	/// <summary>
	/// Twilight type
	/// </summary>
	/// <remarks>
	/// Maps to degrees-below-horizon.
	/// </remarks>
	public enum PATwilightType
	{
		Civil = 6,
		Nautical = 12,
		Astronomical = 18
	}

	public enum PAAngleMeasure
	{
		Degrees,
		Hours
	}

	/// <summary>
	/// Accuracy level of calculation.
	/// </summary>
	public enum PAAccuracyLevel
	{
		Approximate,
		Precise
	}

	public enum PAWarningFlag
	{
		OK,
		Warning
	}
}