namespace OutlookClient.Services.Models
{
    public interface IContact
    {
        string Name { get; set; }
        string Location { get; set; }
        string Address { get; set; }
        string EmailAddress { get; set; }
        string Phone { get; set; }
        string Company { get; set; }
        string Alias { get; set; }
        string Thumbnail { get; set; }
    }
    public class Contact : IContact
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Alias { get; set; }
        public string Thumbnail { get; set; }
    }
}
