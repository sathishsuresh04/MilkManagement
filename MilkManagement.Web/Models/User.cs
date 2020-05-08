namespace MilkManagement.Web.Models
{
    public class User
    {
       // public Guid Id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string DoorNoAndStreet { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string PrimaryEmailAddress { get; set; }

    }
}
