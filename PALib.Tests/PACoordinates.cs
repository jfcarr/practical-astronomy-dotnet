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

		[Fact]
		public void EclipticCoordinateToEquatorialCoordinate()
		{
			Assert.Equal((9, 34, 53.4, 19, 32, 8.52), _paCoordinates.EclipticCoordinateToEquatorialCoordinate(139, 41, 10, 4, 52, 31, 6, 7, 2009));
		}

		[Fact]
		public void EquatorialCoordinateToEclipticCoordinate()
		{
			Assert.Equal((139, 41, 9.97, 4, 52, 30.99), _paCoordinates.EquatorialCoordinateToEclipticCoordinate(9, 34, 53.4, 19, 32, 8.52, 6, 7, 2009));
		}

		[Fact]
		public void EquatorialCoordinateToGalacticCoordinate()
		{
			Assert.Equal((232, 14, 52.38, 51, 7, 20.16), _paCoordinates.EquatorialCoordinateToGalacticCoordinate(10, 21, 0, 10, 3, 11));
		}

		[Fact]
		public void GalacticCoordinateToEquatorialCoordinate()
		{
			Assert.Equal((10, 21, 0, 10, 3, 11), _paCoordinates.GalacticCoordinateToEquatorialCoordinate(232, 14, 52.38, 51, 7, 20.16));
		}

		[Fact]
		public void AngleBetweenTwoObjects()
		{
			Assert.Equal((23, 40, 25.86), _paCoordinates.AngleBetweenTwoObjects(5, 13, 31.7, -8, 13, 30, 6, 44, 13.4, -16, 41, 11, PAAngleMeasure.Hours));
		}

		[Fact]
		public void RisingAndSetting()
		{
			Assert.Equal(("OK", 14, 16, 4, 10, 64.36, 295.64), _paCoordinates.RisingAndSetting(23, 39, 20, 21, 42, 0, 24, 8, 2010, 64, 30, 0.5667));
		}

		[Fact]
		public void CorrectForPrecession()
		{
			Assert.Equal((9, 12, 20.18, 14, 16, 9.12), _paCoordinates.CorrectForPrecession(9, 10, 43, 14, 23, 25, 0.923, 1, 1950, 1, 6, 1979));
		}

		[Fact]
		public void NutationInEclipticLongitudeAndObliquity()
		{
			var result = _paCoordinates.NutationInEclipticLongitudeAndObliquity(1, 9, 1988);

			Assert.Equal((0.001525808, 0.0025671), (Math.Round(result.nutInLongDeg, 9), Math.Round(result.nutInOblDeg, 7)));
		}

		[Fact]
		public void CorrectForAberration()
		{
			Assert.Equal((352, 37, 30.45, -1, 32, 56.33), _paCoordinates.CorrectForAberration(0, 0, 0, 8, 9, 1988, 352, 37, 10.1, -1, 32, 56.4));
		}

		[Fact]
		public void AtmosphericRefraction()
		{
			Assert.Equal((23, 13, 44.74, 40, 19, 45.76), _paCoordinates.AtmosphericRefraction(23, 14, 0, 40, 10, 0, PACoordinateType.True, 0.17, 51.2036110, 0, 0, 23, 3, 1987, 1, 1, 24, 1012, 21.7));
		}

		[Fact]
		public void CorrectionsForGeocentricParallax()
		{
			var result = _paCoordinates.CorrectionsForGeocentricParallax(22, 35, 19, -7, 41, 13, PACoordinateType.True, 1.019167, -100, 50, 60, 0, -6, 26, 2, 1979, 10, 45, 0);

			Assert.Equal((22, 36, 43.22, -8, 32, 17.4), result);
		}

		[Fact]
		public void HeliographicCoordinates()
		{
			Assert.Equal((142.59, -19.94), _paCoordinates.HeliographicCoordinates(220, 10.5, 1, 5, 1988));
		}

		[Fact]
		public void CarringtonRotationNumber()
		{
			Assert.Equal(1624, _paCoordinates.CarringtonRotationNumber(27, 1, 1975));
		}

		[Fact]
		public void SelenographicCoordinates1()
		{
			Assert.Equal((-4.88, 4.04, 19.78), _paCoordinates.SelenographicCoordinates1(1, 5, 1988));
		}

		[Fact]
		public void SelenographicCoordinates2()
		{
			Assert.Equal((6.81, 83.19, 1.19), _paCoordinates.SelenographicCoordinates2(1, 5, 1988));
		}
	}
}