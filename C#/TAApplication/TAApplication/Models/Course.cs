/*
  Authors:   Ray Parker and Daniel He
  Date:      October 18th, 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This is the Course model, which stores all of the info needed for the creation of a new course on the TA application
    website.
 */

using Microsoft.CodeAnalysis.Differencing;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TAApplication.Models
{
    public enum Semester
    {
        Spring, Summer, Fall
    }
    public class Course
    {
        public int ID {get; set;}

        [Required]
        [Display(Name = "Semester", Description = "Semester course offered", Prompt ="Fall")]
        public Semester semester {get; set;}//Semester course offered
        
        [Required]
        [Display(Name = "Year", Description = "Year course offered", Prompt = "2022")]
        [Range(2000,2100)]
        public int year {get; set;}//Year course offered
        
        [Required]
        [Display(Name = "Title", Description = "Title of the course", Prompt = "Web Software")]
        [StringLength(100)]
        public string title{get; set;}//Title of the course(e.g., Web Software)
        
        [Required]
        [Display(Name = "Department", Description = "Department that the course is in", Prompt = "CS")]
        [StringLength(50)]
        public string department{get; set;}//Department(e.g., CS, CE, Comp) that the course is in
        
        [Required]
        [Display(Name = "Course Number", Description = "Course number", Prompt = "2420")]
        [Range(0,10000)]
        public int courseNumber{get; set;}//Course Number(e.g., 2420)
        

        [Display(Name = "Section", Description = "Course section", Prompt = "001")]
        [Range(0,100)]
        public int? section{get; set;}//Section(e.g., 001)
        
        [Display(Name = "Description", Description = "The course catalog description", Prompt = "This is an amazing course!")]
        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        public string? description{get; set;}//Description(i.e., the course catalog description)
        
        [Required]
        [Display(Name = "Professor UNID", Description = "Professor UNID", Prompt = "u1234567")]
        [RegularExpression("^u[0-9]{7}")]
        public string profUNID{get; set;}//Professor UNID
        
        [Required]
        [Display(Name = "Professor Name", Description = "Professor Name", Prompt = "Jim")]
        [StringLength(100)]
        public string profName{get; set;}//Professor Name
        
        [Required]
        [Display(Name = "Time and Days", Description = "Time and Days the course is offered", Prompt = "M/W 3:30-5:00")]
        [StringLength(100)]
        public string time{get; set;}//Time and Days offered(e.g., M/W 3:30-5:00)
       
        [Required]
        [Display(Name = "Location", Description = "Location where the course is held", Prompt = "WEB L104")]
        [StringLength(50)]
        public string location{get; set;}//Location(e.g., WEB L 104)
       
        [Required]
        [Display(Name = "Credit Hours", Description = "Credit hours the course offers", Prompt = "3")]
        [Range(0,5)]
        public int credit{get; set;}//Credit Hours(e.g., 3)
        
        [Required]
        [Display(Name = "Enrollment", Description = "How many students are enrolled", Prompt = "150")]
        [Range(0,1000)]
        public int enroll{get; set;}//Enrollment(i.e., how many students are enrolled, e.g., 150)
        
        [Display(Name = "Note", Description = "A place for the site admin to make notes about the course", Prompt = "Needs Extra TAs!")]
        [StringLength(10000)]
        public string? note{get; set;}//Note(a place for the site admin to make notes about the course, e.g., "Needs Extra TAs!")
}
}
