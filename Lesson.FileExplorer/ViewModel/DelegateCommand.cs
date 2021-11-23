using System;
using System.Windows.Input;

namespace Lesson.FileExplorer.ViewModel
{
	public class DelegateCommand : ICommand
	{
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _execute;

		public DelegateCommand(Action<object> execute,Func<object, bool> canExecute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			_canExecute = (e) => canExecute();
			_execute = (e) => execute();
		}

		public DelegateCommand(Action execute) : this(execute, () => true)
		{
		}

		public bool CanExecute(object? parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object? parameter)
		{
			_execute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}