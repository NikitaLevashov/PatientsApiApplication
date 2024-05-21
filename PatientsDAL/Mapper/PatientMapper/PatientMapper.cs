using PatientsDAL.Models;
using PatientsDAL.Models.Response;

namespace PatientsDAL.Mapper.PatientMapper
{
    public static class PatientMapper
    {
        public static IEnumerable<PatientResponse> MapToEnumerablePatientResponse(this IQueryable<Patient> patients)
        {
            List<PatientResponse> patientResponses = new List<PatientResponse>();

            foreach (var patient in patients)
            {
                var patientResponse = patient.MapToPatientResponse();

                patientResponses.Add(patientResponse);
            }

            return patientResponses;
        }

        public static PatientResponse MapToPatientResponse(this Patient patient)
        {
            var patientResponse = new PatientResponse
            {
                BirthDate = patient.BirthDate,
                Active = patient.Active,
                Gender = patient.Gender,
                Name = new PatientNameResponse
                {
                    Family = patient.Name?.Family,
                    Given = new List<string> { patient.Name?.FirstName, patient.Name?.MiddleName },
                    Id = patient.Name.Id,
                    Use = patient.Name.Use
                }
            };

            return patientResponse;
        }
    }
}
