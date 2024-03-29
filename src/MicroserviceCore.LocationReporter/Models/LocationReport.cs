using System;

namespace MicroserviceCore.LocationReporter.Models
{
    public class LocationReport
    {
        public Guid ReportID { get; set; }
        public string Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberID { get; set; }
    }
}