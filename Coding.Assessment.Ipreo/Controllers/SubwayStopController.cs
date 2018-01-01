using System;
using System.Web.Http;
using Coding.Assessment.Ipreo.Managers.Interfaces;
using Coding.Assessment.Ipreo.Models;
using Coding.Assessment.Ipreo.Models.Messages;

namespace Coding.Assessment.Ipreo.Controllers
{
    [RoutePrefix(Routes.SubwayStops.Prefix)]
    public class SubwayStopController : ApiController
    {
        private readonly ISubwayStopManager _subwayStopManager;

        public SubwayStopController(ISubwayStopManager subwayStopManager)
        {
            if (subwayStopManager == null)
            {
                throw new ArgumentNullException(nameof(subwayStopManager));
            }

            _subwayStopManager = subwayStopManager;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_subwayStopManager.GetSubwayStops());
        }

        [HttpPost]
        [Route(Routes.SubwayStops.CalculateDistance)]
        public IHttpActionResult CalculateDistance(SubwayStopDistanceRequestMessage message)
        {
            SubwayStopDistanceResponseMessage response = _subwayStopManager.CalculateDistance(message);

            return Ok(response);
        }
    }
}