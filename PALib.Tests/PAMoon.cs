using Xunit;

namespace PALib.Tests
{
	public class PAMoon_Tests
	{
		private readonly PAMoon _paMoon;

		public PAMoon_Tests()
		{
			_paMoon = new PAMoon();
		}

		[Fact]
		public void ApproximatePositionOfMoon()
		{
			Assert.Equal((14, 12, 42.31, -11, 31, 38.27), _paMoon.ApproximatePositionOfMoon(0, 0, 0, false, 0, 1, 9, 2003));
		}

		[Fact]
		public void PrecisePositionOfMoon()
		{
			Assert.Equal((14, 12, 10.21, -11, 34, 57.83, 367964, 0.993191), _paMoon.PrecisePositionOfMoon(0, 0, 0, false, 0, 1, 9, 2003));
		}

		[Fact]
		public void MoonPhase()
		{
			Assert.Equal((0.22, -71.58), _paMoon.MoonPhase(0, 0, 0, false, 0, 1, 9, 2003, PAAccuracyLevel.Approximate));
		}

		[Fact]
		public void TimesOfNewMoonAndFullMoon()
		{
			Assert.Equal((17, 27, 27, 8, 2003, 16, 36, 10, 9, 2003), _paMoon.TimesOfNewMoonAndFullMoon(false, 0, 1, 9, 2003));
		}

		[Fact]
		public void MoonDistAngDiamHorParallax()
		{
			Assert.Equal((367964, 0, 32, 0, 59, 35.49), _paMoon.MoonDistAngDiamHorParallax(0, 0, 0, false, 0, 1, 9, 2003));
		}

		[Fact]
		public void MoonriseAndMoonset()
		{
			Assert.Equal((4, 21, 6, 3, 1986, 127.34, 13, 8, 6, 3, 1986, 234.05), _paMoon.MoonriseAndMoonset(6, 3, 1986, false, -5, -71.05, 42.3667));
		}
	}
}