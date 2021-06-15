using System.ComponentModel.DataAnnotations;
using ServiceStation.DAL;

namespace ServiceStation.BLL.Models
{
    public class CarDTO : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string CarNumber { get; set; }
        [Required]
        public string CarModel { get; set; }
        [Required]
        public double EngineCapacity { get; set; }
        [Required]
        public string BodyNumber { get; set; }
        [Required]
        public int YearOfProduction { get; set; }

        public override string ToString()
        {
            return $"{Id} {CarNumber} {CarModel} {EngineCapacity} {BodyNumber} {YearOfProduction}";
        }
    }
}
