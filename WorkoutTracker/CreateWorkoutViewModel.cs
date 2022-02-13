using System.Windows.Input;

namespace WorkoutTracker;

public class CreateWorkoutViewModel : ViewModelBase
{
  public CreateWorkoutViewModel()
  {
    var date = DateTime.Now;
    var workout = new Models.Workout(null, $"Untitled ({date:g}", date, new List<Models.Exercise>());
    Workout = new(workout, true);
  }

  public WorkoutViewModel Workout { get; set; }

  private ICommand? _closeCmd;
  public ICommand CloseCmd => _closeCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync(".."));

  private async Task SaveWorkout()
  {
    await Shell.Current.GoToAsync("..");
    await WorkoutData.Instance.InsertWorkoutTemplate(Workout.GetModel()); 
  }

  private ICommand? _saveCmd;
  public ICommand SaveCmd => _saveCmd ??= new AsyncCommand(SaveWorkout);
}
