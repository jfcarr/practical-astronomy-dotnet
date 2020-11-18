using System;

namespace PALib
{
	public class PACoordinates
	{
		/// <summary>
		/// Convert an Angle (degrees, minutes, and seconds) to Decimal Degrees
		/// </summary>
		/// <param name="degrees"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns>Decimal Degrees (double)</returns>
		public double AngleToDecimalDegrees(double degrees, double minutes, double seconds)
		{
			var a = Math.Abs(seconds) / 60;
			var b = (Math.Abs(minutes) + a) / 60;
			var c = Math.Abs(degrees) + b;
			var d = (degrees < 0 || minutes < 0 || seconds < 0) ? -c : c;

			return d;
		}

		/// <summary>
		/// Convert Decimal Degrees to an Angle (degrees, minutes, and seconds)
		/// </summary>
		/// <param name="degrees"></param>
		/// <param name="minutes"></param>
		/// <param name="decimalDegrees"></param>
		/// <returns>Tuple (degrees, minutes, seconds)</returns>
		public (double degrees, double minutes, double seconds) DecimalDegreesToAngle(double decimalDegrees)
		{
			var unsignedDecimal = Math.Abs(decimalDegrees);
			var totalSeconds = unsignedDecimal * 3600;
			var seconds2DP = Math.Round(totalSeconds % 60, 2);
			var correctedSeconds = (seconds2DP == 60) ? 0 : seconds2DP;
			var correctedRemainder = (seconds2DP == 60) ? totalSeconds + 60 : totalSeconds;
			var minutes = Math.Floor(correctedRemainder / 60) % 60;
			var unsignedDegrees = Math.Floor(correctedRemainder / 3600);
			var signedDegrees = (decimalDegrees < 0) ? -1 * unsignedDegrees : unsignedDegrees;

			return (signedDegrees, minutes, Math.Floor(correctedSeconds));
		}
	}
}