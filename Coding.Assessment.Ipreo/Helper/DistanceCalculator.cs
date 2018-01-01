using System;
using Coding.Assessment.Ipreo.Helper.Interfaces;
using Coding.Assessment.Ipreo.Models;

namespace Coding.Assessment.Ipreo.Helper
{
    public class DistanceCalculator : IDistanceCalculator
    {
        private const int RadiusOfTheEarth = 6371;
        public double CalculateHaversineDistanceInKm(GeographicCoordinates originPoint, GeographicCoordinates destinationPoint)
        {
            double startLatitudeInRadians = DegreesToRadians(originPoint.Latitude);
            double endLatitudeInRadians = DegreesToRadians(destinationPoint.Latitude);
            double latitudeDifferenceRadians = DegreesToRadians(destinationPoint.Latitude - originPoint.Latitude);
            double longitudeDifferenceRadians = DegreesToRadians(destinationPoint.Longitude - originPoint.Longitude);

            double h1 = Math.Sin(latitudeDifferenceRadians / 2) * Math.Sin(latitudeDifferenceRadians / 2) +
                        Math.Cos(startLatitudeInRadians) * Math.Cos(endLatitudeInRadians) * 
                        Math.Sin(longitudeDifferenceRadians / 2) * Math.Sin(longitudeDifferenceRadians / 2);

            double h2 = 2 * Math.Atan2(Math.Sqrt(h1), Math.Sqrt(1 - h1));

            return RadiusOfTheEarth * h2;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180d);
        }
    }
}