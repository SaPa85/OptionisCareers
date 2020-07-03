using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionisJobs.Models
{
	public class Users
	{
		public List<User> AllUsers { get; set; }
	}

	public class User
	{
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public bool IsRecruiter { get; set; }
	}
}
