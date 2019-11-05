using System;
using Microsoft.AspNetCore.Mvc;
using MicroserviceCore.LocationReporter.Events;
using MicroserviceCore.LocationReporter.Models;
using MicroserviceCore.LocationReporter.Services;

namespace MicroserviceCore.LocationReporter.Controllers
{

    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private ICommandEventConverter converter;
        private IEventEmitter eventEmitter;
        private ITeamServiceClient teamServiceClient;

        public LocationReportsController(ICommandEventConverter converter, IEventEmitter eventEmitter, ITeamServiceClient teamServiceClient)
        {
            this.converter = converter;
            this.eventEmitter = eventEmitter;
            this.teamServiceClient = teamServiceClient;
        }

        [HttpPost]
        public IActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = converter.CommandToEvent(locationReport);
            locationRecordedEvent.TeamID = teamServiceClient.GetTeamForMember(locationReport.MemberID);
            eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}", locationReport);
        }
    }
}