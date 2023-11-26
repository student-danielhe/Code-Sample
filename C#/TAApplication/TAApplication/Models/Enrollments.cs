/*
  Authors:   Ray Parker and Daniel He
  Date:      November 11th, 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This is the time slot model. Each slot represents a 15-minute time increment of the user's availability.
 */

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class Enrollments
    {
        [Required]
        public int ID { get; set; }

        [StringLength(10)]
        public string Course { get; set; }

        public ICollection<EnrollmentNav> enrollmentNavs { get; set; }
    }
}
