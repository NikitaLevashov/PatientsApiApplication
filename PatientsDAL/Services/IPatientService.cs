using PatientsApiApplication.Models;
using PatientsDAL.Models.Response;

namespace PatientsDAL.Services
{
    public interface IPatientService
    {
        PatientResponse GetById(int id);
        IEnumerable<PatientResponse> GetByDate(string birthDate);
        IEnumerable<PatientResponse> GetAll();
        void Add(PatientRequestModel patient);
        void Update(PatientRequestModel patient);
        void Delete(int id);
        void GeneratePatients();
    }
}
