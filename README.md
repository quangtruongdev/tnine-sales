# tnine.Web

Clone project: git clone https://github.com/quangtruongdev/tnine-sales.git

Cài nodejs v20.x

Chạy lệnh cmd: 
- npm install -g bower

Mở tnine.Web.Host -> Run CMD -> bower install

Open project tnine.Web.sln
- Click Build -> Clean Solution

CONFIG connect string db:
- Open tnine.Core.Shared -> App.config
- Open App.config -> Config connectionStrings

Enable-Migrations
- Enable-Migrations -ContextTypeName tnine.Core.Shared.DatabaseContext -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host -Force
- Update-Database -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host
