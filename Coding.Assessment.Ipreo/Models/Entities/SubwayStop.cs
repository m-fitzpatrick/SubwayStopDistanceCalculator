using System.Collections.Generic;
using Coding.Assessment.Ipreo.Models.Entities.Interfaces;

namespace Coding.Assessment.Ipreo.Models.Entities
{
    public class SubwayStop : IEntityBase
    {
        public SubwayStop(string id,
                          string name,
                          string description,
                          double latitude,
                          double longitude,
                          string parentId)
        {
            Id = id;
            Name = name;
            Description = description;
            GeographicCoordinates = new GeographicCoordinates(latitude, longitude);
            ChildStations = new List<SubwayStop>();
            ParentId = parentId;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public GeographicCoordinates GeographicCoordinates { get; private set; }

        public string ParentId { get; private set; }

        public IList<SubwayStop> ChildStations { get; private set; }

        public void AddChild(SubwayStop childStop)
        {
            ChildStations.Add(childStop);
        }
    }
}