# practical-astronomy-dotnet

![Main](https://github.com/jfcarr-astronomy/practical-astronomy-dotnet/workflows/Build-Test/badge.svg)

Algorithms from "[Practical Astronomy with your Calculator or Spreadsheet](https://www.amazon.com/Practical-Astronomy-your-Calculator-Spreadsheet/dp/1108436072)" by Peter Duffett-Smith, implemented in .NET 5.

If you're interested in this topic, please buy the book! It provides far more detail and context.

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
- [ ] Calculate -> Heliographic coordinates
- [ ] Calculate -> Carrington rotation number
- [ ] Calculate -> Selenographic (lunar) coordinates (sub-Earth and sub-Solar)

### The Sun

- [ ] Calculate -> Approximate and precise positions of the Sun
- [ ] Calculate -> Sun's distance and angular size
- [ ] Calculate -> Local sunrise and sunset
- [ ] Calculate -> Morning and evening twilight
- [ ] Calculate -> Equation of time
- [ ] Calculate -> Solar elongation

### Planets

- [ ] Calculate -> Approximate position of planet
- [ ] Calculate -> Precise position of planet
- [ ] Calculate -> Visual aspects of planet (distance, angular diameter, phase, light time, position angle of bright limb, and apparent magnitude)
- [ ] Calculate -> Position of comet (elliptical and parabolic)
- [ ] Calculate -> Binary star orbit data

### The Moon

- [ ] Calculate -> Approximate and precise position of Moon
- [ ] Calculate -> Moon phase and position angle of bright limb
- [ ] Calculate -> Times of new Moon and full Moon
- [ ] Calculate -> Moon's distance, angular diameter, and horizontal parallax
- [ ] Calculate -> Local moonrise and moonset

### Eclipses

- [ ] Calculate -> Lunar eclipse occurrence and circumstances
- [ ] Calculate -> Solar eclipse occurrence and circumstances
