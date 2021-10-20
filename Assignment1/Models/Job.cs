using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Job
    {
        [Required, StringLength(100, ErrorMessage = "Job title too long")]
        public string JobTitle { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage = "Invalid salary")]
        public int Salary { get; set; }
    }
}