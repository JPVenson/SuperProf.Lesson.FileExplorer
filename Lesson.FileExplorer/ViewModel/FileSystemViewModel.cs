using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lesson.FileExplorer.ViewModel
{
	public class FileSystemViewModel : ViewModelBase, IFileSystemEntry
	{
		public FileSystemViewModel(FileInfo fileInfo)
		{
			FileInfo = fileInfo;
			Name = fileInfo.Name;
			FilePath = fileInfo.FullName;
			FileIcon = GetIcon(FilePath, false);
		}

		public string FilePath { get; }
		public string Name { get; }
		public ImageSource FileIcon { get; set; }

		public FileInfo FileInfo { get; set; }

		public static ImageSource GetIcon(string strPath, bool bSmall)
		{
			Interop.SHFILEINFO info = new Interop.SHFILEINFO(true);
			int cbFileInfo = Marshal.SizeOf(info);
			Interop.SHGFI flags;
			if (bSmall)
			{
				flags = Interop.SHGFI.Icon | Interop.SHGFI.SmallIcon | Interop.SHGFI.UseFileAttributes;
			}
			else
			{
				flags = Interop.SHGFI.Icon | Interop.SHGFI.LargeIcon | Interop.SHGFI.UseFileAttributes;
			}

			Interop.SHGetFileInfo(strPath, 256, out info, (uint)cbFileInfo, flags);

			IntPtr iconHandle = info.hIcon;
			//if (IntPtr.Zero == iconHandle) // not needed, always return icon (blank)
			//  return DefaultImgSrc;
			ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
				iconHandle,
				Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
			Interop.DestroyIcon(iconHandle);
			return img;
		}
	}
}