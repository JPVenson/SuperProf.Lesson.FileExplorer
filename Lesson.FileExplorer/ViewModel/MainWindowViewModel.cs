using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson.FileExplorer.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			OpenDirectoryCommand = new DelegateCommand(OpenDirectoryExecute, CanOpenDirectoryExecute);
			GoToPathCommand = new DelegateCommand(GoToPathExecute, CanGoToPathExecute);

			FileSystemAccessService = new FileSystemAccessService();
			PathParts = new ObservableCollection<PathPart>();
			SetupHome();
		}

		public FileSystemAccessService FileSystemAccessService { get; set; }

		public DelegateCommand OpenDirectoryCommand { get; private set; }
		public ObservableCollection<PathPart> PathParts { get; set; }

		private DirectoryViewModel _fileSystemEntry;
		private DirectoryViewModel _selectedFileSystemEntry;

		public DirectoryViewModel SelectedFileSystemEntry
		{
			get { return _selectedFileSystemEntry; }
			set
			{
				_selectedFileSystemEntry = value;
				SendPropertyChanged();
			}
		}

		public DirectoryViewModel FileSystemEntry
		{
			get { return _fileSystemEntry; }
			set
			{
				_fileSystemEntry = value;
				SendPropertyChanged();
			}
		}

		public void SetupHome()
		{
			FileSystemEntry = new DirectoryViewModel("This PC", "");
			foreach (var driveViewModel in FileSystemAccessService.GetDrives())
			{
				FileSystemEntry.Entries.Add(driveViewModel);
			}

			RefreshPathParts();
		}

		public DelegateCommand GoToPathCommand { get; private set; }

		private void GoToPathExecute(object sender)
		{
			var path = sender as string;
			if (path.EndsWith(":"))
			{
				path += "\\";
			}

			if (path == "HOME;")
			{
				SetupHome();
				return;
			}

			OpenDirectoryExecute(new DirectoryViewModel(new DirectoryInfo(path)));
		}

		private bool CanGoToPathExecute(object sender)
		{
			return !string.IsNullOrWhiteSpace(sender as string);
		}

		private void OpenDirectoryExecute(object sender)
		{
			var newDirectory = sender as DirectoryViewModel;
			FileSystemEntry = newDirectory;
			newDirectory.Entries.Clear();

			foreach (var directory in FileSystemAccessService.GetDirectories(newDirectory.FilePath))
			{
				newDirectory.Entries.Add(directory);
			}

			foreach (var file in FileSystemAccessService.GetFiles(newDirectory.FilePath))
			{
				newDirectory.Entries.Add(file);
			}

			RefreshPathParts();
		}

		private bool CanOpenDirectoryExecute(object sender)
		{
			return sender is DirectoryViewModel;
		}

		public void RefreshPathParts()
		{
			PathParts.Clear();
			foreach (var part in ViewModel.PathPart.Split(FileSystemEntry.FilePath))
			{
				PathParts.Add(part);
			}
		}
	}
}
