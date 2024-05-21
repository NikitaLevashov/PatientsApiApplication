using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientsDAL.Models
{
    public class Patient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PatientName Name { get; set; } = null!;

        public string? Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public bool? Active { get; set; }
    }
}
