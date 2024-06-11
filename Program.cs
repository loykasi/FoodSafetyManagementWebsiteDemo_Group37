using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAnToanVeSinhThucPhamDemo.Data;
using WebAnToanVeSinhThucPhamDemo.ExtendMethods;
using WebAnToanVeSinhThucPhamDemo.Models;

using WebAnToanVeSinhThucPhamDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ appsettings.json
var gconfig = builder.Configuration.GetSection("Authentication:Google");
var fconfig = builder.Configuration.GetSection("Authentication:Facebook");

// Thêm dịch vụ xác thực
builder.Services.AddAuthentication()
	.AddGoogle(options =>
	{
		options.ClientId = gconfig["ClientId"];
		options.ClientSecret = gconfig["ClientSecret"];
		options.CallbackPath = "/dang-nhap-tu-google"; // Đường dẫn callback
	})
	.AddFacebook(options =>
	{
		options.AppId = fconfig["AppId"];
		options.AppSecret = fconfig["AppSecret"];
		options.CallbackPath = "/dang-nhap-tu-facebook"; // Đường dẫn callback
	});

string contentRootPath = builder.Environment.ContentRootPath;

builder.Services.AddSingleton(sp => new { ContentRootPath = contentRootPath });


var connectionString = builder.Configuration.GetConnectionString("conString");
builder.Services.AddDbContext<QlattpContext>(x => x.UseSqlServer(connectionString));

//Đọc cấu hình từ appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Thêm dịch vụ Options
builder.Services.AddOptions();

// Đăng ký MailSettings từ cấu hình
var mailSettingsSection = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettingsSection);
// Đăng ký dịch vụ gửi email
builder.Services.AddSingleton<IEmailSender, SendMailService>();

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("ViewManageMenu", builder =>
	{
		builder.RequireAuthenticatedUser();
		builder.RequireRole(RoleName.Administrator);
	});
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("MemberManagerMenu", builder =>
	{
		builder.RequireAuthenticatedUser();
		builder.RequireRole(RoleName.Member);
	});
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<QlattpContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Thiết lập về Password
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 1;

	// Cấu hình Lockout - khóa user
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.AllowedForNewUsers = true;

	// Cấu hình về User
	options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;

	// Cấu hình đăng nhập
	options.SignIn.RequireConfirmedEmail = true;
	options.SignIn.RequireConfirmedPhoneNumber = false;
	options.SignIn.RequireConfirmedAccount = true;
});

// Cấu hình ApplicationCookie
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/login/";
	options.LogoutPath = "/logout/";
	options.AccessDeniedPath = "/khongduoctruycap.html";
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
	// /View/Controller/Action.cshtml
	// /MyView/Controller/Action.cshtml
	// {0} -> ten Action
	// {1} -> ten Controller
	// {2} -> ten Area
	options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
	options.AreaViewLocationFormats.Add("/MyAreas/{2}/Views/{1}/{0}.cshtml");
});

builder.Services.AddDbContext<QlattpContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("conString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
	RequestPath = "/contents",
	FileProvider = new PhysicalFileProvider(
		Path.Combine(Directory.GetCurrentDirectory(), "Uploads"))
});


app.AppStatusCodePage(); //tuy bien response khi co  loi: 400 - 599

app.UseRouting();

app.UseAuthentication(); //xac thuc danh tinh
app.UseAuthorization(); //xac thuc quyen truy cap

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/sayhi", async context =>
{
	await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
});
app.MapRazorPages();
app.Run();
