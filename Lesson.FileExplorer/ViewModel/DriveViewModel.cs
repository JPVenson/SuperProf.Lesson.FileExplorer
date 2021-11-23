using System.IO;

namespace Lesson.FileExplorer.ViewModel
{
	public class DriveViewModel : DirectoryViewModel
	{
		public DriveViewModel(DriveInfo driveInfo) : base(driveInfo.VolumeLabel, driveInfo.Name)
		{
			DriveInfo = driveInfo;
		}

		public DriveInfo DriveInfo { get; set; }
	}
}