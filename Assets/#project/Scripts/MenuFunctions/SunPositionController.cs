/*
 * Code by Carlos de la Barrera - 2016
 * Edited by Thomas Van Bouwel - 2016
 */

﻿using UnityEngine;
using System.Collections;
using System;

public class SunPositionController : MonoBehaviour
{
    public Transform azimuth;
    public Transform altitude;

    private const double Deg2Rad = Math.PI / 180.0;
    private const double Rad2Deg = 180.0 / Math.PI;

    public void Start()
    {
        //for debugging: check for each hour the altitude and azimuth
        for(int i = 8; i < 20; i++)
        {
            GetSunPosition(0, ModelInfo.dateTime.Minute, i, ModelInfo.dateTime.Day, ModelInfo.dateTime.Month, ModelInfo.dateTime.Year, (double)ModelInfo.latitude, (double)ModelInfo.longitude);
            //GetSunPos( ModelInfo.dateTime, (double)ModelInfo.latitude, (double)ModelInfo.longitude);
        }

        //set the position of sun to default (current time, location: Brussels)
        Vector2 setCurrent = GetSunPosition(0, ModelInfo.dateTime.Minute, ModelInfo.dateTime.Hour, ModelInfo.dateTime.Day, ModelInfo.dateTime.Month, ModelInfo.dateTime.Year, (double)ModelInfo.latitude, (double)ModelInfo.longitude);
        azimuth.localEulerAngles = new Vector3(0, setCurrent.x, 0);
        altitude.localEulerAngles = new Vector3(setCurrent.y, 180f, 0);

        GetSunPos(ModelInfo.dateTime, (double)ModelInfo.latitude, (double)ModelInfo.longitude);
    }


    public Vector2 GetSunPos(DateTime dt, double longitude, double latitude)
    {
        dt.ToUniversalTime();

        double jd = GetJulianDate(dt); //juliandate
        double d = GetSunDeclination(dt); //sun declination
        double h = GetSunHourAngle(latitude*Deg2Rad , d); //sun hour angle
        double z = GetZenith(d,latitude*Deg2Rad,h); //zenith 

        double elevation = GetElevation(d, latitude * Deg2Rad, h);
        double azimuth = GetAzimuth(d, latitude * Deg2Rad, z);

        Debug.Log("jd:" + jd + " d:" + d + " h:" + h + " z:" + z +" elev:"+elevation+" azim:"+azimuth);

        return new Vector2((float) (elevation*Rad2Deg), (float) (azimuth*Rad2Deg));
    }

    //https://en.wikipedia.org/wiki/Julian_day
    //converts DateTime to Julian Date
    private double GetJulianDate(DateTime time)
    {
        double a = (14 - time.Month) / 12.0;
        double y = time.Year + 4800 - a; //years since March 1, 4801 BC
        double m = time.Month + 12 * a - 3; //same for months
        double JDN = time.Day + (153 * m + 2) / 5.0 + 365 * y + y / 4.0 - y / 100.0 + y / 400.0 - 32045; //julian day number
        double julianDate = JDN + (time.Hour - 12) / 24.0 + time.Minute / 1440.0 + time.Second / 86400.0;

        return julianDate;
    }

    //https://en.wikipedia.org/wiki/Position_of_the_Sun#Declination_of_the_Sun_as_seen_from_Earth
    //The declination of the Sun, δ☉, is the angle between the rays of the Sun and the plane of the Earth's equator.
    //returns declination in radians
    private double GetSunDeclination(DateTime time)
    {
        int N = time.DayOfYear;
        double e = -23.44 * Deg2Rad; //earths axial tilt //SIGN RIGHT?
        double dpd = (360.0/365.0) * Deg2Rad; //degrees per day (approximate earths orbit by circle)
        double decl = -e * Math.Cos(dpd * ((N + 10) % 365)); //less accurate
        //double decl = -Math.Asin(0.39779 * Math.Cos(0.98565 * (N + 10) + 1.914 * Math.Sin(0.98565 * (N - 2)))); //formula stil in degrees, but Math works with rad

        return decl;
    }


    //https://en.wikipedia.org/wiki/Hour_angle / http://suncalc.net/scripts/suncalc.js
    // The hour angle of a point is the angle between two planes: one containing the Earth's axis and the zenith (the meridian plane), and the other containing the Earth's axis and the given point (the hour circle passing through the point)
    private double GetSunHourAngle(double latitude, double declination)
    {
        /*double LST; //local solar time
        double RA; //right ascension https://en.wikipedia.org/wiki/Hour_angle#Solar_hour_angle

        double hourAngle = LST - RA;*/

        double SA = -0.83 * Deg2Rad; //sunset angle
        double hourAngle = Math.Acos( Math.Sin(SA) - Math.Sin(latitude) * Math.Sin(declination)) / (Math.Cos(latitude) * Math.Cos(declination));

        return hourAngle;
    }

    //https://en.wikipedia.org/wiki/Solar_zenith_angle
    //declination, latitude, hour angle
    private double GetZenith(double d, double lat, double h)
    {
        return Math.Acos(Math.Sin(lat) * Math.Sin(d) + Math.Cos(lat) * Math.Cos(d) * Math.Cos(h));
    }

    //https://en.wikipedia.org/wiki/Solar_zenith_angle
    //declination, latitude, hour angle
    private double GetElevation(double d, double lat, double h)
    {
        return Math.Asin( Math.Sin(lat) * Math.Sin(d) + Math.Cos(lat) * Math.Cos(d) * Math.Cos(h) );
    }

    //https://en.wikipedia.org/wiki/Solar_azimuth_angle
    //declination, latitude, solar zenith angle
    private double GetAzimuth(double d, double lat, double SZA)
    {
        double nom = Math.Sin(d) - Math.Cos(SZA) * Math.Sin(lat);
        double denom = Math.Sin(SZA) * Math.Cos(lat);

        double azimuth = Math.Acos(nom / denom);
        return azimuth;
    }

    /*--------------------*/

    public Vector2 GetSunPosition(int gmt, int minute, int hour, int day, int month, int year, double longitude, double latitude)
    {
        DateTime time = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
        time = time.AddHours(-gmt);

        time = time.ToUniversalTime(); //get UTC...

        //number of days in J2000.0...
        double julianDate = 367 * time.Year - (int)((7.0 / 4.0) * (time.Year +
            (int)((time.Month + 9.0) / 12.0))) +
            (int)((275.0 * time.Month) / 9.0) +
            time.Day - 730531.5;

        julianDate = GetJulianDate(time);

        double julianCenturies = julianDate / 36525.0;

        //sideral Time...
        double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;
        double siderealTimeUT = siderealTimeHours + (366.2422 / 365.2422) * (double)time.TimeOfDay.TotalHours;
        double siderealTime = siderealTimeUT * 15 + longitude;

        // Refine to number of days (fractional) to specific time.
        julianDate += (double)time.TimeOfDay.TotalHours / 24.0;
        julianCenturies = julianDate / 36525.0;

        // Solar Coordinates
        double meanLongitude = CorrectAngle(Deg2Rad * (280.466 + 36000.77 * julianCenturies));
        double meanAnomaly = CorrectAngle(Deg2Rad * (357.529 + 35999.05 * julianCenturies));
        double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) * Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));
        double elipticalLongitude = CorrectAngle(meanLongitude + equationOfCenter);
        double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

        // Right Ascension
        double rightAscension = Math.Atan2(Math.Cos(obliquity) * Math.Sin(elipticalLongitude), Math.Cos(elipticalLongitude));
        double declination = Math.Asin(Math.Sin(rightAscension) * Math.Sin(obliquity));

        // Horizontal Coordinates
        double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;
        if (hourAngle > Math.PI)
        {
            hourAngle -= 2 * Math.PI;
        }

        double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) * Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) * Math.Cos(declination) * Math.Cos(hourAngle));

        // Nominator and denominator for calculating Azimuth
        // angle. Needed to test which quadrant the angle is in.
        double aziNom = -Math.Sin(hourAngle);
        double aziDenom =
            Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
            Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

        double azimuth = Math.Atan(aziNom / aziDenom);

        if (aziDenom < 0) // In 2nd or 3rd quadrant
        {
            azimuth += Math.PI;
        }
        else if (aziNom < 0) // In 4th quadrant
        {
            azimuth += 2 * Math.PI;
        }


        Vector2 returnVector = new Vector2( (float) azimuth, (float) altitude); 
        returnVector *= Mathf.Rad2Deg;

        //zenith to altitude?
        returnVector.y = 90f - Mathf.Abs(returnVector.y);
        print(returnVector);
        return returnVector;
    }

    //brief Corrects an angle.
    //param angleInRadians An angle expressed in radians.
    //return An angle in the range 0 to 2*PI.
    private double CorrectAngle(double angleInRadians)
    {
        if (angleInRadians < 0)
        {
            return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
        }
        else if (angleInRadians > 2 * Math.PI)
        {
            return angleInRadians % (2 * Math.PI);
        }
        else
        {
            return angleInRadians;
        }
    }

}