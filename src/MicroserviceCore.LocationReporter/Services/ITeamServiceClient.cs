using System;

namespace MicroserviceCore.LocationReporter.Services
{
    public interface ITeamServiceClient
    {
        Guid GetTeamForMember(Guid memberID);
    }
}