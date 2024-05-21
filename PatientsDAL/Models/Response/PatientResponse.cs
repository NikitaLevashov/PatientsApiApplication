using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsDAL.Models.Response
{
    public class PatientResponse
    {
        public PatientNameResponse Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool? Active { get; set; }
    }
}
