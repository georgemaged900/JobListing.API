using JobListing.API.Models;
using Newtonsoft.Json.Linq;

namespace JobListing.API.Services
{
    public class JobService
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RemoteJob>> GetJobsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://remoteok.com/api");
            var jsonArray = JArray.Parse(response);

            // Skip the first item (it's metadata)
            var jobData = jsonArray.Skip(1).Select(job => new RemoteJob
            {
                Id = job["id"]?.ToString(),
                Company = job["company"]?.ToString(),
                Position = job["position"]?.ToString(),
                Location = job["location"]?.ToString(),
                Url = job["url"]?.ToString(),
                Tags = job["tags"]?.Select(t => t.ToString()).ToList() ?? new List<string>()
            }).ToList();

            return jobData;
        }
    }
}
