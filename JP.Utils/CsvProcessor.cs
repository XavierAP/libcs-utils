using System;
using System.Collections.Generic;
using System.IO;

namespace JP.InvestCalc
{
	public interface ICsvProcessor
	{
		IEnumerable<string[]> Analize(string text);

		void Synthesize<TArray, TCommon>(
			TextWriter destination,
			IEnumerable<TArray> valuesByRowsCols
			) where TArray : IReadOnlyList<TCommon>;
		void Synthesize<TArray>(
			TextWriter destination,
			IEnumerable<TArray> valuesByRowsCols
			) where TArray : IReadOnlyList<object>;
	}

	/// <summary>Lazy processor for CSV with many lines.</summary>
	public class CsvProcessor : ICsvProcessor
	{
		readonly string separator;

		public CsvProcessor(string separator)
		{
			this.separator = separator;
		}

		public IEnumerable<string[]>
		Analize(string text)
		{
			using var reader = new StringReader(text);
			string line;
			while(null != (line = reader.ReadLine()))
			{
				if(string.IsNullOrWhiteSpace(line)) continue;
				yield return line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		public void Synthesize<TArray, TCommon>(
			TextWriter destination,
			IEnumerable<TArray> valuesByRowsCols
			) where TArray : IReadOnlyList<TCommon>
		{
			foreach(var row in valuesByRowsCols)
			{
				var lastCol = row.Count - 1;
				for(int c = 0; c < lastCol; c++)
				{
					destination.Write(row[c]);
					destination.Write(separator);
				}
				destination.Write(row[lastCol]);
				destination.WriteLine();
			}
		}
		public void Synthesize<TArray>(TextWriter destination, IEnumerable<TArray> valuesByRowsCols)
			where TArray : IReadOnlyList<object>
			=> Synthesize<TArray, object>(destination, valuesByRowsCols);
	}
}
