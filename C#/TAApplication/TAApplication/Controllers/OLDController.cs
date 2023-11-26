/*
 *  Authors:   Ray Parker and Daniel He
 *  Date: October 18th, 2022
 *  Course: CS 4540, University of Utah, School of Computing 
 *  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
 *  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
 *  another source.  Any references used in the completion of the assignment are cited in my README file.
 *  
 *    File Contents:
 *  This controller is an old version of the Applications Controller except written by hand. This controller ended up being replaced by 
 *  an automagically created controller created via scaffolding.
 */

using TAApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;


namespace TAApplication.Controllers
{
    public class OLDController: Controller
    {
        //The following is role-restricted code

        [Authorize(Roles = "Professor, Admin")]
        public IActionResult ApplicationList()
        {
            return View();
        }


        [Authorize(Roles = "Applicant")]
        public IActionResult ApplicationCreate()
        {
            return View();
        }

        [Authorize(Roles = "Professor, Admin, Applicant")]
        [Authorize(Policy = "CorrectUnid")]
        public IActionResult ApplicationDetails()
        {
            return View();
        }

    }
}
