using System.Windows.Input;

namespace WorkoutTracker;

// https://johnthiriet.com/mvvm-going-async-with-async-command
internal class AsyncCommand : ICommand
{
  public event EventHandler? CanExecuteChanged;

  private bool _isExecuting;
  private readonly Func<Task> _execute;
  private readonly Func<bool>? _canExecute;

  public AsyncCommand(Func<Task> execute, Func<bool>? canExecute = null)
  {
    _execute = execute;
    _canExecute = canExecute;
  }

  public bool CanExecute()
  {
    return !_isExecuting && (_canExecute?.Invoke() ?? true);
  }

  public async Task ExecuteAsync()
  {
    if (CanExecute())
    {
      try
      {
        _isExecuting = true;
        await _execute();
      }
      finally
      {
        _isExecuting = false;
      }
    }

    RaiseCanExecuteChanged();
  }

  public void RaiseCanExecuteChanged()
  {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }

  #region Explicit implementations
  bool ICommand.CanExecute(object? parameter)
  {
    return CanExecute();
  }

  async void ICommand.Execute(object? parameter)
  {
    try
    {
      await ExecuteAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Exception thrown while waiting on task.", ex);
    }
  }
  #endregion
}
