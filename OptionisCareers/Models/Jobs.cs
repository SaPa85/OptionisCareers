using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionisJobs.Models
{

	public class Jobs
	{
		public List<Job> JobListings { get; set; }
	}

	public class Job
	{
		public int JobID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Hours { get; set; }
		public double Salary { get; set; }
		public string DateCreated { get; set; }
		public string TimeCreated { get; set; }
		public string DateClosed { get; set; }
		public string TimeClosed { get; set; }
		public int AuthorID { get; set; }
	}
}
