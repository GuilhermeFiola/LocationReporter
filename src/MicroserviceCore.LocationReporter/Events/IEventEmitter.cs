namespace MicroserviceCore.LocationReporter.Events
{
    public interface IEventEmitter
    {
        void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent);
    }
}