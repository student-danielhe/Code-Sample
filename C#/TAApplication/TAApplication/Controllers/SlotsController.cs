/*
  Authors:   Ray Parker and Daniel He
  Date:      November 11th, 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This controller is for our Slot model. This controller handles navigation to the Index page with the availability 
    information, and it also gets/sets information about the user's availability and displays it on the page/updates 
    the database accordingly.
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TAApplication.Areas.Data;
using TAApplication.Data;
using TAApplication.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TAApplication.Controllers
{
    public class SlotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TAUser> _um;
        

        public SlotsController(ApplicationDbContext context, UserManager<TAUser> um)
        {
            _context = context;
            _um = um;
        }

        // GET: Slots
        public async Task<IActionResult> Index()
        {
            var user = await _um.GetUserAsync(User);
            var app = await _context.Applicants.FirstOrDefaultAsync(m => m.User == user);
            
            return View(_context.Slot.Where(o => o.Application.ApplicationID == app.ID));
        }

        //Given an array of slots, update the DB to reflect the current users availability
        [HttpPost]
        public async Task<IActionResult> SetSchedule(string schedule)
        {
            List<string> result = schedule.Split(';').ToList();
            foreach(string s in result)
            {
                if (s == "")
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                List<string> timeSlot = s.Split(',').ToList();
                int id=Int32.Parse(timeSlot[0]);
                bool available=bool.Parse(timeSlot[1]);
                var slot = await _context.Slot.
                        FirstOrDefaultAsync(o => o.ID == id);
                    slot.IsAvailable = available;
                
            }
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        //Return an array of slots representing the current users availability
        [HttpGet]
        public async Task<IEnumerable<Slot>> GetSchedule()
        {
            var user = await _um.GetUserAsync(User);
            var app = await _context.Applicants.FirstOrDefaultAsync(m => m.User == user);
            var test = await _context.Slot.FirstOrDefaultAsync(o => o.Application.ApplicationID == app.ID);

            if (test == null) {
                var slots = new Slot[240];
                int x = 0;
                for (int i = 1; i <= 5; i++)
                {
                    for (int j = 1; j <= 48; j++)
                    {
                        Slot slot = new Slot { Day = i, Time = j };
                        ApplicationSlots applicantionSlot = new ApplicationSlots { ApplicationID = app.ID, SlotsID = slot.ID };
                        slot.Application = applicantionSlot;
                        slot.IsAvailable = false;
                        slots[x] = slot;
                        x++;
                    }
                }

                foreach (var slot in slots)
                {
                    _context.Slot.Add(slot);
                }
                await _context.SaveChangesAsync();

            }
            return _context.Slot.Where(o => o.Application.ApplicationID == app.ID);
        }

        private bool SlotExists(int id)
        {
            return _context.Slot.Any(e => e.ID == id);
        }
        
    }
}
