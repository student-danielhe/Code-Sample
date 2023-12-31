﻿/*
 *  Authors:   Ray Parker and Daniel He
 *  Date: October 18th, 2022
 *  Course: CS 4540, University of Utah, School of Computing 
 *  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
 *  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
 *  another source.  Any references used in the completion of the assignment are cited in my README file.
 *  
 *    File Contents:
 *  The courses controller controls the website experience for the courses page. This controller was automagically created by scaffolding
 *  the Course model, and it controls how the user navigates the various course-related pages such as Details and Delete.
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TAApplication.Controllers
{

    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
              return View();
        }
        public async Task<IActionResult> List()
        {
            return View(await _context.Course.ToListAsync());
        }
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


        // GET: Courses/Details/1
        public async Task<IActionResult> Details1(int? id) {
            var course = await _context.Course.FindAsync(1);
            if (course == null)
            {
                return NotFound();
            }
            return RedirectToAction("Details", course);
        }


        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,semester,year,title,department,courseNumber,section,description,profUNID,profName,time,location,credit,enroll,note")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details",course);
            }
            return View();
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit1(int? id)
        {
            var course = await _context.Course.FindAsync(1);
            if (course == null)
            {
                return NotFound();
            }
            return RedirectToAction("Edit", course);
        }


        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,semester,year,title,department,courseNumber,section,description,profUNID,profName,time,location,credit,enroll,note")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details",course);
            }
            return RedirectToAction("Details",course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public async Task<IActionResult> EditNote(string str, int? CourseID)
        {
            //find the course
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(CourseID);
            if (course != null)
            {
                course.note = str;
            }
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        private bool CourseExists(int id)
        {
          return _context.Course.Any(e => e.ID == id);
        }
    }
}
