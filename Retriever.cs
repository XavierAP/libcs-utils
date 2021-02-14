using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JP.Utils
{
	/// <summary>Gets data via HTTP and parses it.</summary>
	public class Retriever<T>
	{
		/// <exception cref="Exception" />
		public async Task<T> Load(string url, Func<string, T> parser)
		{
			var response = await new HttpClient().GetAsync(url);
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return parser(data);
		}
	}
}
