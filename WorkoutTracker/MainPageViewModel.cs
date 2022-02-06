using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkoutTracker
{
  internal class MainPageViewModel : ViewModelBase
  {
    private ICommand? _startWorkoutCmd;
    public ICommand StartWorkoutCmd => _startWorkoutCmd ??= new Command(() => { });
    
    private ICommand? _createTemplateCmd;
    public ICommand CreateTemplateCmd => _createTemplateCmd ??= new AsyncCommand(() => Shell.Current.GoToAsync("createWorkout"));
  }
}
