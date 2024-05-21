using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientsDAL.Models
{
    public class PatientName
    {
        public Guid Id { get; set; } = new Guid();

        public Patient? Patient { get; set; }

        public int PatientKey { get; set; }

        public string? Use { get; set; }

        [Required]
        public string Family { get; set; }

        public string? MiddleName { get; set; }

        public string? FirstName { get; set; }
    }
}
