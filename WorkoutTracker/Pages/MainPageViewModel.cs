using System.Windows.Input;

namespace WorkoutTracker;

public class MainPageViewModel : ViewModelBase
{
  private WorkoutDataService WorkoutDataService { get; }
  public MainPageViewModel(WorkoutDataService workoutDataService)
  {
    WorkoutDataService = workoutDataService;
  }

  public override async Task InitializeAsync()
  {
    var workouts = await WorkoutDataService.GetWorkoutTemplates();
    WorkoutTemplates = workouts.ToList();
  }

  private List<Workout> _workouts = new();
  public List<Workout> WorkoutTemplates
  {
    get => _workouts;
    set => SetProperty(ref _workouts, value);
  }

  private Workout? _selectedWorkout;
  public Workout? SelectedWorkout
  {
    get => _selectedWorkout;
    set => SetProperty(ref _selectedWorkout, value);
  }

  private async Task StartWorkout()
  {
    var template = SelectedWorkout;
    if (template != null)
    {

      SelectedWorkout = null;
    }
  }

  private ICommand? _startWorkoutCmd;
  public ICommand StartWorkoutCmd => _startWorkoutCmd ??= new AsyncCommand(StartWorkout);
  
  private ICommand? _createTemplateCmd;
  public ICommand CreateTemplateCmd => _createTemplateCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync("createWorkout"));
}
