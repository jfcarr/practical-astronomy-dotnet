using System;
using Xunit;

namespace PALib.Tests
{
	public class PASun_Tests
	{
		private readonly PASun _paSun;

		public PASun_Tests()
		{
			_paSun = new PASun();
		}

		[Fact]
		public void ApproximatePositionOfSun()
		{
			Assert.Equal((8, 23, 33.73, 19, 21, 14.33), _paSun.ApproximatePositionOfSun(0, 0, 0, 27, 7, 2003, false, 0));
		}

		[Fact]
		public void PrecisePositionOfSun()
		{
			Assert.Equal((8, 26, 3.83, 19, 12, 49.72), _paSun.PrecisePositionOfSun(0, 0, 0, 27, 7, 1988, false, 0));
		}

		[Fact]
		public void SunDistanceAndAngularSize()
		{
			Assert.Equal((151920130, 0, 31, 29.93), _paSun.SunDistanceAndAngularSize(0, 0, 0, 27, 7, 1988, false, 0));
		}
	}
}