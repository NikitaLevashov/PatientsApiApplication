using Microsoft.AspNetCore.Mvc;
using PatientsApiApplication.Models;
using PatientsDAL.Services;

namespace PatientsApiApplication.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsApiController : ControllerBase
    {
        private readonly IPatientService patientService;
        public PatientsApiController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpGet]
        public IActionResult GetPatientns()
        {
            var patients = this.patientService.GetAll();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            var patient = this.patientService.GetById(id);

            if(patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet("birthDate={birthDate}")]
        public IActionResult GetPatientByBirthDay(string birthDate)
        {
            if (string.IsNullOrEmpty(birthDate))
            {
                return BadRequest();
            }

            var patients = this.patientService.GetByDate(birthDate);

            if(patients != null)
            {
                return Ok(patients);
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult AddPatient(PatientRequestModel patient)
        {
            if(patient != null && ModelState.IsValid)
            {
                this.patientService.Add(patient);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdatePatient(PatientRequestModel patientRequestModel)
        {
            if (patientRequestModel != null && ModelState.IsValid)
            {
                this.patientService.Update(patientRequestModel);

                return Ok();
            }

            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            this.patientService.Delete(id);

            return Ok();
        }

        [HttpPost()]
        [Route("postpatients")]
        public IActionResult Add()
        {
            this.patientService.GeneratePatients();

            return Ok();
        }
    }
}