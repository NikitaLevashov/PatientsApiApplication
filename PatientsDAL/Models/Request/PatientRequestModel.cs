namespace PatientsApiApplication.Models
{
    public class PatientRequestModel
    {
        public int Id { get; set; }

        public PatientNameRequestModel Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool? Active { get; set; }
    }
}
