using System.Collections.Generic;
using System.Linq;

namespace PALib.Data
{
	public class CometDataElliptical
	{
		public string Name { get; set; }
		public double epoch_EpochOfPerihelion { get; set; }
		public double peri_LongitudeOfPerihelion { get; set; }
		public double node_LongitudeOfAscendingNode { get; set; }
		public double period_PeriodOfOrbit { get; set; }
		public double axis_SemiMajorAxisOfOrbit { get; set; }
		public double ecc_EccentricityOfOrbit { get; set; }
		public double incl_InclinationOfOrbit { get; set; }
	}

	public class CometDataParabolic
	{
		public string Name { get; set; }
		public double EpochPeriDay { get; set; }
		public int EpochPeriMonth { get; set; }
		public int EpochPeriYear { get; set; }
		public double ArgPeri { get; set; }
		public double Node { get; set; }
		public double PeriDist { get; set; }
		public double Incl { get; set; }
	}

	public static class CometInfoElliptical
	{
		static List<CometDataElliptical> _cometDataElliptical;

		static CometInfoElliptical()
		{
			_cometDataElliptical = new List<CometDataElliptical>() {
				new CometDataElliptical() {
					Name = "Encke",
					epoch_EpochOfPerihelion = 1974.32,
					peri_LongitudeOfPerihelion = 160.1,
					node_LongitudeOfAscendingNode =  334.2,
					period_PeriodOfOrbit =  3.3,
					axis_SemiMajorAxisOfOrbit =  2.21,
					ecc_EccentricityOfOrbit =  0.85,
					incl_InclinationOfOrbit =  12.0
				},
				new CometDataElliptical() {
					Name = "Temple 2",
					epoch_EpochOfPerihelion = 1972.87,
					peri_LongitudeOfPerihelion = 310.2,
					node_LongitudeOfAscendingNode = 119.3,
					period_PeriodOfOrbit = 5.26,
					axis_SemiMajorAxisOfOrbit = 3.02,
					ecc_EccentricityOfOrbit = 0.55,
					incl_InclinationOfOrbit = 12.5
				},
				new CometDataElliptical() {
					Name = "Haneda-Campos",
					epoch_EpochOfPerihelion = 1978.77,
					peri_LongitudeOfPerihelion = 12.02,
					node_LongitudeOfAscendingNode = 131.7,
					period_PeriodOfOrbit = 5.37,
					axis_SemiMajorAxisOfOrbit = 3.07,
					ecc_EccentricityOfOrbit = 0.64,
					incl_InclinationOfOrbit = 5.81
				},
				new CometDataElliptical() {
					Name = "Schwassmann-Wachmann 2",
					epoch_EpochOfPerihelion = 1974.7,
					peri_LongitudeOfPerihelion = 123.3,
					node_LongitudeOfAscendingNode = 126.0,
					period_PeriodOfOrbit = 6.51,
					axis_SemiMajorAxisOfOrbit = 3.49,
					ecc_EccentricityOfOrbit = 0.39,
					incl_InclinationOfOrbit = 3.7
				},
				new CometDataElliptical() {
					Name = "Borrelly",
					epoch_EpochOfPerihelion = 1974.36,
					peri_LongitudeOfPerihelion = 67.8,
					node_LongitudeOfAscendingNode = 75.1,
					period_PeriodOfOrbit = 6.76,
					axis_SemiMajorAxisOfOrbit = 3.58,
					ecc_EccentricityOfOrbit = 0.63,
					incl_InclinationOfOrbit = 30.2
				},
				new CometDataElliptical() {
					Name = "Whipple",
					epoch_EpochOfPerihelion = 1970.77,
					peri_LongitudeOfPerihelion = 18.2,
					node_LongitudeOfAscendingNode = 188.4,
					period_PeriodOfOrbit = 7.47,
					axis_SemiMajorAxisOfOrbit = 3.82,
					ecc_EccentricityOfOrbit = 0.35,
					incl_InclinationOfOrbit = 10.2
				},
				new CometDataElliptical() {
					Name = "Oterma",
					epoch_EpochOfPerihelion = 1958.44,
					peri_LongitudeOfPerihelion = 150.0,
					node_LongitudeOfAscendingNode = 155.1,
					period_PeriodOfOrbit = 7.88,
					axis_SemiMajorAxisOfOrbit = 3.96,
					ecc_EccentricityOfOrbit = 0.14,
					incl_InclinationOfOrbit = 4.0
				},
				new CometDataElliptical() {
					Name = "Schaumasse",
					epoch_EpochOfPerihelion = 1960.29,
					peri_LongitudeOfPerihelion = 138.1,
					node_LongitudeOfAscendingNode = 86.2,
					period_PeriodOfOrbit = 8.18,
					axis_SemiMajorAxisOfOrbit = 4.05,
					ecc_EccentricityOfOrbit = 0.71,
					incl_InclinationOfOrbit = 12.0
				},
				new CometDataElliptical() {
					Name = "Comas Sola",
					epoch_EpochOfPerihelion = 1969.83,
					peri_LongitudeOfPerihelion = 102.9,
					node_LongitudeOfAscendingNode = 62.8,
					period_PeriodOfOrbit = 8.55,
					axis_SemiMajorAxisOfOrbit = 4.18,
					ecc_EccentricityOfOrbit = 0.58,
					incl_InclinationOfOrbit = 13.4
				},
				new CometDataElliptical() {
					Name = "Schwassmann-Wachmann 1",
					epoch_EpochOfPerihelion = 1974.12,
					peri_LongitudeOfPerihelion = 334.1,
					node_LongitudeOfAscendingNode = 319.6,
					period_PeriodOfOrbit = 15.03,
					axis_SemiMajorAxisOfOrbit = 6.09,
					ecc_EccentricityOfOrbit = 0.11,
					incl_InclinationOfOrbit = 9.7
				},
				new CometDataElliptical() {
					Name = "Neujmin 1",
					epoch_EpochOfPerihelion = 1966.94,
					peri_LongitudeOfPerihelion = 334.0,
					node_LongitudeOfAscendingNode = 347.2,
					period_PeriodOfOrbit = 17.93,
					axis_SemiMajorAxisOfOrbit = 6.86,
					ecc_EccentricityOfOrbit = 0.78,
					incl_InclinationOfOrbit = 15.0
				},
				new CometDataElliptical() {
					Name = "Crommelin",
					epoch_EpochOfPerihelion = 1956.82,
					peri_LongitudeOfPerihelion = 86.4,
					node_LongitudeOfAscendingNode = 250.4,
					period_PeriodOfOrbit = 27.89,
					axis_SemiMajorAxisOfOrbit = 9.17,
					ecc_EccentricityOfOrbit = 0.92,
					incl_InclinationOfOrbit = 28.9
				},
				new CometDataElliptical() {
					Name = "Olbers",
					epoch_EpochOfPerihelion = 1956.46,
					peri_LongitudeOfPerihelion = 150.0,
					node_LongitudeOfAscendingNode = 85.4,
					period_PeriodOfOrbit = 69.47,
					axis_SemiMajorAxisOfOrbit = 16.84,
					ecc_EccentricityOfOrbit = 0.93,
					incl_InclinationOfOrbit = 44.6
				},
				new CometDataElliptical() {
					Name = "Pons-Brooks",
					epoch_EpochOfPerihelion = 1954.39,
					peri_LongitudeOfPerihelion = 94.2,
					node_LongitudeOfAscendingNode = 255.2,
					period_PeriodOfOrbit = 70.98,
					axis_SemiMajorAxisOfOrbit = 17.2,
					ecc_EccentricityOfOrbit = 0.96,
					incl_InclinationOfOrbit = 74.2
				},
				new CometDataElliptical() {
					Name = "Halley",
					epoch_EpochOfPerihelion = 1986.112,
					peri_LongitudeOfPerihelion = 170.011,
					node_LongitudeOfAscendingNode = 58.154,
					period_PeriodOfOrbit = 76.0081,
					axis_SemiMajorAxisOfOrbit = 17.9435,
					ecc_EccentricityOfOrbit = 0.9673,
					incl_InclinationOfOrbit = 162.2384

				}
			};
		}

		public static CometDataElliptical GetCometEllipticalInfo(string name)
		{
			var returnValue = _cometDataElliptical
				.Where(x => x.Name == name)
				.Select(x => x)
				.FirstOrDefault();

			return (returnValue == null) ? new CometDataElliptical() { Name = "NotFound" } : returnValue;
		}
	}
	public static class CometInfoParabolic
	{
		static List<CometDataParabolic> _cometDataParabolic;

		static CometInfoParabolic()
		{
			_cometDataParabolic = new List<CometDataParabolic>() {
				new CometDataParabolic() {
					Name = "Kohler",
					EpochPeriDay = 10.5659,
					EpochPeriMonth = 11,
					EpochPeriYear = 1977,
					ArgPeri = 163.4799,
					Node = 181.8175,
					PeriDist = 0.990662,
					Incl = 48.7196
				}
			};
		}

		public static CometDataParabolic GetCometParabolicInfo(string name)
		{
			var returnValue = _cometDataParabolic
				.Where(x => x.Name == name)
				.Select(x => x)
				.FirstOrDefault();

			return (returnValue == null) ? new CometDataParabolic() { Name = "NotFound" } : returnValue;
		}
	}
}