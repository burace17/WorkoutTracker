using SQLite;
using System.Text.Json;

namespace WorkoutTracker;

public sealed class WorkoutData
{
  [Table("WorkoutTemplates")]
  private class WorkoutTemplateRow
  {
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int ID { get; set; }
    [NotNull]
    public string WorkoutTemplate { get; set; }
  }

  [Table("Workouts")]
  private class WorkoutRow
  {
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int ID { get; set; }
    [NotNull]
    public string Workout { get; set; }
  }

  private const string DatabaseFolder = "Workout Tracker";
  private const string DatabaseFilename = "Workouts.sqlite";
  private const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

  public static string AppStoragePath
  {
    get
    {
      var basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      return Path.Combine(basePath, DatabaseFolder);
    }
  }

  private static string DatabasePath => Path.Combine(AppStoragePath, DatabaseFilename);

  private bool _createdTables;
  private SQLiteAsyncConnection Database { get; init; }
  public static WorkoutData Instance { get; } = new();

  private WorkoutData()
  {
    Database = new SQLiteAsyncConnection(DatabasePath, Flags);
  }

  private async Task CreateTablesIfNeeded()
  {
    if (!_createdTables)
    {
      Directory.CreateDirectory(AppStoragePath);
      await Database.CreateTableAsync<WorkoutTemplateRow>();
      await Database.CreateTableAsync<WorkoutRow>();
      _createdTables = true;
    }
  }

  public async Task<Workout> InsertWorkoutTemplate(Workout workout)
  {
    await CreateTablesIfNeeded();
    var workoutJson = JsonSerializer.Serialize(workout);
    var workoutRow = new WorkoutTemplateRow() { WorkoutTemplate = workoutJson };
    var id = await Database.InsertAsync(workoutRow);
    return workout with { ID = id };
  }

  public async Task<Workout> InsertWorkout(Workout workout)
  {
    await CreateTablesIfNeeded();
    var workoutJson = JsonSerializer.Serialize(workout);
    var workoutRow = new WorkoutRow() { Workout = workoutJson };
    var id = await Database.InsertAsync(workoutRow);
    return workout with { ID = id };
  }

  public async Task UpdateWorkoutTemplate(Workout workout)
  {
    if (!workout.ID.HasValue)
      throw new ArgumentException(nameof(workout));
    await CreateTablesIfNeeded();
    var workoutJson = JsonSerializer.Serialize(workout);
    var workoutRow = new WorkoutTemplateRow() { ID = workout.ID.Value, WorkoutTemplate = workoutJson };
    await Database.UpdateAsync(workoutRow);
  }

  public async Task UpdateWorkout(Workout workout)
  {
    if (!workout.ID.HasValue)
      throw new ArgumentException(nameof(workout));
    await CreateTablesIfNeeded();
    var workoutJson = JsonSerializer.Serialize(workout);
    var workoutRow = new WorkoutRow() { ID = workout.ID.Value, Workout = workoutJson };
    await Database.UpdateAsync(workoutRow);
  }

  public async Task<List<Workout>> GetWorkoutTemplates()
  {
    await CreateTablesIfNeeded();
    var rows = await Database.Table<WorkoutTemplateRow>().ToListAsync();
    var workouts = new List<Workout>();
    foreach (var row in rows)
    {
      var workout = JsonSerializer.Deserialize<Workout>(row.WorkoutTemplate);
      workouts.Add(workout with { ID = row.ID });
    }
    return workouts;
  }

  public async Task<List<Workout>> GetWorkouts()
  {
    await CreateTablesIfNeeded();
    var rows = await Database.Table<WorkoutRow>().ToListAsync();
    var workouts = new List<Workout>();
    foreach (var row in rows)
    {
      var workout = JsonSerializer.Deserialize<Workout>(row.Workout);
      workouts.Add(workout with { ID = row.ID });
    }
    return workouts;
  }
}
