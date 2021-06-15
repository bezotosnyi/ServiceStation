using System;

namespace ServiceStation.DAL.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public int CarId { get; set; }
        public int OwnerId { get; set; }
        public int InspectorId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
