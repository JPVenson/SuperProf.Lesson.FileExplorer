using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.FileExplorer.ViewModel
{
	public class DirectoryViewModel : ViewModelBase, IFileSystemEntry
	{
		public DirectoryViewModel(DirectoryInfo directoryInfo) : this(directoryInfo.Name, directoryInfo.FullName)
		{
			DirectoryInfo = directoryInfo;
		}
		
		public DirectoryViewModel(string name, string filePath) : this()
		{
			Name = name;
			FilePath = filePath;
		}

		public DirectoryViewModel()
		{
			Entries = new ObservableCollection<IFileSystemEntry>();
		}

		public ObservableCollection<IFileSystemEntry> Entries { get; set; }
		public string Name { get; }
		public string FilePath { get; }

		public DirectoryInfo DirectoryInfo { get; set; }
	}
}
