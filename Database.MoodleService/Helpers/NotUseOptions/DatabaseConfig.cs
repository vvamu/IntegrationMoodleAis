using System;
using System.Collections.Generic;
using System.Text;

namespace AisService.Helpers;

public partial class DbConnector
{
	public class DatabaseConfig
	{
		public string Hostname { get; set; }

		public string Database { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

}
