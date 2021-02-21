using System;
namespace PALib
{
	/// <summary>
	/// Coordinate types
	/// </summary>
	public enum PACoordinateType
	{
		/// <summary>
		/// Apparent (observer)
		/// </summary>
		Apparent,

		/// <summary>
		/// Actual/real
		/// </summary>
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
		/// <summary>
		/// First period of twilight.
		/// </summary>
		Civil = 6,

		/// <summary>
		/// Second period of twilight.
		/// </summary>
		Nautical = 12,

		/// <summary>
		/// Second period of twilight.
		/// </summary>
		Astronomical = 18
	}

	/// <summary>
	/// Angle measurement units.
	/// </summary>
	public enum PAAngleMeasure
	{
		/// <summary>
		/// Measurement by degrees.
		/// </summary>
		Degrees,

		/// <summary>
		/// Measurement by hours.
		/// </summary>
		Hours
	}

	/// <summary>
	/// Accuracy level of calculation.
	/// </summary>
	public enum PAAccuracyLevel
	{
		/// <summary>
		/// Approximate value.
		/// </summary>
		Approximate,

		/// <summary>
		/// Precise value.
		/// </summary>
		Precise
	}

	/// <summary>
	/// Warning flags for calculation results.
	/// </summary>
	public enum PAWarningFlag
	{
		/// <summary>
		/// Calculation result is OK.
		/// </summary>
		OK,

		/// <summary>
		/// Calculation result is invalid/inaccurate.
		/// </summary>
		Warning
	}
}