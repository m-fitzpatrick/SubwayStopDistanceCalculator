using System;

namespace Coding.Assessment.Ipreo.Models.Messages
{
    public class SubwayStopDistanceResponseMessage
    {
        public const double KilometersPerMile = 0.621371d; 

        public SubwayStopDistanceResponseMessage(string originName, string destinationName, double distanceInKm)
        {
            OriginName = originName;
            DestinationName = destinationName;
            DistanceInKm = distanceInKm;
            DistanceInMiles = Math.Round(distanceInKm * KilometersPerMile, 2);
        }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public double DistanceInKm { get; set; }

        public double DistanceInMiles { get; set; }
    }
}