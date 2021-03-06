/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace WorkoutTracker;

// https://johnthiriet.com/mvvm-going-async-with-async-command
public class AsyncCommand : ICommand
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

  void ICommand.Execute(object? parameter)
  {
    Utilities.NonAwaitCall(ExecuteAsync());
  }
  #endregion
}
