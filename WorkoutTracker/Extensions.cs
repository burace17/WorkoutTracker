using System.Collections.ObjectModel;
namespace WorkoutTracker;

public static class Extensions
{
  public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll) => new(coll);
}
