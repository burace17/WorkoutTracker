namespace WorkoutTracker.Models;
public readonly record struct Exercise(string Name, IReadOnlyCollection<Set> Sets);
