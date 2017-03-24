using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JiraTasks.Data
{
	public class LoadedTaskLists
	{
		public Dictionary<string, List<FlattenedTask>> JiraIssueTaskLists { get; set; }
		private string Path { get; set; }
		private string Filename { get; set; }

		public LoadedTaskLists(string path, string filename)
		{
			Path = path;
			Filename = filename;
			JiraIssueTaskLists = new Dictionary<string, List<FlattenedTask>>();
		}

		public bool Load()
		{
			if (!Directory.Exists(Path))
			{
				Directory.CreateDirectory(Path);
				File.Create($@"{Path}/{Filename}").Dispose();
				return false;
			}
			try
			{
				var fileText = File.ReadAllText($@"{Path}/{Filename}");
				var deserialized = JsonConvert.DeserializeObject<LoadedTaskLists>(fileText);
				JiraIssueTaskLists = deserialized.JiraIssueTaskLists;
				return true;
			}
			catch (JsonSerializationException ef)
			{
				MessageBox.Show(ef.Message);
				JiraIssueTaskLists = new Dictionary<string, List<FlattenedTask>>();
				Save();
				return false;
			}
		}

		public void Save()
		{
			if (!Directory.Exists(Path))
			{
				Directory.CreateDirectory(Path);
			}
			string serialized = null;
			try
			{
				serialized = JsonConvert.SerializeObject(this);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			File.WriteAllText($@"{Path}/{Filename}", serialized);
		}

		public void SaveBackup()
		{
			if (!Directory.Exists(Path))
			{
				Directory.CreateDirectory(Path);
			}
			string serialized = null;
			try
			{
				serialized = JsonConvert.SerializeObject(this);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			File.WriteAllText($@"{Path}/BUUPSF{DateTime.Now:YYMMDD}.settings", serialized);
		}

		public void ReplaceTasks(string name, List<FlattenedTask> tasks)
		{
			JiraIssueTaskLists[name] = tasks;
			Save();
		}
	}
}