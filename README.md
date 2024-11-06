# tnine.Web

Clone project: git clone https://github.com/quangtruongdev/tnine-sales.git

Cài đặt nodejs v20.x

Chạy lệnh cmd: 
- npm install -g bower

Mở tnine.Web.Host -> Run CMD -> bower install

Open project tnine.Web.sln
- Click Build -> Clean Solution

Click Chuột phải vào tnine.Web.Host -> Set as Startup Project

CONFIG connect DB:
- Open tnine.Core.Shared -> App.config
- Open tnine.Web.Host -> ConnectionStrings.config

Open Package Manager Console
- Enable-Migrations -ContextTypeName tnine.Core.Shared.DatabaseContext -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host -Force
- Update-Database -ProjectName tnine.Core.Shared -StartupProjectName tnine.Web.Host

# Hướng dẫn viết backend
1. Open tnine.Core -> Tạo class <Tên bảng>.cs
2. Open tnine.Core.Shared -> DatabaseContext.cs -> Thêm DbSet<Tên bảng> <Tên bảng>s 
2. Open tnine.Core.Shared -> Repositories -> Tạo class <Tên bảng>Repository.cs
3. Open tnine.Application.Shared -> Tạo folder I<Tên bảng>Service
- Trong folder I<Tên bảng>Service tạo interface I<Tên bảng>Service.cs, folder Dto
- Trong folder Dto tạo class Get<Tên bảng>ForViewDto.cs, CreateOrEdit<Tên bảng>Dto.cs, Get<Tên bảng>ForEditOutputDto.cs, Get<Tên bảng>InputDto.cs
4. Open tnine.Application -> Tạo class <Tên bảng>Service.cs
5. Open tnine.Web.Host -> App_Start -> AutoMapperCongfig.cs -> Thêm CreateMap<Tên bảng, CreateOrEdit<Tên bảng>Dto> trong AutoMapperProfile
6. Open tnine.Web.Host -> Api -> Tạo Web Api Controller Class -> <Tên bảng>ApiController.cs

# Hướng dẫn viết frontend
1. Đăng kí tài khoản với account/register
- body: {Email: , Password: }
2. Lấy token với oauth/token
3. Tạo role 'admin' với api/role
- body: {Name: , Description: , PermissionIds: []}
4. Cập nhật role cho tài khoản api/user
- body: {UserId: , RoleId: []}

5. Cập nhật service trong app -> services -> service-proxies.js
6. Tạo các thư mục trong admin tương tự pages -> todo
7. Cập nhật route trong app.route.js
8. Cập nhật sidebar trong app -> shared -> layout -> sidebar -> sidebar.component.js