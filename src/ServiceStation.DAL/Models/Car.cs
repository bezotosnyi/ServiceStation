namespace ServiceStation.DAL.Models
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string CarModel { get; set; }
        public double EngineCapacity { get; set; }
        public string BodyNumber { get; set; }
        public int YearOfProduction { get; set; }
    }
}
