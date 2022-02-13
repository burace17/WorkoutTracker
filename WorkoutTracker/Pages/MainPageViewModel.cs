using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WorkoutTracker
{
  public class MainPageViewModel : ViewModelBase
  {
    public MainPageViewModel()
    {
      Utilities.NonAwaitCall(LoadWorkoutTemplates());
    }

    private async Task LoadWorkoutTemplates()
    {
      var workouts = await WorkoutData.Instance.GetWorkoutTemplates();
      Workouts.AddRange(workouts);
    }

    private ObservableCollection<Workout> _workouts = new();
    public ObservableCollection<Workout> Workouts
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
}
