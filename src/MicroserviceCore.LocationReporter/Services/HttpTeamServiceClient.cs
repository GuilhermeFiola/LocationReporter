using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MicroserviceCore.LocationReporter.Models;


namespace MicroserviceCore.LocationReporter.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly ILogger logger;
        private HttpClient httpClient;

        public HttpTeamServiceClient(IOptions<TeamServiceOptions> serviceOptions, ILogger<HttpTeamServiceClient> logger)
        {
            this.logger = logger;
            var url = serviceOptions.Value.Url;

            logger.LogInformation("Team Service HTTP client using URL {0}", url);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
        }

        public Guid GetTeamForMember(Guid memberID)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/members/{0}/team", memberID)).Result;

            TeamIDResponse teamIDResponse;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                teamIDResponse = JsonConvert.DeserializeObject<TeamIDResponse>(json);
                return teamIDResponse.TeamID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public class TeamIDResponse
        {
            public Guid TeamID { get; set; }
        }
    }
}