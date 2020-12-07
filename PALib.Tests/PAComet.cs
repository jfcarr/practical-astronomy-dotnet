using Xunit;

namespace PALib.Tests
{
	public class PAComet_Tests
	{
		private readonly PAComet _paComet;

		public PAComet_Tests()
		{
			_paComet = new PAComet();
		}

		[Fact]
		public void PositionOfEllipticalComet()
		{
			Assert.Equal((6, 29, 10, 13, 8.13), _paComet.PositionOfEllipticalComet(0, 0, 0, false, 0, 1, 1, 1984, "Halley"));
		}

		[Fact]
		public void PositionOfParabolicComet()
		{
			Assert.Equal((23, 17, 11.53, -33, 42, 26.42, 1.11), _paComet.PositionOfParabolicComet(0, 0, 0, false, 0, 25, 12, 1977, "Kohler"));
		}
	}
}