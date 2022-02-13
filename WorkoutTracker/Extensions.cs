﻿using System.Collections.ObjectModel;
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
}
