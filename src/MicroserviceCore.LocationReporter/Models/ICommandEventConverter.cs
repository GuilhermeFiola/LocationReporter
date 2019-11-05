using MicroserviceCore.LocationReporter.Events;

namespace MicroserviceCore.LocationReporter.Models
{
    public interface ICommandEventConverter
    {
        MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport);
    }
}