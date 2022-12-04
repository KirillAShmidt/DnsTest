using Microsoft.EntityFrameworkCore;

namespace DnsWebShell.Models;

public class ApplicationContext : DbContext
{
	public DbSet<ComString> ComStrings { get; set; } = null!;

	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{
		Database.EnsureCreated();
	}
}
