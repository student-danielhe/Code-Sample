```
Authors: Daniel He and Ray Parker
Date: December 2nd, 2022
Course: CS 4540, University of Utah, School of Computing
GitHub ID: iomjio, Raydowi
Repo: https://github.com/uofu-cs4540-fall2022/taapplication-danielray
Commit Tag: a13a6845
Project: PS9 - Charting
Copyright: CS 4540 and Daniel He and Ray Parker - This work may not be copied for use in Academic Coursework.
```

 # Overview of TA Application Functionality
 The TA Application is a website for managing prospective TA Applicants for the University of Utah's school of computing. From previous assignments, 
 we have implemented role authentication (Applicant, Professor, and Admin) for accessing different pages on the site, and users can use Google authentication 
 or a built-in login page to access their account. All accounts use email authentication to verify their account, except for database-seeded accounts created 
 for testing purposes. We utilized CRUD for creation of applications and courses in the system.

 In PS8, we implemented an Availabliity Tracker, which allows applicants to record and upload their availability during the workweek to the system. This 
 information can be edited and saved again by the user at any point.

 # Comments to Evaluators
 PS9:
 Our two above-and-beyond implementations are the following:
    1) Dark Mode for Highcharts
 For the dark mode implementation, we referenced the dark-unica library provided by Highcharts directly in order to 
 implement it. There were some background hexcode conflicts with the following cdn--
 https://cdnjs.cloudflare.com/ajax/libs/highcharts/10.3.2/highcharts.js
 --so we opted to remove it in order to utilize the dark mode features.
    2) Used another type of Highchart
 We used another type of Highchart and placed it on the same page as the original. The original
 Highchart we used was the basic line, and the second Highchart we used was the basic area. They 
 are both displayed on the same page, under the TA dropdown in EnrollmentTrends View.

 PS8:
 In our AdminController, we changed the params of GetEnrollmentData so that the course name and the course number were just a single string. 

 # Assignment-Specific Questions:
 No assignment-specific questions were specified.

 # Consulted Peers
N/A

 # Peers Helped
N/A

 # Acknowledgements

 - Highcharts Library, Version 10.3.2: https://cdnjs.cloudflare.com/ajax/libs/highcharts/10.3.2/highcharts.js

 - Highcharts Demo, Line-Basic: https://www.highcharts.com/demo/line-basic

 - Highcharts Dark Unica Theme: https://code.highcharts.com/themes/dark-unica.js

 - JQuery DatePicker Source Code: https://jqueryui.com/datepicker/

 -  Pixi.js Library, Version 6.5.8: https://cdnjs.com/libraries/pixi.js/6.5.8

 - jQuery 3.6.0: https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js

 # References:

 Assignment 4:

 - DataTables Installation: https://datatables.net/manual/installation

 - Writing Javascript in cshtml: https://stackoverflow.com/questions/34519342/asp-net-mvc-5-write-javascript-in-view-cshtml-file

 - Toggle Switch Reference: https://getbootstrap.com/docs/5.0/forms/checks-radios/

 - Security and Creating User Roles: https://www.c-sharpcorner.com/UploadFile/asmabegam/Asp-Net-mvc-5-security-and-creating-user-role/

 - Tables and ForEach Loops: https://stackoverflow.com/questions/18395085/foreach-loop-for-tables-in-mvc4

 - Table Borders: https://www.w3schools.com/html/html_table_borders.asp

 - Accessing UserManager: https://stackoverflow.com/questions/29292582/accessing-usermanager-outside-accountcontroller#:~:text=You%20can%20get%20that%20instance%20from%20OWIN%20pipeline,code%20that%20makes%20access%20to%20the%20ApplicationUserManager%20possible%3A?adlt=strict&toWww=1&redig=282AFF5DDFE3439DAFC03994EC6524CF

 - Policy-based Authorization: https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1

 Assignment 5:

 - Upload Files in ASP.NET Core: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0

 - Scaffold a File Uploader: https://stackoverflow.com/questions/41724188/scaffold-a-fileuploader

 - File Upload ASP.NET Core: https://code-maze.com/file-upload-aspnetcore-mvc/

 - Get File Name Without Extension: https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-6.0

 - Checking for Null in C#: https://www.thomasclaudiushuber.com/2020/03/12/c-different-ways-to-check-for-null/

 Assignment 6: 
 
 - Window Prompt(): https://www.w3schools.com/jsreF/met_win_prompt.asp

 - AJAX Intro: https://www.w3schools.com/js/js_ajax_intro.asp

 - Form Input Types: https://www.w3schools.com/html/html_form_input_types.asp
 
 - ASP.NET MVC Routing: https://stackoverflow.com/questions/1265151/asp-net-mvc-routing-can-i-have-an-action-name-with-a-slash-in-it

 Assignment 8: 

 - Professor De St. Germain Event Handling Example:  https://www.cs.utah.edu/~germain/cs4540_examples/PixiBasics/event_handling.html

 - Professor De St. Germain PS8 Starter Code Example: https://www.cs.utah.edu/~germain/cs4540_examples/PixiBasics/start_ps8.html

 - JQuery Hide/Show: https://www.w3schools.com/howto/howto_js_toggle_hide_show.asp

 - CSS Loader: https://www.w3schools.com/howto/howto_css_loader.asp

 - CSS Loader Pt II: https://www.w3schools.com/howto/tryit.asp?filename=tryhow_css_loader

 - JQuery POST Array: https://stackoverflow.com/questions/950673/jquery-post-array

 - JQuery GET/POST: https://www.w3schools.com/jquery/jquery_ajax_get_post.asp

 Assignment 9: 

  - JQuery DatePicker Source Code: https://jqueryui.com/datepicker/

  - Grabbing Date from JQuery UI Datepicker: https://stackoverflow.com/questions/4919873/how-to-get-the-date-from-jquery-ui-datepicker

  - Get Selected Value from Dropdown List JavaScript: https://stackoverflow.com/questions/1085801/get-selected-value-in-dropdown-list-using-javascript

  - Grab Current Date in JavaScript: https://stackoverflow.com/questions/1531093/how-do-i-get-the-current-date-in-javascript

  - JavaScript setDate(): https://www.w3schools.com/jsref/jsref_setdate.asp

  - DatePicker Set Default Date: https://stackoverflow.com/questions/28112677/jquery-ui-datepicker-set-default-date

  - Pass DateTime in JavaScript to C# Controller: https://stackoverflow.com/questions/5523870/pass-a-datetime-from-javascript-to-c-sharp-controller
  
  - DateTime Set Time to Midnight for Current Day: https://stackoverflow.com/questions/13467230/how-to-set-time-to-midnight-for-current-day

  - Change Theme of HighCharts: https://stackoverflow.com/questions/43690337/change-theme-of-highscharts-highstock-chart

 # Time Expenditures:
  - Assignment Six: Predicted Hours: 10 Actual Hours: 8