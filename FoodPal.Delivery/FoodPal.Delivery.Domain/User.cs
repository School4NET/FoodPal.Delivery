
using FoodPal.Delivery.Domain.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodPal.Delivery.Domain
{
    public class User : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } 
        public List<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
