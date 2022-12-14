using DnsWebShell;
using DnsWebShell.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cmd = new CmdProcess();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddSingleton(cmd);

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "main",
	pattern: "{action}/{id?}",
	defaults: new { controller = "Main", action = "Index" }
	);

app.Run();
