using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    //I'm not quite sure setting foreign keys is in the scope of this challenge, but Compensation can be added without checking if the employee is in the database
    public class Compensation
    {
        //Set employee as one part of composite key
        [Key]
        public Employee employee { get; set; }

        public float salary { get; set; }

        //Set effectiveDate as other part
        [Key]
        public DateTime effectiveDate { get; set; }
    }
}