using System;

namespace JP.Utils
{
	/// <summary>Extension methods to manipulate the output of <see cref="ICsvParser.Parse(string)"/>.</summary>
	public static class CsvParse
	{
		public static int RowCount(this string[][] table) => table.Length - 1;

		public static string[] Headers(this string[][] table) => table[0];

		/// <param name="row">1-based</param>
		public static string[] Row(this string[][] table, int row) => table[row];
		
		/// <exception cref="IndexOutOfRangeException" />
		public static int IndexOf(this string[] words, string word)
		{
			var pos = Array.IndexOf(words, word);

			if(pos < 0 || pos >= words.Length)
				throw new IndexOutOfRangeException(
					$"Header {word} not found in CSV.");

			return pos;
		}
		
		/// <summary>Trims the table to only the specified columns.
		/// Removes headers row from result.</summary>
		/// <exception cref="IndexOutOfRangeException" />
		public static string[][] Filter(this string[][] table, params string[] filterHeaders)
		{
			var ncols = filterHeaders.Length;
			var headerIndices = new int[ncols];
			var allHeaders = table.Headers();
			for(int c = 0; c < ncols; c++)
				headerIndices[c] = allHeaders.IndexOf(filterHeaders[c]);

			var ans = new string[table.Length - 1][];
			for(int r = 1; r < table.Length; r++)
			{
				var row = ans[r-1] = new string[ncols];
				for(int c = 0; c < ncols; c++)
					row[c] = table[r][headerIndices[c]];
			}
			return ans;
		}
	}
}
