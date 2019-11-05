using System;
using Newtonsoft.Json;

namespace MicroserviceCore.LocationReporter.Events
{
    public class MemberLocationRecordedEvent
    {
        public string Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberId { get; set; }
        public long RecordedTime { get; set; }
        public Guid ReportID { get; set; }
        public Guid TeamID { get; set; }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}