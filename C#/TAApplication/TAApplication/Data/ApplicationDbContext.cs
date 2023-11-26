/*
  Authors:   Ray Parker and Daniel He
  Date:      September 27th, 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
  We, Daniel He and Ray Parker, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This application database context helps in setting up a newly created database for the website. It seeds in three roles 
    (Applicant, Professor, Admin), five different users, and five different courses for testing purposes.
 */

using TAApplication.Areas.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TAApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;
using TAApplication.Controllers;
using SendGrid.Helpers.Mail;

namespace TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor http)
            : base(options)
        {

            _httpContextAccessor = http;
        }

        public DbSet<Application> Applicants { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }

        public async Task InitializeUsers(UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            
            if (rm.Roles.Any())
            {
                return;   // DB has been seeded
            }
            //Add Roles

            var roles = new IdentityRole[]
            {
            new IdentityRole{Name="Admin"},
            new IdentityRole{Name="Professor"},
            new IdentityRole{Name="Applicant"},

            };
            foreach (IdentityRole s in roles)
            {
                await rm.CreateAsync(s);
            }

            //Add Users

            if (um.Users.Any())
            {
                return;
            }


            //Creation and database seeding of five different users

            var user = new TAUser {UserName= "admin@utah.edu", Email = "admin@utah.edu", EmailConfirmed = true, Unid = "u1234567", RefferedTo = "he" };
            var check = await um.CreateAsync(user, "123ABC!@#def");
            //Assign Roles
            if (check.Succeeded)
            {
                await um.AddToRoleAsync(user, "Admin");
                
            }
            user = new TAUser {UserName= "professor@utah.edu", Email = "professor@utah.edu", EmailConfirmed = true, Unid = "u7654321", RefferedTo = "he" };
            check = await um.CreateAsync(user, "123ABC!@#def");
            if (check.Succeeded)
            {
                await um.AddToRoleAsync(user, "Professor");
            }
            user = new TAUser {UserName= "u0000000@utah.edu", Email = "u0000000@utah.edu", EmailConfirmed = true, Unid = "u0000000", RefferedTo = "he" };
            check = await um.CreateAsync(user, "123ABC!@#def");
            if (check.Succeeded)
            {
                await um.AddToRoleAsync(user, "Applicant");
            }
            var user0 = user;
            
            user = new TAUser {UserName= "u0000001@utah.edu", Email = "u0000001@utah.edu", EmailConfirmed = true, Unid = "u0000001", RefferedTo = "he" };
            check = await um.CreateAsync(user, "123ABC!@#def");
            if (check.Succeeded)
            {
                await um.AddToRoleAsync(user, "Applicant");
            }
            var user1 = user;
            user=new TAUser {UserName= "u0000002@utah.edu", Email = "u0000002@utah.edu", EmailConfirmed = true, Unid = "u0000002", RefferedTo = "he"};
            check = await um.CreateAsync(user, "123ABC!@#def");
            if (check.Succeeded)
            {
                await um.AddToRoleAsync(user, "Applicant");
            }            
            
        }
        public async Task<Application[]> InitializeApplication(UserManager<TAUser> um)
        {
            
            if (Applicants.Any())
            {
                return null;
            }
            var users = um.Users.ToArray();
            var user0 = await um.Users.FirstOrDefaultAsync();
            var user1 = await um.Users.FirstOrDefaultAsync();
            foreach (var user in users)
            {
                if (user.UserName == "u0000000@utah.edu")
                {
                    user0 = user;
                }
                if (user.UserName == "u0000001@utah.edu")
                {
                    user1 = user;
                }
            }
            var applicants = new Application[]
            {
                //TODO: user0 need all optional field filled, user1 need none.
                new Application{ User=user0, department="CS", GPA=4, workHours=10, weekBefore=true, pursuit=Degree.PhD, semesters=4, personalStatement="I am user0", transfer="SLLC", linkedin="https//:www.helloworld.com"},
                new Application{ User=user1, department="CS", GPA=0, workHours=20, weekBefore=false, pursuit=Degree.BS, semesters=1},
            };
            return applicants;

        }

        public async Task<Course[]> InitializeCourse()
        {

            if (Course.Any())
            {
                return null;
            }

            var courses = new Course[]
            {
                //TODO: user0 need all optional field filled, user1 need none.
                new Course { semester=Semester.Spring, year=2023, title="CS Course", department="CS", courseNumber=1400, section=001,description="This is a cool course", profUNID="u1234567",profName="Jim", time="M/W 3:30-5:00", location="WEB L104", credit=3,enroll=5, note="Needs Extra TAs"},
                new Course { semester=Semester.Spring, year=2023, title="CS Course", department="CS", courseNumber=1400, section=002,description="This is a cool course", profUNID="u1234567",profName="Jim", time="M/W 3:30-5:00", location="WEB L104", credit=3,enroll=5},
                new Course { semester=Semester.Spring, year=2023, title="CS Course", department="CS", courseNumber=2400, section=001,description="This is a cool course", profUNID="u1234567",profName="Jim", time="M/W 3:30-5:00", location="WEB L104", credit=3,enroll=5},
                new Course { semester=Semester.Spring, year=2023, title="CS Course", department="CS", courseNumber=3400, section=001,description="This is a cool course", profUNID="u1234567",profName="Jim", time="M/W 3:30-5:00", location="WEB L104", credit=3,enroll=5},
                new Course { semester=Semester.Spring, year=2023, title="CS Course", department="CS", courseNumber=4400, section=001,description="This is a cool course", profUNID="u1234567",profName="Jim", time="M/W 3:30-5:00", location="WEB L104", credit=3,enroll=5, note="Needs Extra TAs"},
            };
            return courses;

        }
        public async Task<Slot[]> InitializeSchedule(UserManager<TAUser> um)
        {
            if (Slot.Any())
            {
                return null;
            }
            var users = um.Users.ToArray();
            var user0 = await um.Users.FirstOrDefaultAsync();
            foreach (var user in users)
            {
                if (user.UserName == "u0000000@utah.edu")
                {
                    user0 = user;
                }
                
            }
            var applications=Applicants.ToArray();
            var app0=await Applicants.FirstOrDefaultAsync();
            foreach(var app in applications)
            {
                if (app.User == user0)
                {
                    app0 = app;
                }
            }
            
            var slots = new Slot[240];
            int x = 0;
            for (int i = 1; i <= 5; i++)
            {
                for(int j=1; j <= 48; j++)
                {
                    
                    Slot slot = new Slot { Day=i,Time=j};
                    ApplicationSlots applicantionSlot = new ApplicationSlots { ApplicationID = app0.ID, SlotsID=slot.ID};
                    slot.Application = applicantionSlot;
                    if (i == 1 || i == 5)
                    {
                        if (j <= 16)
                        {
                            slot.IsAvailable= true;
                        }
                        else
                        {
                            slot.IsAvailable = false;
                        }
                    }else if (i == 2 || i == 4)
                    {
                        if (j >= 16 && j <= 36)
                        {
                            slot.IsAvailable = true;
                        }
                        else
                        {
                            slot.IsAvailable = false;
                        }
                    }
                    else
                    {
                        slot.IsAvailable = false;
                    }
                    slots[x]=slot;
                    x++;
                }
            }
            return slots;

        }

        public async Task<List<Enrollments>> InitializeEnrollment(string filename)
        {
            if (Enrollments.Any())
            {
                return null;
            }
            using (var reader = new StreamReader(filename))
            {
                List<Enrollments> content = new List<Enrollments>();
                var line = reader.ReadLine();
                var dates = line.Split(',');
                dates=dates.Skip(1).ToArray();
                while (!reader.EndOfStream)
                {
                    
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    string coursename = values[0];
                    //make a new enrollment for the course
                    Enrollments enrollments = new Enrollments();
                    enrollments.Course = coursename;
                    enrollments.enrollmentNavs=new List<EnrollmentNav>();
                    values=values.Skip(1).ToArray();
                    int x = 0;
                    foreach (var value in values)
                    {//add the enrollment amount to the course
                        
                            int enrolled = Int32.Parse(value);
                            EnrollmentNav enroll = new EnrollmentNav();
                            //enroll.EnrollmentsID = enrollments.ID;
                            enroll.Enrollment = enrollments;
                            enroll.date = dates[x];
                            enroll.enrolled = enrolled;
                            enrollments.enrollmentNavs.Add(enroll);
                            x++;
                    }

                    content.Add(enrollments);

                }
                reader.Close();
                return content;
            }
        }
        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAApplication.Models.Course> Course { get; set; }
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<TAApplication.Models.Slot> Slot { get; set; }


    }    
}