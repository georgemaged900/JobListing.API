namespace JobListing.API.Models
{
    public class RemoteJob
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public List<string> Tags { get; set; }
        public string Url { get; set; }
    }
}
