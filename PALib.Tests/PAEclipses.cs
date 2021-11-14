using Xunit;

namespace PALib.Tests;

public class PAEclipses_Tests
{
	private readonly PAEclipses _paEclipses;

	public PAEclipses_Tests()
	{
		_paEclipses = new PAEclipses();
	}

	[Fact]
	public void LunarEclipseOccurrence()
	{
		Assert.Equal(("Lunar eclipse certain", 4, 4, 2015), _paEclipses.LunarEclipseOccurrence(1, 4, 2015, false, 10));
	}

	[Fact]
	public void LunarEclipseCircumstances()
	{
		Assert.Equal((4, 4, 2015, 9, 0, 10, 16, 11, 55, 12, 1, 12, 7, 13, 46, 15, 1, 1.01), _paEclipses.LunarEclipseCircumstances(1, 4, 2015, false, 10));
	}

	[Fact]
	public void SolarEclipseOccurrence()
	{
		Assert.Equal(("Solar eclipse certain", 20, 3, 2015), _paEclipses.SolarEclipseOccurrence(1, 4, 2015, false, 0));
	}

	[Fact]
	public void SolarEclipseCircumstances()
	{
		Assert.Equal((20, 3, 2015, 8, 55, 9, 57, 10, 58, 1.016), _paEclipses.SolarEclipseCircumstances(20, 3, 2015, false, 0, 0, 68.65));
	}
}
