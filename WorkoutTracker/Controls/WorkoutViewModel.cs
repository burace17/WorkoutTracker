using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WorkoutTracker;

public class WorkoutViewModel : ViewModelBase
{
  public WorkoutViewModel(Workout workout, bool isDesignMode)
  {
    ID = workout.ID;
    _name = workout.Name;
    _date = workout.Date;
    Exercises = workout.Exercises.Select(exercise => new ExerciseViewModel(this, exercise)).ToObservableCollection();
    IsDesignMode = isDesignMode;
  }

  private int? ID { get; init; }

  private string _name;
  public string Name
  {
    get => _name;
    set => SetProperty(ref _name, value);
  }

  private DateTime _date;
  public DateTime Date
  {
    get => _date;
    set => SetProperty(ref _date, value);
  }

  public ObservableCollection<ExerciseViewModel> Exercises { get; init; }

  public bool IsDesignMode { get; init; }

  private async Task AddExercise()
  {
    var name = await Shell.Current.DisplayPromptAsync("Add Exercise", "Enter the name of the new exercise");
    if (!string.IsNullOrEmpty(name))
      Exercises.Add(new(this, name));
  }

  private AsyncCommand? _addExerciseCmd;
  public ICommand AddExerciseCmd => _addExerciseCmd ??= new AsyncCommand(AddExercise);

  public void RemoveExercise(ExerciseViewModel exercise) => Exercises.Remove(exercise);

  public Workout GetModel() => new(ID, Name, Date, Exercises.Select(evm => evm.GetModel()).ToList());
}
