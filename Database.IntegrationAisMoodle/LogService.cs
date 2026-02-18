using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IntegrateAIS.LogsService;

internal class LogService
{
	public async Task ChangeOldLogFile(Object obj, LogType? logType)
	{
		string workingDirectory = Environment.CurrentDirectory;
		var path = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
		var fileName = logType == null ? "" : logType.LogLevel + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + ".json";
		string destPath = Path.Combine(path, "logs", fileName);

		var fileContent = JsonSerializer.Serialize<Object>(obj);
 
		if(!string.IsNullOrEmpty(logType?.Message))
			File.AppendAllText(destPath, logType.Message);

		File.AppendAllText(destPath, fileContent);
	}

}
