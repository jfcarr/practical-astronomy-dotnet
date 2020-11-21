using System;

namespace PALib.Helpers
{
	public static class MathExtensions
	{
		/// <summary>
		/// Returns the angle whose tangent is the quotient of two specified numbers.
		/// </summary>
		/// <param name="y"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double AngleTangent(this double y, double x)
		{
			return Math.Atan2(y, x);
		}

		/// <summary>
		/// Returns the angle whose sine is the specified number.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double ASine(this double d)
		{
			return Math.Asin(d);
		}

		/// <summary>
		/// Returns the cosine of the specified angle.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static double Cosine(this double d)
		{
			return Math.Cos(d);
		}

		/// <summary>
		/// Returns the largest integral value less than or equal to the specified double-precision floating-point number.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double Floor(this double d)
		{
			return Math.Floor(d);
		}

		/// <summary>
		/// Returns the sine of the specified angle.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static double Sine(this double a)
		{
			return Math.Sin(a);
		}

		/// <summary>
		/// Returns the tangent of the specified angle.
		/// /// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static double Tangent(this double a)
		{
			return Math.Tan(a);
		}

		/// <summary>
		/// Convert radians to degrees.
		/// </summary>
		/// <param name="radians"></param>
		/// <returns></returns>
		public static double ToDegrees(this double radians)
		{
			return radians * Math.PI / 180;
		}

		/// <summary>
		/// Convert degrees to radians.
		/// </summary>
		/// <param name="degrees"></param>
		/// <returns></returns>
		public static double ToRadians(this double degrees)
		{
			return degrees * (Math.PI / 180);
		}
	}
}