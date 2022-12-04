namespace DnsWebShell
{
	public static class Extentions
	{
		public static string RemoveTrash(this string x)
		{
			string tmp = "";
			var arr = x.Split('\n');

			for (int i = 3; i < arr.Count(); i++)
				tmp += arr[i] + "\n";

			return tmp;
		}
	}
}
