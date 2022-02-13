using System.Windows.Input;

namespace WorkoutTracker;

public class CreateWorkoutViewModel : ViewModelBase
{
  public CreateWorkoutViewModel()
  {
    Workout = new(Models.Workout.Create(), true);
  }

  public WorkoutViewModel Workout { get; set; }

  private ICommand? _closeCmd;
  public ICommand CloseCmd => _closeCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync(".."));
}
