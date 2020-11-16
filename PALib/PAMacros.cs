using System;

namespace PALib
{
	public static class PAMacros
	{
		/// <summary>
		/// Convert a Civil Time (hours,minutes,seconds) to Decimal Hours
		/// </summary>
		/// <remarks>
		/// Original macro name: HMSDH
		/// </remarks>
		/// <param name="hours"></param>
		/// <param name="minutes"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static double HMStoDH(double hours, double minutes, double seconds)
		{
			double fHours = hours;
			double fMinutes = minutes;
			double fSeconds = seconds;

			var a = Math.Abs(fSeconds) / 60;
			var b = (Math.Abs(fMinutes) + a) / 60;
			var c = Math.Abs(fHours) + b;

			return (fHours < 0 || fMinutes < 0 || fSeconds < 0) ? -c : c;
		}
	}
}