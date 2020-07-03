using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OptionisCareers.Models;
using OptionisJobs.Models;

namespace OptionisCareers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Jobs _jobs = new Jobs();
        private Users users = new Users();

        public static XDocument data = XDocument.Load("Data.xml");

        public List<Job> _joblistings = data.Root.Element("Jobs").Elements("Job").Select(x => new Job
        {
            JobID = (int)x.Element("JobID"),
            Title = (string)x.Element("Title"),
            Description = (string)x.Element("Description"),
            Hours = (string)x.Element("Hours"),
            Salary = (double)x.Element("Salary"),
            DateCreated = (string)x.Element("DateCreated"),
            TimeCreated = (string)x.Element("TimeCreated"),
            DateClosed = (string)x.Element("DateClosed"),
            TimeClosed = (string)x.Element("TimeClosed"),
            AuthorID = (int)x.Element("AuthorID")
        }).ToList();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_joblistings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Job JobToCreate)
        {
            if (!ModelState.IsValid)

                return View();
            
            XElement jobs = data.Root.Element("Jobs");
            jobs.Add(new XElement("Job",
                new XElement("JobID", JobToCreate.JobID),
                new XElement("Title", JobToCreate.Title),
                new XElement("Description", JobToCreate.Description),
                new XElement("Hours", JobToCreate.Hours),
                new XElement("Salary", JobToCreate.Salary),
                new XElement("DateCreated", JobToCreate.DateCreated),
                new XElement("TimeCreated", JobToCreate.TimeCreated),
                new XElement("DateClosed", JobToCreate.DateClosed),
                new XElement("TimeClosed", JobToCreate.TimeClosed),
                new XElement("AuthorID", JobToCreate.AuthorID)));

            data.Save("Data.xml");

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var JobToEdit = _joblistings.Where(x => x.JobID == id).First();

            return View(JobToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Job JobToEdit)
        {
            var originalJob = _joblistings.Where(x => x.JobID == JobToEdit.JobID).First();

            if (!ModelState.IsValid)

                return View(originalJob);

            IEnumerable<XElement> jobNodes = data.Root.Element("Jobs").Elements("Job");

            foreach (XElement node in jobNodes)
            {                
                if (node.Element("JobID").Value == originalJob.JobID.ToString())
                {
                    node.Element("JobID").SetValue(JobToEdit.JobID);
                    node.Element("Title").SetValue(JobToEdit.Title);
                    node.Element("Description").SetValue(JobToEdit.Description);
                    node.Element("Hours").SetValue(JobToEdit.Hours);
                    node.Element("Salary").SetValue(JobToEdit.Salary);
                    node.Element("DateCreated").SetValue(JobToEdit.DateCreated);
                    node.Element("TimeCreated").SetValue(JobToEdit.TimeCreated);
                    node.Element("DateClosed").SetValue(JobToEdit.DateClosed);
                    node.Element("TimeClosed").SetValue(JobToEdit.TimeClosed);
                    node.Element("AuthorID").SetValue(JobToEdit.AuthorID);
                }
            }

            data.Save("Data.xml");

            return RedirectToAction("Index");

        }
         
        public ActionResult Delete(int id)
        {
            IEnumerable<XElement> jobNodes = data.Root.Element("Jobs").Elements("Job");

            jobNodes.Where(e => e.Element("JobID").Value == id.ToString()).Remove();

            data.Save("Data.xml");

            return RedirectToAction("Index");
        }
    }
}
