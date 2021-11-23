namespace Lesson.FileExplorer.ViewModel
{
	public interface IFileSystemEntry
	{
		string Name { get; }
		string FilePath { get; }
	}
}