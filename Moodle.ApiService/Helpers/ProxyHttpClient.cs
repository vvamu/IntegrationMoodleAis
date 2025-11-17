using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MoodleApiService.Helpers;

internal static class ProxyHttpClient
{
	private static HttpClient _client;
	public static HttpClient Client => _client ?? SetHttpClient();

	private static HttpClient SetHttpClient()
	{
		_client = CreateHttpClient();
		return _client;
	}
	private static HttpClient CreateHttpClient()
	{
		var proxyHost = "172.16.0.101";
		var proxyPort = "3128";
		var proxyUsername = "14749349";
		var proxyPassword = "14749349";

		var proxy = new WebProxy
		{
			Address = new Uri($"http://{proxyHost}:{proxyPort}"),
			BypassProxyOnLocal = false,
			UseDefaultCredentials = false,

			Credentials = new NetworkCredential(
			userName: proxyUsername,
			password: proxyPassword)
		};


		var httpClientHandler = new HttpClientHandler
		{
			Proxy = proxy,
		};

		return new HttpClient(handler: httpClientHandler, disposeHandler: true);

	}
}
