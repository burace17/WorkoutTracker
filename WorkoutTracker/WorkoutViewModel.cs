using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkoutTracker.Models;

namespace WorkoutTracker;

public class WorkoutViewModel : ViewModelBase
{
  public WorkoutViewModel(Workout workout, bool isDesignMode)
  {
    _name = workout.Name;
    _date = workout.Date;
    Exercises = workout.Exercises.Select(exercise => new ExerciseViewModel(this, exercise)).ToObservableCollection();
    IsDesignMode = isDesignMode;
  }

  private string _name;
  public string Name
  {
    get => _name;
    set
    {
      if (SetProperty(ref _name, value))
        NotifyPropertyChanged(nameof(DisplayName));
    }
  }

  private DateTime _date;
  public DateTime Date
  {
    get => _date;
    set
    {
      if (SetProperty(ref _date, value))
        NotifyPropertyChanged(nameof(DisplayName));
    }
  }

  public string DisplayName => $"{Name} ({Date:g})";

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
}
