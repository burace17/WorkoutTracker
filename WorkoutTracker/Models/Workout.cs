/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

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
