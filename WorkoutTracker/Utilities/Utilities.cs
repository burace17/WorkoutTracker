/*
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/.
 */

using System.Runtime.CompilerServices;

namespace WorkoutTracker;

public static class Utilities
{
  public static async void NonAwaitCall(Task t, [CallerFilePath] string path = "", [CallerLineNumber] int lineNumber = 0)
  {
    try
    {
      await t;
    }
    catch (Exception ex)
    {
      throw new Exception($"Exception thrown while waiting on task: {ex}. Called from: {path}, line: {lineNumber}", ex);
    }
  }

  public static async void NonAwaitCall(Func<Task> f, [CallerFilePath] string path = "", [CallerLineNumber] int lineNumber = 0)
  {
    try
    {
      await f();
    }
    catch (Exception ex)
    {
      throw new Exception($"Exception thrown while waiting on task: {ex}. Called from: {path}, line: {lineNumber}", ex);
    }
  }
}
