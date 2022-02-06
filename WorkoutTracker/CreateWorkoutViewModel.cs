using System.Windows.Input;

namespace WorkoutTracker;

internal class CreateWorkoutViewModel : ViewModelBase
{
  private ICommand? _closeCmd;
  public ICommand CloseCmd => _closeCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync(".."));
}
