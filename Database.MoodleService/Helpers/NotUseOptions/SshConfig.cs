using System;
using System.Collections.Generic;
using System.Text;

namespace AisService.Helpers;

public class SshConfig
{
	public string Hostname { get; set; }
	public int Port { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
}

