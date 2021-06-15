using System;
using System.ComponentModel.DataAnnotations;
using ServiceStation.DAL;

namespace ServiceStation.BLL.Models
{
    public class OrderDTO : IEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public int InspectorId { get; set; }
        [Required]
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Id} {Date} {ServiceName} {Price} {CarId} {OwnerId} {InspectorId} {IsCompleted}";
        }
    }
}
