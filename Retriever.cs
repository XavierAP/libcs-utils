using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JP.Utils
{
	public class Retriever
	{
		/// <summary>Gets data via HTTP.</summary>
		/// <exception cref="Exception" />
		public async Task<string> Load(string url)
		{
			var response = await new HttpClient().GetAsync(url);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
	}
}
