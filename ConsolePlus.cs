using System;
using System.IO;
using System.Runtime.InteropServices;

namespace JP.Utils
{
	/// <summary>Extends console functionality.
	/// Not thread safe.</summary>
	public static class ConsolePlus
	{
		/// <summary>Gets or sets the existence of the console for the calling process.</summary>
		/// <exception cref="IOException"></exception>
		public static bool Enabled
		{
			get => GetConsoleWindow() != IntPtr.Zero;
			set
			{
				bool initially = Enabled;
				bool success = true;
				if(value)
				{
					if(!initially)
						success = AllocConsole();
				}
				else if(initially)
					success = FreeConsole();

				if(!success) throw new IOException(
					$"Error while setting ConsolePlus.Enabled from {initially} to {value}.");
			}
		}
		
		[DllImport("Kernel32.dll")]
		static extern IntPtr GetConsoleWindow();
		[DllImport("Kernel32.dll")]
		static extern bool FreeConsole();
		[DllImport("Kernel32.dll")]
		static extern bool AllocConsole();
	}
}
