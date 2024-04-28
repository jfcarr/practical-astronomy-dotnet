using System.Collections.Generic;
using System.Linq;

namespace PALib.Data;

/// <summary>
/// Information about a planet.
/// </summary>
public class PlanetData
{
	/// <summary>
	/// Name of planet.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Period of orbit.
	/// </summary>
	/// <remarks>
	/// Original element name: tp
	/// </remarks>
	/// <value></value>
	public double tp_PeriodOrbit { get; set; }

	/// <summary>
	/// Longitude at the epoch.
	/// </summary>
	/// <remarks>
	/// Original element name: long
	/// </remarks>
	/// <value></value>
	public double long_LongitudeEpoch { get; set; }

	/// <summary>
	/// Longitude of the perihelion.
	/// </summary>
	/// <remarks>
	/// Original element name: peri
	/// </remarks>
	/// <value></value>
	public double peri_LongitudePerihelion { get; set; }

	/// <summary>
	/// Eccentricity of the orbit.
	/// </summary>
	/// <remarks>
	/// Original element name: ecc
	/// </remarks>
	/// <value></value>
	public double ecc_EccentricityOrbit { get; set; }

	/// <summary>
	/// Semi-major axis of the orbit.
	/// </summary>
	/// <remarks>
	/// Original element name: axis
	/// </remarks>
	/// <value></value>
	public double axis_AxisOrbit { get; set; }

	/// <summary>
	/// Orbital inclination.
	/// </summary>
	/// <remarks>
	/// Original element name: incl
	/// </remarks>
	/// <value></value>
	public double incl_OrbitalInclination { get; set; }

	/// <summary>
	/// Longitude of the ascending node.
	/// </summary>
	/// <remarks>
	/// Original element name: node
	/// </remarks>
	/// <value></value>
	public double node_LongitudeAscendingNode { get; set; }

	/// <summary>
	/// Angular diameter at 1 AU.
	/// </summary>
	/// <remarks>
	/// Original element name: theta0
	/// </remarks>
	/// <value></value>
	public double theta0_AngularDiameter { get; set; }

	/// <summary>
	/// Visual magnitude at 1 AU.
	/// </summary>
	/// <remarks>
	/// Original element name: v0
	/// </remarks>
	/// <value></value>
	public double v0_VisualMagnitude { get; set; }
}

/// <summary>
/// Working data for precise planet calculations.
/// </summary>
public class PlanetDataPrecise
{
	/// <summary>
	/// Name of planet.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Working value 1.
	/// </summary>
	public double Value1 { get; set; }

	/// <summary>
	/// Working value 2.
	/// </summary>
	public double Value2 { get; set; }

	/// <summary>
	/// Working value 3.
	/// </summary>
	public double Value3 { get; set; }

	/// <summary>
	/// Working value 4.
	/// </summary>
	public double Value4 { get; set; }

	/// <summary>
	/// Working value 5.
	/// </summary>
	public double Value5 { get; set; }

	/// <summary>
	/// Working value 6.
	/// </summary>
	public double Value6 { get; set; }

	/// <summary>
	/// Working value 7.
	/// </summary>
	public double Value7 { get; set; }

	/// <summary>
	/// Working value 8.
	/// </summary>
	public double Value8 { get; set; }

	/// <summary>
	/// Working value 9.
	/// </summary>
	public double Value9 { get; set; }

	/// <summary>
	/// Working AP value.
	/// </summary>
	public double APValue { get; set; }
}

/// <summary>
/// Data manager for planets.
/// </summary>
public static class PlanetInfo
{
	static List<PlanetData> _planetData;

	static PlanetInfo()
	{
		_planetData = new List<PlanetData>() {
				new PlanetData() {
					Name = "Mercury",
					tp_PeriodOrbit = 0.24085,
					long_LongitudeEpoch = 75.5671,
					peri_LongitudePerihelion = 77.612,
					ecc_EccentricityOrbit = 0.205627,
					axis_AxisOrbit = 0.387098,
					incl_OrbitalInclination = 7.0051,
					node_LongitudeAscendingNode = 48.449,
					theta0_AngularDiameter = 6.74,
					v0_VisualMagnitude = -0.42
				},
				new PlanetData() {
					Name = "Venus",
					tp_PeriodOrbit = 0.615207,
					long_LongitudeEpoch = 272.30044,
					peri_LongitudePerihelion = 131.54,
					ecc_EccentricityOrbit = 0.006812,
					axis_AxisOrbit = 0.723329,
					incl_OrbitalInclination = 3.3947,
					node_LongitudeAscendingNode = 76.769,
					theta0_AngularDiameter = 16.92,
					v0_VisualMagnitude = -4.4
				},
				new PlanetData() {
					Name = "Earth",
					tp_PeriodOrbit = 0.999996,
					long_LongitudeEpoch = 99.556772,
					peri_LongitudePerihelion = 103.2055,
					ecc_EccentricityOrbit = 0.016671,
					axis_AxisOrbit = 0.999985,
					incl_OrbitalInclination = -99.0,
					node_LongitudeAscendingNode = -99.0,
					theta0_AngularDiameter = -99.0,
					v0_VisualMagnitude = -99.0
				},
				new PlanetData() {
					Name = "Mars",
					tp_PeriodOrbit = 1.880765,
					long_LongitudeEpoch = 109.09646,
					peri_LongitudePerihelion = 336.217,
					ecc_EccentricityOrbit = 0.093348,
					axis_AxisOrbit = 1.523689,
					incl_OrbitalInclination = 1.8497,
					node_LongitudeAscendingNode = 49.632,
					theta0_AngularDiameter = 9.36,
					v0_VisualMagnitude = -1.52
				},
				new PlanetData() {
					Name = "Jupiter",
					tp_PeriodOrbit = 11.857911,
					long_LongitudeEpoch = 337.917132,
					peri_LongitudePerihelion = 14.6633,
					ecc_EccentricityOrbit = 0.048907,
					axis_AxisOrbit = 5.20278,
					incl_OrbitalInclination = 1.3035,
					node_LongitudeAscendingNode = 100.595,
					theta0_AngularDiameter = 196.74,
					v0_VisualMagnitude = -9.4
				},
				new PlanetData() {
					Name = "Saturn",
					tp_PeriodOrbit = 29.310579,
					long_LongitudeEpoch = 172.398316,
					peri_LongitudePerihelion = 89.567,
					ecc_EccentricityOrbit = 0.053853,
					axis_AxisOrbit = 9.51134,
					incl_OrbitalInclination = 2.4873,
					node_LongitudeAscendingNode = 113.752,
					theta0_AngularDiameter = 165.6,
					v0_VisualMagnitude = -8.88
				},
				new PlanetData() {
					Name = "Uranus",
					tp_PeriodOrbit = 84.039492,
					long_LongitudeEpoch = 356.135400,
					peri_LongitudePerihelion = 172.884833,
					ecc_EccentricityOrbit = 0.046321,
					axis_AxisOrbit = 19.21814,
					incl_OrbitalInclination = 0.773059,
					node_LongitudeAscendingNode = 73.926961,
					theta0_AngularDiameter = 65.8,
					v0_VisualMagnitude = -7.19
				},
				new PlanetData() {
					Name = "Neptune",
					tp_PeriodOrbit = 165.845392,
					long_LongitudeEpoch = 326.895127,
					peri_LongitudePerihelion = 23.07,
					ecc_EccentricityOrbit = 0.010483,
					axis_AxisOrbit = 30.1985,
					incl_OrbitalInclination = 1.7673,
					node_LongitudeAscendingNode = 131.879,
					theta0_AngularDiameter = 62.2,
					v0_VisualMagnitude = -6.87
				}
			};
	}

	/// <summary>
	/// Get information about a planet.
	/// </summary>
	public static PlanetData GetPlanetInfo(string name)
	{
		PlanetData returnValue = _planetData
			.Where(x => x.Name == name)
			.Select(x => x)
			.FirstOrDefault();

		return (returnValue == null) ? new PlanetData() { Name = "NotFound" } : returnValue;
	}
}
