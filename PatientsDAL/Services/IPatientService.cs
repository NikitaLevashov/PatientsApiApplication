using PatientsApiApplication.Models;
using PatientsDAL.Models.Response;

namespace PatientsDAL.Services
{
    public interface IPatientService
    {
        PatientResponse GetById(Guid id);
        IEnumerable<PatientResponse> GetByDate(string birthDate);
        IEnumerable<PatientResponse> GetAll();
        void Add(PatientRequestModel patient);
        void Update(PatientRequestModel patient);
        void Delete(Guid id);
        void GeneratePatients();
    }
}
