using System;
using System.Collections.Generic;
using System.Text;

namespace MoodleApiService.Helpers;

internal class BaseMoodleService
{
	protected const string baseRequest = "https://dist.belstu.by/webservice/rest/server.php?" + "wstoken=" + "db96611aa177b0bf9e7da69e967b98f7";
	protected static HttpClient _httpClient => ProxyHttpClient.Client;

}
