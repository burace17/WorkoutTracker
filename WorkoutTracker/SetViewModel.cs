using System.Windows.Input;
using WorkoutTracker.Models;
namespace WorkoutTracker;

public class SetViewModel : ViewModelBase
{
  public SetViewModel(ExerciseViewModel parent, Set set)
  {
    _weight = set.Weight;
    _repetitions = set.Repetitions;
    _completed = set.Completed;
    Parent = parent;
  }

  public SetViewModel(ExerciseViewModel parent, double weight, int repetitions)
  {
    _weight = weight;
    _repetitions = repetitions;
    Parent = parent;
  }

  private double _weight;
  public double Weight
  {
    get => _weight;
    set => SetProperty(ref _weight, value);
  }

  private int _repetitions;
  public int Repetitions
  {
    get => _repetitions;
    set => SetProperty(ref _repetitions, value);
  }

  private int _completedRepetitions;
  public int CompletedRepetitions
  {
    get => _completedRepetitions;
    set => SetProperty(ref _completedRepetitions, value);
  }

  private bool _completed;
  public bool Completed
  {
    get => _completed;
    set => SetProperty(ref _completed, value);
  }

  private ExerciseViewModel Parent { get; init; }

  public bool IsDesignMode => Parent.IsDesignMode;

  private Command? _removeFromExerciseCmd;
  public ICommand RemoveFromExerciseCmd => _removeFromExerciseCmd ??= new Command(() => Parent.RemoveSet(this));

  public Set GetModel() => new(Weight, Repetitions, CompletedRepetitions, Completed);
}
