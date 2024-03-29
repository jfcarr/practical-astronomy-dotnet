# practical-astronomy-dotnet

![Main](https://github.com/jfcarr-astronomy/practical-astronomy-dotnet/workflows/Build-Test/badge.svg)

Algorithms from "[Practical Astronomy with your Calculator or Spreadsheet](https://www.amazon.com/Practical-Astronomy-your-Calculator-Spreadsheet/dp/1108436072)" by Peter Duffett-Smith, implemented in .NET 6.

If you're interested in this topic, please buy the book! It provides far more detail and context.

## Getting Started (for clients)

Create a console application:

```bash
dotnet new console -o PAConsoleTest

cd PAConsoleTest
```

Using the NuGet package is the easiest way to consume the library in a client application.  Add a NuGet reference for the Practical Astronomy library, following the directions [here](https://www.nuget.org/packages/PracticalAstronomyDotNet/).

Open Program.cs, and make a few changes.  First, add a `using` statement for Practical Astronomy:

```csharp
using PALib;
```

Then, replace the 'Hello World' boilerplate with this:

```csharp
// Coordinates test (angle degrees to decimal degrees)
var paCoordinates = new PALib.PACoordinates();

var decimalDegrees = Math.Round(paCoordinates.AngleToDecimalDegrees(182, 31, 27), 3);

Console.WriteLine($"Decimal degrees value is {decimalDegrees}");

// Moon test (approximate moon phase)
var paMoon = new PALib.PAMoon();

var (moonPhase, brightLimbDegrees) = paMoon.MoonPhase(0, 0, 0, false, 0, 1, 9, 2003, PAAccuracyLevel.Approximate);

Console.WriteLine($"Moon phase value is {moonPhase}, bright limb degrees value is {brightLimbDegrees}");
```

When you run, you should see this:

```
Decimal degrees value is 182.524
Moon phase value is 0.22, bright limb degrees value is -71.58
```
## Library Functions - Status

### Date/Time

- [x] Calculate -> Date of Easter
- [x] Convert -> Civil Date to Day Number
- [x] Convert -> Civil Time <-> Decimal Hours
- [x] Extract -> Hour, Minutes, and Seconds parts of Decimal Hours
- [x] Convert -> Local Civil Time <-> Universal Time
- [x] Convert -> Universal Time <-> Greenwich Sidereal Time
- [x] Convert -> Greenwich Sidereal Time <-> Local Sidereal Time

### Coordinates

- [x] Convert -> Angle <-> Decimal Degrees
- [x] Convert -> Right Ascension <-> Hour Angle
- [x] Convert -> Equatorial Coordinates <-> Horizon Coordinates
- [x] Calculate -> Obliquity of the Ecliptic
- [x] Convert -> Ecliptic Coordinates <-> Equatorial Coordinates
- [x] Convert -> Equatorial Coordinates <-> Galactic Coordinates
- [x] Calculate -> Angle between two objects
- [x] Calculate -> Rising and Setting times for an object
- [x] Calculate -> Precession (corrected coordinates between two epochs)
- [x] Calculate -> Nutation (in ecliptic longitude and obliquity) for a Greenwich date
- [x] Calculate -> Effects of aberration for ecliptic coordinates
- [x] Calculate -> RA and Declination values, corrected for atmospheric refraction
- [x] Calculate -> RA and Declination values, corrected for geocentric parallax
- [x] Calculate -> Heliographic coordinates
- [x] Calculate -> Carrington rotation number
- [x] Calculate -> Selenographic (lunar) coordinates (sub-Earth and sub-Solar)

### The Sun

- [x] Calculate -> Approximate and precise positions of the Sun
- [x] Calculate -> Sun's distance and angular size
- [x] Calculate -> Local sunrise and sunset
- [x] Calculate -> Morning and evening twilight
- [x] Calculate -> Equation of time
- [x] Calculate -> Solar elongation

### Planets

- [x] Calculate -> Approximate position of planet
- [x] Calculate -> Precise position of planet
- [x] Calculate -> Visual aspects of planet (distance, angular diameter, phase, light time, position angle of bright limb, and apparent magnitude)
- [x] Calculate -> Position of comet (elliptical and parabolic)
- [x] Calculate -> Binary star orbit data

### The Moon

- [x] Calculate -> Approximate and precise position of Moon
- [x] Calculate -> Moon phase and position angle of bright limb
- [x] Calculate -> Times of new Moon and full Moon
- [x] Calculate -> Moon's distance, angular diameter, and horizontal parallax
- [x] Calculate -> Local moonrise and moonset

### Eclipses

- [x] Calculate -> Lunar eclipse occurrence and circumstances
- [x] Calculate -> Solar eclipse occurrence and circumstances
