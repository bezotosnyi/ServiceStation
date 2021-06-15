using System.ComponentModel.DataAnnotations;
using ServiceStation.DAL;

namespace ServiceStation.BLL.Models
{
    public class InspectorDTO : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"{Id} {Firstname} {LastName} {MiddleName} {Position} {Salary}";
        }
    }
}
