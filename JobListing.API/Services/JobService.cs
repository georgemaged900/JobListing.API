using JobListing.API.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Xml;

namespace JobListing.API.Services
{
    public class JobService
    {
        private readonly HttpClient _httpClient; // DI

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RemoteJob>> GetJobsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://remoteok.com/api");
            var jsonArray = JArray.Parse(response);

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
        /* GetJobsAsync Method(Server-side)
        This method is responsible for fetching job data from the RemoteOK API and converting it into a structured list of RemoteJob objects.

         How It Works:
        The method sends an HTTP GET request to https://remoteok.com/api using HttpClient.

        It parses the JSON response into a JArray(JSON array).

        The first element in the API response is metadata, so we skip it using .Skip(1).

        Each job object in the array is mapped to the custom RemoteJob model, which includes:

        Id: Unique identifier of the job.

        Company: Name of the hiring company.

        Position: Job title or role.

        Location: Job location (usually "Remote").

        Url: Direct link to the job post.
        */
        //Tags: List of technologies or categories related to the job.
    }
}
