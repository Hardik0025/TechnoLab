using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Models
{
    public class jobs
    {
		public int job_id { get; set; }
		public string job_title { get; set; }
		public decimal min_salary { get; set; }
		public decimal max_salary { get; set; }

		public jobs(int job_id_, string job_title_, decimal min_salary_, decimal max_salary_)
		{
			this.job_id = job_id_;
			this.job_title = job_title_;
			this.min_salary = min_salary_;
			this.max_salary = max_salary_;
		}
	}
}
