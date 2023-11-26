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
    public class Slot
    {
        /*
         * Requirements:
         * -Navigation property for the associated user, specifically the UserID from ASP Core Identity Table
         * -The idea of a time slot, like Thursday 11:45am
         * -Whether the slot is open/closed
         */
        //Linked to the UserID value from the ASP Core Identity Table in the TAApplication Database
        [Required]
        public int ID { get; set; }


        //Any weekday Mon-Fri, uses the Weekdays enum
        [Required]
        [Range(1,5)]
        public int Day { get; set; }

        //Any hour time increment between 8am and 8pm.
        [Required]
        [Range(1, 48)]
        public int Time { get; set; }


        //Marks if the time slot is available or unavailable. 0 = unavailable; 1 = available
        [Required]
        public bool IsAvailable { get; set; }

        //Navigation property to connect the availability to a user's application
        public ApplicationSlots Application { get; set; }
    }
}
