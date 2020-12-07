using Xunit;

namespace PALib.Tests
{
	public class PABinary_Tests
	{
		private readonly PABinary _paBinary;

		public PABinary_Tests()
		{
			_paBinary = new PABinary();
		}

		[Fact]
		public void BinaryStarOrbit()
		{
			Assert.Equal((318.5, 0.41), _paBinary.BinaryStarOrbit(1, 1, 1980, "eta-Cor"));
		}
	}
}