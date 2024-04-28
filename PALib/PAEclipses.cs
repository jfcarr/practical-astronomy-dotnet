using System;
using PALib.Helpers;

namespace PALib;

/// <summary>
/// Eclipse calculations.
/// </summary>
public class PAEclipses
{
	/// <summary>
	/// Determine if a lunar eclipse is likely to occur.
	/// </summary>
	/// <returns>
	/// <para>status -- One of "Lunar eclipse certain", "Lunar eclipse possible", or "No lunar eclipse".</para>
	/// <para>eventDateDay -- Date of eclipse event (day).</para>
	/// <para>eventDateMonth -- Date of eclipse event (month).</para>
	/// <para>eventDateYear -- Date of eclipse event (year).</para>
	/// </returns>
	public (string status, double eventDateDay, int eventDateMonth, int eventDateYear) LunarEclipseOccurrence(double localDateDay, int localDateMonth, int localDateYear, bool isDaylightSaving, int zoneCorrectionHours)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double julianDateOfFullMoon = PAMacros.FullMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		double gDateOfFullMoonDay = PAMacros.JulianDateDay(julianDateOfFullMoon);
		double integerDay = gDateOfFullMoonDay.Floor();
		int gDateOfFullMoonMonth = PAMacros.JulianDateMonth(julianDateOfFullMoon);
		int gDateOfFullMoonYear = PAMacros.JulianDateYear(julianDateOfFullMoon);
		double utOfFullMoonHours = gDateOfFullMoonDay - integerDay;

		double localCivilDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int localCivilDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int localCivilDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);

		string eclipseOccurrence = PAMacros.LunarEclipseOccurrence(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		string status = eclipseOccurrence;
		double eventDateDay = localCivilDateDay;
		int eventDateMonth = localCivilDateMonth;
		int eventDateYear = localCivilDateYear;

		return (status, eventDateDay, eventDateMonth, eventDateYear);
	}

	/// <summary>
	/// Calculate the circumstances of a lunar eclipse.
	/// </summary>
	/// <returns>
	/// <para>lunarEclipseCertainDateDay -- Lunar eclipse date (day)</para>
	/// <para>lunarEclipseCertainDateMonth -- Lunar eclipse date (month)</para>
	/// <para>lunarEclipseCertainDateYear -- Lunar eclipse date (year)</para>
	/// <para>utstartPenPhaseHour -- Start of penumbral phase (hour)</para>
	/// <para>utStartPenPhaseMinutes -- Start of penumbral phase (minutes)</para>
	/// <para>utStartUmbralPhaseHour -- Start of umbral phase (hour)</para>
	/// <para>utStartUmbralPhaseMinutes -- Start of umbral phase (minutes)</para>
	/// <para>utStartTotalPhaseHour -- Start of total phase (hour)</para>
	/// <para>utStartTotalPhaseMinutes -- Start of total phase (minutes)</para>
	/// <para>utMidEclipseHour -- Mid-eclipse (hour)</para>
	/// <para>utMidEclipseMinutes -- Mid-eclipse (minutes)</para>
	/// <para>utEndTotalPhaseHour -- End of total phase (hour)</para>
	/// <para>utEndTotalPhaseMinutes -- End of total phase (minutes)</para>
	/// <para>utEndUmbralPhaseHour -- End of umbral phase (hour)</para>
	/// <para>utEndUmbralPhaseMinutes -- End of umbral phase (minutes)</para>
	/// <para>utEndPenPhaseHour -- End of penumbral phase (hour)</para>
	/// <para>utEndPenPhaseMinutes -- End of penumbral phase (minutes)</para>
	/// <para>eclipseMagnitude -- Eclipse magnitude</para>
	/// </returns>
	public (double lunarEclipseCertainDateDay, double lunarEclipseCertainDateMonth, double lunarEclipseCertainDateYear, double utStartPenPhaseHour, double utStartPenPhaseMinutes, double utStartUmbralPhaseHour, double utStartUmbralPhaseMinutes, double utStartTotalPhaseHour, double utStartTotalPhaseMinutes, double utMidEclipseHour, double utMidEclipseMinutes, double utEndTotalPhaseHour, double utEndTotalPhaseMinutes, double utEndUmbralPhaseHour, double utEndUmbralPhaseMinutes, double utEndPenPhaseHour, double utEndPenPhaseMinutes, double eclipseMagnitude) LunarEclipseCircumstances(double localDateDay, int localDateMonth, int localDateYear, bool isDaylightSaving, int zoneCorrectionHours)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double julianDateOfFullMoon = PAMacros.FullMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double gDateOfFullMoonDay = PAMacros.JulianDateDay(julianDateOfFullMoon);
		double integerDay = gDateOfFullMoonDay.Floor();
		int gDateOfFullMoonMonth = PAMacros.JulianDateMonth(julianDateOfFullMoon);
		int gDateOfFullMoonYear = PAMacros.JulianDateYear(julianDateOfFullMoon);
		double utOfFullMoonHours = gDateOfFullMoonDay - integerDay;

		double localCivilDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int localCivilDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);
		int localCivilDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfFullMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfFullMoonMonth, gDateOfFullMoonYear);

		double utMaxEclipse = PAMacros.UTMaxLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utFirstContact = PAMacros.UTFirstContactLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utLastContact = PAMacros.UTLastContactLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utStartUmbralPhase = PAMacros.UTStartUmbraLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utEndUmbralPhase = PAMacros.UTEndUmbraLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utStartTotalPhase = PAMacros.UTStartTotalLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);
		double utEndTotalPhase = PAMacros.UTEndTotalLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);

		double eclipseMagnitude1 = PAMacros.MagLunarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours);

		double lunarEclipseCertainDateDay = localCivilDateDay;
		int lunarEclipseCertainDateMonth = localCivilDateMonth;
		int lunarEclipseCertainDateYear = localCivilDateYear;

		double utStartPenPhaseHour = (utFirstContact == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utFirstContact + 0.008333);
		double utStartPenPhaseMinutes = (utFirstContact == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utFirstContact + 0.008333);

		double utStartUmbralPhaseHour = (utStartUmbralPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utStartUmbralPhase + 0.008333);
		double utStartUmbralPhaseMinutes = (utStartUmbralPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utStartUmbralPhase + 0.008333);

		double utStartTotalPhaseHour = (utStartTotalPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utStartTotalPhase + 0.008333);
		double utStartTotalPhaseMinutes = (utStartTotalPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utStartTotalPhase + 0.008333);

		double utMidEclipseHour = (utMaxEclipse == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utMaxEclipse + 0.008333);
		double utMidEclipseMinutes = (utMaxEclipse == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utMaxEclipse + 0.008333);

		double utEndTotalPhaseHour = (utEndTotalPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utEndTotalPhase + 0.008333);
		double utEndTotalPhaseMinutes = (utEndTotalPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utEndTotalPhase + 0.008333);

		double utEndUmbralPhaseHour = (utEndUmbralPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utEndUmbralPhase + 0.008333);
		double utEndUmbralPhaseMinutes = (utEndUmbralPhase == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utEndUmbralPhase + 0.008333);

		double utEndPenPhaseHour = (utLastContact == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utLastContact + 0.008333);
		double utEndPenPhaseMinutes = (utLastContact == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utLastContact + 0.008333);

		double eclipseMagnitude = (eclipseMagnitude1 == -99.0) ? -99.0 : Math.Round(eclipseMagnitude1, 2);

		return (lunarEclipseCertainDateDay, lunarEclipseCertainDateMonth, lunarEclipseCertainDateYear, utStartPenPhaseHour, utStartPenPhaseMinutes, utStartUmbralPhaseHour, utStartUmbralPhaseMinutes, utStartTotalPhaseHour, utStartTotalPhaseMinutes, utMidEclipseHour, utMidEclipseMinutes, utEndTotalPhaseHour, utEndTotalPhaseMinutes, utEndUmbralPhaseHour, utEndUmbralPhaseMinutes, utEndPenPhaseHour, utEndPenPhaseMinutes, eclipseMagnitude);
	}

	/// <summary>
	/// Determine if a solar eclipse is likely to occur.
	/// </summary>
	/// <returns>
	/// <para>status -- One of "Solar eclipse certain", "Solar eclipse possible", or "No solar eclipse".</para>
	/// <para>event_date_day -- Date of eclipse event (day).</para>
	/// <para>event_date_month -- Date of eclipse event (month).</para>
	/// <para>event_date_year -- Date of eclipse event (year).</para>
	/// </returns>
	public (string status, double eventDateDay, int eventDateMonth, int eventDateYear) SolarEclipseOccurrence(double localDateDay, int localDateMonth, int localDateYear, bool isDaylightSaving, int zoneCorrectionHours)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double julianDateOfNewMoon = PAMacros.NewMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double gDateOfNewMoonDay = PAMacros.JulianDateDay(julianDateOfNewMoon);
		double integerDay = gDateOfNewMoonDay.Floor();
		int gDateOfNewMoonMonth = PAMacros.JulianDateMonth(julianDateOfNewMoon);
		int gDateOfNewMoonYear = PAMacros.JulianDateYear(julianDateOfNewMoon);
		double utOfNewMoonHours = gDateOfNewMoonDay - integerDay;

		double localCivilDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int localCivilDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int localCivilDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);

		string eclipseOccurrence = PAMacros.SolarEclipseOccurrence(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);

		string status = eclipseOccurrence;
		double eventDateDay = localCivilDateDay;
		int eventDateMonth = localCivilDateMonth;
		int eventDateYear = localCivilDateYear;

		return (status, eventDateDay, eventDateMonth, eventDateYear);
	}

	/// <summary>
	/// Calculate the circumstances of a solar eclipse.
	/// </summary>
	/// <returns>
	/// <para>solarEclipseCertainDateDay -- Solar eclipse date (day)</para>
	/// <para>solarEclipseCertainDateMonth -- Solar eclipse date (month)</para>
	/// <para>solarEclipseCertainDateYear -- Solar eclipse date (year)</para>
	/// <para>utFirstContactHour -- First contact of shadow (hour)</para>
	/// <para>utFirstContactMinutes -- First contact of shadow (minutes)</para>
	/// <para>utMidEclipseHour -- Mid-eclipse (hour)</para>
	/// <para>utMidEclipseMinutes -- Mid-eclipse (minutes)</para>
	/// <para>utLastContactHour -- Last contact of shadow (hour)</para>
	/// <para>utLastContactMinutes -- Last contact of shadow (minutes)</para>
	/// <para>eclipseMagnitude -- Eclipse magnitude</para>
	/// </returns>
	public (double solarEclipseCertainDateDay, int solarEclipseCertainDateMonth, int solarEclipseCertainDateYear, double utFirstContactHour, double utFirstContactMinutes, double utMidEclipseHour, double utMidEclipseMinutes, double utLastContactHour, double utLastContactMinutes, double eclipseMagnitude) SolarEclipseCircumstances(double localDateDay, int localDateMonth, int localDateYear, bool isDaylightSaving, int zoneCorrectionHours, double geogLongitudeDeg, double geogLatitudeDeg)
	{
		int daylightSaving = isDaylightSaving ? 1 : 0;

		double julianDateOfNewMoon = PAMacros.NewMoon(daylightSaving, zoneCorrectionHours, localDateDay, localDateMonth, localDateYear);
		double gDateOfNewMoonDay = PAMacros.JulianDateDay(julianDateOfNewMoon);
		double integerDay = gDateOfNewMoonDay.Floor();
		int gDateOfNewMoonMonth = PAMacros.JulianDateMonth(julianDateOfNewMoon);
		int gDateOfNewMoonYear = PAMacros.JulianDateYear(julianDateOfNewMoon);
		double utOfNewMoonHours = gDateOfNewMoonDay - integerDay;
		double localCivilDateDay = PAMacros.UniversalTime_LocalCivilDay(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int localCivilDateMonth = PAMacros.UniversalTime_LocalCivilMonth(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);
		int localCivilDateYear = PAMacros.UniversalTime_LocalCivilYear(utOfNewMoonHours, 0.0, 0.0, daylightSaving, zoneCorrectionHours, integerDay, gDateOfNewMoonMonth, gDateOfNewMoonYear);

		double utMaxEclipse = PAMacros.UTMaxSolarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongitudeDeg, geogLatitudeDeg);
		double utFirstContact = PAMacros.UTFirstContactSolarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongitudeDeg, geogLatitudeDeg);
		double utLastContact = PAMacros.UTLastContactSolarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongitudeDeg, geogLatitudeDeg);
		double magnitude = PAMacros.MagSolarEclipse(localDateDay, localDateMonth, localDateYear, daylightSaving, zoneCorrectionHours, geogLongitudeDeg, geogLatitudeDeg);

		double solarEclipseCertainDateDay = localCivilDateDay;
		int solarEclipseCertainDateMonth = localCivilDateMonth;
		int solarEclipseCertainDateYear = localCivilDateYear;

		double utFirstContactHour = (utFirstContact == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utFirstContact + 0.008333);
		double utFirstContactMinutes = (utFirstContact == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utFirstContact + 0.008333);

		double utMidEclipseHour = (utMaxEclipse == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utMaxEclipse + 0.008333);
		double utMidEclipseMinutes = (utMaxEclipse == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utMaxEclipse + 0.008333);

		double utLastContactHour = (utLastContact == -99.0) ? -99.0 : PAMacros.DecimalHoursHour(utLastContact + 0.008333);
		double utLastContactMinutes = (utLastContact == -99.0) ? -99.0 : PAMacros.DecimalHoursMinute(utLastContact + 0.008333);

		double eclipseMagnitude = (magnitude == -99.0) ? -99.0 : Math.Round(magnitude, 3);

		return (solarEclipseCertainDateDay, solarEclipseCertainDateMonth, solarEclipseCertainDateYear, utFirstContactHour, utFirstContactMinutes, utMidEclipseHour, utMidEclipseMinutes, utLastContactHour, utLastContactMinutes, eclipseMagnitude);
	}
}
