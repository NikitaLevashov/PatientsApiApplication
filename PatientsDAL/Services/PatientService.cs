using Microsoft.EntityFrameworkCore;
using PatientsApiApplication.Models;
using PatientsDAL.Helpers;
using PatientsDAL.Mapper.PatientMapper;
using PatientsDAL.Models;
using PatientsDAL.Models.Response;

namespace PatientsDAL.Services
{
    public class PatientService : IPatientService
    {
        PatientDbContext patientDBContext;

        public PatientService(PatientDbContext patientDBContext)
        {
            this.patientDBContext = patientDBContext ?? throw new ArgumentNullException(nameof(patientDBContext));
        }

        public IEnumerable<PatientResponse> GetAll()
        {
            var patients = patientDBContext.Patients.Include(x => x.Name).Select(patient => patient.MapToPatientResponse());

            return patients;
        }

        public PatientResponse GetById(Guid id)
        {
            var patient = patientDBContext.Patients.Include(x => x.Name).FirstOrDefault(x => x.Name.Id == id);

            if (patient != null)
            {
                var patientResponse = patient.MapToPatientResponse();

                return patientResponse;
            }

            return null;
        }

        public IEnumerable<PatientResponse> GetByDate(string birthDate)
        {
            var dateTimePrefix = birthDate.Substring(0, 2).ToLower();

            if (Enum.TryParse(dateTimePrefix, out DatePrefixes result))
            {
                switch (result)
                {
                    case DatePrefixes.eq:
                        return this.GetPatientsByDateTimeWithEqPrefix(birthDate);
                    case DatePrefixes.gt:
                        return this.GetPatientsByDateTimeWithGtPrefix(birthDate);
                    case DatePrefixes.ne:
                        return this.GetPatientsByDateTimeWithNePrefix(birthDate);
                    case DatePrefixes.lt:
                        return this.GetPatientsByDateTimeWithLtPrefix(birthDate);
                    case DatePrefixes.ge:
                    case DatePrefixes.sa:
                        return this.GetPatientsByDateTimeWithGePrefix(birthDate);
                    case DatePrefixes.le:
                    case DatePrefixes.eb:
                        return this.GetPatientsByDateTimeWithLePrefix(birthDate);
                    case DatePrefixes.ap:
                        return this.GetPatientsByDateTimeWithApPrefix(birthDate);
                    default:
                        break;
                }
            }

            if (DateTime.TryParse(birthDate, out DateTime date))
            {
                return this.GetPatientsByDateTimeWithEqPrefix(birthDate);
            }

            return null;
        }

        public void Add(PatientRequestModel patientData)
        {
            if (patientData != null)
            {
                Patient patient = new Patient
                {
                    BirthDate = patientData.BirthDate,
                    Active = patientData.Active,
                    Gender = patientData.Gender,
                };

                PatientName patientName = new PatientName
                {
                    Family = patientData.Name.Family,
                    FirstName = patientData.Name.Given.Count > 0 ? patientData.Name.Given[0] : string.Empty,
                    MiddleName = patientData.Name.Given.Count > 1 ? patientData.Name.Given[0] : string.Empty,
                    Use = patientData.Name.Use,
                };

                patient.Name = patientName;

                patientDBContext.Patients.Add(patient);
                patientDBContext.SaveChanges();
            }
        }

        public void Update(PatientRequestModel patientData)
        {
            var patient = patientDBContext.Patients.Include(x => x.Name).FirstOrDefault(x => x.Name.Id == patientData.Name.Id);

            if (patient != null)
            {
                patient.BirthDate = patientData.BirthDate;
                patient.Active = patientData.Active;
                patient.Gender = patientData.Gender;
                patient.Name.Family = patientData.Name?.Family;
                patient.Name.FirstName = patientData.Name?.Given.Count > 0 ? patientData.Name.Given[0] : string.Empty;
                patient.Name.MiddleName = patientData.Name?.Given.Count > 1 ? patientData.Name.Given[1] : string.Empty;
                patient.Name.Use = patientData.Name?.Use;

                patientDBContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var patient = patientDBContext.Patients.Include(x => x.Name).FirstOrDefault(x => x.Name.Id == id);

            if (patient != null)
            {
                patientDBContext.Patients.Remove(patient);
                patientDBContext.SaveChanges();
            }
        }

        public void GeneratePatients()
        {
            PatientsGenerator.GeneratePatients(patientDBContext);
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithEqPrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate.Date == result.Date);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithNePrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate.Date != result.Date);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithLtPrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate < result);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithGtPrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate > result);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithGePrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate >= result);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithLePrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).Where(x => x.BirthDate <= result);

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }

        private IEnumerable<PatientResponse> GetPatientsByDateTimeWithApPrefix(string birthDay)
        {
            if (DateTime.TryParse(birthDay.Substring(2).ToLower(), out DateTime result))
            {
                var patients = patientDBContext.Patients.Include(x => x.Name).
                    Where(x => x.BirthDate <= result && x.BirthDate >= result.AddMonths(-2)
                    || x.BirthDate >= result && x.BirthDate <= result.AddMonths(2));

                var patientsResponse = patients.MapToEnumerablePatientResponse();

                return patientsResponse;
            }

            return null;
        }
    }
}
