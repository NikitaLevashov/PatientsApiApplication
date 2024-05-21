namespace PatientsApiApplication.Models
{
    public class PatientNameRequestModel
    {
        public Guid Id { get; set; }

        public string Use { get; set; }

        public string Family { get; set; }

        public List<string> Given { get; set; }
    }
}
