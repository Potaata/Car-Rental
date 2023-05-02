using CarRental.Domain.Enums;
using CarRental.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRental.Domain.Entities
{
    public class RentHistory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        [ForeignKey("User")]
        public string? ApprovedBy { get; set; }

        [ForeignKey("Cars")]
        public int CarId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public StatusEnums Status { get; set; } = StatusEnums.Pending;

        public float Price { get; set; }

    } 
}
