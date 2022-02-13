using System.Collections.Immutable;
namespace WorkoutTracker.Models;
public readonly record struct Workout(string Name, DateTime Date, ImmutableList<Exercise> Exercises)
{
  public static Workout Create() => new("Untitled", DateTime.Now, ImmutableList<Exercise>.Empty);
}
