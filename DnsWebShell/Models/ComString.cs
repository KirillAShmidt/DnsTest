namespace DnsWebShell.Models;
public class ComString
{
	public int Id { get; set; }
	public string? Body { get; set; }
	public required DateTime Date { get; set; }
	public required string ConnectionName { get; set; }
}
