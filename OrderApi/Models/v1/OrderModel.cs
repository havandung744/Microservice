using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models.v1
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerGuid { get; set; }

        [Required]
        public string CustomerFullName { get; set; }
    }
}
