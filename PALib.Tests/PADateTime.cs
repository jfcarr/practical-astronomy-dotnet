using System;
using Xunit;
using PALib;

namespace PALib.Tests
{
	public class PADateTime_Tests
	{
		[Fact]
		public void GetDateOfEaster()
		{
			var paDateTime = new PADateTime();

			var result1 = paDateTime.GetDateOfEaster(2003);

			Assert.Equal(4, result1.Month);
			Assert.Equal(20, result1.Day);
			Assert.Equal(2003, result1.Year);

			var result2 = paDateTime.GetDateOfEaster(2019);

			Assert.Equal(4, result2.Month);
			Assert.Equal(21, result2.Day);
			Assert.Equal(2019, result2.Year);
		}

		[Fact]
		public void CivilDateToDayNumber()
		{
			var paDateTime = new PADateTime();

			Assert.Equal(1, paDateTime.CivilDateToDayNumber(1, 1, 2000));
			Assert.Equal(61, paDateTime.CivilDateToDayNumber(3, 1, 2000));
			Assert.Equal(152, paDateTime.CivilDateToDayNumber(6, 1, 2003));
			Assert.Equal(331, paDateTime.CivilDateToDayNumber(11, 27, 2009));
		}
	}
}
