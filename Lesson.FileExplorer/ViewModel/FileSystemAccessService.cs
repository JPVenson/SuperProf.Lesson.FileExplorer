using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.FileExplorer.ViewModel
{
	public class FileSystemAccessService
	{
		public IEnumerable<DriveViewModel> GetDrives()
		{
			return Directory.GetLogicalDrives().Select(e => new DriveInfo(e)).Where(e => e.IsReady).Select(e => new DriveViewModel(e));
		}

		public IEnumerable<DirectoryViewModel> GetDirectories(string parent)
		{
			return Directory.EnumerateDirectories(parent).Select(e => new DirectoryInfo(e)).Select(e => new DirectoryViewModel(e));
		}

		public IEnumerable<FileSystemViewModel> GetFiles(string directory)
		{
			return Directory.EnumerateFiles(directory).Select(e => new FileInfo(e)).Select(e => new FileSystemViewModel(e));
		}
	}
}