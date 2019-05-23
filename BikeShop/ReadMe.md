# Overview
This is a mastery project from the Software Guild that had many “stretch goals.” That is, the project description contained more features than apprentices at the Software Guild could complete before the end of the course. It’s also important to mention that is assignment was not a tutorial. I had build it from scratch using similar but different lessons at the Software Guild.

More specifically, we were assigned to make a website for a car dealership. Since I enjoy cycling, I changed the project to a bike dealership. There were many general and specific goals for this website. For the sake of brevity, this summary will focus on general goals.

The general features are listed below.
- Allow customers to see featured bikes, search for new bikes, and used bikes. Customers should also be able to request a purchase by easily sending a message to staff.
- Allow staff to record bikes as sold, add new bikes, edit existing bikes, and make bikes available as “featured bikes” on the front page of the website.
- Staff can also  change common characteristics that can be recorded, such as colors available, frame types, makes, and models.
- 
The primary general requirements are listed below.
- Using SQL, create scripts that build the database, create tables, and form relationships between tables
- Using C#, create unit tests for stored procedures, ADO controls to process data, and webpages/views to present data to users
- Using WebApi, AJAX, and jQuery allow users to search the database

The rest of this document shows what’s complete and the plan for completing other parts of the project.

Currently complete:

SQL
- Scripts to create the SQL database, populate sample data, and create 22 stored procedures

C#
- 22 unit tests to confirm the stored procedures are returning accurate data
- Controllers for showing featured bikes, new bikes, and used bikes
- Controllers to search new and used bikes using JSON, AJAX, and WebAPI
- Web pages for users to easily contact staff about a bike they’re like to purchase

Plan for completing remaining items:

**By June 6**: 
Finish a ‘control panel’ for staff that allows them to access the web pages described above, which change database tables about bike characteristics.

**By June 13**: 
Add an admin role for some staff. It will allow admin users to add staff to the website.

**By May 20**: 
Finish all verification to assure users type in correct data and test the website to find bugs.

**By June 27**: 
Fix all bugs and other small problems.
