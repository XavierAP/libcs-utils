using System;
using System.IO;

namespace JP.Utils
{
	public static class Files
	{
		private const string backupExtension = ".backup";

		public static void DeleteWithBackup(string filePath)
		{
			string backupPath = filePath + backupExtension;
			File.Delete(backupPath);
			File.Move(filePath, backupPath);
		}

		/// <exception cref="FileNotFoundException" />
		public static void RestoreBackup(string filePath)
		{
			string backupPath = filePath + backupExtension;
			File.Move(backupPath, filePath);
		}

		public static SystemException
		TryCopy(string originPath, string destinationPath)
		{
			bool replacing = File.Exists(destinationPath);
			if(replacing)
				DeleteWithBackup(destinationPath);

			try {
				File.Copy(originPath, destinationPath);
			}
			catch(SystemException err)
			{
				File.Delete(destinationPath); // in case it was created but not completed
				RestoreBackup(destinationPath);
				return err;
			}
			return null;
		}
	}
}
