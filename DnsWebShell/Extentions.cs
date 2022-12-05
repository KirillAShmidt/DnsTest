namespace DnsWebShell;

public static class Extentions
{
	/// <summary>
	/// Removes first three lines of string
	/// </summary>
	/// <param name="x"></param>
	/// <returns></returns>

	public static string RemoveTrash(this string x)
	{
		string tmp = "";
		var arr = x.Split('\n');

		if(arr.Length > 3)
			for (int i = 3; i < arr.Count(); i++)
				tmp += arr[i] + "\n";

		return tmp;
	}
}
