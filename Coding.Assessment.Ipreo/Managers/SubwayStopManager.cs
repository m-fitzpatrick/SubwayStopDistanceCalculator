using System;
using System.Collections.Generic;
using System.Linq;
using Coding.Assessment.Ipreo.Helper.Interfaces;
using Coding.Assessment.Ipreo.Managers.Interfaces;
using Coding.Assessment.Ipreo.Models.Entities;
using Coding.Assessment.Ipreo.Models.Messages;
using Coding.Assessment.Ipreo.Repositories.Interfaces;

namespace Coding.Assessment.Ipreo.Managers
{
    public class SubwayStopManager : ISubwayStopManager
    {
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly ISubwayStopRepository _subwayStopRepository;
        public SubwayStopManager(IDistanceCalculator distanceCalculator, ISubwayStopRepository subwayStopRepository)
        {
            if (distanceCalculator == null)
            {
                throw new ArgumentNullException(nameof(distanceCalculator));
            }

            if (subwayStopRepository == null)
            {
                throw new ArgumentNullException(nameof(subwayStopRepository));
            }

            _distanceCalculator = distanceCalculator;
            _subwayStopRepository = subwayStopRepository;
        }

        public IEnumerable<GetSubwayStopsResponseMessage> GetSubwayStops()
        {
            return _subwayStopRepository.List().Select(s => new GetSubwayStopsResponseMessage
                                                            {
                                                                Id = s.Id,
                                                                Name = $"{s.Name} [{s.Id}]"
                                                            })
                                        .OrderBy(s => s.Name)
                                        .ToList();
        }

        public SubwayStopDistanceResponseMessage CalculateDistance(SubwayStopDistanceRequestMessage request)
        {
            SubwayStop originSubwayStop = _subwayStopRepository.FirstOrDefault(s => s.Id == request.OriginSubwayStopId);
            if (originSubwayStop == null)
            {
                throw new Exception("[SubwayStopManager.CalculateDistance] Unable to locate origin subway stop");
            }

            SubwayStop destinationSubwayStop = _subwayStopRepository.FirstOrDefault(s => s.Id == request.DestinationSubwayStopId);
            if (destinationSubwayStop == null)
            {
                throw new Exception("[SubwayStopManager.CalculateDistance] Unable to locate destination subway stop");
            }

            double distance = _distanceCalculator.CalculateHaversineDistanceInKm(originSubwayStop.GeographicCoordinates, destinationSubwayStop.GeographicCoordinates);

            return new SubwayStopDistanceResponseMessage(originSubwayStop.Name, destinationSubwayStop.Name, Math.Round(distance, 2));
        }
    }
}