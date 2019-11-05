using System;
using MicroserviceCore.LocationReporter.Events;

namespace MicroserviceCore.LocationReporter.Models
{
    public class CommandEventConverter : ICommandEventConverter
    {
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = new MemberLocationRecordedEvent
            {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberId = locationReport.MemberID,
                ReportID = locationReport.ReportID,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };

            return locationRecordedEvent;
        }
    }
}