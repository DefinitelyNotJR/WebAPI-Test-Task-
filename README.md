

# About

So the idea is to use two separated programs:
1) **Parser**  —  .NET Core console app that parses web pages and adds the data to existing database (SQLIte is used in this project).
2) **WebAPI** — ASP.NET Core Web API project, that connects to the existing database and returns data. 

# How to use

1) Add database connection string to ```API\appsettings.json``` and to ```Parser\Services\DataService\DbDataService.cs```.
2) Create a SQLite database (if you don't want to be using the existing one) using commands from ```Init.sql```.
3) Run **Parser** project to fill database with values.
4) Run **Web API** project.

 
