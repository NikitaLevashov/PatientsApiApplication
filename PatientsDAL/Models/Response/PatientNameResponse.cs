using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsDAL.Models.Response
{
    public class PatientNameResponse
    {
        public Guid Id { get; set; }

        public string Use { get; set; }

        public string Family { get; set; }

        public List <string> Given { get; set; }
    }
}
