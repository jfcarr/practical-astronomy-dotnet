using System;
using Xunit;

namespace PALib.Tests
{
	public class PACoordinates_Tests
	{
		private readonly PACoordinates _paCoordinates;

		public PACoordinates_Tests()
		{
			_paCoordinates = new PACoordinates();
		}

		[Fact]
		public void AngleToDecimalDegrees()
		{
			Assert.Equal(182.524167, Math.Round(_paCoordinates.AngleToDecimalDegrees(182, 31, 27), 6));
		}

		[Fact]
		public void DecimalDegreesToAngle()
		{
			Assert.Equal((182, 31, 27), _paCoordinates.DecimalDegreesToAngle(182.524167));
		}

		[Fact]
		public void RightAscensionToHourAngle()
		{
			Assert.Equal((9, 52, 23.66), _paCoordinates.RightAscensionToHourAngle(18, 32, 21, 14, 36, 51.67, false, -4, 22, 4, 1980, -64));
		}

		[Fact]
		public void HourAngleToRightAscension()
		{
			Assert.Equal((18, 32, 21), _paCoordinates.HourAngleToRightAscension(9, 52, 23.66, 14, 36, 51.67, false, -4, 22, 4, 1980, -64));
		}

		[Fact]
		public void EquatorialCoordinatesToHorizonCoordinates()
		{
			Assert.Equal((283, 16, 15.7, 19, 20, 3.64), _paCoordinates.EquatorialCoordinatesToHorizonCoordinates(5, 51, 44, 23, 13, 10, 52));
		}

		[Fact]
		public void HorizonCoordinatesToEquatorialCoordinates()
		{
			Assert.Equal((5, 51, 44, 23, 13, 10), _paCoordinates.HorizonCoordinatesToEquatorialCoordinates(283, 16, 15.7, 19, 20, 3.64, 52));
		}

		[Fact]
		public void MeanObliquityOfTheEcliptic()
		{
			Assert.Equal(23.43805531, Math.Round(_paCoordinates.MeanObliquityOfTheEcliptic(6, 7, 2009), 8));
		}
	}
}