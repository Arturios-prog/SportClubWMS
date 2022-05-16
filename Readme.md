# Welcome to a Sport Club Warehouse Management System!
The aim of this project is to demonstrate my skills as a beginner developer.    
This project may not contain every aspect of my knowledge, but I hope that everything that is present here will satisfy a potential Employer enough.    

## Functionality
This project modulates a situation where a sport club requires a system for managing clients and sport goods. This system is made for employees only.    
There are two types of clients in this sport club: with a general subscribe and subscribe plus.    
Clients with general subscribe can only have 1 good of each category and clients with a subscribe plus can have as many goods as are present in the warehouse.    

The responsibility of an employee is to assign certain goods to clients and remove them when clients are giving these goods back.    
![Image alt](clients.jpg)

An employee can also add/edit clients and sport goods if needed.    
![Image alt](addCustomer.jpg)

All the entities are stored in the SQL database which is managed by EntityFramework Core.
Entities are connected with a many-to-many connection.

The interaction between front-end and back-end is happening via RESTful API which can also be inspected with Swagger UI.    
![Image alt](swagger.jpg)

Front-end is made using RadzenBlazor framework and client application is a webassembly project.    
The target platform is .NET 6.0    

## Startup
Since this project is not published, it is required to localy configure it's startup.    
It is most likely that you will have to provide an sql connection string in your appsettings.json file.   
You will also need to start both projects at the same time for client application to work.

## Problems and flaws
- I haven't figured out a way to reload the DataGrid on Dialog's window closing.
- Authentication and authorization are not yet added.
- The subscribe functionality is not yet fully implemented.
- This project is not yet published on Web and it can only start in a debug mode localy.
- Testing library is not yet implemented.
- application UI may display incorrectly in some parts

