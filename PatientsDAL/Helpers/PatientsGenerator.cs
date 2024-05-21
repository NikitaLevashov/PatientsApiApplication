using PatientsDAL.Models;

namespace PatientsDAL.Helpers
{
    public static class PatientsGenerator
    {
        private static readonly string[] genders = { "male", "female", "other", "unknown" };

        public static void GeneratePatients(PatientDbContext db)
        {
            db.Database.EnsureCreated();

            List<Patient> patients = new List<Patient>();

            for (int i = 1; i < 100; i++)
            {
                Random rand = new Random();
                int index = rand.Next(genders.Length);
                bool randomBool = rand.NextDouble() >= 0.5;

                var randomDate = RandomDate(new DateTime(2019, 01, 01), new DateTime(2024, 01, 01), rand);

                Patient patient = new Patient();
                {
                    patient.Active = randomBool;
                    patient.BirthDate = randomDate;
                    patient.Gender = genders[index];
                    patient.Name = new PatientName
                    {
                        Family = "Ivanov" + i,
                        FirstName = "Ivan" + i,
                        MiddleName = "Ivanovich" + i,
                        Use = "official",
                        Id = Guid.NewGuid(),
                        PatientKey = patient.Id
                    };

                    patients.Add(patient);
                }
            }

            db.Patients.AddRange(patients);
            db.SaveChanges();
        }

        private static DateTime RandomDate(DateTime from, DateTime to, Random random)
        {
            return from.AddTicks((long)(random.NextDouble() * (to.Ticks - from.Ticks)));
        }
    }
}
