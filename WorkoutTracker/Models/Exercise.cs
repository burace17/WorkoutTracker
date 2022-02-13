namespace WorkoutTracker;
public readonly record struct Exercise(string Name, IReadOnlyCollection<Set> Sets);
