using System.Collections.Immutable;
namespace WorkoutTracker.Models;
public readonly record struct Exercise(string Name, ImmutableList<Set> Sets)
{
  public static Exercise Create(string name) => new(name, ImmutableList<Set>.Empty);
}
