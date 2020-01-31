# Messenger
.Net Core 3.1 Messenger without using SignalR.

1. Insert db_for_MSSQL.dtsx to Microsoft SQL Server Management Studio 18.
2. Open appsettings.json in HireMe folder and change "DefaultConnection", "SiteUrl" and "ProjectRootDir"
3. Build project
4. There are 2 registered users with name and picture (requiered from search system) 


------------------------------------------------------------------

If you want to migrate or update use these commands:

1. Add-Migration init -Context BaseDbContext
2. Add-Migration init -Context FeaturesDbContext

3. Update-Database -Context BaseDbContext
4. Update-Database -Context FeaturesDbContext 


Messenger System include:

- Person to person messages sending
- Autocomplate receiver profile by name in SendMessage page
- Notification showing with numbers of unread messages in navbar
- Add to Important / Starred folder by clicking icon buttons over message in inbox folder
- If only one side deleted message the msg will stand at the db but will hide for remover. (Message will remove if two sides remove it from inbox)
 
