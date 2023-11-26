/*
 *  Authors:   Ray Parker and Daniel He
 *  Date: September 21st, 2022
 *  Course: CS 4540, University of Utah, School of Computing 
 *  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
 *  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
 *  another source.  Any references used in the completion of the assignment are cited in my README file.
 *  
 *    File Contents:
 *  This admin controller controls the website experience for anyone with the admin role. It utilizes role manager and user managers and
 *  grants access to the admin page.
 */

using TAApplication.Areas.Data;
using TAApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using TAApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace TAApplication.Controllers
{

    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private RoleManager<IdentityRole> _rm;

        private UserManager<TAUser> _um;

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, ILogger<AdminController> logger, RoleManager<IdentityRole> rm, UserManager<TAUser> um)
        {
            _context = context;
            _logger = logger;
            _rm = rm;
            _um = um;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        /*
        [HttpPost]
        public async Task<IActionResult> ChangeRole(string user_id, string role, string add_remove)
        {
        }
        */

        /*
        if asked to remove
          user <= look this up based on the id
          user_manager <= remove "role" from "user"
             return success
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //The following endpoints correspond to the Enrollments Over Time pages

        //EnrollmentTrends
        public IActionResult EnrollmentTrends() {
            return View();
        }

        [HttpPost]
        //TODO GetEnrollmentData
        public async Task<List<int>> GetEnrollmentData(DateTime startDate, DateTime endDate, string courseName) {
            /*
             Using courseDeptNum, get a list of ints from EnrollmentNav
             Then, using startdate and enddate, trim down the list to the specified dates
             Return type should be List<int> once implemented
             */

            // _context.Enrollments.Include(s => s.enrollmentNavs);

            //set time to midnight 
            
            DateTime startDateFix = new DateTime(startDate.Year, startDate.Month, startDate.Day,  0, 0, 0);
            DateTime endDateFix = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);

            var course = await _context.Enrollments.Include(s => s.enrollmentNavs).FirstOrDefaultAsync(o => o.Course == courseName);
            var enrollmentList = course.enrollmentNavs;
            var result = new List<int>();
            bool start = false;
            foreach(var item in enrollmentList)
            {

                string date = item.date.Substring(1);
                date = date + ", 2022";
                DateTime dateTime = Convert.ToDateTime(date);
                //check the date is within range
                if (!start)
                {
                    start = (dateTime == startDateFix);
                }
                //At the end, add the last element and return the result.
                else if(dateTime == endDateFix)
                {
                    
                    result.Add(item.enrolled);
                    return result;
                }
                //If the date matches, add the enrollment number to the result list;
                if (start)
                {
                   
                    result.Add(item.enrolled);

                }
            }
            return result;
            
        }

        [HttpGet]
        public IEnumerable<string> GetCourseList()
        {
            List<string> courseNames = new List<string>();
            foreach(var enroll in _context.Enrollments)
            {
                courseNames.Add(enroll.Course);
            }
            return courseNames;
        }
    }
}