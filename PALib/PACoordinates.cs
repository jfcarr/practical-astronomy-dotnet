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

		/// <summary>
		/// Convert Right Ascension to Hour Angle
		/// </summary>
		/// <param name="raHours"></param>
		/// <param name="raMinutes"></param>
		/// <param name="raSeconds"></param>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="isDaylightSavings"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public (double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds) RightAscensionToHourAngle(double raHours, double raMinutes, double raSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSavings, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var daylightSaving = (isDaylightSavings) ? 1 : 0;

			var hourAngle = PAMacros.RightAscensionToHourAngle(raHours, raMinutes, raSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

			var hourAngleHours = PAMacros.DecimalHoursHour(hourAngle);
			var hourAngleMinutes = PAMacros.DecimalHoursMinute(hourAngle);
			var hourAngleSeconds = PAMacros.DecimalHoursSecond(hourAngle);

			return (hourAngleHours, hourAngleMinutes, hourAngleSeconds);
		}

		/// <summary>
		/// Convert Hour Angle to Right Ascension
		/// </summary>
		/// <param name="hourAngleHours"></param>
		/// <param name="hourAngleMinutes"></param>
		/// <param name="hourAngleSeconds"></param>
		/// <param name="lctHours"></param>
		/// <param name="lctMinutes"></param>
		/// <param name="lctSeconds"></param>
		/// <param name="isDaylightSaving"></param>
		/// <param name="zoneCorrection"></param>
		/// <param name="localDay"></param>
		/// <param name="localMonth"></param>
		/// <param name="localYear"></param>
		/// <param name="geographicalLongitude"></param>
		/// <returns></returns>
		public (double raHours, double raMinutes, double raSeconds) HourAngleToRightAscension(double hourAngleHours, double hourAngleMinutes, double hourAngleSeconds, double lctHours, double lctMinutes, double lctSeconds, bool isDaylightSaving, int zoneCorrection, double localDay, int localMonth, int localYear, double geographicalLongitude)
		{
			var daylightSaving = (isDaylightSaving) ? 1 : 0;

			var rightAscension = PAMacros.HourAngleToRightAscension(hourAngleHours, hourAngleMinutes, hourAngleSeconds, lctHours, lctMinutes, lctSeconds, daylightSaving, zoneCorrection, localDay, localMonth, localYear, geographicalLongitude);

			var rightAscensionHours = PAMacros.DecimalHoursHour(rightAscension);
			var rightAscensionMinutes = PAMacros.DecimalHoursMinute(rightAscension);
			var rightAscensionSeconds = PAMacros.DecimalHoursSecond(rightAscension);

			return (rightAscensionHours, rightAscensionMinutes, rightAscensionSeconds);
		}
	}
}