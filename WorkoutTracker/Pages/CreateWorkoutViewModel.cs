using System.Windows.Input;

namespace WorkoutTracker;

public class CreateWorkoutViewModel : ViewModelBase
{
  private WorkoutDataService WorkoutDataService { get; }
  public CreateWorkoutViewModel(WorkoutDataService workoutDataService)
  {
    WorkoutDataService = workoutDataService;
    var date = DateTime.Now;
    var workout = new Workout(null, "Untitled Routine", date, new List<Exercise>());
    Workout = new(workout, true);
  }

  public WorkoutViewModel Workout { get; set; }

  private ICommand? _closeCmd;
  public ICommand CloseCmd => _closeCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync(".."));

  private async Task SaveWorkout()
  {
    await WorkoutDataService.InsertWorkoutTemplate(Workout.GetModel()); 
    await Shell.Current.GoToAsync("..");
  }

  private ICommand? _saveCmd;
  public ICommand SaveCmd => _saveCmd ??= new AsyncCommand(SaveWorkout);
}
