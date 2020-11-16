using System;
using Xunit;

namespace PALib.Tests
{
	public class PADateTime_Tests
	{
		private readonly PADateTime _paDateTime;

		public PADateTime_Tests()
		{
			_paDateTime = new PADateTime();
		}

		[Fact]
		public void GetDateOfEaster()
		{
			Assert.Equal((4, 20, 2003), _paDateTime.GetDateOfEaster(2003));
			Assert.Equal((4, 21, 2019), _paDateTime.GetDateOfEaster(2019));
			Assert.Equal((4, 12, 2020), _paDateTime.GetDateOfEaster(2020));
		}

		[Fact]
		public void CivilDateToDayNumber()
		{
			Assert.Equal(1, _paDateTime.CivilDateToDayNumber(1, 1, 2000));
			Assert.Equal(61, _paDateTime.CivilDateToDayNumber(3, 1, 2000));
			Assert.Equal(152, _paDateTime.CivilDateToDayNumber(6, 1, 2003));
			Assert.Equal(331, _paDateTime.CivilDateToDayNumber(11, 27, 2009));
		}

		[Fact]
		public void CivilTimeToDecimalHours()
		{
			Assert.Equal(18.52416667, Math.Round(_paDateTime.CivilTimeToDecimalHours(18, 31, 27), 8));
		}

		[Fact]
		public void DecimalHoursToCivilTime()
		{
			Assert.Equal((18, 31, 27), _paDateTime.DecimalHoursToCivilTime(18.52416667));
		}
	}
}
