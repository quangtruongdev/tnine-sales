# tnine.Web

Clone project: git clone https://github.com/quangtruongdev/tnine-sales.git

Open project
- Click Build -> Clean Solution

Rename connect string db:
- Open tnine.Core.Shared
- Open App.config -> Config connectionStrings

Enable-Migrations
Enable-Migrations -ContextTypeName tnine.Core.Shared.DatabaseContext -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host -Force
Update-Database -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host
