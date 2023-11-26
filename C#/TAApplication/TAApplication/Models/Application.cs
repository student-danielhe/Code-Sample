/*
  Authors:   Ray Parker and Daniel He
  Date:      September 25th, 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This is the application model, which stores all of the info needed for the creation of a TA application.
 */


using TAApplication.Areas.Data;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TAApplication.Models;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace TAApplication.Models
{

    public enum Degree { 
        BS, MS, PhD
    }

    //[Index(nameof(User), IsUnique = true)]
    public class Application:ModificationTracking
    {
        
        public int ID { get; set; }

        [Required]
        [Display(Name ="Degree Pursuing", Description = "Current Degree you are pursuing", Prompt = "PhD")]
        public Degree pursuit{ get; set; }

        
        public TAUser User { get; set; }

        [Required]
        [Display(Name = "Department", Description = "The department or program you are in", Prompt = "CS")]
        [StringLength(50)]
        public string department { get; set; }


        [Display(Name = "Grade Point Average", Description = "Please provide a GPA between 0.0 and 4.0.", Prompt ="4.0")]
        [Range(0,5)]
        [Required]
        public float GPA { get; set; }


        [Display(Name = "Desired Work Hours", Description = "Please provide an ideal number of work hours between 5 and 20.", Prompt ="10")]
        [Range(5,20)]
        [Required]
        public int workHours { get; set; }

        [Display(Name = "Available Early", Description = "Are you available the week before school starts")]
        public bool weekBefore { get; set; }

        [Display(Name = "Semesters completed at Utah", Description = "Enter your semesters completed at Utah", Prompt = "1")]
        [Range(0,999)]
        [Required]
        public int semesters { get; set; }

        [Display(Name = "Personal Statement", Description = "Enter your Personal Statement (Max 50000 letters)", Prompt = "I am a good person.")]
        [StringLength(50000)]
        [DataType(DataType.MultilineText)]
        public string ?personalStatement { get; set; }

        [Display(Name = "Transfer School", Description = "Enter your previous school if any", Prompt = "")]
        [StringLength(150)]
        public string ?transfer { get; set; }

        [Display(Name = "LinkedIn URL", Description = "", Prompt = "")]
        [Url]
        public string ?linkedin { get; set; }

        [ScaffoldColumn(false)]
        public string ?resume { get; set; } //must be pdf file name

        [ScaffoldColumn(false)]
        public string ?photo { get; set; } //must be a photo file type 
        
        public ICollection<ApplicationSlots> Slots { get; set; }
    }
}
