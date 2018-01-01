using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Coding.Assessment.Ipreo.Models.Entities;
using Coding.Assessment.Ipreo.Repositories.Interfaces;

namespace Coding.Assessment.Ipreo.Repositories
{
    public class SubwayStopRepository : Repository<SubwayStop>, ISubwayStopRepository
    {
        private const string DataFilePath = "~/App_Data/stops.txt";

        protected override IQueryable<SubwayStop> FetchDbSet()
        {
            IList<SubwayStop> subwayStops = new List<SubwayStop>();
            
            using (StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath(DataFilePath)))
            {
                streamReader.ReadLine(); //skip first line
                string subwayStopLine;
                while ((subwayStopLine = streamReader.ReadLine()) != null)
                {
                    string[] subwayStopData = subwayStopLine.Split(',');
                    string id = subwayStopData[0];
                    string name = subwayStopData[2];
                    string description = subwayStopData[3];
                    string parentId = subwayStopData[9];
                    double latitude;
                    double longitude;

                    if (!double.TryParse(subwayStopData[4], out latitude))
                    {
                        throw new ArgumentException($"[SubwayStopRepository.FetchDbSet] Invalid value for latitude for Subway Stop with id {id}.");
                    }

                    if (!double.TryParse(subwayStopData[5], out longitude))
                    {
                        throw new ArgumentException($"[SubwayStopRepository.FetchDbSet] Invalid value for longitude for Subway Stop with id {id}.");
                    }
                    
                    subwayStops.Add(new SubwayStop(id,
                                                   name,
                                                   description,
                                                   latitude,
                                                   longitude,
                                                   parentId));
                }
            }

            foreach (SubwayStop subwayStop in subwayStops)
            {
                if (!string.IsNullOrEmpty(subwayStop.ParentId))
                {
                    SubwayStop parentStop = subwayStops.FirstOrDefault(s => s.Id == subwayStop.ParentId);
                    if (parentStop != null)
                    {
                        parentStop.AddChild(subwayStop);
                    }
                }
            }

            // just get the parents
            IEnumerable<SubwayStop> subwayStopQueryable = subwayStops.Where(s => subwayStops.All(p => s.ParentId != p.Id));
            return subwayStopQueryable.AsQueryable();
        }
    }
}