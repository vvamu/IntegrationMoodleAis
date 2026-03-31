namespace Database.MoodleService.Helpers;

using MySqlConnector;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
internal class Configuration : IDisposable
{
	private SshClient? _sshClient;
	protected MySqlConnection? _mySqlConnection;


	protected SshClient? SetSshConnection()
	{
		string host = "127.0.0.1";
		int port = 3306;

		string SshHostName = "172.16.0.24";
		int SshPort = 2504;
		string SshUserName = "distadmin";
		string SshPassword = "75distMain1";

		var proxyHost = "172.16.0.101";
		var proxyPort = 3128;
		var proxyUsername = "14749349";
		var proxyPassword = "14749349";

		var auth = new PasswordAuthenticationMethod(SshUserName, SshPassword);
		ConnectionInfo connectionInfo = new ConnectionInfo(SshHostName, SshPort, SshUserName,
			ProxyTypes.Http, proxyHost, proxyPort, proxyUsername, proxyPassword, auth);

		try
		{
			_sshClient = new SshClient(connectionInfo);
			_sshClient.Connect();

			if (!_sshClient.IsConnected)
				throw new Exception("Connection ssh not configured");

			var portForwarded = new ForwardedPortLocal(host, (uint)port, host, (uint)port);
			_sshClient.AddForwardedPort(portForwarded);
			portForwarded.Start();

			Console.WriteLine("SSH connection established and port forwarding started");
			return _sshClient;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"SSH Connection failed: {ex}");
			_sshClient?.Dispose();
			return null;
		}
	}

	protected MySqlConnection? RunDatabaseConnection()
	{
		string host = "127.0.0.1";
		int port = 3306;

		string database = "moodle";
		string user = "root";
		string password = "75distMain";

		string connectionString = $"Server={host};Database={database};Uid={user};Password={password};";

		_mySqlConnection = new MySqlConnection(connectionString); //new OleDbConnection(connectionString))
		_mySqlConnection.Open();
		return _mySqlConnection;

	}

	public void Dispose()
	{
		Console.WriteLine("Disposing Configuration and SSH connection");

		_mySqlConnection?.Close();
		_mySqlConnection?.Dispose();
		_mySqlConnection = null;

		_sshClient?.Disconnect();
		_sshClient?.Dispose();
		_sshClient = null;
	}
}

