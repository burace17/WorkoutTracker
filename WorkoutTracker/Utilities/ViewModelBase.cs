/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkoutTracker;

public class ViewModelBase : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;
  protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
  {
    var equal = EqualityComparer<T>.Default.Equals(backingField, value);
    if (!equal)
    {
      backingField = value;
      NotifyPropertyChanged(propertyName);
    }

    return equal;
  }
  public virtual Task InitializeAsync() => Task.CompletedTask;
}
