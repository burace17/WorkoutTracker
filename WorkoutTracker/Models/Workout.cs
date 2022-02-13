using System.Text.Json.Serialization;
namespace WorkoutTracker;
public readonly record struct Workout
{
  public Workout(int? id, string name, DateTime date, IReadOnlyCollection<Exercise> exercises)
  {
    ID = id;
    Name = name;
    Date = date;
    Exercises = exercises;
  }

  [JsonIgnore]
  public int? ID { get; init; }

  public string Name { get; init; }

  public DateTime Date { get; init; }

  public IReadOnlyCollection<Exercise> Exercises { get; init; }
}
