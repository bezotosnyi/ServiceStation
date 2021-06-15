namespace ServiceStation.DAL.Models
{
    public class Owner : IEntity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNum { get; set; }

    }
}
