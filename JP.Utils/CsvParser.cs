﻿using System;

namespace JP.Utils
{
	public interface ICsvParser
	{
		string[][] Parse(string text);
	}

	/// <summary>Eager processor for CSV with few lines.</summary>
	public class CsvParser : ICsvParser
	{
		/// <param name="rowSeparator">Extra characters e.g. \r don't hurt even if not present.</param>
		public CsvParser(string columnSeparator = ",", string rowSeparator = "\r\n")
		{
			this.RowSeparator = rowSeparator.ToCharArray();
			this.ColSeparator = columnSeparator.ToCharArray();
		}

		readonly char[]
			RowSeparator,
			ColSeparator;

		/// <summary>Splits CSV into values by rows and columns.</summary>
		public string[][] Parse(string text)
		{
			var rows = text.Split(RowSeparator, StringSplitOptions.RemoveEmptyEntries);
			var table = new string[rows.Length][];

			for(int r = 0; r < rows.Length; r++)
				table[r] = rows[r].Split(ColSeparator, StringSplitOptions.None);

			return table;
		}
	}
}
