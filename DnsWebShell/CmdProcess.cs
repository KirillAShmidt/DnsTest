using System.Diagnostics;

namespace DnsWebShell;

public class CmdProcess
{
	public string? Output => _output;
	public string? Error => _error;

	private string? _output;
	private string? _error;

	private ProcessStartInfo _processInfo = new ProcessStartInfo();

	public CmdProcess()
	{
		_processInfo.FileName = "cmd.exe";
		//_processInfo.WorkingDirectory = "C://";
		_processInfo.UseShellExecute = false;
		_processInfo.RedirectStandardInput = true;
		_processInfo.RedirectStandardOutput = true;
		_processInfo.RedirectStandardError = true;
	}

	public async Task ExecuteCommand(string command)
	{
		var process = new Process();

		process.StartInfo = _processInfo;
		process.Start();

		await process.StandardInput.WriteLineAsync(command);
		process.StandardInput.Close();

		_output = await process.StandardOutput.ReadToEndAsync();
		_error = await process.StandardError.ReadToEndAsync();

		process.WaitForExit();
		process.Close();
	}
}
