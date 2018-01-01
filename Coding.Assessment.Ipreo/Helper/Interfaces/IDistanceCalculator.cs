using Coding.Assessment.Ipreo.Models;

namespace Coding.Assessment.Ipreo.Helper.Interfaces
{
    public interface IDistanceCalculator
    {
        double CalculateHaversineDistanceInKm(GeographicCoordinates originPoint, GeographicCoordinates destinationPoint);
    }
}