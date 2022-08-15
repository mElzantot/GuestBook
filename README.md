# GuestBook
-----------------------------------------
Project Structure  :=
--------------------
1. DAL projects 
    - Entites
    - Repos and Interfaces 
2. BAL 
   -- business logic classes and interfaces 
   -- Services used (Hashing and Token Provider)
3. Web API 
    -- Auth Controller {Register , Login}
    -- Message Controller {Write , Update , Delete}
          -- Write API can used to reply on messages also by sendind parent message id in request body
---------------------------------------------------------------------------------------------------------
In this Project I Used 
1. Dapper as a micro ORM to connect to sql server 
2. Fluent Validation to validate user inputs
3. JWT Token to authorize users

---------------------------------------------------------------------------------------------------------
Before you start 
--------------------
1. Create a new DataBase in SQL Server 
2. Update appsettings with the new connectionstring for your DB 
3. Run scripts to create (Guest & Message) Tables which can founded in DAL project in Scripts folder
-----------------------------------------------------------------------------------------------------
