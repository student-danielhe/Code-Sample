/*
 *  Authors:   Ray Parker and Daniel He
 *  Date: October 3rd, 2022
 *  Course: CS 4540, University of Utah, School of Computing 
 *  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
 *  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
 *  another source.  Any references used in the completion of the assignment are cited in my README file.
 *  
 *    File Contents:
 *  The applications controller controls the website experience for the applications pages. This controller was automagically created by scaffolding
 *  the Application model, and it controls how the user navigates the various application-related pages such as Details and Delete.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TAApplication.Data;
using TAApplication.Models;
using TAApplication.Areas.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using Application = TAApplication.Models.Application;

namespace TAApplication.Controllers
{
    //for the authorization, we will allow admin to do everything to make testing easier
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TAUser> _um;
        IConfiguration _configuration;
        public ApplicationsController(ApplicationDbContext context, UserManager<TAUser> um, IConfiguration configuration)
        {
            _context = context;
            _um = um;
            _configuration = configuration;
        }

        // GET: Applications/
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Applications/List
        [Authorize(Roles = "Professor, Admin")]
        public async Task<IActionResult> List()
        {
              return View(await _context.Applicants.ToListAsync());
        }

        // GET: Applications/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {

            if (User.IsInRole("Applicant"))
            {
                var user = await _um.GetUserAsync(User);
                var app = await _context.Applicants.FirstOrDefaultAsync(m => m.User == user);
                return View(app);
            }

            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var application = await _context.Applicants
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }
        [Authorize(Roles ="Admin, Applicant")]
        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Applicant")]
        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,pursuit,department,GPA,workHours,weekBefore,semesters,personalStatement,transfer,linkedin")] Application application)
        {
            var user = await _um.GetUserAsync(User);
            var checkApp = await _context.Applicants
    .FirstOrDefaultAsync(m => m.User == user);           
            if (checkApp != null)
            {
                return View("Details", checkApp);
            }

                ModelState.Remove("User"); //tells program that user has been taken care of
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");
            ModelState.Remove("resume");
            ModelState.Remove("photo");
            
            application.User = await _um.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View("Details", application);
        }
        [Authorize(Roles = "Admin, Applicant")]
        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var application = await _context.Applicants.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }
        [Authorize(Roles = "Admin, Applicant")]
        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,pursuit,department,GPA,workHours,weekBefore,semesters,personalStatement,transfer,linkedin")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(application);
        }
        [Authorize(Roles = "Admin, Applicant")]
        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var application = await _context.Applicants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        [Authorize(Roles = "Admin, Applicant")]
        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applicants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Applicants'  is null.");
            }
            var application = await _context.Applicants.FindAsync(id);
            if (application != null)
            {
                _context.Applicants.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }


        private bool ApplicationExists(int id)
        {
          return _context.Applicants.Any(e => e.ID == id);
        }
        [Authorize(Roles = "Admin, Applicant")]
        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files, string? category, int? applicationID)
        {

            var application = await _context.Applicants.FindAsync(applicationID);
            
            var file = files.FirstOrDefault();

            if (file is null)
            {
                ViewData["ErrorMessage"] = "Did not select file for upload";
                return View("Details", application);
            }

            string filename = file.FileName;


            //check for valid category/applicationID
            if (category == null || applicationID == null)
            {
                return BadRequest("Something went wrong...");
            }

            //check if current user own the application
            var user = await _um.GetUserAsync(User);

            if (application.User != user)
            {
                return BadRequest("Wrong User");
            }

            //check if there is only one file
            if (files.Count != 1)
            {
                ViewData["ErrorMessage"] = "Only upload one file at a time";
                return View("Details", application);
            }

            //check file size
            if (file.Length == 0)
            {
                ViewData["ErrorMessage"] = "File does not exist";
                return View("Details", application);
            }
            if (file.Length > 100000000)
            {
                ViewData["ErrorMessage"] = "File is too large";
                return View("Details", application);
            }

            //generate a random file name for submitted file
            string new_name = Path.GetRandomFileName();
            //check if is a pdf file
            if (category == "resume")
            {
                if (!filename.EndsWith(".pdf"))
                {
                    ViewData["ErrorMessage"] = "Resume files must end with a .pdf";
                    return View("Details", application);
                }
                new_name = Path.GetFileNameWithoutExtension(new_name) + ".pdf";
            }
            else if (category == "photo")
            {
                //check if file is a png file
                if (!filename.EndsWith(".png"))
                {
                    ViewData["ErrorMessage"] = "Photo files must end with a .png";
                    return View("Details", application);
                }
                new_name = Path.GetFileNameWithoutExtension(new_name) + ".png";
            }
            else //not a pdf or photo, so share an error message
            {
                ViewData["ErrorMessage"] = "Invalid file type";
                return View("Details", application);
            }

            string path = Path.Combine(_configuration["FilePath"], new_name);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            if (category == "resume")
            {
                application.resume = new_name;
                _context.SaveChanges();
            }
            else
            {
                application.photo = new_name;
                _context.SaveChanges();
            }

            return View("Details", application);

        }
    }
}
