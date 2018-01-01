using System.Collections.Generic;
using Coding.Assessment.Ipreo.Models.Messages;

namespace Coding.Assessment.Ipreo.Managers.Interfaces
{
    public interface ISubwayStopManager
    {
        IEnumerable<GetSubwayStopsResponseMessage> GetSubwayStops();

        SubwayStopDistanceResponseMessage CalculateDistance(SubwayStopDistanceRequestMessage request);
    }
}