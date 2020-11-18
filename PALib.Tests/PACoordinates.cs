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
	}
}