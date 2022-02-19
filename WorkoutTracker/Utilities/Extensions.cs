/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

namespace WorkoutTracker;

public static class Extensions
{
  public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll) => new(coll);
  public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
  {
    if (coll == null) 
      throw new ArgumentNullException(nameof(coll));
    if (items == null)
      throw new ArgumentNullException(nameof(items));
    foreach (var item in items)
      coll.Add(item);
  }

  public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
  {
    builder.Services.AddSingleton<WorkoutDataService>();
    return builder;
  }
  public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
  {
    builder.Services.AddSingleton<MainPageViewModel>();
    builder.Services.AddTransient<CreateWorkoutViewModel>();
    return builder;
  }
}
