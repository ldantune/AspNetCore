using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevJobs.API.Controllers
{
    [ApiController]
    [Route("api/job-vacancies/{id}/applications")]
    public class JobApplicationsController : ControllerBase
    {
        // POST api/job-vacancies/4/applications
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            // var jobVacancy = _context.JobVacancies
            //     .SingleOrDefault(jv => jv.Id == id);

            // if (jobVacancy == null)
            //     return NotFound();
            
            // var application = new JobApplication(
            //     model.ApplicantName,
            //     model.ApplicantEmail,
            //     id
            // );

            // _context.JobApplications.Add(application);
            // _context.SaveChanges();
            
            return NoContent();
        }
    }
}