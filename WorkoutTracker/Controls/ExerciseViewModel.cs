/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace WorkoutTracker;

public class ExerciseViewModel : ViewModelBase
{
  public ExerciseViewModel(WorkoutViewModel parent, Exercise exercise)
  {
    _name = exercise.Name;
    Sets = exercise.Sets.Select(set => new SetViewModel(this, set)).ToObservableCollection();
    Parent = parent;
  }

  public ExerciseViewModel(WorkoutViewModel parent, string name)
  {
    _name = name;
    Sets = new();
    Sets.Add(new(this, 10, 5));
    Parent = parent;
  }

  private string _name;
  public string Name
  {
    get => _name;
    set => SetProperty(ref _name, value);
  }

  public ObservableCollection<SetViewModel> Sets { get; init; }

  private WorkoutViewModel Parent { get; init; }

  public bool IsDesignMode => Parent.IsDesignMode;

  private void AddSet()
  {
    Sets.Add(new(this, 10, 5)); // add constants for defaults..?
  }

  public void RemoveSet(SetViewModel set) => Sets.Remove(set);

  private Command? _addSetCmd;
  public ICommand AddSetCmd => _addSetCmd ??= new Command(AddSet);

  private Command? _removeFromWorkoutCmd;
  public ICommand RemoveFromWorkoutCmd => _removeFromWorkoutCmd ??= new Command(() => Parent.RemoveExercise(this));

  public Exercise GetModel() => new(Name, Sets.Select(svm => svm.GetModel()).ToList());
}
