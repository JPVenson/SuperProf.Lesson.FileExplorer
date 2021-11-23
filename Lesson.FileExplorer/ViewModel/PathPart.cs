using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson.FileExplorer.ViewModel
{
	public class PathPart
	{
		private PathPart(string part, string path)
		{
			Part = part;
			Path = path;
		}

		public string Part { get; set; }
		public string Path { get; }

		public static IEnumerable<PathPart> Split(string path)
		{
			yield return new PathPart("This PC", "HOME;");
			if (string.IsNullOrWhiteSpace(path))
			{
				yield break;
			}

			var sb = new StringBuilder();
			foreach (var s in path.Split("\\", StringSplitOptions.RemoveEmptyEntries))
			{
				sb.Append(s);
				yield return new PathPart(s, sb.ToString());
				sb.Append("/");
			}
		}
	}
}