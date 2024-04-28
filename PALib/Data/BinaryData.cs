using System.Collections.Generic;
using System.Linq;

namespace PALib.Data;

/// <summary>
/// Holds information about binary star systems.
/// </summary>
public class BinaryData
{
	/// <summary>
	/// Name of binary system.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Period of the orbit.
	/// </summary>
	public double Period { get; set; }

	/// <summary>
	/// Epoch of the perihelion.
	/// </summary>
	public double EpochPeri { get; set; }

	/// <summary>
	/// Longitude of the perihelion.
	/// </summary>
	public double LongPeri { get; set; }

	/// <summary>
	/// Eccentricity of the orbit.
	/// </summary>
	public double Ecc { get; set; }

	/// <summary>
	/// Semi-major axis of the orbit.
	/// </summary>
	public double Axis { get; set; }

	/// <summary>
	/// Orbital inclination.
	/// </summary>
	public double Incl { get; set; }

	/// <summary>
	/// Position angle of the ascending node.
	/// </summary>
	public double PANode { get; set; }
}

/// <summary>
/// Binary star system data manager.
/// </summary>
public static class BinaryInfo
{
	static List<BinaryData> _binaryData;

	static BinaryInfo()
	{
		_binaryData = new List<BinaryData>()
			{
				new BinaryData() {
					Name = "eta-Cor",
					Period = 41.623,
					EpochPeri = 1934.008,
					LongPeri = 219.907,
					Ecc = 0.2763,
					Axis = 0.907,
					Incl = 59.025,
					PANode = 23.717
				},
				new BinaryData() {
					Name = "gamma-Vir",
					Period = 171.37,
					EpochPeri = 1836.433,
					LongPeri = 252.88,
					Ecc = 0.8808,
					Axis = 3.746,
					Incl = 146.05,
					PANode = 31.78,
				},
				new BinaryData() {
					Name = "eta-Cas",
					Period = 480.0,
					EpochPeri = 1889.6,
					LongPeri = 268.59,
					Ecc = 0.497,
					Axis = 11.9939,
					Incl = 34.76,
					PANode = 278.42,
				},
				new BinaryData() {
					Name = "zeta-Ori",
					Period = 1508.6,
					EpochPeri = 2070.6,
					LongPeri = 47.3,
					Ecc = 0.07,
					Axis = 2.728,
					Incl = 72.0,
					PANode = 155.5,
				},
				new BinaryData() {
					Name = "alpha-CMa",
					Period = 50.09,
					EpochPeri = 1894.13,
					LongPeri = 147.27,
					Ecc = 0.5923,
					Axis = 7.5,
					Incl = 136.53,
					PANode = 44.57,
				},
				new BinaryData() {
					Name = "delta-Gem",
					Period = 1200.0,
					EpochPeri = 1437.0,
					LongPeri = 57.19,
					Ecc = 0.11,
					Axis = 6.9753,
					Incl = 63.28,
					PANode = 18.38,
				},
				new BinaryData() {
					Name = "alpha-Gem",
					Period = 420.07,
					EpochPeri = 1965.3,
					LongPeri = 261.43,
					Ecc = 0.33,
					Axis = 6.295,
					Incl = 115.94,
					PANode = 40.47,
				},
				new BinaryData() {
					Name = "aplah-CMi",
					Period = 40.65,
					EpochPeri = 1927.6,
					LongPeri = 269.8,
					Ecc = 0.4,
					Axis = 4.548,
					Incl = 35.7,
					PANode = 284.3,
				},
				new BinaryData() {
					Name = "alpha-Cen",
					Period = 79.92,
					EpochPeri = 1955.56,
					LongPeri = 231.56,
					Ecc = 0.516,
					Axis = 17.583,
					Incl = 79.24,
					PANode = 204.868,
				},
				new BinaryData() {
					Name = "alpha Sco",
					Period = 900.0,
					EpochPeri = 1889.0,
					LongPeri = 0.0,
					Ecc = 0.0,
					Axis = 3.21,
					Incl = 86.3,
					PANode = 273.0,
				}
			};
	}

	/// <summary>
	/// Retrieve information about a specific binary star system.
	/// </summary>
	/// <returns></returns>
	public static BinaryData GetBinaryInfo(string name)
	{
		BinaryData returnValue = _binaryData
			.Where(x => x.Name == name)
			.Select(x => x)
			.FirstOrDefault();

		return (returnValue == null) ? new BinaryData() { Name = "NotFound" } : returnValue;
	}
}
